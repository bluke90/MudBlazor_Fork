using AwesomeAssertions;
using Bunit;
using NUnit.Framework;
using ProtonBlazor.UnitTests.TestComponents.Element;
namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class ElementTests : BunitTest
    {
        /// <summary>
        /// ProElement renders first an anchor and then a button
        /// </summary>
        [Test]
        public async Task Should_Render_An_Anchor_And_Then_A_Button()
        {
            var comp = Context.Render<ProElement>(parameters => parameters
                .Add(x => x.HtmlTag, "a")
                .Add(x => x.Class, "pro-button-root"));
            comp.MarkupMatches("<a class=\"pro-button-root\"></a>");
            await comp.SetParametersAndRenderAsync(parameters => parameters
                .Add(x => x.HtmlTag, "button")
                .Add(x => x.Class, "pro-button-root"));
            comp.MarkupMatches("<button class=\"pro-button-root\"></button>");
        }

        /// <summary>
        /// In this example, there is a mouseover event conditionally attached
        /// if the property Attached is set to true is attached
        /// if not, there shouldn't have any event present
        /// </summary>
        [Test]
        public void ProElement_Should_Not_Attach_A_Null_Event()
        {
            var comp = Context.Render<ElementTestEventNull>();

            //initially, renders just an empty span, because AttachEvent is false;
            comp.MarkupMatches("<span></span>");

            //we set AttachEvent to true, so it has to attach the mouseover event
            var comp2 = Context.Render<ElementTestEventNull>(parameters => parameters.Add(x => x.AttachEvent, true));

            //because we didn't hovered yet the element, the WasHovered property is false
            comp2.Instance.WasHovered.Should().BeFalse();

            //after hovered the element, the property WasHovered should be true
            comp2.Find("span").MouseOver();
            comp2.Instance.WasHovered.Should().BeTrue();
        }

        [Test]
        public async Task ElementReferenceCapture()
        {
            var comp = Context.Render<ElementReferenceExceptionTest>();
            await comp.Find("#element-button").ClickAsync();
        }
    }
}
