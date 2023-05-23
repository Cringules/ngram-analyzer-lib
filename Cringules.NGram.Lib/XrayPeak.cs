using Python.Included;
using Python.Runtime;

namespace Cringules.NGram.Lib;

/// <summary>
/// Класс пика дифрактограммы.
/// </summary>
public class XrayPeak
{
    /// <summary>
    /// Список точек.
    /// </summary>
    public List<Point> Points { get; }

    /// <summary>
    /// Уровень фона.
    /// </summary>
    public double BackgroundLevel { get; private set; }

    /// <summary>
    /// Конструктор класса, принимает на вход список точек пика.
    /// </summary>
    /// <param name="points">Список точек пика.</param>
    public XrayPeak(IEnumerable<Point> points)
    {
        Points = new List<Point>(points);
        BackgroundLevel = Math.Max(Points[0].Y, Points[^1].Y);
    }

    /// <summary>
    /// Метод для получения точки-вершины пика.
    /// </summary>
    /// <returns>Точка-вершина пика.</returns>
    public Point GetPeakTop()
    {
        return Points[Points.FindIndex(p => p.Y == Points.Max(p0 => p0.Y))];
    }

    /// <summary>
    /// Метод для получения самого левого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <returns>Новый уровень фона - самое левое значение пика.</returns>
    public double GetLeftYValue()
    {
        BackgroundLevel = Points[0].Y;
        return BackgroundLevel;
    }

    /// <summary>
    /// Метод для получения самого правого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <returns>Новый уровень фона - самое правое значение пика.</returns>
    public double GetRightYValue()
    {
        BackgroundLevel = Points[^1].Y;
        return BackgroundLevel;
    }

    /// <summary>
    /// Метод для ручной установки уровня фона.
    /// </summary>
    /// <param name="newBackgroundLevel">Новое значение фона.</param>
    /// <returns>True, если новый уровень в допустимых интервалах, иначе false.</returns>
    public bool SetBackgroundLevel(double newBackgroundLevel)
    {
        if (newBackgroundLevel < Math.Max(Points[0].Y, Points[^1].Y) ||
            newBackgroundLevel >= GetPeakTop().Y)
        {
            return false;
        }

        BackgroundLevel = newBackgroundLevel;
        return true;
    }

    /// <summary>
    /// Метод для симметризации пика (по левой части).
    /// </summary>
    /// <returns>Новый пик, отсимметризованный по левой части.</returns>
    public XrayPeak SymmetrizePeakLeft()
    {
        List<Point> newPoints =
            new(Points.GetRange(0, Points.FindIndex(p => p.Y == GetPeakTop().Y)));
        List<Point> symmetricalPoints = new(newPoints);
        symmetricalPoints.Reverse();
        var xTop = GetPeakTop().X;
        newPoints = newPoints.Concat(symmetricalPoints.Select(p => new Point(2 * xTop - p.X, p.Y)))
            .ToList();

        return new XrayPeak(newPoints);
    }

    /// <summary>
    /// Метод для симметризации пика (по правой части).
    /// </summary>
    /// <returns>Новый пик, отсимметризованный по правой части.</returns>
    public XrayPeak SymmetrizePeakRight()
    {
        var topIndex = Points.FindIndex(p => p.Y == GetPeakTop().Y);

        List<Point> newPoints =
            new(Points.GetRange(topIndex, Points.Count - topIndex));
        List<Point> symmetricalPoints = new(newPoints);
        symmetricalPoints.Reverse();
        var xTop = GetPeakTop().X;
        newPoints = (symmetricalPoints.Select(p => new Point(2 * xTop - p.X, p.Y)))
            .Concat(newPoints).ToList();

        return new XrayPeak(newPoints);
    }
}