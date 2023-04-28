namespace Cringules.NGram.Lib;

/// <summary>
/// Интерфейс, отвечающий за метод ручной аппроксимации.
/// </summary>
public interface IManualApproximator
{
    /// <summary>
    /// Метод для ручной аппроксимации пика.
    /// </summary>
    /// <param name="peak">Аппроксимируемый пик.</param>
    /// <param name="height">Параметр высоты.</param>
    /// <param name="width">Параметр ширины.</param>
    /// <param name="corr">Параметр корреляции.</param>
    /// <param name="lambda">Опциональный параметр.</param>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakManual(XrayPeak peak, double height, double width,
        double corr, double lambda = 0);
}