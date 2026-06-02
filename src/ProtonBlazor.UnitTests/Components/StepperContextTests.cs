using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class StepperContextTests
    {
        [Test]
        public void StepContext_NullStepper_Throws()
        {
            Assert.That(() => _ = new ProStepContext(null!, new ProStep()),
                Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("stepper"));
        }

        [Test]
        public void StepContext_NullStep_Throws()
        {
            Assert.That(() => _ = new ProStepContext(new ProStepper(), null!),
                Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("step"));
        }
    }
}
