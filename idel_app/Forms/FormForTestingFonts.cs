using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace idel_app.Forms
{
    public partial class FormForTestingFonts : Form
    {
        public FormForTestingFonts()
        {
            InitializeComponent();
        }

        private void resizeFont(Button b)
        {
            const int minFontSize = 7;
            //определение подходящего шрифта по ширине control'a
            float neededSizeX = (b.Width - b.Font.Size * 2) / b.Text.Length;
            int tempSizeXInPoint = minFontSize;
            while (true)
            {
                Graphics g = this.CreateGraphics();
                Font tempFont = new Font(b.Font.FontFamily, tempSizeXInPoint);
                int tempSize = (int)g.MeasureString(b.Text, tempFont).Width / b.Text.Length;
                if (neededSizeX - tempSize < 0.5)
                {
                    break;
                }
                tempSizeXInPoint++;
                #region forDebug
                if (tempSizeXInPoint == 300)
                {
                    MessageBox.Show("while true in X");
                    break;
                }
                #endregion
            }
            //определение подходящего шрифта по высоте control'a
            int tempSizeYInPoint = minFontSize;
            while (true)
            {
                Graphics g = this.CreateGraphics();
                Font tempFont = new Font(b.Font.FontFamily, tempSizeYInPoint);
                if (b.Height - g.MeasureString(b.Text, tempFont).Height < 5)
                {
                    break;
                }
                tempSizeYInPoint++;
                #region forDebug
                if (tempSizeYInPoint == 300)
                {
                    MessageBox.Show("while true in Y");
                    break;
                }
                #endregion
            }
            //выбор наиболее подходящего из двух найденых шрифтов
            int resSize;
            Font resFont;
            if (tempSizeYInPoint > tempSizeXInPoint) // выбираем минимальный размер шрифта, т.к. главное что бы отобразилось все содержимое
            {
                resSize = tempSizeXInPoint;//размер определенный по ширине подошел
                resFont = new Font(b.Font.FontFamily, resSize);
                int numberOfLines = (int)((b.Height - 10) / resFont.GetHeight());//есть ли возможность разбить содержимое на несколько строк 
                if (numberOfLines > 1)
                {
                    int tempResSize = resSize;
                    while (true)
                    {
                        Graphics g = this.CreateGraphics();
                        Font tempFont = new Font(b.Font.FontFamily, tempResSize);
                        numberOfLines = (int)((b.Height - 10) / tempFont.GetHeight());
                        if ((b.Width - tempResSize * 2) * numberOfLines - g.MeasureString(b.Text, tempFont).Width < 100)
                        {
                            break;
                        }
                        tempResSize++;
                        #region forDebug
                        if (tempResSize == 300)
                        {
                            MessageBox.Show("while true in Res");
                            break;
                        }
                        #endregion

                    }
                    //подходящий размер шрифта определен, теперь проверяем не слишком ли он мал
                    if (tempResSize < minFontSize + 2)
                    {
                        resFont = new Font(b.Font.FontFamily, minFontSize);
                    }
                    else if (tempResSize < 25)
                    {
                        resFont = new Font(b.Font.FontFamily, tempResSize - 1);
                    }
                    else // -1,-2 расходуется на погрешности отступов от краев control'a до текста
                    {
                        resFont = new Font(b.Font.FontFamily, tempResSize - 2);
                    }
                }

            }
            else
            {
                resSize = tempSizeYInPoint; //размер определенный по высоте подошел
                resFont = new Font(b.Font.FontFamily, resSize);
            }
            b.Font = resFont;
        }


        //Старая версия лежит "на всякий пожарный"
        private void resizeFontOldVersion(Button b)
        {
            float neededSizeX = (b.Width - b.Font.Size * 2) / b.Text.Length;
            int tempSizeXInPoint = 8;
            while (true)
            {
                Font tempFont = null;
                Graphics g = this.CreateGraphics();
                tempFont = new Font(b.Font.FontFamily, tempSizeXInPoint);
                //int accuracy = (tempSizeXInPoint > 40) ? (int)(tempSizeXInPoint * b.Text.Length * 0.6) : 10;
                int tempSize = (int)g.MeasureString(b.Text, tempFont).Width / b.Text.Length;
                if (neededSizeX - tempSize < 0.5)
                {
                    break;
                }
                if (tempSizeXInPoint == 300)
                {
                    MessageBox.Show("while true in X");
                    break;
                }
                tempSizeXInPoint++;
            }
            int tempSizeYInPoint = 8;
            while (true)
            {
                Font tempFont = null;
                Graphics g = this.CreateGraphics();
                tempFont = new Font(b.Font.FontFamily, tempSizeYInPoint);
                if (b.Height - g.MeasureString(b.Text, tempFont).Height < 5)
                {
                    break;
                }
                if (tempSizeYInPoint == 300)
                {
                    MessageBox.Show("while true in Y");
                    break;
                }
                tempSizeYInPoint++;
            }
            int resSize;
            Font resFont;
            if (tempSizeYInPoint > tempSizeXInPoint)
            {
                resSize = tempSizeXInPoint;
                resFont = new Font(b.Font.FontFamily, resSize);
                int numberOfLines = (int)((b.Height - 10) / resFont.GetHeight());
                if (numberOfLines > 1)
                {
                    int tempResSize = resSize;
                    while (true)
                    {
                        Font tempFont = null;
                        Graphics g = this.CreateGraphics();
                        tempFont = new Font(b.Font.FontFamily, tempResSize);
                        numberOfLines = (int)((b.Height - 10) / tempFont.GetHeight());
                        if ((b.Width - tempResSize * 2) * numberOfLines - g.MeasureString(b.Text, tempFont).Width < 100)
                        {
                            break;
                        }
                        if (tempResSize == 300)
                        {
                            MessageBox.Show("while true in Res");
                            break;
                        }
                        tempResSize++;
                    }
                    if (tempResSize < 10)
                    {
                        resFont = new Font(b.Font.FontFamily, 8);
                    }
                    else
                    {
                        resFont = new Font(b.Font.FontFamily, tempResSize - 2);
                    }
                }

            }
            else
            {
                resSize = tempSizeYInPoint;
                resFont = new Font(b.Font.FontFamily, resSize);
            }
            //resSize = (resSize > 10) ? resSize - 1 : 8;
            b.Font = resFont;


        }

        private void run_Click(object sender, EventArgs e)
        {
            resizeFont(buttonText);
            resizeFont(button1);
            resizeFont(button2);
            resizeFont(button3);
            resizeFont(button4);
            resizeFont(button5);
            resizeFont(button6);
            resizeFont(button7);
            resizeFont(button8);
            resizeFont(button9);
        }
    }
}
