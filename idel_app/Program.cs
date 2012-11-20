using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace idel_app {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(mainWindow = new MainWindow());
    }

    internal static MainWindow mainWindow;
    internal static idel_app.Middle.MiddleClass mainMiddleClass = new Middle.MiddleClass();
  }
}