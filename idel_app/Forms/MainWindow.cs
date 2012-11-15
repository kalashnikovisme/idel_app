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
using idel_app.Functions;

namespace idel_app {
  public partial class MainWindow : ManagerAppForm {
    private FunctionPanel leftFunctionPanel;
    private FunctionPanel rightFunctionPanel;

    private FunctionPanel[] functionsGroup;

    public MainWindow() {
      InitializeComponent();
      InitializeMainFunctionPanels();
      InitializeFunctionPanelsForFunctions();
      RequestFunctionGroupInitialize();
    }

    private void InitializeMainFunctionPanels() {
      leftFunctionPanel = new FunctionPanel() {
        Width = ConstForms.WIDTH_LEFT_FUNCTION_PANEL,
        Dock = DockStyle.Left,
        TableBorderStyle = BorderStyle.Fixed3D
      };
      leftFunctionPanel.ReverseGradient = true;
      this.Controls.Add(leftFunctionPanel);

      rightFunctionPanel = new FunctionPanel() {
        Width = this.ClientRectangle.Width - ConstForms.WIDTH_LEFT_FUNCTION_PANEL,
        Dock = DockStyle.Right,
        TableBorderStyle = BorderStyle.Fixed3D
      };
      this.Controls.Add(rightFunctionPanel);
    }

    private void InitializeFunctionPanelsForFunctions() {
      functionsGroup = new FunctionPanel[ConstFunctions.FUNCTION_GROUPS_COUNT];
      for (int i = 0; i < functionsGroup.Length; i++) {
        functionsGroup[i] = new FunctionPanel() {
          Dock = DockStyle.Top,
          ReverseGradient = true
        };
      }

      functionsGroup[ConstFunctions.REQUEST_INDEX].ColumnCount = 1;
      functionsGroup[ConstFunctions.REQUEST_INDEX].RowCount = ConstFunctions.REQUEST_FUNCTIONS_COUNT;
      functionsGroup[ConstFunctions.REQUEST_INDEX].Size = new System.Drawing.Size(functionsGroup[ConstFunctions.REQUEST_INDEX].Width, ConstForms.ROW_HEIGHT * ConstFunctions.REQUEST_FUNCTIONS_COUNT);

      leftFunctionPanel.Controls.AddRange(functionsGroup);
    }

    private void RequestFunctionGroupInitialize() {
      OpacityLinkLabel[] requestFunctionGroupLinkLabels = new OpacityLinkLabel[ConstFunctions.REQUEST_WORKSPACE_COUNT];
      for (int i = 0; i < ConstFunctions.REQUEST_WORKSPACE_COUNT; i++) {
        requestFunctionGroupLinkLabels[i] = new OpacityLinkLabel() {
          Dock = DockStyle.Fill,
          Font = new Font("Times New Roman", 12F),
          Indent = OpacityLinkLabel.ControlIndent.MemberOfList
        };
        functionsGroup[ConstFunctions.REQUEST_INDEX].Controls.AddRange(requestFunctionGroupLinkLabels);
      }
      requestFunctionGroupLinkLabels[0].Indent = OpacityLinkLabel.ControlIndent.FirstOfList;
      requestFunctionGroupLinkLabels[requestFunctionGroupLinkLabels.Length - 1].Indent = OpacityLinkLabel.ControlIndent.LastOfList;

      List<string> textLinkLabels = new List<string>() { ConstFunctions.REQUEST_VIEW_TITLE, 
                                                         ConstFunctions.REQUEST_PROVIDERS_VIEW_TITLE, 
                                                         ConstFunctions.REQUEST_PRODUCTS_VIEW_TITLE };

      for (int i = 0; i < requestFunctionGroupLinkLabels.Length; i++) {
        requestFunctionGroupLinkLabels[i].Text = textLinkLabels[i];
      }

      AppButton createRequestButton = new AppButton() {
        Dock = DockStyle.Fill,
        Text = "Создать заявку",
        Font = new Font("Times New Roman", 15F)
      };

      functionsGroup[ConstFunctions.REQUEST_INDEX].Controls.Add(createRequestButton);

      requestFunctionGroupLinkLabels[textLinkLabels.LastIndexOf(ConstFunctions.REQUEST_VIEW_TITLE)].Click += new EventHandler(RequestView_Click);
      requestFunctionGroupLinkLabels[textLinkLabels.LastIndexOf(ConstFunctions.REQUEST_PROVIDERS_VIEW_TITLE)].Click += new EventHandler(RequestViewProvider_Click);
      requestFunctionGroupLinkLabels[textLinkLabels.LastIndexOf(ConstFunctions.REQUEST_PRODUCTS_VIEW_TITLE)].Click += new EventHandler(RequestViewProducts_Click);
    }

    private void RequestViewProducts_Click(object sender, EventArgs e) {
      throw new NotImplementedException();
    }

    private void RequestViewProvider_Click(object sender, EventArgs e) {
      throw new NotImplementedException();
    }

    private void RequestView_Click(object sender, EventArgs e) {
      throw new NotImplementedException();
    }
  }
}