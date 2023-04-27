namespace Cringules.NGram.Lib;

/// <summary>
/// Класс дифрактограммы.
/// </summary>
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
    /// <returns>Новый экземпляр класса - сглаженный график.</returns>
    public Xray SmoothXray()
    {
        Xray newXray = new(X, Y);
        return newXray;
    }

    /// <summary>
    /// TODO: Метод для выделения примерных границ пиков графика.
    /// </summary>
    /// <returns>Список координат по X - границ пиков.</returns>
    public List<double> GetPeakCoords()
    {
        List<double> coordsList = new();

        coordsList.Add(X[0]);

        // не менять на LINQ-запрос, я мб тут чет умнее придумаю:D
        for (var i = 0; i < X.Count; i++)
        {
            if (Y[i - 1] < Y[i] && Y[i + 1] < Y[i])
            {
                coordsList.Add(X[i]);
            }
        }

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
        var begin = X.FindIndex(0, X.Count - 1, x => x >= xBegin);
        var end = X.FindIndex(0, X.Count - 1, x => x <= xEnd);
        return new XrayPeak(X.GetRange(begin, end), Y.GetRange(begin, end));
    }
}