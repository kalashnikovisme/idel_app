using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idel_app.Forms {
    public class DeleteClientForm : MiniManagerAppForm {

        private System.Windows.Forms.Button button1;
        private UpgradeControls.OpacityRadioButton deleteAllRadioButton;
        private UpgradeControls.OpacityRadioButton deleteAllCheckRadioButton;
        private List<int> indexes;
        private void InitializeComponent() {
			this.button1 = new System.Windows.Forms.Button();
			this.deleteAllRadioButton = new UpgradeControls.OpacityRadioButton();
			this.deleteAllCheckRadioButton = new UpgradeControls.OpacityRadioButton();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(208, 83);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(108, 39);
			this.button1.TabIndex = 6;
			this.button1.Text = "Удалить";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// deleteAllRadioButton
			// 
			this.deleteAllRadioButton.AutoSize = true;
			this.deleteAllRadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(15)))), ((int)(((byte)(0)))));
			this.deleteAllRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F);
			this.deleteAllRadioButton.Location = new System.Drawing.Point(12, 36);
			this.deleteAllRadioButton.Name = "deleteAllRadioButton";
			this.deleteAllRadioButton.Size = new System.Drawing.Size(159, 23);
			this.deleteAllRadioButton.TabIndex = 5;
			this.deleteAllRadioButton.TabStop = true;
			this.deleteAllRadioButton.Text = "Удалить все записи";
			this.deleteAllRadioButton.UseVisualStyleBackColor = true;
			// 
			// deleteAllCheckRadioButton
			// 
			this.deleteAllCheckRadioButton.AutoSize = true;
			this.deleteAllCheckRadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(15)))), ((int)(((byte)(0)))));
			this.deleteAllCheckRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F);
			this.deleteAllCheckRadioButton.Location = new System.Drawing.Point(12, 13);
			this.deleteAllCheckRadioButton.Name = "deleteAllCheckRadioButton";
			this.deleteAllCheckRadioButton.Size = new System.Drawing.Size(222, 23);
			this.deleteAllCheckRadioButton.TabIndex = 4;
			this.deleteAllCheckRadioButton.TabStop = true;
			this.deleteAllCheckRadioButton.Text = "Удалить выделенные записи";
			this.deleteAllCheckRadioButton.UseVisualStyleBackColor = true;
			// 
			// DeleteClientForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(329, 134);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.deleteAllRadioButton);
			this.Controls.Add(this.deleteAllCheckRadioButton);
			this.Name = "DeleteClientForm";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        public DeleteClientForm(List<int> ind) {
            InitializeComponent();
            indexes = ind;
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (deleteAllCheckRadioButton.Checked) {
                Program.mainWindow.DeleteCheckClientsInvoke();
            }
            if (deleteAllRadioButton.Checked) {
                Program.mainWindow.DeleteAllClientsInvoke();
            }
            this.Close();
        }
    }
}