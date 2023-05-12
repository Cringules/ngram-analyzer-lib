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
    public Xray SmoothXray(int coefficient = 70)
    {
        if (!PyLibs.isInstalled)
        {
            throw new NotSupportedException();
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
        List<double> diff = new();
        var points = new List<Point>(Points);

        for (int i = 1; i < points.Count; i++)
        {
            diff.Add(points[i].Y - points[i - 1].Y);
        }

        bool isCorrect = true;
        while (isCorrect)
        {
            for (int i = 1; i < points.Count - 2; i++)
            {
                if (Math.Sign(diff[i]) == Math.Sign(diff[i - 1]) ||
                    Math.Sign(diff[i]) == Math.Sign(diff[i + 1])) continue;
                isCorrect = false;
                points[i + 1] = new Point(points[i + 1].X, (points[i].Y + points[i + 2].Y) / 2);
                diff[i] = points[i].Y - points[i + 1].Y;
            }
        }

        List<Point> peakBoundaries = new();
        int prev = 0;
        for (int i = 1; i < points.Count - 1; i++)
        {
            if (Math.Sign(diff[i]) == 0) continue;
            
            if (Math.Sign(diff[i]) == -1 && Math.Sign(diff[prev]) == 1)
            {
                peakBoundaries.Add(points[i + 1]);
            }

            prev = i;
        }
        
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