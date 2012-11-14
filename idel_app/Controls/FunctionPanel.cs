using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SpecialControls {
  public class FunctionPanel : TableLayoutPanel {

    public FunctionPanel() {
      this.BorderStyle = BorderStyle.Fixed3D;
      this.Paint += new PaintEventHandler(FunctionGroupBox_Paint);
      this.SizeChanged += new EventHandler(FunctionPanel_SizeChanged);
      this.Dock = DockStyle.Top;
    }

    private void FunctionPanel_SizeChanged(object sender, EventArgs e) {
      this.Invalidate();
    }

    public BorderStyle TableBorderStyle {
      get {
        return this.BorderStyle;
      }
      set {
        this.BorderStyle = value;
      }
    }

    private bool reverseGradient = false;
    public bool ReverseGradient {
      get {
        return reverseGradient;
      }
      set {
        reverseGradient = value;
        this.Invalidate();
      }
    }

    public void InitializeCellsCount(int columnCount, int rowCount) {
      this.RowCount = rowCount;
      this.ColumnCount = columnCount;
    }

    private void FunctionGroupBox_Paint(object sender, PaintEventArgs e) {
      Graphics g = e.Graphics;
      DrawRectangle(g, 0, 0, this.Width, this.Height);
    }

    private void DrawRectangle(Graphics g, int x, int y, int widht, int height) {
      Rectangle rec = new Rectangle(x, y, widht, height);
      if ((widht != 0) && (height != 0)) {
        System.Drawing.Drawing2D.LinearGradientBrush gradient;
        if (reverseGradient == false) {
          gradient = new System.Drawing.Drawing2D.LinearGradientBrush(rec, idel_app.Middle.Const.GRADIENT_COLOR_1, idel_app.Middle.Const.GRADIENT_COLOR_2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
        } else {
          gradient = new System.Drawing.Drawing2D.LinearGradientBrush(rec, Color.White, Color.FromArgb(251, 188, 59), System.Drawing.Drawing2D.LinearGradientMode.Horizontal);          
        }
        g.FillRectangle(gradient, rec);
        return;
      }
      Brush brush = new SolidBrush(Color.FromArgb(251, 188, 59));
      g.FillRectangle(brush, rec);
    }

    public void ReverseControls() {
      Control[] con = new Control[this.Controls.Count];
      this.Controls.CopyTo(con, 0);
      con = con.Reverse<Control>().ToArray<Control>();
      while (this.Controls.Count > 0) {
        this.Controls.RemoveAt(Controls.Count - 1);
      }
      for (int i = 0; i < con.Length; i++) {
        this.Controls.Add(con[i]);
      }
    }
  }
}