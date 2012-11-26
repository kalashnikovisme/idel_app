using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UpgradeControls;

namespace idel_app.Forms {
    public partial class DeleteRequestForm : MiniManagerAppForm {
        public DeleteRequestForm(List<int> ind) {
            InitializeComponent();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (deleteAllCheckRadioButton.Checked) {

            }
            if (deleteAllRadioButton.Checked) {

            }
            if (deleteAllPassedRadioButton.Checked) {

            }
            this.Close();
        }
    }
}