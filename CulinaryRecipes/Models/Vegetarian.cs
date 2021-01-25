using System.Drawing;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public static class Vegetarian
    {
        public static Color GreenLabel(this Label caption, string checkboxName)
        {
            caption.ForeColor = Color.Green;
            caption.Font = new Font("Corbel", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(238)));

            return caption.ForeColor;
        }

        public static Color GreenLabel(this Label caption)
        {
            caption.ForeColor = Color.Green;
            caption.Font = new Font("Corbel", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(238)));

            return caption.ForeColor;
        }

        public static Color WhiteLabel(this Label caption, string checkboxName)
        {
            caption.ForeColor = Color.White;
            caption.Font = new Font("Corbel", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));

            return caption.ForeColor;
        }

        public static Color WhiteLabel(this Label caption)
        {
            caption.ForeColor = Color.White;
            caption.Font = new Font("Corbel", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));

            return caption.ForeColor;
        }

        /// <summary>
        /// zmiana kolorów labelów podczas zaznaczania i odznaczania vegetarian - wykorzystuje nazwy
        /// </summary>
        /// <param name="set"></param>
        /// <param name="nameCheckbox"></param>
        public static void ChangeLabelColorForVeg(Control set, string nameCheckbox)
        {
            foreach (Control item in set.Controls)
            {
                if (item is Label)
                {
                    if (item.Name.Contains(nameCheckbox.Substring(3)))
                    {
                        if (((Label)item).ForeColor == ((Label)item).GreenLabel())
                        {
                            ((Label)item).WhiteLabel(nameCheckbox);
                        }
                        else
                        {
                            ((Label)item).GreenLabel(nameCheckbox);
                        }
                        bool block = false;
                        if (item.Name.Substring(item.Name.Length - 3, 3) == "Veg" && item.Visible == false)
                        {
                            if (item.Name.Substring(3).Contains(nameCheckbox.Substring(3)))
                            {
                                item.Visible = true;
                                ((Label)item).WhiteLabel();
                                block = true;
                            }
                        }

                        if (item.Name.Substring(item.Name.Length - 3, 3) == "Veg" && item.Visible == true && block == false)
                        {
                            if (item.Name.Substring(3).Contains(nameCheckbox.Substring(3)))
                            {
                                item.Visible = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
