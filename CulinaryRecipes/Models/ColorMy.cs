using System.Drawing;
using System.Windows.Forms;

namespace CulinaryRecipes.Models
{
    public static class ColorMy
    {
        /// <summary>
        /// Change the menu text color to red
        /// </summary>
        /// <param name="elementName"></param>
        public static void RedForeToolStrip(ToolStripMenuItem elementName)
        {
            elementName.ForeColor = Color.Red;
        }

        /// <summary>
        /// Change the menu text color to green
        /// </summary>
        /// <param name="elementName"></param>
        public static void GreenForeColorToolStrip(ToolStripMenuItem elementName)
        {
            elementName.ForeColor = Color.Green;
        }

        /// <summary>
        /// Kolor czerwony zaznaczenie - po nacisnięciu 
        /// </summary>
        /// <returns></returns>
        public static Color Red()
        {
            return Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        }

        /// <summary>
        /// Create DeepBlue - background color
        /// </summary>
        /// <returns></returns>
        public static Color CreateDeepBlue()
        {
            Color gray = new Color();
            gray = Color.FromArgb(41, 43, 57);
            return gray;
        }

        /// <summary>
        /// Create BlueAtlantic - Item blocking color
        /// </summary>
        /// <returns></returns>
        public static Color CreateBlueAtlantic()
        {
            Color gray = new Color();
            gray = Color.FromArgb(40, 50, 60);
            return gray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Color CreateBright()
        {
            Color gray = new Color();
            gray = Color.FromArgb(87, 93, 135);
            return gray;
        }

        /// <summary>
        /// Settings for Highlight background color
        /// </summary>
        /// <returns></returns>
        public static Color BackgroundColorHighlighted()
        {
            Color gray = new Color();
            gray = Color.FromArgb(167, 171, 197);
            return gray;
        }
    }
}
