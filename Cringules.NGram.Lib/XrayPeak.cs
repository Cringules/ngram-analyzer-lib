namespace Cringules.NGram.Lib;

/// <summary>
/// Класс пика дифрактограммы.
/// </summary>
public class XrayPeak
{
    /// <summary>
    /// Список координат пика по оси абсцисс.
    /// </summary>
    public List<double> X { get; }

    /// <summary>
    /// Список координат пика по оси ординат.
    /// </summary>
    public List<double> Y { get; }

    private double backgroundLevel; // как нормально эти переменные называть XD

    /// <summary>
    /// Конструктор класса, принимает на вход два списка координат точек пика.
    /// </summary>
    /// <param name="x">Список координат пика по оси абсцисс.</param>
    /// <param name="y">Список координат пика по оси ординат.</param>
    public XrayPeak(IEnumerable<double> x, IEnumerable<double> y)
    {
        X = new List<double>(x);
        Y = new List<double>(y);
        backgroundLevel = Y[0];
    }

    /// <summary>
    /// TODO: Метод для получения координаты по X вершины пика.
    /// </summary>
    /// <returns>Угол при максимальном значении пика (координата по X).</returns>
    public double GetPeakTop()
    {
        return X[Y.IndexOf(Y.Max())];
    }

    /// <summary>
    /// Метод для получения самого левого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <returns>Новый уровень фона - самое левое значение пика.</returns>
    public double getLeftYValue()
    {
        backgroundLevel = Y[0];
        return backgroundLevel;
    }

    /// <summary>
    /// Метод для получения самого правого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <returns>Новый уровень фона - самое правое значение пика.</returns>
    public double getRightYValue()
    {
        backgroundLevel = Y[^1];
        return backgroundLevel;
    }

    /// <summary>
    /// TODO: Метод для ручной установки уровня фона.
    /// TODO: Можно сделать методом, возвращающим bool, чтобы была проверка на уровень фона не больше вершины.
    /// TODO: А может backgroundLevel свойством сделать...
    /// </summary>
    /// <param name="newBackgroundLevel">Новое значение фона.</param>
    public void setBackgroundLevel(double newBackgroundLevel)
    {
        backgroundLevel = newBackgroundLevel;
    }

    /// <summary>
    /// Метод для симметризации пика (по левой части).
    /// </summary>
    /// <returns>Новый пик, отсимметризованный по левой части.</returns>
    public XrayPeak SymmetrizePeakLeft()
    {
        List<double> newX = new(X.GetRange(0, Y.IndexOf(Y.Max())));
        List<double> symmetricalX = new(newX);
        symmetricalX.Reverse();
        var xTop = GetPeakTop();
        newX = newX.Concat(symmetricalX.Select(x => (2 * xTop - x))).ToList();

        List<double> newY = new(Y.GetRange(0, Y.IndexOf(Y.Max())));
        List<double> symmetricalY = new(newY);
        symmetricalY.Reverse();
        newY = newY.Concat(symmetricalY).ToList();

        return new XrayPeak(newX, newY);
    }

    /// <summary>
    /// Метод для симметризации пика (по правой части).
    /// </summary>
    /// <returns>Новый пик, отсимметризованный по правой части.</returns>
    public XrayPeak SymmetrizePeakRight()
    {
        List<double> newX = new(X.GetRange(Y.IndexOf(Y.Max()), Y.Count - 1));
        List<double> symmetricalX = new(newX);
        symmetricalX.Reverse();
        var xTop = GetPeakTop();
        newX = (symmetricalX.Select(x => (2 * xTop - x))).Concat(newX).ToList();

        List<double> newY = new(Y.GetRange(Y.IndexOf(Y.Max()), Y.Count - 1));
        List<double> symmetricalY = new(newY);
        symmetricalY.Reverse();
        newY = symmetricalY.Concat(newX).ToList();

        return new XrayPeak(newX, newY);
    }

    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Гауссу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakGaussianAuto()
    {
        return new XrayPeak(X, Y);
    }

    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Лоренцу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakLorentzAuto()
    {
        return new XrayPeak(X, Y);
    }

    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Войту.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakVoigtAuto()
    {
        return new XrayPeak(X, Y);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Гауссу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakGaussianManual(double height, double width, double corr)
    {
        return new XrayPeak(X, Y);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Лоренцу.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakLorentzManual(double height, double width, double corr)
    {
        return new XrayPeak(X, Y);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Войту.
    /// </summary>
    /// <returns>Новый пик.</returns>
    public XrayPeak ApproximatePeakVoigtManual(double height, double width, double corr, double coef)
    {
        return new XrayPeak(X, Y);
    }
}