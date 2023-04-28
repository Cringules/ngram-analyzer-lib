namespace Cringules.NGram.Lib;

/// <summary>
/// Класс, отвечающий за расчет характеристик пика.
/// TODO: тут еще будут доп.методы + добавить зависимость от лямбды.
/// </summary>
public class XrayPeakAnalyzer
{
    /// <summary>
    /// Исследуемый пик.
    /// </summary>
    private XrayPeak peak;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    public XrayPeakAnalyzer(XrayPeak peak)
    {
        this.peak = peak; // тут копировать или так оставить?
    }

    /// <summary>
    /// Метод для получения максимальной интенсивности.
    /// </summary>
    /// <returns>Максимальная интенсивность.</returns>
    public double GetIntensityMax()
    {
        return peak.GetPeakTop().Y - peak.backgroundLevel; // тут же надо вычитать, или не надо, хмхм
    }
    
    /// <summary>
    /// Метод для получения угла отражения (оно же, да...).
    /// </summary>
    /// <returns>Угол отражения.</returns>
    public double GetTopAngle()
    {
        return peak.GetPeakTop().X;
    }
    
    /// <summary>
    /// TODO: Метод для получения интегральной интенсивности.
    /// </summary>
    /// <returns>Угол отражения.</returns>
    public double GetIntensityIntegral()
    {
        return 0; // тут будет площадь
    }
    
    // TODO: еще всякие методы.
}