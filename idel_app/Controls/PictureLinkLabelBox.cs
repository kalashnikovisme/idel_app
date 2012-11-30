using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace UpgradeControls {
    public class PictureLinkLabelBox : TableLayoutPanel {
        /* public присвоен для использования один раз - это присвоение .Click 
         Место присвоение отмечено комментарием PublicClick */
        public OpacityLinkLabel linkLabel;
        public IconPictureBox pictureBox;

        private const int iconSize = 60;

        private const int margin = 20;

        public PictureLinkLabelBox() {
            initLinkLabel();
            initPictureBox();
            this.BackColor = Color.FromArgb(0);
            this.RowCount = 1;
            this.ColumnCount = 2;
            this.Controls.Add(pictureBox, 0, 0);
            this.Controls.Add(linkLabel, 1, 0);
            this.SizeChanged += new EventHandler(PictureLinkLabelBox_SizeChanged);
        }

        public delegate void EventHadler(object sender, EventArgs e);

        private void PictureLinkLabelBox_SizeChanged(object sender, EventArgs e) {
            pictureBox.Size = new Size(iconSize, iconSize);
        }

        private void initLinkLabel() {
            linkLabel = new OpacityLinkLabel() {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, margin, 0, margin),
                Font = new Font("Times New Roman", 15F),
                AutoSize = true
            };
        }

        private void initPictureBox() {
            pictureBox = new IconPictureBox() {
                Size = new Size(iconSize, iconSize),
                BackgroundImageLayout = ImageLayout.Zoom,
                Cursor = Cursors.Hand
            };
        }

        public string Text {
            get {
                return linkLabel.Text;
            }
            set {
                linkLabel.Text = value;
            }
        }

        public Image Image {
            get {
                return pictureBox.Image;
            }
            set {
                pictureBox.BackgroundImage = value;
            }
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