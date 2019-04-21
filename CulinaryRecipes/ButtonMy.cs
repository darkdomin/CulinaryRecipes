using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    static class ButtonMy
    {
        public static IEnumerable<Control> Controls { get; private set; }

        public static void TurnOnTheButton(this Button _name)
        {
            _name.BackColor = Color.White;
            _name.ForeColor = Color.Black;
        }

        public static void TurnOFFTheButton(this Button _name)
        {
            _name.BackColor = Color.Maroon;
            _name.ForeColor = Color.White;
        }

        public static void TurnOFFAllTheButtons(this Control _name)
        {
            foreach (Control przyciski in _name.Controls)
            {
                if (przyciski is Button)
                {
                    ((Button)przyciski).BackColor = Color.Maroon;

                    ((Button)przyciski).ForeColor = Color.White;
                }
            }
        }

        public static void CheckedTypeOfCuisine(string name, Control set)
        {
            foreach (Control c in set.Controls)
            {
                if (c is RadioButton)
                {
                    if (c.Tag.ToString() == name)
                    {
                        ((RadioButton)c).Checked = true;
                    }
                }
            }
        }

        public static void ZmienKolor(this ReadOnlyRichTextBox rich)
        {
            if (rich.ReadOnly == false)
            {
                rich.BackColor = Podkreslenie();
            }

        }

        public static void ZmienKolor(this RichTextBox rich)
        {
            if (rich.ReadOnly == false)
            {
                rich.BackColor = Podkreslenie();
            }
        }

        public static void ZmienKolor(this TextBox rich)
        {
            if (rich.ReadOnly == false)
            {
                rich.BackColor = Podkreslenie();
            }
        }

        public static void BorderColor(RichTextBox rich, Control set,Panel panPicture)
        {
            if (rich.ReadOnly == false)
            {
                WyzerujKolor(rich, set, panPicture);//nazwa do zmiany

                foreach (Control item in set.Controls)
                {
                    if (item is PictureBox)
                    {
                        if (
                            (rich.Location.X - item.Location.X <= 10 &&
                              rich.Location.X - item.Location.X >= 0) &&

                             (rich.Location.Y - item.Location.Y <= 10 &&
                              rich.Location.Y - item.Location.Y >= 0) ||

                             (item.Location.X - rich.Location.X == rich.Size.Width)
                            )
                        {
                            ((PictureBox)item).BackColor = Podkreslenie();

                        }

                    }
                }
            }
        }

        public static void ChangeForeColorToBlack(this RichTextBox fore)
        {
            fore.ForeColor = Color.Black; 
        }


          public static void ChangeForeColorToWhite(Control set)
          {
            
            foreach (Control fore in set.Controls)
            {
                if (fore is RichTextBox)
                {
                    ((RichTextBox)fore).ForeColor = Color.White;
                }
                if (fore is TextBox)
                {
                    ((TextBox)fore).ForeColor = Color.White;
                }
            }
           
          }

        public static void BorderColor(TextBox rich, Control set, Panel panPicture)
        {
            if (rich.ReadOnly == false)
            {
                WyzerujKolor(rich, set, panPicture);//nazwa do zmiany

                foreach (Control item in set.Controls)
                {
                    if (item is PictureBox)
                    {
                        if (
                            (rich.Location.X - item.Location.X <= 10 &&
                              rich.Location.X - item.Location.X >= 0) &&

                             (rich.Location.Y - item.Location.Y <= 10 &&
                              rich.Location.Y - item.Location.Y >= 0) ||

                             (item.Location.X - rich.Location.X == rich.Size.Width)
                            )
                        {
                            ((PictureBox)item).BackColor = Podkreslenie();
                        }

                    }
                }
            }
        }

        public static Color Podkreslenie()
        {
            Color gray = new Color();
            gray = Color.FromArgb(167, 171, 197);
            return gray;
        }
        public static void WyzerujKolor(RichTextBox rich, Control set, Panel panPicture)
        {
            foreach (Control c in set.Controls)
            {
                if (c is PictureBox)
                {
                    if (c.Name == Star(panPicture)) continue;
                    else ((PictureBox)c).BackColor = Function.CreateBrightColor();
                }

                if (c is RichTextBox)
                {
                    if (c.Name == rich.Name) continue;
                    else
                    {
                        ((RichTextBox)c).BackColor = Function.CreateBrightColor();
                        ((RichTextBox)c).ForeColor = Color.White;
                    }
                }
                if (c is TextBox)
                {
                    
                    ((TextBox)c).BackColor = Function.CreateBrightColor();
                    ((TextBox)c).ForeColor = Color.White;
                }

            }
        }

        public static void WyzerujKolor(TextBox rich, Control set, Panel panPicture)
        {
            foreach (Control c in set.Controls)
            {
                if (c is PictureBox)
                {
                    if (c.Name == Star(panPicture)) continue;
                    else ((PictureBox)c).BackColor = Function.CreateBrightColor();
                }

                if (c is RichTextBox)
                {
                    if (c.Name == rich.Name) continue;
                    else ((RichTextBox)c).BackColor = Function.CreateBrightColor();
                }
             

            }
        }

        public static string Star(Control set)
        {
            string NameofPictureBox = string.Empty;
            foreach (Control c in set.Controls)
            {
                if (c is PictureBox)
                    NameofPictureBox = c.Name;
            }
            return NameofPictureBox;
        }
    }
}
