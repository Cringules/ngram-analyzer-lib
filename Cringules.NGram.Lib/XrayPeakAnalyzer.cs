using Cringules.NGram.Lib.Approximation;

namespace Cringules.NGram.Lib;

/// <summary>
/// Класс, отвечающий за расчет характеристик пика:
/// - максимальная интенсивность;
/// - измеренная интегральная интенсивность (эксп.);
/// - расчетная интегральная интенсивность;
/// - отношение эксп.интенсивности к расчетной;
/// - интегральная ширина;
/// - ширина отражения на половине высоты пика;
/// - угол отражения;
/// - межплоскостное расстояние,
/// и выгрузку характеристик в структуру для записи в итоговую таблицу.
/// </summary>
public class XrayPeakAnalyzer
{
    /// <summary>
    /// Метод для получения максимальной интенсивности.
    /// </summary>
    /// <returns>Максимальная интенсивность.</returns>
    public double GetIntensityMaximum(XrayPeak peak)
    {
        return peak.GetPeakTop().Y - peak.BackgroundLevel;
    }

    /// /// <summary>
    /// Метод для получения измеренной интегральной интенсивности.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <returns>Измеренная интегральная интенсивность.</returns>
    public double GetIntensityIntegral(XrayPeak peak)
    {
        double intensity = 0;
        for (int i = 0; i < peak.Points.Count - 1; i++)
        {
            if (peak.Points[i].Y > peak.BackgroundLevel)
            {
                intensity += (peak.Points[i + 1].X - peak.Points[i].X) *
                             (peak.Points[i].Y - peak.BackgroundLevel);
            }
        }

        return intensity;
    }

    /// <summary>
    /// Метод для получения расчетной интегральной интенсивности.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <param name="approximation">Результат аппроксимации пика.</param>
    /// <returns>Расчетная интегральная интенсивность.</returns>
    public double GetIntensityApproximated(XrayPeak peak, ApproximationResult approximation)
    {
        double intensity = 0;
        for (int i = 0; i < approximation.Points.Count - 1; i++)
        {
            if (peak.Points[i].Y > peak.BackgroundLevel)
            {
                intensity += (approximation.Points[i + 1].X - approximation.Points[i].X) *
                             (approximation.Points[i].Y - peak.BackgroundLevel);
            }
        }

        return intensity;
    }

    /// <summary>
    /// Метод для получения отношения эксп.интенсивности к расчетной.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <param name="approximation">Результат аппроксимации пика.</param>
    /// <returns>Отношение эксп.интенсивности к расчетной.</returns>
    public double GetIntensityDifference(XrayPeak peak, ApproximationResult approximation)
    {
        return GetIntensityIntegral(peak) / GetIntensityApproximated(peak, approximation);
    }

    /// <summary>
    /// Метод для получения интегральной ширины.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <returns>Интегральная ширина.</returns>
    public double GetIntegralWidth(XrayPeak peak)
    {
        return (GetIntensityIntegral(peak) / GetIntensityMaximum(peak));
    }

    /// <summary>
    /// Метод для получения ширины отражения на половине высоты пика.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <returns>Ширина отражения на половине высоты пика.</returns>
    public double GetPeakWidth(XrayPeak peak)
    {
        double halfHeight = (peak.GetPeakTop().Y + peak.BackgroundLevel) / 2;

        return peak.Points.FindLast(p => p.Y >= halfHeight).X -
               peak.Points.Find(p => p.Y >= halfHeight).X;
    }

    /// <summary>
    /// Метод для получения угла отражения.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <returns>Угол отражения.</returns>
    public double GetTopAngle(XrayPeak peak)
    {
        return peak.GetPeakTop().X;
    }

    /// <summary>
    /// Метод для получения межплоскостного расстояния.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <param name="lambda">Длина волны.</param>
    /// <returns></returns>
    public double GetInterplanarDistance(XrayPeak peak, double lambda)
    {
        return lambda / (2 * Math.Sin(GetTopAngle(peak) * Math.PI / 360));
    }

    /// <summary>
    /// Метод для получения структуры со всеми характеристиками пика.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <param name="approximation">Результат аппроксимации пика.</param>
    /// <param name="lambda">Длина волны.</param>
    /// <returns>Структура характеристик.</returns>
    public PeakInfo GetPeakInfo(XrayPeak peak, ApproximationResult approximation, double lambda)
    {
        return new PeakInfo(GetIntensityMaximum(peak), GetIntensityIntegral(peak),
            GetIntensityApproximated(peak, approximation),
            GetIntensityDifference(peak, approximation),
            GetIntegralWidth(peak), GetPeakWidth(peak), GetTopAngle(peak),
            GetInterplanarDistance(peak, lambda));
    }
}