namespace Cringules.NGram.Lib.Approximation;

/// <summary>
/// Интерфейс, отвечающий за метод ручной аппроксимации.
/// </summary>
public interface IManualApproximator
{
    /// <summary>
    /// Метод для ручной аппроксимации пика.
    /// </summary>
    /// <param name="peak">Аппроксимируемый пик.</param>
    /// <param name="xCoefficient">Параметр по оси абсцисс.</param>
    /// <param name="yCoefficient">Параметр по оси ординат.</param>
    /// <param name="backCoefficient">Параметр уровня фона.</param>
    /// <param name="n">Опциональный параметр.</param>
    /// <returns>Новый пик.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double xCoefficient, double yCoefficient,
        double backCoefficient, double n = 0);
}