namespace Cringules.NGram.Lib;

/// <summary>
/// Класс дифрактограммы.
/// </summary>
public class Xray
{
    /// <summary>
    /// Список точек.
    /// </summary>
    public List<Point> points { get; }

    /// <summary>
    /// Конструктор класса, принимает на вход два списка координат точек графика.
    /// </summary>
    /// <param name="points">Список точек пика.</param>
    public Xray(IEnumerable<Point> points)
    {
        this.points = new List<Point>(points);
    }

    /// <summary>
    /// TODO: Метод для сглаживания графика.
    /// TODO: Может быть тут добавим параметр для сглаживания, я еще не решила.
    /// </summary>
    /// <returns>Новый экземпляр класса - сглаженный график.</returns>
    public Xray SmoothXray()
    {
        Xray newXray = new(points);
        return newXray;
    }

    /// <summary>
    /// TODO: Метод для выделения примерных границ пиков графика.
    /// </summary>
    /// <returns>Список координат по X - границ пиков.</returns>
    public List<Point> GetPeakBoundaries()
    {
        List<Point> coordsList = new() { points[0] };

        // не менять на LINQ-запрос, я мб тут чет умнее придумаю:D
        for (var i = 1; i < points.Count - 1; i++)
        {
            if (points[i - 1].Y > points[i].Y && points[i + 1].Y > points[i].Y)
            {
                coordsList.Add(points[i]);
            }
        }

        coordsList.Add(points[^1]);
        
        return coordsList;
    }

    /// <summary>
    /// TODO: Метод для выделения пика по заданным координатам (по X) начала и конца.
    /// </summary>
    /// <param name="xBegin">Координата начала по X.</param>
    /// <param name="xEnd">Координата конца по X.</param>
    /// <returns>Объект класса XrayPeak - выделенный пик.</returns>
    public XrayPeak GetPeak(double xBegin, double xEnd)
    {
        var begin = points.FindIndex(0, points.Count, p => p.X >= xBegin);
        var end = points.FindLastIndex(0, points.Count, p => p.X <= xEnd);
        return new XrayPeak(points.GetRange(begin, end - begin + 1));
    }
}