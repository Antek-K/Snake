using NUnit.Framework;

namespace Game.UnitTests
{
    class MultiplicationConverterTests
    {
        [Test]
        public void Convert_WhenValueIsNotInt_ResultIs1()
        {
            var multiplicationConverter = new MultiplicationConverter();

            var result = multiplicationConverter.Convert(3.14, typeof(int), 3, null);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Convert_WhenParameterIsNotInt_ReturnsValue()
        {
            var multiplicationConverter = new MultiplicationConverter();

            var result = multiplicationConverter.Convert(3, typeof(int), "", null);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Convert_WhenArgumentsAre3And3_ResultIs9()
        {
            var multiplicationConverter = new MultiplicationConverter();

            var result = multiplicationConverter.Convert(3, typeof(int), 3, null);

            Assert.AreEqual(9, result);
        }

        [Test]
        public void Convert_WhenArgumentsAre2And4_ResultIs8()
        {
            var multiplicationConverter = new MultiplicationConverter();

            var result = multiplicationConverter.Convert(2, typeof(int), 4, null);

            Assert.AreEqual(8, result);
        }

    }
}
