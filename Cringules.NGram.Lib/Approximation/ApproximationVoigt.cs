using System.Reflection;
using System.Runtime.Versioning;
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

        var x = peak.Points.Select(p => p.X).ToList();
        var y = peak.Points.Select(p => p.Y).ToList();

        var peakTopX = peak.GetPeakTop().X;
        var peakTopY = peakAnalyzer.GetIntensityMaximum(peak);
        var halfWidth = 0.5 * peakAnalyzer.GetPeakWidth(peak);
        var integralBreadth = 0.5 * peakAnalyzer.GetPeakWidth(peak) *
                              Math.Pow(Math.PI / Math.Log(2), 0.5);
        
        double n;

        using (var scope = Py.CreateScope())
        {
            Stream? stream = GetType().Assembly.GetManifestResourceStream("Cringules.NGram.Lib.PythonScripts.AutoVoigt.py");
            string code;
            using (StreamReader s = new StreamReader(stream))
            {
                code = s.ReadToEnd();
            }
            
            var compiledCode = PythonEngine.Compile(code, "AutoVoigt.py");
            scope.Execute(compiledCode);
            PyObject exampleClass = scope.Get("approximation");
            PyObject pythongReturn =
                exampleClass.InvokeMethod("run", new PyObject[]
                {
                    exampleClass, x.ToPython(), y.ToPython(), peak.BackgroundLevel.ToPython(),
                    peakTopX.ToPython(), peakTopY.ToPython(), halfWidth.ToPython(),
                    integralBreadth.ToPython()
                });
            dynamic result = pythongReturn;
            n = result;
        }

        PythonEngine.Shutdown();

        var gaussian = (new ApproximationGaussian()).ApproximatePeakAuto(peak);
        var lorentz = (new ApproximationLorentz()).ApproximatePeakAuto(peak);

        var newPoints = gaussian.Points.Select((t, i) => new Point(t.X, n * t.Y + (1 - n) * lorentz
            .Points[i].Y)).ToList();

        return new ApproximationResult(newPoints, n);
    }

    /// <summary>
    /// Метод для ручной аппроксимации пика по Войту.
    /// </summary>
    /// <returns>Результат аппроксимации.</returns>
    public ApproximationResult ApproximatePeakManual(XrayPeak peak, double xCoefficient,
        double yCoefficient, double backCoefficient, double n = 0)
    {
        var gaussian = (new ApproximationGaussian()).ApproximatePeakManual(peak, xCoefficient,
            yCoefficient, backCoefficient);
        var lorentz = (new ApproximationLorentz()).ApproximatePeakManual(peak, xCoefficient,
            yCoefficient, backCoefficient);

        var newPoints = gaussian.Points.Select((t, i) => new Point(t.X, n * t.Y + (1 - n) * lorentz
            .Points[i].Y)).ToList();

        return new ApproximationResult(peak.Points, n);
    }
}