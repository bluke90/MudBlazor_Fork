using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProtonBlazor.Resources;

namespace ProtonBlazor;

/// <summary>
/// Base class for localization interceptors that can swap or augment ProtonBlazor translations.
/// </summary>
/// <remarks>
/// Derive from this when you need custom resource sources or fallback logic beyond the defaults provided by <see cref="DefaultLocalizationInterceptor"/>.
/// </remarks>
public abstract class AbstractLocalizationInterceptor : ILocalizationInterceptor
{
    /// <summary>
    /// Gets the <see cref="IStringLocalizer"/> for internal translations.
    /// </summary>
    protected internal IStringLocalizer Localizer { get; }

    /// <summary>
    /// Gets the custom <see cref="ProtonBlazor.ProLocalizer"/> for additional translations, if provided.
    /// </summary>
    protected internal ProLocalizer? ProLocalizer { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractLocalizationInterceptor"/> class.
    /// This creates an ResX reader for builtin <see cref="LanguageResource"/> with the default <see cref="LocalizationOptions"/>.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="mudLocalizer">The optional custom ProLocalizer.</param>
    /// <remarks>
    /// For more custom options use <see cref="AbstractLocalizationInterceptor(IStringLocalizer,ProtonBlazor.ProLocalizer)"/> constuctor.
    /// </remarks>
    protected AbstractLocalizationInterceptor(ILoggerFactory loggerFactory, ProLocalizer? mudLocalizer = null)
        : this(DefaultLanguageResourceReader(loggerFactory), mudLocalizer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractLocalizationInterceptor"/> class.
    /// </summary>
    /// <param name="localizer">The instance of <see cref="IStringLocalizer"/>.</param>
    /// <param name="mudLocalizer">The optional custom ProLocalizer.</param>
    protected AbstractLocalizationInterceptor(IStringLocalizer localizer, ProLocalizer? mudLocalizer = null)
    {
        Localizer = localizer;
        ProLocalizer = mudLocalizer;
    }

    /// <inheritdoc />
    public abstract LocalizedString Handle(string key, params object[] arguments);

    internal static IStringLocalizer DefaultLanguageResourceReader(ILoggerFactory loggerFactory)
    {
        var options = Options.Create(new LocalizationOptions());
        var factory = new ResourceManagerStringLocalizerFactory(options, loggerFactory);

        return factory.Create(typeof(LanguageResource));
    }
}
