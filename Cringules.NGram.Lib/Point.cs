namespace Cringules.NGram.Lib;

/// <summary>
/// Структура, реализующая точку.
/// </summary>
public struct Point
{
    /// <summary>
    /// Координата по оси абсцисс.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Координата по оси ординат.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Конструктор точки.
    /// </summary>
    /// <param name="X">Координата по оси абсцисс.</param>
    /// <param name="Y">Координата по оси ординат.</param>
    public Point(double X, double Y)
    {
        this.X = X;
        this.Y = Y;
    }
}