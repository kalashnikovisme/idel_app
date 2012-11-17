using SpecialControls;

namespace idel_app.Forms {
  partial class DeleteRequestForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.deleteAllCheckRadioButton = new SpecialControls.OpacityRadioButton();
      this.deleteAllPassedRadioButton = new SpecialControls.OpacityRadioButton();
      this.deleteAllRadioButton = new SpecialControls.OpacityRadioButton();
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // deleteAllCheckRadioButton
      // 
      this.deleteAllCheckRadioButton.AutoSize = true;
      this.deleteAllCheckRadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(15)))), ((int)(((byte)(0)))));
      this.deleteAllCheckRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F);
      this.deleteAllCheckRadioButton.Location = new System.Drawing.Point(12, 12);
      this.deleteAllCheckRadioButton.Name = "deleteAllCheckRadioButton";
      this.deleteAllCheckRadioButton.Size = new System.Drawing.Size(219, 23);
      this.deleteAllCheckRadioButton.TabIndex = 0;
      this.deleteAllCheckRadioButton.TabStop = true;
      this.deleteAllCheckRadioButton.Text = "Удалить выделенные заявки";
      this.deleteAllCheckRadioButton.UseVisualStyleBackColor = true;
      // 
      // deleteAllPassedRadioButton
      // 
      this.deleteAllPassedRadioButton.AutoSize = true;
      this.deleteAllPassedRadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(15)))), ((int)(((byte)(0)))));
      this.deleteAllPassedRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F);
      this.deleteAllPassedRadioButton.Location = new System.Drawing.Point(12, 35);
      this.deleteAllPassedRadioButton.Name = "deleteAllPassedRadioButton";
      this.deleteAllPassedRadioButton.Size = new System.Drawing.Size(256, 23);
      this.deleteAllPassedRadioButton.TabIndex = 1;
      this.deleteAllPassedRadioButton.TabStop = true;
      this.deleteAllPassedRadioButton.Text = "Удалить все выполненные заявки";
      this.deleteAllPassedRadioButton.UseVisualStyleBackColor = true;
      // 
      // deleteAllRadioButton
      // 
      this.deleteAllRadioButton.AutoSize = true;
      this.deleteAllRadioButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(15)))), ((int)(((byte)(0)))));
      this.deleteAllRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F);
      this.deleteAllRadioButton.Location = new System.Drawing.Point(12, 58);
      this.deleteAllRadioButton.Name = "deleteAllRadioButton";
      this.deleteAllRadioButton.Size = new System.Drawing.Size(178, 23);
      this.deleteAllRadioButton.TabIndex = 2;
      this.deleteAllRadioButton.TabStop = true;
      this.deleteAllRadioButton.Text = "Удалить все элементы";
      this.deleteAllRadioButton.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(208, 82);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(108, 39);
      this.button1.TabIndex = 3;
      this.button1.Text = "Удалить";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // DeleteRequestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(328, 133);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.deleteAllRadioButton);
      this.Controls.Add(this.deleteAllPassedRadioButton);
      this.Controls.Add(this.deleteAllCheckRadioButton);
      this.Name = "DeleteRequestForm";
      this.Text = "DeleteRequestForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private OpacityRadioButton deleteAllCheckRadioButton;
    private OpacityRadioButton deleteAllPassedRadioButton;
    private OpacityRadioButton deleteAllRadioButton;
    private System.Windows.Forms.Button button1;
  }
}