namespace Cringules.NGram.Lib.approximation;

/// <summary>
/// Интерфейс для классов-аппроксиматоров (объединение автоматической и ручной аппроксимации).
/// </summary>
public interface IApproximator : IAutoApproximator, IManualApproximator
{
}