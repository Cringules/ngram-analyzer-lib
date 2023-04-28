namespace Cringules.NGram.Lib;

/// <summary>
/// Класс, реализующий аппроксимацию пика по Лоренцу.
/// </summary>
public class ApproximationLorentz : IAutoApproximator, IManualApproximator
{
    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Лоренцу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakAuto(XrayPeak peak)
    {
        return new XrayPeak(peak.points);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Лоренцу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakManual(XrayPeak peak, double height, double width,
        double corr, double lambda = 0)
    {
        return new XrayPeak(peak.points);
    }
}