using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpecialControls;

namespace idel_app.Forms {
  public class AddRequestWindow : MiniManagerAppForm {

    private DisTextBox[] fields;
    public List<string> Datas;
    private AppButton addButton;
    private FunctionPanel all;

    private void InitializeComponent() {
      this.addButton = new AppButton();
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
      this.Name = "AddRequestWindow";
      this.ResumeLayout(false);

    }

    public AddRequestWindow(string[] fieldsNames) {
      InitializeComponent();
      addButton.Indent = AppButton.ControlIndent.Middle;
      addButton.Click += new EventHandler(addButton_Click);

      fields = new DisTextBox[fieldsNames.Length];
      for (int i = 0; i < fields.Length; i++) {
        fields[i] = new DisTextBox() {
          DisText = fieldsNames[i], 
          Font = new System.Drawing.Font("Times New Roman", 12F),
          Dock = DockStyle.Fill
        };
      }
      all = new FunctionPanel() {
        ColumnCount = 1,
        Dock = DockStyle.Fill
      };

      for (int i = 0; i < fields.Length; i++) {
        all.Controls.Add(fields[i], 0, i);
      }
      
      this.all.Controls.Add(addButton, 0, all.RowCount - 1);
      this.Controls.Add(all);
      ChangeWindowSizeByTextBox();
      this.Show();
    }

    private void addButton_Click(object sender, EventArgs e) {
      if (fields.Any(p => p.EmptyText)) {
        MessageBox.Show("Требуется ввести данные всех ячеек");
        return;
      }
      foreach (DisTextBox t in fields) {
        try {
          if (t.IntType) {
            Int32.Parse(t.Text);
          }
        } catch {
          MessageBox.Show("Введите корректные данные!");
          return;
        }
      }
      Datas = new List<string>();
      foreach (DisTextBox t in fields) {
        Datas.Add(t.Text);
      }
      this.Close();
    }

    private void ChangeWindowSizeByTextBox() {
      int width = this.Width;     
      int height = addButton.Height + (fields.Length * 50); // badcode
      this.Size = new System.Drawing.Size(width, height);
    }

    public void SetIntTypeField(string fieldName) {
      for (int i = 0; i < fields.Length; i++) {
        if (fields[i].DisText == fieldName) {
          fields[i].IntType = true;
          return;
        }
      }
    }

    public void SetIntTypeField(int fieldIndex) {
      fields[fieldIndex].IntType = true;
    }
  }
}