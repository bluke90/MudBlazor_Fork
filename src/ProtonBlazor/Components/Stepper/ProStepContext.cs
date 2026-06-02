namespace ProtonBlazor;

/// <summary>
/// Provides contextual information about a <see cref="ProStep"/> within a <see cref="ProStepper"/>.
/// </summary>
public sealed class ProStepContext
{
    /// <summary>
    /// Gets the owning <see cref="ProStepper"/>.
    /// </summary>
    public ProStepper Stepper { get; }

    /// <summary>
    /// Gets the <see cref="ProStep"/> associated with the context.
    /// </summary>
    public ProStep Step { get; }

    /// <summary>
    /// Gets a value indicating whether the associated step is currently active.
    /// </summary>
    public bool IsActive => Stepper.ActiveStep == Step;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProStepContext"/> class.
    /// </summary>
    /// <param name="stepper">The owning stepper.</param>
    /// <param name="step">The step associated with the context.</param>
    public ProStepContext(ProStepper stepper, ProStep step)
    {
        Stepper = stepper ?? throw new ArgumentNullException(nameof(stepper));
        Step = step ?? throw new ArgumentNullException(nameof(step));
    }
}
