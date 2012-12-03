using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using V82;

namespace idel_app {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      idel_app.DB.CommandTo1C.Connect1C(@"C:\Documents and Settings\Филиппов Александр\Рабочий стол\Trade 82", "Александр", "301767", ref v82Base, ref connector);
      Application.Run(mainWindow = new MainWindow());
    }

    internal static MainWindow mainWindow;
    internal static idel_app.Middle.MiddleClass mainMiddleClass = new Middle.MiddleClass();
    internal static object v82Base = null;
    internal static COMConnectorClass connector = new COMConnectorClass();
  }
}