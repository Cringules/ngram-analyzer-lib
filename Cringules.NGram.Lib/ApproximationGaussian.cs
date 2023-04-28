namespace Cringules.NGram.Lib;

/// <summary>
/// Класс, реализующий аппроксимацию пика по Гауссу.
/// </summary>
public class ApproximationGaussian : IAutoApproximator, IManualApproximator
{
    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Гауссу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakAuto(XrayPeak peak)
    {
        return new XrayPeak(peak.points);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Гауссу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakManual(XrayPeak peak, double height, double width,
        double corr, double lambda = 0)
    {
        return new XrayPeak(peak.points);
    }
}