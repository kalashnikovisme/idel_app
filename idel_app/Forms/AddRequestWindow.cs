using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpecialControls;

namespace idel_app.Forms {
  public class AddRequestWindow : MiniManagerAppForm {

    private TextBox[] fields;
    private Button addButton;
    private FunctionPanel all;

    private void InitializeComponent() {
      this.addButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // addButton
      // 
      this.addButton.Location = new System.Drawing.Point(314, 153);
      this.addButton.Name = "addButton";
      this.addButton.Size = new System.Drawing.Size(155, 35);
      this.addButton.TabIndex = 2;
      this.addButton.Text = "addButton";
      this.addButton.UseVisualStyleBackColor = true;
      // 
      // AddRequestWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(481, 200);
      this.Controls.Add(this.addButton);
      this.Name = "AddRequestWindow";
      this.ResumeLayout(false);

    }

    public AddRequestWindow(string[] fieldsNames) {
      fields = new TextBox[fieldsNames.Length];
      
      all = new FunctionPanel() {
        ColumnCount = 1, 
        RowCount = 8
      };

      for (int i = 0; i < fields.Length; i++) {
        all.Controls.Add(fields[i], 0, i);
      }

      this.Controls.Add(all);
      this.Show();
    }
  }
}