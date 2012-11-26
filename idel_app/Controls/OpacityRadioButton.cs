using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UpgradeControls {
    public class OpacityRadioButton : RadioButton {
        public OpacityRadioButton() {
            this.BackColor = System.Drawing.Color.FromArgb(0, 255, 15, 0);
            this.Font = new System.Drawing.Font("Times New Roman", 12F);
        }
    }
}