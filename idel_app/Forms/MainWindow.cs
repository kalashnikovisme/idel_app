using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.Forms;
using UpgradeControls;
using idel_app.Functions;

namespace idel_app {
	public partial class MainWindow : ManagerAppForm {
		#region Variables

		private FunctionPanel fullFunctionPanel;
		private FunctionPanel leftFunctionPanel;
		private FunctionPanel rightFunctionPanel;

		private FunctionPanel[] functionsGroup;

		private AddDataForm addDataForm;
		private DeleteRequestForm deleteRequestForm;
		private DeleteClientForm deleteClientForm;
		private AppDataGridView workDataGridView;
		private DataGridViewSelectedRowCollection selectedRows;

		public event EventHandler DeleteAllRequest;
		public event EventHandler DeleteCheckedRequest;
		public event EventHandler DeletePassedRequest;

		private bool RequestValueChanged = false;

		private int CheckColumnIndex = idel_app.Middle.Const.THERE_IS_NOT;

		#endregion

		#region MainInitialize

		public MainWindow() {
			InitializeComponent();
			InitializeMainFunctionPanels();
			InitializeFunctionPanelsForFunctions();
			RequestFunctionGroupInitialize();
			this.DeleteAllRequest += new EventHandler(MainWindow_DeleteAllRequest);
			this.DeleteCheckedRequest += new EventHandler(MainWindow_DeleteCheckedRequest);
			this.DeletePassedRequest += new EventHandler(MainWindow_DeletePassedRequest);
			this.FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
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
			for (int i = 0; i < ConstFunctions.REQUEST_WORKSPACE_COUNT + 1; i++) {
				functionsGroup[ConstFunctions.REQUEST_INDEX].RowStyles.Add(new RowStyle(SizeType.Absolute, ConstForms.ROW_HEIGHT));
			}
			PictureLinkLabelBox[] requestFunctionGroupLinkLabels = new PictureLinkLabelBox[ConstFunctions.REQUEST_WORKSPACE_COUNT];
			for (int i = 0; i < ConstFunctions.REQUEST_WORKSPACE_COUNT; i++) {
				requestFunctionGroupLinkLabels[i] = new PictureLinkLabelBox() {
					Dock = DockStyle.Fill,
					Font = new Font("Times New Roman", 12F),
					Indent = PictureLinkLabelBox.ControlIndent.MemberOfList
				};
				functionsGroup[ConstFunctions.REQUEST_INDEX].Controls.AddRange(requestFunctionGroupLinkLabels);
			}
			requestFunctionGroupLinkLabels[0].Indent = PictureLinkLabelBox.ControlIndent.FirstOfList;
			requestFunctionGroupLinkLabels[0].Image = global::idel_app.Properties.Resources.request_done;
			requestFunctionGroupLinkLabels[1].Image = idel_app.Properties.Resources.provider1;
			requestFunctionGroupLinkLabels[2].Image = idel_app.Properties.Resources.product;
			requestFunctionGroupLinkLabels[requestFunctionGroupLinkLabels.Length - 1].Indent = PictureLinkLabelBox.ControlIndent.LastOfList;

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

			requestFunctionGroupLinkLabels[textLinkLabels.LastIndexOf(ConstFunctions.REQUEST_VIEW_TITLE)].Click += new EventHandler(ClientView_Click);
			/* PublicClick */
			requestFunctionGroupLinkLabels[textLinkLabels.LastIndexOf(ConstFunctions.REQUEST_VIEW_TITLE)].linkLabel.Click += new EventHandler(ClientView_Click);
			requestFunctionGroupLinkLabels[textLinkLabels.LastIndexOf(ConstFunctions.REQUEST_VIEW_TITLE)].pictureBox.Click += new EventHandler(ClientView_Click);
			/* End PublicClick */
			createRequestButton.Click += new EventHandler(createRequestButton_Click);
		}

		#endregion

		#region RequestFunctions

		private void ClientView_Click(object sender, EventArgs e) {
			viewClients();
		}

