using HelperClassLibrary;
using Xunit;

namespace XUnitTestProject
{
    public class StringToBytesTest
    {
        [Fact]
        public void BytesEqualsTest_Equals_ReturnTrue()
        {
            // Arrange
            var a = new byte[] { 23, 19, 39, 29 };
            var b = new byte[] { 23, 19, 39, 29 };
            var c = a;

            // Act
            var bothNull = Base64Encode.BytesEquals(null, null);
            var valueEquals = Base64Encode.BytesEquals(a, b);
            var referenceEquals = Base64Encode.BytesEquals(a, c);

            // Assert
            Assert.True(bothNull, nameof(bothNull));
            Assert.True(valueEquals, nameof(valueEquals));
            Assert.True(referenceEquals, nameof(referenceEquals));
        }

        [Fact]
        public void BytesEqualsTest_NotEquals_ReturnFalse()
        {
            // Arrange
            var a = new byte[] { 23, 19, 39, 29 };
            var b = new byte[] { 23, 19, 39, 23 };
            var c = new byte[] { 23, 19, 39, 29, 56 };

            // Act
            var rightIsNull = Base64Encode.BytesEquals(a, null);
            var leftIsNull = Base64Encode.BytesEquals(null, b);

            var valueNotEquals = Base64Encode.BytesEquals(a, b);

            var lengthNotEquals = Base64Encode.BytesEquals(a, c);

            // Assert
            Assert.False(rightIsNull, nameof(rightIsNull));
            Assert.False(leftIsNull, nameof(leftIsNull));

            Assert.False(valueNotEquals, nameof(valueNotEquals));

            Assert.False(lengthNotEquals, nameof(lengthNotEquals));
        }

        [Fact]
        public void StringToBytes_Equals_ReturnTrue()
        {
            // Arrange

            var stringValue = "Hello World";

            // Act
            var bytesWithStream = Base64Encode.StringToBytesWithStream(stringValue);
            var bytesWithEncoding = Base64Encode.StringToBytesWithEncoding(stringValue);

            var compareResult = Base64Encode.BytesEquals(bytesWithStream, bytesWithEncoding);

            // Assert
            Assert.True(compareResult, "CompareResult for Stream vs Encoding");
        }
    }
}
