﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    class Function
    {
        public static void UncheckedCheckBox(Control set)
        {
            foreach (Control p in set.Controls)
            {
                if (p is CheckBox)
                {

                    ((CheckBox)p).Checked = false;
                }
            }
        }
        public static Color CreateColor()
        {
            Color gray = new Color();
            gray = Color.FromArgb(41, 43, 57);
            return gray;
        }
        public static Color CreateColorBlockingFields()
        {
            Color gray = new Color();
            gray = Color.FromArgb(40, 50, 60);
            return gray;
        }
        public static Color CreateBrightColor()
        {
            Color gray = new Color();
            gray = Color.FromArgb(87, 93, 135);
            return gray;
        }


        public static void ColorAreaAfterUnblocking(Control set)
        {
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is RichTextBox) ((RichTextBox)kolorOdblokowania).BackColor = CreateBrightColor();
            }
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is TextBox) ((TextBox)kolorOdblokowania).BackColor = CreateBrightColor();
            }
        }
        public static void UnblockingFields(Control set)
        {
            foreach (Control unblock in set.Controls)
            {
                if (unblock is RichTextBox)
                    ((RichTextBox)unblock).ReadOnly = false;
            }
            foreach (Control unblock in set.Controls)
            {
                if (unblock is TextBox)
                    ((TextBox)unblock).ReadOnly = false;
            }
        }
        public static void BlockingFields(Control set)
        {
            foreach (Control block in set.Controls)
            {
                if (block is RichTextBox)
                    ((RichTextBox)block).ReadOnly = true;
            }
            foreach (Control block in set.Controls)
            {
                if (block is TextBox)
                    ((TextBox)block).ReadOnly = true;
            }
        }

        public static void ColorFieldsAfterBlocking(Control set,RichTextBox name)
        {
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is RichTextBox) ((RichTextBox)kolorOdblokowania).BackColor = CreateColorBlockingFields();
            }
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is TextBox) ((TextBox)kolorOdblokowania).BackColor = CreateColorBlockingFields();
            }
            name.BackColor = Function.CreateColor();//txtPortion
        }
        public static void BlockCheckbox(Control set)
        {
            foreach (Control check in set.Controls)
            {
                if (check.GetType() == typeof(CheckBox))
                {
                    check.Enabled = false;
                }
            }
        }
        public static void ClearFields(Control set)
        {
            foreach (Control clear in set.Controls)
            {
                if (clear is TextBox)
                {
                    ((TextBox)clear).Text = "";
                }
            }
            foreach (Control clear in set.Controls)
            {
                if (clear is RichTextBox)
                {
                    ((RichTextBox)clear).Text = "";
                }
            }
        }
        public static void CheckName(TextBox name)
        {
            foreach (var r in RecipesBase.getAll())
            {

                if (r.RecipesName == name.Text)
                {
                    MessageBox.Show("Taka nazwa już istnieje w bazie danych");
                    name.Text = "";
                }
            }
        }
        public static void UnblockCheckbox(Control set)
        {
            foreach (Control check in set.Controls)
            {
                if (check.GetType() == typeof(CheckBox))
                {
                    check.Enabled = true;
                }
            }
        }
        public static void StartPositionFromZero(TextBox name)
        {
            if (name.SelectedText.Length >= 0) name.SelectionStart = 0;
        }
      
    }
}
