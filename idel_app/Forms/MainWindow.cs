using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.Forms;
using SpecialControls;

namespace idel_app {
  public partial class MainWindow : ManagerAppForm {
    private FunctionPanel LeftFunctionPanel;
    private FunctionPanel RightFunctionPanel;

    private FunctionPanel[] FunctionsGroup;

    public MainWindow() {
      InitializeComponent();
      InitializeFunctionPanels();
    }

    private void InitializeFunctionPanels() {
      LeftFunctionPanel = new FunctionPanel() {
        Width = ConstForms.WIDTH_LEFT_FUNCTION_PANEL,
        Dock = DockStyle.Left,
        TableBorderStyle = BorderStyle.Fixed3D
      };
      this.Controls.Add(LeftFunctionPanel);

      RightFunctionPanel = new FunctionPanel() {
        Width = this.ClientRectangle.Width - ConstForms.WIDTH_LEFT_FUNCTION_PANEL,
        Dock = DockStyle.Right,
        TableBorderStyle = BorderStyle.Fixed3D
      };
      this.Controls.Add(RightFunctionPanel);
    }
  }
}