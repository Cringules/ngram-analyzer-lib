namespace Cringules.NGram.Lib.Approximation;

/// <summary>
/// Структура, хранящая список точек - результат аппроксимации.
/// </summary>
public struct ApproximationResult
{
    /// <summary>
    /// Список точек.
    /// </summary>
    public List<Point> Points { get; }

    /// <summary>
    /// Конструктор структуры.
    /// </summary>
    /// <param name="points">Список точек.</param>
    public ApproximationResult(List<Point> points)
    {
        Points = new List<Point>(points);
    }
}