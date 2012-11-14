using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpecialControls {
  public class OpacityLinkLabel : LinkLabel {
    public OpacityLinkLabel() {
      this.BackColor = System.Drawing.Color.FromArgb(0);
    }

    public enum ControlIndent { None, Small, Middle, Big, MemberOfList, FirstOfList, LastOfList };

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
          return;
        }
        if (indent == ControlIndent.MemberOfList) {
          this.Margin = new Padding(ConstControls.CONTROL_INDENT_SMALL, 0, 0, 0);
          return;
        }
        if (indent == ControlIndent.FirstOfList) {
          this.Margin = new Padding(ConstControls.CONTROL_INDENT_SMALL, ConstControls.CONTROL_INDENT_SMALL, 0, 0);
          return;
        }
        if (indent == ControlIndent.LastOfList) {
          this.Margin = new Padding(ConstControls.CONTROL_INDENT_SMALL, 0, 0, ConstControls.CONTROL_INDENT_SMALL);
          return;
        }
      }
    }
  }
}