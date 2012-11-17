using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpecialControls;

namespace idel_app.Forms {
  public partial class DeleteRequestForm : MiniManagerAppForm {
    private List<int> indexes;

    public DeleteRequestForm(List<int> ind) {
      indexes = ind.ToList<int>();
      InitializeComponent();
      this.Show();
    }

    private void button1_Click(object sender, EventArgs e) {
      if (deleteAllCheckRadioButton.Checked) {
        foreach (int i in indexes) {
          Program.mainMiddleClass.DeleteRequestByIndex(i);
        }
      }
      if (deleteAllRadioButton.Checked) {
        Program.mainMiddleClass.DeleteAll();
      }
      if (deleteAllPassedRadioButton.Checked) {
        Program.mainMiddleClass.DeletePassedRequests();
      }
      this.Close();
    }
  }
}