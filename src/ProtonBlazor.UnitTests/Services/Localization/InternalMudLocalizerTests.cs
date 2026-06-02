using AwesomeAssertions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using ProtonBlazor.Resources;

namespace ProtonBlazor.UnitTests.Services.Localization;

#nullable enable
[TestFixture]
public class InternalMudLocalizerTests
{
    [Test]
    public void Constructor_WithNullInterceptor_ShouldThrowArgumentNullException()
    {
        // Arrange
        ILocalizationInterceptor? interceptor = null;

        // Act
        var construct = () => new InternalMudLocalizer(interceptor!);

        // Assert
        construct.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Constructor_WithNullEnumInterceptor_ShouldThrowArgumentNullException()
    {
        // Arrange
        var interceptorMock = new Mock<ILocalizationInterceptor>();
        ILocalizationEnumInterceptor? enumInterceptor = null;

        // Act
        var construct = () => new InternalMudLocalizer(interceptorMock.Object, enumInterceptor!);

        // Assert
        construct.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Constructor_WithValidInterceptor_ShouldNotThrowException()
    {
        // Arrange
        var interceptorMock = new Mock<ILocalizationInterceptor>();

        // Act
        var construct = () => new InternalMudLocalizer(interceptorMock.Object);

        // Assert
        construct.Should().NotThrow();
    }

    [Test]
    [SetUICulture("en-US")]
    public void CustomLocalizationInterceptor_EnglishUICulture()
    {
        var interceptorMock = new Mock<ILocalizationInterceptor>();
        interceptorMock.Setup(mock => mock.Handle(LanguageResource.ProDataGrid_Clear)).Returns(new LocalizedString(LanguageResource.ProDataGrid_Clear, "Reset", false));
        var internalMudLocalizer = new InternalMudLocalizer(interceptorMock.Object);

        // Act
        var result = internalMudLocalizer[LanguageResource.ProDataGrid_Clear];

        // Assert
        result.Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_Clear, "Reset", false));
    }

    [Test]
    [SetUICulture("de-DE")]
    public void CustomLocalizationInterceptor_NonEnglishUICulture()
    {
        // Assert
        var interceptorMock = new Mock<ILocalizationInterceptor>();
        interceptorMock.Setup(mock => mock.Handle(LanguageResource.ProDataGrid_Clear)).Returns(new LocalizedString(LanguageResource.ProDataGrid_Clear, "Reset", false));
        var internalMudLocalizer = new InternalMudLocalizer(interceptorMock.Object);

        // Act
        var result = internalMudLocalizer[LanguageResource.ProDataGrid_Clear];

        result.Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_Clear, "Reset", false));
    }

    [Test]
    [SetUICulture("en-US")]
    public void DefaultLocalizationInterceptor_EnglishUICulture()
    {
        // Arrange
        var interceptorMock = new DefaultLocalizationInterceptor(NullLoggerFactory.Instance, mudLocalizer: null);
        var internalMudLocalizer = new InternalMudLocalizer(interceptorMock);

        // Act & Assert
        internalMudLocalizer[LanguageResource.ProDataGrid_Contains].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_Contains, "contains", false, typeof(LanguageResource).FullName));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsEmpty, "is empty", false, typeof(LanguageResource).FullName));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsNotEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsNotEmpty, "is not empty", false, typeof(LanguageResource).FullName));
    }

    [Test]
    [SetUICulture("de-DE")]
    public void DefaultLocalizationInterceptor_NonEnglishUICulture()
    {
        // Arrange
        var interceptorMock = new DefaultLocalizationInterceptor(NullLoggerFactory.Instance, mudLocalizer: null);
        var internalMudLocalizer = new InternalMudLocalizer(interceptorMock);

        // Act & Assert
        internalMudLocalizer[LanguageResource.ProDataGrid_Contains].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_Contains, "contains", false, typeof(LanguageResource).FullName));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsEmpty, "is empty", false, typeof(LanguageResource).FullName));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsNotEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsNotEmpty, "is not empty", false, typeof(LanguageResource).FullName));
    }

    [Test]
    [SetUICulture("en-US")]
    public void DefaultLocalizationInterceptor_WithCustomMudLocalizer_EnglishUICulture()
    {
        // Arrange
        var mudLocalizerMock = new Mock<ProLocalizer> { CallBase = true };
        mudLocalizerMock.Setup(mock => mock[LanguageResource.ProDataGrid_IsEmpty]).Returns(new LocalizedString(LanguageResource.ProDataGrid_IsEmpty, "XXX", false));
        mudLocalizerMock.Setup(mock => mock[LanguageResource.ProDataGrid_IsNotEmpty]).Returns(new LocalizedString(LanguageResource.ProDataGrid_IsNotEmpty, "ProDataGrid_IsNotEmpty", true));
        var interceptor = new DefaultLocalizationInterceptor(NullLoggerFactory.Instance, mudLocalizerMock.Object);
        var internalMudLocalizer = new InternalMudLocalizer(interceptor);

        // Act & Assert
        internalMudLocalizer[LanguageResource.ProDataGrid_Contains].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_Contains, "contains", false, typeof(LanguageResource).FullName));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsEmpty, "is empty", false, typeof(LanguageResource).FullName));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsNotEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsNotEmpty, "is not empty", false, typeof(LanguageResource).FullName));
    }

    [Test]
    [SetUICulture("de-DE")]
    public void DefaultLocalizationInterceptor_WithCustomMudLocalizer_NonEnglishUICulture()
    {
        // Arrange
        var mudLocalizerMock = new Mock<ProLocalizer> { CallBase = true };
        mudLocalizerMock.Setup(mock => mock[LanguageResource.ProDataGrid_IsEmpty]).Returns(new LocalizedString(LanguageResource.ProDataGrid_IsEmpty, "XXX", false));
        mudLocalizerMock.Setup(mock => mock[LanguageResource.ProDataGrid_IsNotEmpty]).Returns(new LocalizedString(LanguageResource.ProDataGrid_IsNotEmpty, "ProDataGrid_IsNotEmpty", true));
        var interceptor = new DefaultLocalizationInterceptor(NullLoggerFactory.Instance, mudLocalizerMock.Object);
        var internalMudLocalizer = new InternalMudLocalizer(interceptor);

        // Act & Assert
        internalMudLocalizer[LanguageResource.ProDataGrid_Contains].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_Contains, "contains", false, typeof(LanguageResource).FullName));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsEmpty, "XXX", false));
        internalMudLocalizer[LanguageResource.ProDataGrid_IsNotEmpty].Should().BeEquivalentTo(new LocalizedString(LanguageResource.ProDataGrid_IsNotEmpty, "is not empty", false, typeof(LanguageResource).FullName));
    }
}
