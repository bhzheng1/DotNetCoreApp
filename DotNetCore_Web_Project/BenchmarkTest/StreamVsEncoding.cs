using BenchmarkDotNet.Attributes;
using HelperClassLibrary;

namespace BenchmarkTest
{
    [SimpleJob(launchCount: 2)]
    [MemoryDiagnoser]
    public class StreamVsEncoding
    {
        [Params("Hello Wilson!", "使用【BenchmarkDotNet】基准测试，Encoding vs Stream")]
        public string _stringValue;

        [Benchmark]
        public void Encoding() =>Base64Encode.StringToBytesWithEncoding(_stringValue);

        [Benchmark]
        public void Stream() => Base64Encode.StringToBytesWithStream(_stringValue);

    }
}
