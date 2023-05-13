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
    /// Коэффициент Псевдо-Войта.
    /// </summary>
    public double N { get; }

    /// <summary>
    /// Конструктор структуры.
    /// </summary>
    /// <param name="points">Список точек.</param>
    /// <param name="n">Коэффициент Псевдо-Войта.</param>
    public ApproximationResult(IEnumerable<Point> points, double n)
    {
        Points = new List<Point>(points);
        N = n;
    }
}