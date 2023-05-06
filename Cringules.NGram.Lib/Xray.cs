using Python.Included;
using Python.Runtime;

namespace Cringules.NGram.Lib;

/// <summary>
/// Класс дифрактограммы.
/// </summary>
public class Xray
{
    /// <summary>
    /// Список точек.
    /// </summary>
    public List<Point> Points { get; }

    /// <summary>
    /// Конструктор класса, принимает на вход два списка координат точек графика.
    /// </summary>
    /// <param name="points">Список точек пика.</param>
    public Xray(IEnumerable<Point> points)
    {
        this.Points = new List<Point>(points);
    }

    /// <summary>
    /// Метод для сглаживания графика c помощью алгоритма Савицкого-Голея.
    /// </summary>
    /// <param name="coefficient">Коэффициент сглаживания графика.</param>
    /// <returns>Новый экземпляр класса - сглаженный график.</returns>
    public Xray SmoothXray(int coefficient)
    {
        if (!PyLibs.isInstalled)
        {
            return new Xray(Points);
        }
        
        PythonEngine.Initialize();

        var y = Points.Select(x => x.Y).ToList();
        var newY = new List<double>();
        
        dynamic smoothedY = (PyLibs.SciPySignal.savgol_filter(y, coefficient, 2));
        foreach (double el in smoothedY)
        {
            newY.Add(el);
        }
        
        PythonEngine.Shutdown();

        List<Point> newPoints = Points.Select((t, i) => new Point(t.X, newY[i])).ToList();

        Xray newXray = new(newPoints);
        return newXray;
    }

    /// <summary>
    /// Метод для выделения примерных границ пиков графика.
    /// </summary>
    /// <returns>Список координат по X - границ пиков.</returns>
    public List<Point> GetPeakBoundaries()
    {
        List<Point> peakBoundaries = new() { Points[0] };
        
        // Показатель того, что мы в начале дифрактограммы - надо узнать, в данный момент пик растет или уменьшается.
        bool isChecking = true;
        // Показатель состояния пика - true, если растет, иначе false.
        bool isRaising = true;

        for (var i = 1; i < Points.Count - 1; i++)
        {
            if (isChecking && Points[i - 1].Y != Points[i].Y)
            {
                isRaising = (Points[i - 1].Y < Points[i].Y);
                isChecking = false;
            }
            else if (isRaising && Points[i - 1].Y > Points[i].Y)
            {
                isRaising = false;
            }
            else if (!isRaising && Points[i - 1].Y < Points[i].Y)
            {
                peakBoundaries.Add(Points[i - 1]);
                isRaising = true;
            }
        }
        
        peakBoundaries.Add(Points[^1]);

        return peakBoundaries;
    }

    /// <summary>
    /// Метод для выделения пика по заданным координатам (по X) начала и конца.
    /// </summary>
    /// <param name="xBegin">Координата начала по X.</param>
    /// <param name="xEnd">Координата конца по X.</param>
    /// <returns>Объект класса XrayPeak - выделенный пик.</returns>
    public XrayPeak GetPeak(double xBegin, double xEnd)
    {
        var begin = Points.FindIndex(p => p.X >= xBegin);
        var end = Points.FindLastIndex(p => p.X <= xEnd);
        return new XrayPeak(Points.GetRange(begin, end - begin + 1));
    }
}
