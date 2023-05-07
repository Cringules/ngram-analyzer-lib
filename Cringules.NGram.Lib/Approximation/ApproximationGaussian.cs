﻿using Python.Included;
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
    /// <returns>Новый пик.</returns>
    public ApproximationResult ApproximatePeakAuto(XrayPeak peak)
    {
        var peakTopX = peak.GetPeakTop().X;
        var peakTopY = (new XrayPeakAnalyzer()).GetIntensityMaximum(peak);
        var integralBreadth = 0.5 * (new XrayPeakAnalyzer()).GetPeakWidth(peak) *
                              Math.Pow(Math.PI / Math.Log(2), 0.5);
        Console.WriteLine(peakTopX + " " + peakTopY + " " + integralBreadth);

        var newPoints = (from point in peak.Points
            select point.X
            into x
            let y = peak.BackgroundLevel + peakTopY * Math.Exp(-Math.PI * Math.Pow(x - peakTopX, 2) / integralBreadth)
            select new Point(x, y)).ToList();

        return new ApproximationResult(newPoints);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Гауссу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double height, double width,
        double corr, double lambda = 0)
    {
        return new ApproximationResult(peak.Points);
    }
}