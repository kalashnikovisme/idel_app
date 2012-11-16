using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idel_app.Forms {
  public class MiniManagerAppForm : ManagerAppForm {
    private void InitializeComponent() {
      this.SuspendLayout();
      // 
      // MiniManagerAppForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(481, 172);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MiniManagerAppForm";
      this.ResumeLayout(false);

    }

    public MiniManagerAppForm() {
      InitializeComponent();
    }
  }
}