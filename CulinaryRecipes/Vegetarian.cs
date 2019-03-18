﻿using System.Drawing;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public static class Vegetarian
    {
        static public void GreenLabel(CheckBox check, Label caption)
        {
            if (check.Checked)
            {
                caption.ForeColor = Color.Green;
                caption.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
        }

        static public void WhiteLabel(CheckBox check, Label caption)
        {
            caption.ForeColor = Color.White;
            caption.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
        }
    }
}