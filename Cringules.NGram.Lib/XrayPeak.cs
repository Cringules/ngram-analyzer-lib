namespace Cringules.NGram.Lib;

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
    public XrayPeak(List<double> x, List<double> y)
    {
        X = new List<double>(x);
        Y = new List<double>(y);
    }

    /// <summary>
    /// TODO: Метод для получения координаты по X вершины пика.
    /// </summary>
    /// <param name="peak">Объект класса XrayPeak - данный пик.</param>
    /// <returns>Угол при максимальном значении пика (координата по X).</returns>
    public static double GetPeakTop(XrayPeak peak)
    {
        return peak.X[peak.Y.IndexOf(peak.Y.Max())];
    }

    /// <summary>
    /// Метод для получения самого левого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <param name="peak">Пик дифрактограммы.</param>
    /// <returns>Новый уровень фона - самое левое значение пика.</returns>
    public static double getLeftYValue(XrayPeak peak)
    {
        peak.backgroundLevel = peak.Y[0];
        return peak.backgroundLevel;
    }

    /// <summary>
    /// Метод для получения самого правого значения пика по Y и установки его как уровня фона.
    /// </summary>
    /// <param name="peak">Пик дифрактограммы.</param>
    /// <returns>Новый уровень фона - самое правое значение пика.</returns>
    public static double getRightYValue(XrayPeak peak)
    {
        peak.backgroundLevel = peak.Y[^1];
        return peak.backgroundLevel;
    }

    /// <summary>
    /// TODO: Метод для ручной установки уровня фона.
    /// TODO: Можно сделать методом, возвращающим bool, чтобы была проверка на уровень фона < вершины?
    /// </summary>
    /// <param name="peak"></param>
    /// <param name="backgroundLevel"></param>
    public static void setBackgroundLevel(XrayPeak peak, double backgroundLevel)
    {
        peak.backgroundLevel = backgroundLevel;
    }

    /// <summary>
    /// Метод для симметризации пика (по левой части).
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик, отсимметризованный по левой части.</returns>
    public static XrayPeak SymmetrizePeakLeft(XrayPeak oldPeak)
    {
        List<double> newX = new(oldPeak.X.GetRange(0, oldPeak.Y.IndexOf(oldPeak.Y.Max())));
        List<double> symmetricalX = new(newX);
        symmetricalX.Reverse();
        var xTop = GetPeakTop(oldPeak);
        newX = newX.Concat(symmetricalX.Select(x => (2 * xTop - x))).ToList();

        List<double> newY = new(oldPeak.Y.GetRange(0, oldPeak.Y.IndexOf(oldPeak.Y.Max())));
        List<double> symmetricalY = new(newY);
        symmetricalY.Reverse();
        newY = newY.Concat(symmetricalY).ToList();

        return new XrayPeak(newX, newY);
    }

    /// <summary>
    /// Метод для симметризации пика (по правой части).
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик, отсимметризованный по правой части.</returns>
    public static XrayPeak SymmetrizePeakRight(XrayPeak oldPeak)
    {
        List<double> newX = new(oldPeak.X.GetRange(oldPeak.Y.IndexOf(oldPeak.Y.Max()),
            oldPeak.Y.Count - 1));
        List<double> symmetricalX = new(newX);
        symmetricalX.Reverse();
        var xTop = GetPeakTop(oldPeak);
        newX = (symmetricalX.Select(x => (2 * xTop - x))).Concat(newX).ToList();

        List<double> newY = new(oldPeak.Y.GetRange(oldPeak.Y.IndexOf(oldPeak.Y.Max()),
            oldPeak.Y.Count - 1));
        List<double> symmetricalY = new(newY);
        symmetricalY.Reverse();
        newY = symmetricalY.Concat(newX).ToList();

        return new XrayPeak(newX, newY);
    }

    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Гауссу.
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик.</returns>
    public static XrayPeak ApproximatePeakGaussianAuto(XrayPeak oldPeak)
    {
        return new XrayPeak(oldPeak.X, oldPeak.Y);
    }

    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Лоренцу.
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик.</returns>
    public static XrayPeak ApproximatePeakLorentzAuto(XrayPeak oldPeak)
    {
        return new XrayPeak(oldPeak.X, oldPeak.Y);
    }

    /// <summary>
    /// TODO: Метод для автоматической аппроксимации пика по Войту.
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик.</returns>
    public static XrayPeak ApproximatePeakVoigtAuto(XrayPeak oldPeak)
    {
        return new XrayPeak(oldPeak.X, oldPeak.Y);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Гауссу.
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик.</returns>
    public static XrayPeak ApproximatePeakGaussianManual(XrayPeak oldPeak, double height,
        double width, double corr)
    {
        return new XrayPeak(oldPeak.X, oldPeak.Y);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Лоренцу.
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик.</returns>
    public static XrayPeak ApproximatePeakLorentzManual(XrayPeak oldPeak, double height,
        double width, double corr)
    {
        return new XrayPeak(oldPeak.X, oldPeak.Y);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Войту.
    /// </summary>
    /// <param name="oldPeak">Пик дифрактограммы.</param>
    /// <returns>Новый пик.</returns>
    public static XrayPeak ApproximatePeakVoigtManual(XrayPeak oldPeak, double height,
        double width, double corr, double coef)
    {
        return new XrayPeak(oldPeak.X, oldPeak.Y);
    }
}