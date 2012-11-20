using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UpgradeControls {
  public class IconPictureBox : PictureBox {
    public IconPictureBox() {
      this.SizeChanged += new EventHandler(IconPictureBox_SizeChanged);
    }

    private void IconPictureBox_SizeChanged(object sender, EventArgs e) {
      this.Size = new System.Drawing.Size(this.Width, this.Width);
    }
  }
}