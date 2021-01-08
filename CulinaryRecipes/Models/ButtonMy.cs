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

        public static void TurnOffTheButton(this Button _name)
        {
            _name.BackColor = Color.Maroon;
            _name.ForeColor = Color.White;
        }

        public static void TurnOffAllTheButtons(this Control _name)
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

        public static void SelectTypeOfCuisine(string name, Control set)
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
