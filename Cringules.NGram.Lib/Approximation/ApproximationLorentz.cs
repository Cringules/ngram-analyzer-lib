using Python.Included;
using Python.Runtime;

namespace Cringules.NGram.Lib.Approximation;

/// <summary>
/// Класс, реализующий аппроксимацию пика по Лоренцу.
/// </summary>
public class ApproximationLorentz : IApproximator
{
    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Лоренцу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public ApproximationResult ApproximatePeakAuto(XrayPeak peak)
    {
        return new ApproximationResult(peak.Points);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Лоренцу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double height, double width,
        double corr, double lambda = 0)
    {
        return new ApproximationResult(peak.Points);
    }
}