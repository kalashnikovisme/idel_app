﻿using System;
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
    private FunctionPanel fullFunctionPanel;
    private FunctionPanel leftFunctionPanel;
    private FunctionPanel rightFunctionPanel;

    private FunctionPanel[] functionsGroup;

    public MainWindow() {
      InitializeComponent();
      InitializeMainFunctionPanels();
      InitializeFunctionPanelsForFunctions();
      RequestFunctionGroupInitialize();
    }

    /// <summary>
    /// Инициализирует два основных поля интерфейса
    /// </summary>
    private void InitializeMainFunctionPanels() {
      fullFunctionPanel = new FunctionPanel() {
        Dock = DockStyle.Fill,
        RowCount = 1,       // badcode
        ColumnCount = 2,     // badcode
      };
      fullFunctionPanel.ColumnStyles.Insert(0, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, ConstForms.COLUMN_WIDTH));
      fullFunctionPanel.ColumnStyles.Insert(1, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
      this.Controls.Add(fullFunctionPanel);

      leftFunctionPanel = new FunctionPanel() {
        Dock = DockStyle.Fill
      };
      leftFunctionPanel.ReverseGradient = true;
      this.fullFunctionPanel.Controls.Add(leftFunctionPanel, 0, 0);   // badcode

      rightFunctionPanel = new FunctionPanel() {
        Dock = DockStyle.Fill
      };
      this.fullFunctionPanel.Controls.Add(rightFunctionPanel, 1, 0);  // badcode
    }

    /// <summary>
    /// Инициализирует поля для групп функций внутри левого основного поля интерфейса
    /// </summary>
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

    /// <summary>
    /// Добавляет контролы управления в поле групп функций для заявок
    /// </summary>
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

      createRequestButton.Click += new EventHandler(createRequestButton_Click);
    }

    private void RequestView_Click(object sender, EventArgs e) {
      viewRequests();
    }

    private void viewRequests() {
      rightFunctionPanel.RowCount = 2;      // badcode
      rightFunctionPanel.ColumnCount = 3;   // badcode
      rightFunctionPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 35F));
      rightFunctionPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.AutoSize));
      rightFunctionPanel.ColumnStyles.Insert(2, new ColumnStyle(SizeType.Absolute, ConstForms.COLUMN_WIDTH / 2));
      rightFunctionPanel.RowStyles.Insert(0, new RowStyle(SizeType.Percent, 80F));
      rightFunctionPanel.RowStyles.Insert(1, new RowStyle(SizeType.Absolute, ConstForms.ROW_HEIGHT));
      
      AppDataGridView requestDataGridView = new AppDataGridView() {
        Indent = AppDataGridView.ControlIndent.Big
      };
      List<string> columnNames = Program.mainMiddleClass.RequestFields();
      List<DataGridViewTextBoxColumn> columns = new List<DataGridViewTextBoxColumn>();
      foreach (string c in columnNames) {
        columns.Add(new DataGridViewTextBoxColumn() { HeaderText = c, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
      }
      requestDataGridView.Columns.AddRange(columns.ToArray<DataGridViewColumn>());

      DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn() {
        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
        FillWeight = 20F,
        ToolTipText = "Отметить выполнение"
      };
      requestDataGridView.Columns.Add(checkColumn);
      requestDataGridView.MinimumSize = new System.Drawing.Size(400, 400);
      requestDataGridView.DataSource = Program.mainMiddleClass.AllRequests();
      rightFunctionPanel.Controls.Add(requestDataGridView, 0, 0);
      requestDataGridView.Columns.Remove("Capacity");
      requestDataGridView.Columns.Remove("Count");
      rightFunctionPanel.SetColumnSpan(requestDataGridView, 2);
    }

    private void createRequestButton_Click(object sender, EventArgs e) {
      throw new NotImplementedException();
    }

    private void RequestViewProducts_Click(object sender, EventArgs e) {
      throw new NotImplementedException();
    }

    private void RequestViewProvider_Click(object sender, EventArgs e) {
      throw new NotImplementedException();
    }

    
  }
}