// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using AwesomeAssertions;
using ProtonBlazor.Utilities;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Utilities;

#nullable enable
[TestFixture]
public class ProColorComparerTests
{
    [Test]
    public void Singleton_Instances_ShouldBeCreated()
    {
        // Arrange & Act
        var rgba = ProColor.ProColorComparer.Rgba;
        var hsl = ProColor.ProColorComparer.Hsl;
        var both = ProColor.ProColorComparer.RgbaAndHsl;

        // Assert
        rgba.Should().NotBeNull();
        hsl.Should().NotBeNull();
        both.Should().NotBeNull();
    }

    [Test]
    public void Singleton_Instances_ShouldHaveCorrectComparisonModes()
    {
        // Arrange & Act
        var rgba = ProColor.ProColorComparer.Rgba;
        var hsl = ProColor.ProColorComparer.Hsl;
        var both = ProColor.ProColorComparer.RgbaAndHsl;

        // Assert
        rgba.Comparison.Should().Be(ProColorComparison.Rgba);
        hsl.Comparison.Should().Be(ProColorComparison.Hsl);
        both.Comparison.Should().Be(ProColorComparison.RgbaAndHsl);
    }

    [Test]
    public void Equals_ShouldUseFallback_WhenComparisonIsInvalid()
    {
        // Arrange
        var comparer = ConstructInvalidComparer();

        var color1 = new ProColor(10, 20, 30, 40);
        var color2 = new ProColor(10, 20, 30, 40);

        // Act
        var result = comparer.Equals(color1, color2);

        // Assert
        result.Should().BeTrue("fallback: x.Equals(y)");
    }

