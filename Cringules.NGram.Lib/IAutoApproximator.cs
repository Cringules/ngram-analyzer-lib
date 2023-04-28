namespace Cringules.NGram.Lib;

/// <summary>
/// Интерфейс, отвечающий за метод автоматической аппроксимации.
/// </summary>
public interface IAutoApproximator
{
    /// <summary>
    /// Метод для автоматической аппроксимации пика.
    /// </summary>
    /// <param name="peak">Аппроксимируемый пик.</param>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakAuto(XrayPeak peak);
}