		/// <summary>
		/// Показывает поля работы с заявками
		/// </summary>
		private void viewRequests(string client) {
			rightFunctionPanel.Controls.Clear();
			rightFunctionPanel.RowCount = 2;      // badcode
			rightFunctionPanel.ColumnCount = 3;   // badcode
			rightFunctionPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 33F));
			rightFunctionPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 33F));
			rightFunctionPanel.ColumnStyles.Insert(2, new ColumnStyle(SizeType.Percent, 33F));
			rightFunctionPanel.RowStyles.Insert(0, new RowStyle(SizeType.Percent, 90F));
			rightFunctionPanel.RowStyles.Insert(1, new RowStyle(SizeType.Absolute, ConstForms.ROW_HEIGHT));

			workDataGridView = initializeRequestDataGridView();
			List<List<string>> list = Program.mainMiddleClass.AllRequestsOfClient(client);
			for (int i = 0; i < list.Count; i++) {
				workDataGridView.Rows.Add(list[i].ToArray<string>());
				if (list[i][list[i].Count - 1] == "True") {
					workDataGridView.Rows[i].Cells[workDataGridView.Rows.Count - 1].Value = true;
				}
			}
			workDataGridView.Sort(workDataGridView.Columns[3], ListSortDirection.Descending);
			rightFunctionPanel.Controls.Add(workDataGridView, 0, 0);
			rightFunctionPanel.SetColumnSpan(workDataGridView, 3);

			AppButton addRequestButton = createAppButton("Добавить заявку");
			addRequestButton.Click += new EventHandler(addRequestButton_Click);
			rightFunctionPanel.Controls.Add(addRequestButton, 0, 1);

			AppButton deleteRequestButton = createAppButton("Удалить заявку");
			deleteRequestButton.Click += new EventHandler(deleteRequestButton_Click);
			rightFunctionPanel.Controls.Add(deleteRequestButton, 1, 1);

			AppButton passRequestButton = createAppButton("Отметить заявки выполненными");
			passRequestButton.Click += new EventHandler(passRequestButton_Click);
			rightFunctionPanel.Controls.Add(passRequestButton);
		}

		private void passRequestButton_Click(object sender, EventArgs e) {
			foreach (DataGridViewRow row in workDataGridView.SelectedRows) {
				Program.mainMiddleClass.MarkRequestPassed(row.Index);
				workDataGridView.Rows[row.Index].Cells[CheckColumnIndex].Value = true;
			}
		}

		private AppButton createAppButton(string text) {
			return new AppButton() { Text = text, Indent = AppButton.ControlIndent.Middle };
		}

		private void deleteRequestButton_Click(object sender, EventArgs e) {
			List<int> indexes = new List<int>();
			foreach (DataGridViewRow row in workDataGridView.SelectedRows) {
				indexes.Add(row.Index);
			}
			deleteRequestForm = new DeleteRequestForm(indexes);
			deleteRequestForm.FormClosing += new FormClosingEventHandler(deleteRequestForm_FormClosing);
			this.Enabled = false;
		}

		private void deleteRequestForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.Enabled = true;
			viewRequests(Program.mainMiddleClass.CurrentClient.Name);
		}

		private void addRequestButton_Click(object sender, EventArgs e) {
			AddNewRequest();
		}

		/// <summary>
		/// Открывает окно добавления новых заявок
		/// </summary>
		private void AddNewRequest() {
			addDataForm = new AddDataForm(Program.mainMiddleClass.RequestFields().ToArray<string>());
			addDataForm.SetIntTypeField("id");
			addDataForm.FormClosing += new FormClosingEventHandler(add_FormClosing);
			this.Enabled = false;
		}

		private void add_FormClosing(object sender, FormClosingEventArgs e) {
			Program.mainMiddleClass.AddNewRequest(addDataForm.Datas, Program.mainMiddleClass.CurrentClient.Name);
			viewRequests(Program.mainMiddleClass.CurrentClient.Name);
			this.Enabled = true;
		}

		/// <summary>
		/// Инициализирует DataGridView с заявками
		/// </summary>
		private AppDataGridView initializeRequestDataGridView() {
			AppDataGridView requestDataGridView = new AppDataGridView() {
				Indent = AppDataGridView.ControlIndent.Middle,
				Font = new Font("Times New Roman", 11F),
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
			requestDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			requestDataGridView.Columns[0].Width = 40;
			requestDataGridView.CellValueChanged += new DataGridViewCellEventHandler(requestDataGridView_CellValueChanged);
			CheckColumnIndex = requestDataGridView.Columns.Count - 1;
			for (int i = 0; i < requestDataGridView.Rows.Count; i++) {
				requestDataGridView.DoubleClick += new EventHandler(requestDataGridView_DoubleClick);
			}
			return requestDataGridView;
		}

		private void requestDataGridView_DoubleClick(object sender, EventArgs e) {
			RequestCreator reqCreate = new RequestCreator(Program.mainMiddleClass.GetDescriptionByIndexOfRequest(workDataGridView.SelectedRows[0].Index));
			reqCreate.FormClosing += new FormClosingEventHandler(reqCreate_FormClosing);
			this.Enabled = false;
		}

		private void reqCreate_FormClosing(object sender, FormClosingEventArgs e) {
			this.Enabled = true;
			viewRequests(Program.mainMiddleClass.CurrentClient.Name);
		}

		private void requestDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
			RequestValueChanged = true;
			workDataGridView.CellValueChanged -= new DataGridViewCellEventHandler(requestDataGridView_CellValueChanged);
		}

		private void MainWindow_DeleteAllRequest(object sender, EventArgs e) {
			Program.mainMiddleClass.DeleteAll();
			viewRequests(Program.mainMiddleClass.CurrentClient.Name);
		}

		private void MainWindow_DeletePassedRequest(object sender, EventArgs e) {
			Program.mainMiddleClass.DeletePassedRequests();
		}

		private void MainWindow_DeleteCheckedRequest(object sender, EventArgs e) {
			foreach (DataGridViewRow d in workDataGridView.SelectedRows) {
				Program.mainMiddleClass.DeleteRequestByIndex(d.Index);
			}
			viewRequests(Program.mainMiddleClass.CurrentClient.Name);
		}

		private void createRequestButton_Click(object sender, EventArgs e) {
			AddNewRequest();
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
			if (RequestValueChanged) {
				List<List<string>> list = new List<List<string>>();
				int RowCount = workDataGridView.RowCount;
				if (workDataGridView.AllowUserToAddRows) {
					RowCount -= 1;
				}
				for (int i = 0; i < RowCount; i++) {
					List<string> l = new List<string>();
					for (int j = 0; j < workDataGridView.ColumnCount; j++) {
						if (checkColumnValueNULL(ref l, i, j)) {
							l.Add("False");
							continue;
						}
						try {
							l.Add(workDataGridView.Rows[i].Cells[j].Value.ToString());
						} catch (NullReferenceException nre) {
						}
					}
					list.Add(l);
				}
				Program.mainMiddleClass.SaveChanges(list);
			}
		}

		/// <summary>
		/// Проверяет checkColumn на наличие null значений
		/// </summary>
		/// <returns>true, если указанная ячейка содержит null-значение</returns>
		private bool checkColumnValueNULL(ref List<string> l, int i, int j) {
			return ((j == 3) && (workDataGridView.Rows[i].Cells[j].Value == null));
		}

		#endregion

		#region Сlients

		private void viewClients() {
			rightFunctionPanel.Controls.Clear();
			rightFunctionPanel.RowCount = 2;      // badcode
			rightFunctionPanel.ColumnCount = 3;   // badcode
			rightFunctionPanel.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 33F));
			rightFunctionPanel.ColumnStyles.Insert(1, new ColumnStyle(SizeType.Percent, 33F));
			rightFunctionPanel.ColumnStyles.Insert(2, new ColumnStyle(SizeType.Percent, 33F));
			rightFunctionPanel.RowStyles.Insert(0, new RowStyle(SizeType.Percent, 90F));
			rightFunctionPanel.RowStyles.Insert(1, new RowStyle(SizeType.Absolute, ConstForms.ROW_HEIGHT));

			workDataGridView = initializeClientDataGridView();
			List<List<string>> list = Program.mainMiddleClass.AllClients();
			for (int i = 0; i < list.Count; i++) {
				workDataGridView.Rows.Add(list[i].ToArray<string>());
				if (list[i][list[i].Count - 1] == "True") {
					workDataGridView.Rows[i].Cells[workDataGridView.Rows.Count - 1].Value = true;
				}
			}
			rightFunctionPanel.Controls.Add(workDataGridView, 0, 0);
			rightFunctionPanel.SetColumnSpan(workDataGridView, 3);

			AppButton addClientButton = createAppButton("Добавить клиента");
			addClientButton.Click += new EventHandler(addClientButton_Click);
			rightFunctionPanel.Controls.Add(addClientButton, 0, 1);

			AppButton deleteClientButton = createAppButton("Удалить клиента");
			deleteClientButton.Click += new EventHandler(deleteClientButton_Click);
			rightFunctionPanel.Controls.Add(deleteClientButton, 1, 1);
		}

		private void deleteClientButton_Click(object sender, EventArgs e) {
			List<int> indexes = new List<int>();
			foreach (DataGridViewRow row in workDataGridView.SelectedRows) {
				indexes.Add(row.Index);
			}
			deleteClientForm = new DeleteClientForm(indexes);
			deleteClientForm.FormClosing += new FormClosingEventHandler(deleteClientForm_FormClosing);
			this.Enabled = false;
		}

		private void deleteClientForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.Enabled = true;
			viewClients();
		}

		private void addClientButton_Click(object sender, EventArgs e) {
			AddNewClient();
		}

		private void AddNewClient() {
			addDataForm = new AddDataForm(Program.mainMiddleClass.ClientFields().ToArray<string>());
			addDataForm.FormClosing += new FormClosingEventHandler(addClientForm_FormClosing);
			this.Enabled = false;
		}

		private void addClientForm_FormClosing(object sender, FormClosingEventArgs e) {
			Program.mainMiddleClass.AddNewRequest(addDataForm.Datas, Program.mainMiddleClass.CurrentClient.Name);
			viewClients();
			this.Enabled = true;
		}
		
		private AppDataGridView initializeClientDataGridView() {
			AppDataGridView clientDataGridView = new AppDataGridView() {
				Indent = AppDataGridView.ControlIndent.Middle,
				Font = new Font("Times New Roman", 11F),
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				RowHeadersVisible = false,
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
			};
			List<string> columnNames = Program.mainMiddleClass.ClientFields();
			List<DataGridViewTextBoxColumn> columns = new List<DataGridViewTextBoxColumn>();
			foreach (string c in columnNames) {
				columns.Add(new DataGridViewTextBoxColumn() { HeaderText = c, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
			}
			clientDataGridView.Columns.AddRange(columns.ToArray<DataGridViewColumn>());
			clientDataGridView.MinimumSize = new System.Drawing.Size(400, 400);
			clientDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			clientDataGridView.Columns[0].Width = 40;
			clientDataGridView.CellValueChanged += new DataGridViewCellEventHandler(requestDataGridView_CellValueChanged);
			for (int i = 0; i < clientDataGridView.Rows.Count; i++) {
				clientDataGridView.DoubleClick += new EventHandler(clientDataGridView_DoubleClick);
				clientDataGridView.SelectionChanged += new EventHandler(clientDataGridView_SelectionChanged);
			}
			return clientDataGridView;
		}

		private void clientDataGridView_SelectionChanged(object sender, EventArgs e) {
			selectedRows = workDataGridView.SelectedRows;
		}

		private void clientDataGridView_DoubleClick(object sender, EventArgs e) {
			viewRequests(Program.mainMiddleClass.CurrentClient.Name);
			Program.mainMiddleClass.CurrentClient = new BisnessLogic.Client(selectedRows[0].Cells[0].Value.ToString());
		}

		#endregion
	}
}