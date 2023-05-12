using Python.Included;
using Python.Runtime;

namespace Cringules.NGram.Lib.Approximation;

/// <summary>
/// Класс, реализующий аппроксимацию пика по Войту.
/// </summary>
public class ApproximationVoigt : IApproximator
{
    /// <summary>
    /// Метод для автоматической аппроксимации пика по Войту.
    /// </summary>
    /// <param name="peak">Исследуемый пик.</param>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakAuto(XrayPeak peak)
    {
        if (!PyLibs.isInstalled)
        {
            throw new NotSupportedException();
        }

        PythonEngine.Initialize();
        
        var peakAnalyzer = new XrayPeakAnalyzer();

        var peakTopX = peak.GetPeakTop().X;
        var peakTopY = peakAnalyzer.GetIntensityMaximum(peak);
        var halfWidth = 0.5 * peakAnalyzer.GetPeakWidth(peak);
        var integralBreadth = 0.5 * peakAnalyzer.GetPeakWidth(peak) *
                              Math.Pow(Math.PI / Math.Log(2), 0.5);

        var file =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
            @"\PythonScripts\approximateVoigt.py";
        
        using (var scope = Py.CreateScope())
        {
            string code = File.ReadAllText(file);
            var compiledCode = PythonEngine.Compile(code, file);
            scope.Execute(compiledCode);
            PyObject exampleClass = scope.Get("approximation");
            PyObject pythongReturn = exampleClass.InvokeMethod("run", new PyObject[] { peakTopX.ToPython() });
            string? result = pythongReturn.AsManagedObject(typeof(string)) as string; // convert the returned string to managed string object
        }
        
        PythonEngine.Shutdown();
        
        return new(peak.Points);
    }

    /// <summary>
    /// TODO: Метод для ручной аппроксимации пика по Войту.
    /// </summary>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double height, double width,
        double corr, double lambda = 0)
    {
        return new ApproximationResult(peak.Points);
    }
}