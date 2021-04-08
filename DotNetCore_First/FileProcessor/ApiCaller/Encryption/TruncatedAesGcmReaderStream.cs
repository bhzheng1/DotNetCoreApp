using System;
using System.IO;
using Org.BouncyCastle.Crypto.Modes;

namespace FileProcessor.ApiCaller.Encryption
{
    public class TruncatedAesGcmReaderStream : Stream
    {   
        private readonly Stream _source;
        private readonly IAeadBlockCipher _cipher;
        private readonly byte[] _overflow;
        private int _numOverflowBytes;
        private bool _reachedEndOfStream;

        internal TruncatedAesGcmReaderStream(Stream source, IAeadBlockCipher cipher)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            if (cipher == null)
                throw new ArgumentNullException(nameof(cipher));
            
            if (!source.CanRead)
                throw new ArgumentException("must be a readable stream", nameof(source));
            
            _source = source;
            _cipher = cipher;
            _overflow = new byte[_cipher.GetBlockSize()];
        }

        public override void Flush()
        {
            _source.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer.Length < offset)
                throw new ArgumentOutOfRangeException(nameof(offset), $"greater than {nameof(buffer)} length");
            
            if (buffer.Length < count)
                throw new ArgumentOutOfRangeException(nameof(count), $"greater than {nameof(buffer)} length");
            
            if (buffer.Length < offset + count)
                throw new ArgumentException($"{nameof(offset)} with {nameof(count)} is greater than {nameof(buffer)} length");
            
            var processedBytes = new byte[0];
            var needMoreBlocksToFulfillRead = count > _numOverflowBytes;
            if (needMoreBlocksToFulfillRead)
            {
                var blockSize = _cipher.GetBlockSize();
                var blockCount = Math.Max(blockSize, count / blockSize * blockSize);
                var readBytes = ReadBytes(blockCount);
                _reachedEndOfStream = readBytes.Length != blockCount;
                
                var bytesOutSize = _cipher.GetOutputSize(readBytes.Length);
                processedBytes = new byte[bytesOutSize];
                var numBytesProcessed = _cipher.ProcessBytes(readBytes, 0, readBytes.Length, processedBytes, 0);
                if (_reachedEndOfStream)
                    _cipher.DoFinal(processedBytes, numBytesProcessed);
            
                // truncate extra block
                var nextProcessedBytes = new byte[processedBytes.Length - _cipher.GetBlockSize()];
                Buffer.BlockCopy(processedBytes, 0, nextProcessedBytes, 0, nextProcessedBytes.Length);
                processedBytes = nextProcessedBytes;
            }
            
            // if we have bytes still in overflow we must provide those first then we can fill
            // remaining count with processed bytes
            var hasOverflowBytes = _numOverflowBytes != 0;
            if (hasOverflowBytes)
            {
                var readFromOverflow = Math.Min(count, _numOverflowBytes);
                TakeFromOverflow(buffer, offset, readFromOverflow);
                offset += readFromOverflow;
                count -= readFromOverflow;
            }
            
            // next we need to fill remaining buffer with processed byte
            // and then whatever processedBytes are left over needs to go into overflow
            // otherwise take all of processed bytes and put them into overflow
            var hasSpaceLeftInBuffer = count != 0;
            if (hasSpaceLeftInBuffer)
            {
                var bytesToCopy = Math.Min(count, processedBytes.Length);
                var bytesLeft = processedBytes.Length - bytesToCopy;
                Buffer.BlockCopy(processedBytes, 0, buffer, offset, bytesToCopy);
                offset += bytesToCopy;
                count -= bytesToCopy;
                PutIntoOverflow(processedBytes, bytesToCopy, bytesLeft);
            }
            else
                PutIntoOverflow(processedBytes, 0, processedBytes.Length);
            
            return offset;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            set => throw new NotSupportedException();
            get => throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _source?.Dispose();
            }
            base.Dispose(disposing);
        }

        private byte[] ReadBytes(int count)
        {
            if (count == 0)
                return new byte[0];

            var buffer = new byte[count];
            var offset = 0;

            do
            {
                var num = _source.Read(buffer, offset, count);
                if (num != 0)
                {
                    offset += num;
                    count -= num;
                }
                else
                    break;
            } while (count > 0);

            if (offset != buffer.Length)
            {
                var nextBuffer = new byte[offset];
                Buffer.BlockCopy(buffer, 0, nextBuffer, 0, offset);
                buffer = nextBuffer;
            }
            
            return buffer;
        }

        private void PutIntoOverflow(byte[] bytes, int offset, int count)
        {
            if (_numOverflowBytes + count > _overflow.Length)
                throw new ArgumentOutOfRangeException(nameof(count), "overflowing the overflow buffer will cause data loss");
            
            Buffer.BlockCopy(bytes, offset, _overflow, _numOverflowBytes, count);
            _numOverflowBytes += count;
        }

        /// <summary>
        /// fills @buffer with @count bytes then compacts overflow buffer with bytes that were left over
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void TakeFromOverflow(byte[] buffer, int offset, int count)
        {
            if (count > _numOverflowBytes)
                throw new ArgumentOutOfRangeException(nameof(count), "not enough bytes in overflow buffer");
            
            if (count > _cipher.GetBlockSize())
                throw new ArgumentOutOfRangeException(nameof(count), "requested overflow read is greater than overflow size");
            
            if (count == 0)
                return;

            Buffer.BlockCopy(_overflow, 0, buffer, offset, count);
            _numOverflowBytes = _numOverflowBytes - count;
            var hasMoreOverflowBytes = _numOverflowBytes > 0;
            if (hasMoreOverflowBytes)
                Buffer.BlockCopy(_overflow, count, _overflow, 0, _numOverflowBytes);
        }
    }
}