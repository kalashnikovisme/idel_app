using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.BisnessLogic;

namespace idel_app.Forms {
	public partial class RequestCreator : MiniManagerAppForm {

		private bool form_Closing = false;

		public RequestCreator(List<List<string>> req) {
			InitializeComponent();
			this.dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
			this.FormClosing += new FormClosingEventHandler(RequestCreator_FormClosing);
			foreach (List<string> r in req) {
				dataGridView1.Rows.Add(r);
			}
			this.Show();
		}

		private	void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
		}

		private void RequestCreator_FormClosing(object sender, FormClosingEventArgs e) {
			if (form_Closing) {
				List<List<string>> list = new List<List<string>>();
				int RowCount = dataGridView1.RowCount;
				if (dataGridView1.AllowUserToAddRows) {
					RowCount -= 1;
				}
				for (int i = 0; i < RowCount; i++) {
					List<string> l = new List<string>();
					for (int j = 0; j < dataGridView1.ColumnCount; j++) {
						try {
							l.Add(dataGridView1.Rows[i].Cells[j].Value.ToString());
						} catch (NullReferenceException nre) {
						}
					}
					list.Add(l);
				}
				Program.mainMiddleClass.SaveChanges(list);
			}
		}

		private void button1_Click(object sender, EventArgs e) {

		}
	}
}