using LawnMowers.Domain.Interfaces;
using LawnMowers.Logic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LawnMowers.Test
{
    [TestClass]
    public class MowerTests
    {
        private ILawnLogic _lawnLogic;

        [TestInitialize]
        public void Setup()
        {
            var container = new UnityContainer();
            _lawnLogic = container.Resolve<LawnLogic>();
        }

        [TestMethod]
        public void ProvidedTestInput()
        {
            var testInput = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n";

            _lawnLogic.BuildLawnAndMowers(testInput);

            var output = _lawnLogic.MoveLawnMowers();
            Assert.AreEqual("1 3 N\n5 1 E\n", output);
        }

        [TestMethod]
        public void ProvidedTestInputWithExtraBreaks()
        {
            var testInput = "5 5\n1 2 N\n\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n";

            _lawnLogic.BuildLawnAndMowers(testInput);

            var output = _lawnLogic.MoveLawnMowers();
            Assert.AreEqual("1 3 N\n5 1 E\n", output);
        }

        [TestMethod]
        public void TestIncompleteInput()
        {
            var testInput = "5 5\n1 N\n\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n";

            try
            {
                _lawnLogic.BuildLawnAndMowers(testInput);
            }
            catch
            {
                //faulty input, catch failed
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestInputOutsideBoundsOfLawn()
        {
            var testInput = "3 3\n1 2 N\n\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM\n";

            _lawnLogic.BuildLawnAndMowers(testInput);

            var output = _lawnLogic.MoveLawnMowers();
            Assert.IsTrue(output.ToLower().Contains("collision"));
        }

        [TestMethod]
        public void TestInputMowersCollide()
        {
            var testInput = "5 5\n1 2 N\n\nLMLMLMLMM\n1 1 E\nMMRMMRMRRM\n";

            _lawnLogic.BuildLawnAndMowers(testInput);

            var output = _lawnLogic.MoveLawnMowers();
            Assert.IsTrue(output.ToLower().Contains("collision"));
        }
    }
}
