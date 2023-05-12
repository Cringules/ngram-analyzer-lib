using Python.Included;
using Python.Runtime;

namespace Cringules.NGram.Lib;

/// <summary>
/// Класс, отвечающий за установку и доступ к необходимым библиотекам из Python.
/// </summary>
public class PyLibs
{
    /// <summary>
    /// Библиотека numpy.
    /// </summary>
    public static dynamic NumPy { get; }

    /// <summary>
    /// Библиотека scipy.
    /// </summary>
    public static dynamic SciPy { get; }

    /// <summary>
    /// Подмодуль scipy.signal.
    /// </summary>
    public static dynamic SciPySignal { get; }
    
    /// <summary>
    /// Подмодуль scipy.optimize.
    /// </summary>
    public static dynamic SciPyOptimize { get; }

    /// <summary>
    /// Индикатор того, что библиотеки установлены.
    /// </summary>
    public static bool isInstalled { get; }

    /// <summary>
    /// Статический конструктор - установка библиотек.
    /// </summary>
    static PyLibs()
    {
        Installer.SetupPython();

        Installer.InstallWheel(typeof(PyLibs).Assembly, "numpy-1.24.3-cp311-cp311-win_amd64.whl")
            .Wait();
        Installer.InstallWheel(typeof(PyLibs).Assembly, "scipy-1.10.1-cp311-cp311-win_amd64.whl")
            .Wait();

        PythonEngine.Initialize();

        NumPy = Py.Import("numpy");
        SciPy = Py.Import("scipy");
        
        SciPySignal = Py.Import("scipy.signal");
        SciPyOptimize = Py.Import("scipy.optimize");

        PythonEngine.Shutdown();

        isInstalled = true;
    }
}