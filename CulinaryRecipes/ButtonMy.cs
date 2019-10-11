using System.Drawing;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    static class ButtonMy
    {
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
            foreach (Control button in _name.Controls)
            {
                if (button is Button)
                {
                    ((Button)button).BackColor = Color.Maroon;

                    ((Button)button).ForeColor = Color.White;
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

        public static void ChangeColorOneElement(this ReadOnlyRichTextBox rich)
        {
            if (rich.ReadOnly == false)
            {
                rich.BackColor = BackgroundColorLightened();
            }
        }

        public static void ChangeColorOneElement(this RichTextBox rich)
        {
            if (rich.ReadOnly == false)
            {
                rich.BackColor = BackgroundColorLightened();
            }
        }

        public static void ChangeColorOneElement(this TextBox rich)
        {
            if (rich.ReadOnly == false)
            {
                rich.BackColor = BackgroundColorLightened();
            }
        }

        public static void BorderColor(RichTextBox rich, Control set, Panel panPicture)
        {
            if (rich.ReadOnly == false)
            {
                ResetColor(rich, set, panPicture);

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
                            ((PictureBox)item).BackColor = BackgroundColorLightened();

                        }

                    }
                }
            }
        }

        public static void ChangeForeColorToBlack(this RichTextBox fore)
        {
            fore.ForeColor = Color.Black;
        }

        public static void ChangeForeColorToBlack(this TextBox fore)
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
                ResetColor(rich, set, panPicture);

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
                            ((PictureBox)item).BackColor = BackgroundColorLightened();
                        }
                    }
                }
            }
        }

        public static Color BackgroundColorLightened()
        {
            Color gray = new Color();
            gray = Color.FromArgb(167, 171, 197);
            return gray;
        }

        public static void ResetColor(RichTextBox rich, Control set, Panel panPicture)
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

        public static void ResetColor(TextBox rich, Control set, Panel panPicture)
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
