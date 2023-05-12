using Python.Included;
using Python.Runtime;

namespace Cringules.NGram.Lib.Approximation;

/// <summary>
/// Класс, реализующий аппроксимацию пика по Лоренцу.
/// </summary>
public class ApproximationLorentz : IApproximator
{
    /// <summary>
    /// Метод для автоматической аппроксимации пика по Лоренцу.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakAuto(XrayPeak peak)
    {
        var peakAnalyzer = new XrayPeakAnalyzer();

        var peakTopX = peak.GetPeakTop().X;
        var peakTopY = peakAnalyzer.GetIntensityMaximum(peak);
        var halfWidth = 0.5 * peakAnalyzer.GetPeakWidth(peak);
        
        var newPoints = (from point in peak.Points
            select point.X
            into x
            let y = peak.BackgroundLevel + peakTopY *
                (Math.Pow(halfWidth, 2) / (Math.Pow(halfWidth, 2) + Math.Pow(x - peakTopX, 2)))
            select new Point(x, y)).ToList();

        return new ApproximationResult(newPoints);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Лоренцу.
    /// </summary>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double height, double width,
        double corr, double lambda = 0)
    {
        return new ApproximationResult(peak.Points);
    }
}