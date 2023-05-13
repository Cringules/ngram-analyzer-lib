using Python.Included;
using Python.Runtime;

namespace Cringules.NGram.Lib.Approximation;

/// <summary>
/// Класс, реализующий аппроксимацию пика по Гауссу.
/// </summary>
public class ApproximationGaussian : IApproximator
{
    /// <summary>
    /// Метод для автоматической аппроксимации пика по Гауссу.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakAuto(XrayPeak peak)
    {
        return ApproximatePeakManual(peak, 1, 1, 0);
    }

    /// <summary>
    /// Метод для ручной аппроксимации пика по Гауссу.
    /// </summary>
    /// <param name="peak"></param>
    /// <param name="xCoefficient"></param>
    /// <param name="yCoefficient"></param>
    /// <param name="backCoefficient"></param>
    /// <param name="n"></param>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double xCoefficient,
        double yCoefficient,
        double backCoefficient, double n = 0)
    {
        var peakAnalyzer = new XrayPeakAnalyzer();

        var peakTopX = peak.GetPeakTop().X;
        var peakTopY = peakAnalyzer.GetIntensityMaximum(peak);
        var integralBreadth = 0.5 * peakAnalyzer.GetPeakWidth(peak) *
                              Math.Pow(Math.PI / Math.Log(2), 0.5);

        Console.WriteLine(peakTopX + " " + peakTopY + " " + integralBreadth);

        var newPoints = (from point in peak.Points
            select point.X
            into x
            let y = peak.BackgroundLevel + backCoefficient + peakTopY * yCoefficient *
                Math.Exp(-Math.PI * Math.Pow(xCoefficient * x - peakTopX, 2) / integralBreadth)
            select new Point(x, y)).ToList();

        return new ApproximationResult(newPoints, 1);
    }
}