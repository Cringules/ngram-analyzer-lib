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
        return ApproximatePeakManual(peak, 1, 1, 0);
    }

    /// <summary>
    /// Метод для ручной аппроксимации пика по Лоренцу.
    /// </summary>
    /// <param name="peak">Аппроксимируемый пик.</param>
    /// <param name="xCoefficient">Параметр по оси абсцисс.</param>
    /// <param name="yCoefficient">Параметр по оси ординат.</param>
    /// <param name="backCoefficient">Параметр уровня фона.</param>
    /// <param name="n">Опциональный параметр (не используется).</param>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double xCoefficient, double yCoefficient,
        double backCoefficient, double n = 0)
    {
        var peakAnalyzer = new XrayPeakAnalyzer();

        var peakTopX = peak.GetPeakTop().X;
        var peakTopY = peakAnalyzer.GetIntensityMaximum(peak);
        var halfWidth = 0.5 * peakAnalyzer.GetPeakWidth(peak);
        
        var newPoints = (from point in peak.Points
            select point.X
            into x
            let y = peak.BackgroundLevel + backCoefficient + peakTopY * yCoefficient *
                (Math.Pow(halfWidth, 2) / (Math.Pow(halfWidth, 2) + Math.Pow(xCoefficient * x - peakTopX, 2)))
            select new Point(x, y)).ToList();

        return new ApproximationResult(newPoints, 0);
    }
}