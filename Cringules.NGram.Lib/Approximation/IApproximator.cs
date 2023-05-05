namespace Cringules.NGram.Lib.Approximation;

/// <summary>
/// Интерфейс для классов-аппроксиматоров (объединение автоматической и ручной аппроксимации).
/// </summary>
public interface IApproximator : IAutoApproximator, IManualApproximator
{
}