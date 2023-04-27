namespace Cringules.NGram.Lib;

public class Xray
{
    /// <summary>
    /// Список координат графика по оси абсцисс.
    /// </summary>
    public List<double> X { get; }

    /// <summary>
    /// Список координат графика по оси ординат.
    /// </summary>
    public List<double> Y { get; }

    /// <summary>
    /// Конструктор класса, принимает на вход два списка координат точек графика.
    /// </summary>
    /// <param name="x">Список координат графика по оси абсцисс.</param>
    /// <param name="y">Список координат графика по оси ординат.</param>
    public Xray(List<double> x, List<double> y)
    {
        X = new List<double>(x);
        Y = new List<double>(y);
    }

    /// <summary>
    /// TODO: Метод для сглаживания графика.
    /// TODO: Может быть тут добавим параметр для сглаживания, я еще не решила.
    /// </summary>
    /// <param name="oldXray">Объект класса Xray - дифрактограмма для сглаживания.</param>
    /// <returns>Новый экземпляр класса - сглаженный график.</returns>
    public static Xray SmoothXray(Xray oldXray)
    {
        Xray newXray = new(oldXray.X, oldXray.Y);
        return newXray;
    }

    /// <summary>
    /// TODO: Метод для выделения примерных границ пиков графика.
    /// </summary>
    /// <param name="xray">Дифрактограмма.</param>
    /// <returns>Список координат по X - границ пиков.</returns>
    public static List<double> GetPeakCoords(Xray xray)
    {
        List<double> coordsList = new();

        coordsList.Add(xray.X[0]);

        // не менять на LINQ-запрос, я мб тут чет умнее придумаю:D
        for (var i = 0; i < xray.X.Count; i++)
        {
            if (xray.Y[i - 1] < xray.Y[i] && xray.Y[i + 1] < xray.Y[i])
            {
                coordsList.Add(xray.X[i]);
            }
        }

        return coordsList;
    }

    /// <summary>
    /// TODO: Метод для выделения пика по заданным координатам (по X) начала и конца.
    /// </summary>
    /// <param name="xray">Дифрактограмма.</param>
    /// <param name="xBegin">Координата начала по X.</param>
    /// <param name="xEnd">Координата конца по X.</param>
    /// <returns>Объект класса XrayPeak - выделенный пик.</returns>
    public static XrayPeak GetPeak(Xray xray, double xBegin, double xEnd)
    {
        var begin = xray.X.FindIndex(0, xray.X.Count - 1, x => x >= xBegin);
        var end = xray.X.FindIndex(0, xray.X.Count - 1, x => x <= xEnd);
        return new XrayPeak(xray.X.GetRange(begin, end), xray.Y.GetRange(begin, end));
    }
}