    [Test]
    [TestCaseSource(nameof(AllComparers))]
    public void Equals_ShouldReturnTrue_WhenBothNull(ProColor.ProColorComparer comparer)
    {
        // Arrange & Act
        var result = comparer.Equals(null, null);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    [TestCaseSource(nameof(AllComparers))]
    public void Equals_ShouldReturnFalse_WhenOneNull(ProColor.ProColorComparer comparer)
    {
        // Arrange
        var color = new ProColor("#ff0000");

        // Act
        var result1 = comparer.Equals(color, null);
        var result2 = comparer.Equals(null, color);

        // Assert
        result1.Should().BeFalse();
        result2.Should().BeFalse();
    }

    [Test]
    [TestCaseSource(nameof(AllComparers))]
    public void Equals_ShouldReturnTrue_WhenSameReference(ProColor.ProColorComparer comparer)
    {
        // Arrange
        var color = new ProColor("#ff0000");

        // Act
        var result = comparer.Equals(color, color);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_RGBA_ShouldReturnTrue_WhenRgbaMatches()
    {
        // Arrange
        var color1 = new ProColor("#ff0000");
        var color2 = new ProColor("#ff0000");

        // Act
        var result = ProColor.ProColorComparer.Rgba.Equals(color1, color2);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_RGBA_ShouldReturnFalse_WhenRgbaDiffers()
    {
        // Arrange
        var red = new ProColor("#ff0000");
        var blue = new ProColor("#0000ff");

        // Act
        var result = ProColor.ProColorComparer.Rgba.Equals(red, blue);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void GetHashCode_RGBA_ShouldMatchForEqualColors()
    {
        // Arrange
        var color1 = new ProColor("#ff0000");
        var color2 = new ProColor("#ff0000");

        // Act
        var h1 = ProColor.ProColorComparer.Rgba.GetHashCode(color1);
        var h2 = ProColor.ProColorComparer.Rgba.GetHashCode(color2);

        // Assert
        h1.Should().Be(h2);
    }

    [Test]
    [TestCaseSource(nameof(AllComparers))]
    public void GetHashCode_NullObject(ProColor.ProColorComparer comparer)
    {
        // Arrange & Act
        var h1 = comparer.GetHashCode(null);
        var h2 = comparer.GetHashCode(null);

        // Assert
        h1.Should().Be(h2);
    }

    [Test]
    public void GetHashCode_RGBA_ShouldDifferForDifferentColors()
    {
        // Arrange
        var color1 = new ProColor("#ff0000");
        var color2 = new ProColor("#0000ff");

        // Act
        var h1 = ProColor.ProColorComparer.Rgba.GetHashCode(color1);
        var h2 = ProColor.ProColorComparer.Rgba.GetHashCode(color2);

        // Assert
        h1.Should().NotBe(h2);
    }

    [Test]
    public void Equals_HSL_ShouldReturnTrue_WhenHslMatches()
    {
        // Arrange
        var color1 = new ProColor(245, 0.34, 0.95, 1);
        var color2 = new ProColor(245, 0.34, 0.95, 1);

        // Act
        var result = ProColor.ProColorComparer.Hsl.Equals(color1, color2);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_HSL_ShouldReturnFalse_WhenHslDiffers()
    {
        // Arrange
        var color1 = new ProColor(245, 0.34, 0.95, 1);
        var color2 = new ProColor(245, 0.35, 0.95, 1);

        // Act
        var result = ProColor.ProColorComparer.Hsl.Equals(color1, color2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void GetHashCode_HSL_ShouldMatchForEqualColors()
    {
        // Arrange
        var color1 = new ProColor(245, 0.34, 0.95, 1);
        var color2 = new ProColor(245, 0.34, 0.95, 1);

        // Act
        var h1 = ProColor.ProColorComparer.Hsl.GetHashCode(color1);
        var h2 = ProColor.ProColorComparer.Hsl.GetHashCode(color2);

        // Assert
        h1.Should().Be(h2);
    }

    [Test]
    public void GetHashCode_HSL_ShouldDifferForDifferentColors()
    {
        // Arrange
        var color1 = new ProColor(245, 0.34, 0.95, 1);
        var color2 = new ProColor(245, 0.35, 0.95, 1);

        // Act
        var h1 = ProColor.ProColorComparer.Hsl.GetHashCode(color1);
        var h2 = ProColor.ProColorComparer.Hsl.GetHashCode(color2);

        // Assert
        h1.Should().NotBe(h2);
    }

    [Test]
    public void Equals_Both_ShouldReturnTrue_WhenBothRgbaAndHslMatch()
    {
        // Arrange
        var color1 = new ProColor(239, 238, 247, 1);
        var color2 = new ProColor(color1.H, color1.S, color1.L, 1);

        // Act
        var result = ProColor.ProColorComparer.RgbaAndHsl.Equals(color1, color2);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void Equals_Both_ShouldReturnFalse_WhenRgbaMatchesButHslDiffers()
    {
        // Arrange
        var color1 = new ProColor(239, 238, 247, 1);
        var color2 = new ProColor(color1.H, color1.S + 0.01, color1.L, 1);

        // Act
        var result = ProColor.ProColorComparer.RgbaAndHsl.Equals(color1, color2);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void GetHashCode_Both_ShouldMatchForEqualColors()
    {
        // Arrange
        var color1 = new ProColor(239, 238, 247, 1);
        var color2 = new ProColor(color1.H, color1.S, color1.L, 1);

        // Act
        var h1 = ProColor.ProColorComparer.RgbaAndHsl.GetHashCode(color1);
        var h2 = ProColor.ProColorComparer.RgbaAndHsl.GetHashCode(color2);

        // Assert
        h1.Should().Be(h2);
    }

    [Test]
    public void GetHashCode_Both_ShouldDiffer_WhenOnlyOneAspectMatches()
    {
        // Arrange
        var color1 = new ProColor(239, 238, 247, 1);
        var color2 = new ProColor(color1.H, color1.S + 0.01, color1.L, 1);

        // Act
        var h1 = ProColor.ProColorComparer.RgbaAndHsl.GetHashCode(color1);
        var h2 = ProColor.ProColorComparer.RgbaAndHsl.GetHashCode(color2);

        // Assert
        h1.Should().NotBe(h2);
    }

    [Test]
    public void GetHashCode_Both_ShouldDiffer_ForCompletelyDifferentColors()
    {
        // Arrange
        var color1 = new ProColor("#ff0000");
        var color2 = new ProColor("#0000ff");

        // Act
        var h1 = ProColor.ProColorComparer.RgbaAndHsl.GetHashCode(color1);
        var h2 = ProColor.ProColorComparer.RgbaAndHsl.GetHashCode(color2);

        // Assert
        h1.Should().NotBe(h2);
    }

    [Test]
    public void GetHashCode_ShouldUseFallback_WhenComparisonIsInvalid()
    {
        // Arrange
        var comparer = ConstructInvalidComparer();

        var color = new ProColor(1, 2, 3, 4);

        // Act
        var hash = comparer.GetHashCode(color);

        // Assert
        hash.Should().Be(color.GetHashCode(), because: "fallback: mudColor.GetHashCode()");
    }

    private static ProColor.ProColorComparer ConstructInvalidComparer()
    {
        var invalidComparison = (ProColorComparison)(-1);
        var comparer = (ProColor.ProColorComparer)typeof(ProColor.ProColorComparer)
            .GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                [typeof(ProColorComparison)],
                modifiers: null
            )!
            .Invoke([invalidComparison]);

        return comparer;
    }

    private static IEnumerable<ProColor.ProColorComparer> AllComparers()
    {
        yield return ProColor.ProColorComparer.Rgba;
        yield return ProColor.ProColorComparer.Hsl;
        yield return ProColor.ProColorComparer.RgbaAndHsl;
    }
}
