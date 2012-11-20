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
    private FunctionPanel fullFunctionPanel;
    private FunctionPanel leftFunctionPanel;
    private FunctionPanel rightFunctionPanel;

    private FunctionPanel[] functionsGroup;

    private AddRequestWindow addRequestWindow;
    private DeleteRequestForm deleteRequestForm;

    private AppDataGridView requestDataGridView;

    public event EventHandler DeleteAllRequest;
    public event EventHandler DeleteCheckedRequest;
    public event EventHandler DeletePassedRequest;

    public MainWindow() {
      InitializeComponent();
      InitializeMainFunctionPanels();
      InitializeFunctionPanelsForFunctions();
      RequestFunctionGroupInitialize();
      this.DeleteAllRequest += new EventHandler(MainWindow_DeleteAllRequest);
      this.DeleteCheckedRequest += new EventHandler(MainWindow_DeleteCheckedRequest);
      this.DeletePassedRequest += new EventHandler(MainWindow_DeletePassedRequest);
    }

    private void MainWindow_Click(object sender, EventArgs e) {
      MessageBox.Show(requestDataGridView.SelectedRows[0].Index.ToString());
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
        Dock = DockStyle.Fill,
        AutoScroll = true
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

    /// <summary>
    /// Показывает поля работы с заявками
    /// </summary>
    private void viewRequests() {
      rightFunctionPanel.Controls.Clear();
      rightFunctionPanel.RowCount = 2;      // badcode
      rightFunctionPanel.ColumnCount = 3;   // badcode
      rightFunctionPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 33F));
      rightFunctionPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 33F));
      rightFunctionPanel.ColumnStyles.Insert(2, new ColumnStyle(SizeType.Percent, 33F));
      rightFunctionPanel.RowStyles.Insert(0, new RowStyle(SizeType.Percent, 80F));
      rightFunctionPanel.RowStyles.Insert(1, new RowStyle(SizeType.Absolute, ConstForms.ROW_HEIGHT * 2));

      requestDataGridView = initializeRequestDataGridView();
      List<List<string>> list = Program.mainMiddleClass.AllRequests();
      foreach (List<string> l in list) {
        requestDataGridView.Rows.Add(l.ToArray<string>());
      }
      rightFunctionPanel.Controls.Add(requestDataGridView, 0, 0);
      rightFunctionPanel.SetColumnSpan(requestDataGridView, 3);

      AppButton addRequestButton = new AppButton() {
        Text = "Добавить заявку",
        Indent = AppButton.ControlIndent.Middle
      };
      addRequestButton.Click += new EventHandler(addRequestButton_Click);
      rightFunctionPanel.Controls.Add(addRequestButton, 0, 1);

      AppButton deleteRequestButton = new AppButton() {
        Text = "Удалить заявку",
        Indent = AppButton.ControlIndent.Middle
      };
      deleteRequestButton.Click += new EventHandler(deleteRequestButton_Click);
      rightFunctionPanel.Controls.Add(deleteRequestButton, 1, 1);
    }

    private void deleteRequestButton_Click(object sender, EventArgs e) {
      List<int> indexes = new List<int>();
      foreach (DataGridViewRow row in requestDataGridView.SelectedRows) {
        indexes.Add(row.Index);
      }
      deleteRequestForm = new DeleteRequestForm(indexes);
      deleteRequestForm.FormClosing += new FormClosingEventHandler(deleteRequestForm_FormClosing);
      this.Enabled = false;
    }

    private void deleteRequestForm_FormClosing(object sender, FormClosingEventArgs e) {
      this.Enabled = true;
      viewRequests();
    }

    private void addRequestButton_Click(object sender, EventArgs e) {
      addRequestWindow = new AddRequestWindow(Program.mainMiddleClass.RequestFields().ToArray<string>());
      addRequestWindow.SetIntTypeField("id");
      addRequestWindow.FormClosing += new FormClosingEventHandler(add_FormClosing);
      this.Enabled = false;
    }

    private void add_FormClosing(object sender, FormClosingEventArgs e) {
      Program.mainMiddleClass.AddNewRequest(addRequestWindow.Datas);
      viewRequests();
      this.Enabled = true;
    }

    /// <summary>
    /// Инициализирует DataGridView с заявками
    /// </summary>
    private AppDataGridView initializeRequestDataGridView() {
      AppDataGridView requestDataGridView = new AppDataGridView() {
        Indent = AppDataGridView.ControlIndent.Middle,
        Font = new Font("Times New Roman", 15F),
        AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
        RowHeadersVisible = false,
        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
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
      requestDataGridView.Columns[0].Width = 40;
      requestDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      return requestDataGridView;
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

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

    }

    private void MainWindow_DeleteAllRequest(object sender, EventArgs e) {
      Program.mainMiddleClass.DeleteAll();
      viewRequests();
    }

    private void MainWindow_DeletePassedRequest(object sender, EventArgs e) {
      Program.mainMiddleClass.DeletePassedRequests();
    }

    private void MainWindow_DeleteCheckedRequest(object sender, EventArgs e) {
      foreach (DataGridViewRow d in requestDataGridView.SelectedRows) {
        Program.mainMiddleClass.DeleteRequestByIndex(d.Index);
      }
      viewRequests();
    }
  }
}