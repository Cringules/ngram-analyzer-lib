namespace Cringules.NGram.Lib;

/// <summary>
/// Структура для хранения информации (характеристик) пика.
/// </summary>
public struct PeakInfo
{
    /// <summary>
    /// Максимальная интенсивность.
    /// </summary>
    public double IntensityMaximum { get; }

    /// <summary>
    /// Измеренная интегральная интенсивность (эксп.).
    /// </summary>
    public double IntensityIntegral { get; }

    /// <summary>
    /// Расчетная интегральная интенсивность.
    /// </summary>
    public double IntensityApproximated { get; }

    /// <summary>
    /// Отношение эксп.интенсивности к расчетной;
    /// </summary>
    public double IntensityDifference { get; }

    /// <summary>
    /// Интегральная ширина.
    /// </summary>
    public double IntegralWidth { get; }

    /// <summary>
    /// Ширина отражения на половине высоты пика.
    /// </summary>
    public double PeakWidth { get; }

    /// <summary>
    /// Угол отражения.
    /// </summary>
    public double TopAngle { get; }

    /// <summary>
    /// Межплоскостное расстояние.
    /// </summary>
    public double InterplanarDistance { get; }

    /// <summary>
    /// Конструктор структуры характеристик.
    /// </summary>
    /// <param name="intensityMaximum">Максимальная интенсивность.</param>
    /// <param name="intensityIntegral">Измеренная интегральная интенсивность (эксп.).</param>
    /// <param name="intensityApproximated">Расчетная интегральная интенсивность.</param>
    /// <param name="intensityDifference">Отношение эксп.интенсивности к расчетной;</param>
    /// <param name="integralWidth">Интегральная ширина.</param>
    /// <param name="peakWidth">Ширина отражения на половине высоты пика.</param>
    /// <param name="topAngle">Угол отражения.</param>
    /// <param name="interplanarDistance">Межплоскостное расстояние.</param>
    public PeakInfo(double intensityMaximum, double intensityIntegral, double intensityApproximated,
        double intensityDifference, double integralWidth, double peakWidth, double topAngle,
        double interplanarDistance)
    {
        IntensityMaximum = intensityDifference;
        IntensityIntegral = intensityIntegral;
        IntensityApproximated = intensityApproximated;
        IntensityDifference = intensityDifference;
        IntegralWidth = integralWidth;
        PeakWidth = peakWidth;
        TopAngle = topAngle;
        InterplanarDistance = interplanarDistance;
    }
}