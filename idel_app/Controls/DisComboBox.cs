using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpecialControls {
  public class DisComboBox : ComboBox {

    private string disText;

    public string DisText {
      get {
        return disText;
      }
      set {
        disText = value;
        Text = value;
      }
    }

    public DisComboBox() {
      Click += new EventHandler(DisComboBox_Click);
      Leave += new EventHandler(DisComboBox_Leave);
      ForeColor = System.Drawing.Color.Gray;
      Margin = new Padding(ConstControls.CONTROL_INDENT_SMALL);
    }

    private void DisComboBox_Leave(object sender, EventArgs e) {
      if (Text == "") {
        Text = disText;
        ForeColor = System.Drawing.Color.Gray;
      }
    }

    private void DisComboBox_Click(object sender, EventArgs e) {
      if (ForeColor == System.Drawing.Color.Gray) {
        Text = "";
        ForeColor = System.Drawing.Color.Black;
      }
    }

    public enum ControlIndent { None, Small, Middle, Big, VeryBig };

    private ControlIndent indent = ControlIndent.None;
    public ControlIndent Indent {
      get {
        return indent;
      }
      set {
        indent = value;
        if (indent == ControlIndent.None) {
          this.Margin = new Padding(0);
          return;
        }
        if (indent == ControlIndent.Small) {
          this.Margin = new Padding(ConstControls.CONTROL_INDENT_SMALL);
          return;
        }
        if (indent == ControlIndent.Middle) {
          this.Margin = new Padding(ConstControls.CONTROL_INDENT_MIDDLE);
          return;
        }
        if (indent == ControlIndent.Big) {
          this.Margin = new Padding(ConstControls.CONTROL_INDENT_BIG);
        }
        if (indent == ControlIndent.VeryBig) {
          this.Margin = new Padding(ConstControls.CONTROL_INDENT_VERY_BIG);
        }
      }
    }
  }
}