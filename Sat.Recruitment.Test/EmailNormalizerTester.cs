using Sat.Recruitment.Api;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class EmailNormalizerTester
    {
        [Fact]
        public void RemoveDotBeforeAtTest()
        {
            var emailWithDotBeforeAtNormalized = EmailNormalizer.Normalize("facu.1@test.com");

            Assert.Equal("facu1@test.com", emailWithDotBeforeAtNormalized);
        }

        [Fact]
        public void RemoveDotsBeforeAtTest()
        {
            var emailWithDotsBeforeAtNormalized = EmailNormalizer.Normalize("facu.test.1@test.com");

            Assert.Equal("facutest1@test.com", emailWithDotsBeforeAtNormalized);
        }

        [Fact]
        public void DontRemoveDotsAfterAtTest()
        {
            var emailWithDotsAfterAtNormalized = EmailNormalizer.Normalize("facu@facu.test.com");

            Assert.Equal("facu@facu.test.com", emailWithDotsAfterAtNormalized);
        }

        [Fact]
        public void RemovePlusBeforeAtTest()
        {
            var emailWithPlusBeforeAtNormalized = EmailNormalizer.Normalize("facutest+test@test.com");

            Assert.Equal("facutest@test.com", emailWithPlusBeforeAtNormalized);
        }

        [Fact]
        public void DontRemovePlusAfterAtTest()
        {
            var emailWithDotsBeforeAtNormalized = EmailNormalizer.Normalize("facutest@test+test.com");

            Assert.Equal("facutest@test+test.com", emailWithDotsBeforeAtNormalized);
        }
    }
}
