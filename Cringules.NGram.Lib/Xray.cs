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

        dynamic smoothedY = PyLibs.NumPy.average(
            PyLibs.NumPy.lib.stride_tricks.sliding_window_view(
                PyLibs.SciPySignal.savgol_filter(
                    PyLibs.NumPy.average(
                        PyLibs.NumPy.lib.stride_tricks.sliding_window_view(y, window_shape: 11),
                        axis: 1), coefficient, 4), window_shape: 11), axis: 1);
            
        foreach (double el in smoothedY)
        {
            newY.Add(el);
        }

        PythonEngine.Shutdown();
        
        List<Point> newPoints = Points.GetRange(10, Points.Count - 20).Select((t, i) => new Point(t.X, newY[i])).ToList();

        Xray newXray = new(newPoints);
        return newXray;
    }

    /// <summary>
    /// Метод для выделения примерных границ пиков графика.
    /// </summary>
    /// <returns>Список координат по X - границ пиков.</returns>
    public List<Point> GetPeakBoundaries()
    {
        List<Point> peakBoundaries = new();
        
        if (!PyLibs.isInstalled)
        {
            throw new NotSupportedException();
        }

        PythonEngine.Initialize();

        var y = Points.Select(p => p.Y).ToList();

        dynamic mins = PyLibs.SciPySignal.argrelextrema(PyLibs.NumPy.array(y), PyLibs.NumPy.less_equal, order: 50)[0];
            
        foreach (int el in mins)
        {
            peakBoundaries.Add(Points[el]);
        }

        PythonEngine.Shutdown();
        
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