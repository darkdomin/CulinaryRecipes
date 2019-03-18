using System.Drawing;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    static class ButtonMy
    {
        public static void TurnOnTheButton(Button _name)
        {
            _name.BackColor = Color.White;
            _name.ForeColor = Color.Black;
        }

        public static void TurnOFFTheButton(Control _name)
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
    }
}
