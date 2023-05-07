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
    /// Исследуемый пик.
    /// </summary>
    private XrayPeak _peak;

    /// <summary>
    /// Последняя полученная расчетная интенсивность.
    /// </summary>
    private double _intensityApproximated = 1;

    /// <summary>
    /// Последнее полученное межплоскостное расстояние
    /// </summary>
    private double _interplanarDistance = 1;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    public XrayPeakAnalyzer(XrayPeak peak)
    {
        this._peak = peak; // тут копировать или так оставить?
    }

    /// <summary>
    /// Метод для получения максимальной интенсивности.
    /// </summary>
    /// <returns>Максимальная интенсивность.</returns>
    public double GetIntensityMaximum()
    {
        return _peak.GetPeakTop().Y - _peak.BackgroundLevel;
    }

    /// <summary>
    /// Метод для получения измеренной интегральной интенсивности.
    /// </summary>
    /// <returns>Измеренная интегральная интенсивность.</returns>
    public double GetIntensityIntegral()
    {
        double intensity = 0;
        for (int i = 0; i < _peak.Points.Count - 1; i++)
        {
            if (_peak.Points[i].Y > _peak.BackgroundLevel)
            {
                intensity += (_peak.Points[i + 1].X - _peak.Points[i].X) *
                             (_peak.Points[i].Y - _peak.BackgroundLevel);
            }
        }

        return intensity;
    }

    /// <summary>
    /// Метод для получения расчетной интегральной интенсивности.
    /// </summary>
    /// <param name="approximation">Результат аппроксимации пика.</param>
    /// <returns>Расчетная интегральная интенсивность.</returns>
    public double GetIntensityApproximated(ApproximationResult approximation)
    {
        double intensity = 0;
        for (int i = 0; i < approximation.Points.Count - 1; i++)
        {
            if (_peak.Points[i].Y > _peak.BackgroundLevel)
            {
                intensity += (approximation.Points[i + 1].X - approximation.Points[i].X) *
                             (approximation.Points[i].Y - _peak.BackgroundLevel);
            }
        }

        _intensityApproximated = intensity;

        return _intensityApproximated;
    }

    /// <summary>
    /// Метод для получения отношения эксп.интенсивности к расчетной.
    /// </summary>
    /// <returns>Отношение эксп.интенсивности к расчетной.</returns>
    public double GetIntensityDifference()
    {
        return GetIntensityIntegral() / _intensityApproximated;
    }

    /// <summary>
    /// Метод для получения интегральной ширины.
    /// </summary>
    /// <returns>Интегральная ширина.</returns>
    public double GetIntegralWidth()
    {
        return GetIntensityIntegral() / GetIntensityMaximum();
    }

    /// <summary>
    /// Метод для получения ширины отражения на половине высоты пика.
    /// </summary>
    /// <returns>Ширина отражения на половине высоты пика.</returns>
    public double GetPeakWidth()
    {
        double halfHeight = (_peak.GetPeakTop().Y + _peak.BackgroundLevel) / 2;

        return _peak.Points.FindLast(p => p.Y >= halfHeight).X -
               _peak.Points.Find(p => p.Y >= halfHeight).X;
    }

    /// <summary>
    /// Метод для получения угла отражения.
    /// </summary>
    /// <returns>Угол отражения.</returns>
    public double GetTopAngle()
    {
        return _peak.GetPeakTop().X;
    }

    /// <summary>
    /// Метод для получения межплоскостного расстояния.
    /// </summary>
    /// <param name="lambda"></param>
    /// <returns></returns>
    public double GetInterplanarDistance(double lambda)
    {
        _interplanarDistance = lambda / (2 * Math.Sin(GetTopAngle() / 2));
        return _interplanarDistance;
    }

    /// <summary>
    /// Метод для получения структуры со всеми характеристиками пика.
    /// </summary>
    /// <returns>Структура характеристик.</returns>
    public PeakInfo GetPeakInfo()
    {
        return new PeakInfo();
    }
}