namespace Cringules.NGram.Lib;

/// <summary>
/// Класс пика дифрактограммы.
/// </summary>
public class XrayPeak
{
    private static double TOLERANCE = 0.000001;

    /// <summary>
    /// Список точек.
    /// </summary>
    public List<Point> points { get; }

    /// <summary>
    /// Уровень фона.
    /// </summary>
    public double backgroundLevel { get; private set; } // как нормально эти переменные называть XD

    /// <summary>
    /// Конструктор класса, принимает на вход список точек пика.
    /// </summary>
    /// <param name="points">Список точек пика.</param>
    public XrayPeak(IEnumerable<Point> points)
    {
        this.points = new List<Point>(points);
        backgroundLevel = this.points[0].Y;
    }

    /// <summary>
    /// TODO: Метод для получения координаты по X вершины пика.
    /// </summary>
    /// <returns>Угол при максимальном значении пика (координата по X).</returns>
    public Point GetPeakTop()
    {
        return points[points.FindIndex(p => Math.Abs(p.Y - points.Max(p0 => p0.Y)) < TOLERANCE)];
    }

    /// <summary>
    /// Метод для получения самого левого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <returns>Новый уровень фона - самое левое значение пика.</returns>
    public double GetLeftYValue()
    {
        backgroundLevel = points[0].Y;
        return backgroundLevel;
    }

    /// <summary>
    /// Метод для получения самого правого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <returns>Новый уровень фона - самое правое значение пика.</returns>
    public double GetRightYValue()
    {
        backgroundLevel = points[^1].Y;
        return backgroundLevel;
    }

    /// <summary>
    /// TODO: Метод для ручной установки уровня фона.
    /// TODO: Можно сделать методом, возвращающим bool, чтобы была проверка на уровень фона не больше вершины.
    /// TODO: А может backgroundLevel свойством сделать...
    /// </summary>
    /// <param name="newBackgroundLevel">Новое значение фона.</param>
    public void SetBackgroundLevel(double newBackgroundLevel)
    {
        backgroundLevel = newBackgroundLevel;
    }

    /// <summary>
    /// Метод для симметризации пика (по левой части).
    /// </summary>
    /// <returns>Новый пик, отсимметризованный по левой части.</returns>
    public XrayPeak SymmetrizePeakLeft()
    {
        List<Point> newPoints =
            new(points.GetRange(0,
                points.FindIndex(p => Math.Abs(p.Y - points.Max(p0 => p0.Y)) < TOLERANCE)));
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
        List<Point> newPoints =
            new(points.GetRange(0,
                points.FindIndex(p => Math.Abs(p.Y - points.Max(p0 => p0.Y)) < TOLERANCE)));
        List<Point> symmetricalPoints = new(newPoints);
        symmetricalPoints.Reverse();
        var xTop = GetPeakTop().X;
        newPoints = newPoints.Concat(symmetricalPoints.Select(p => new Point(2 * xTop - p.X, p.Y)))
            .ToList();

        return new XrayPeak(newPoints);
    }
}