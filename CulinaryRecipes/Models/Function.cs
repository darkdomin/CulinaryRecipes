using CulinaryRecipes.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public static class Function
    {
        //zmienia color czcionki na biały w polach textbox i richtextbox
        public static void ChangeForeColorToWhiteAllItems(Control set)
        {
            foreach (Control element in set.Controls)
            {
                if (element is RichTextBox || element is TextBox)
                {
                    element.ForeColor = Color.White;
                }
            }
        }

        public static void ColorAreaAfterUnblocking(Control set)
        {
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is RichTextBox)
                {
                    ((RichTextBox)kolorOdblokowania).BackColor = ColorMy.CreateBright();

                }
            }
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is TextBox)
                {
                    ((TextBox)kolorOdblokowania).BackColor = ColorMy.CreateBright();
                }
            }
        }

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

        public static void ColorFieldsAfterBlocking(Control set, RichTextBox name)
        {
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is RichTextBox)
                {
                    ((RichTextBox)kolorOdblokowania).BackColor = ColorMy.CreateBlueAtlantic();
                }
            }
            foreach (Control kolorOdblokowania in set.Controls)
            {
                if (kolorOdblokowania is TextBox)
                {
                    ((TextBox)kolorOdblokowania).BackColor = ColorMy.CreateBlueAtlantic();
                }
            }

            name.BackColor = ColorMy.CreateDeepBlue();
        }

        /// <summary>
        /// Block the possibility of clicking on the checkbox
        /// </summary>
        /// <param name="set"></param>
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

        /// <summary>
        /// Unlock checkboxes
        /// </summary>
        /// <param name="set"></param>
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

        /// <summary>
        /// Clear all area (TextBox And RichTextBox ) 
        /// </summary>
        /// <param name="set"></param>
        public static void ClearArae(Control set)
        {
            foreach (Control field in set.Controls)
            {
                if (field is TextBox)
                {
                    ((TextBox)field).Text = "";
                }
            }
            foreach (Control field in set.Controls)
            {
                if (field is RichTextBox)
                {
                    ((RichTextBox)field).Text = "";
                }
            }
        }

        //do poprawy
        /// <summary>
        /// Checks if the given name already exists in the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="correctName"></param>
        /// <returns></returns>
        public static bool CheckName(TextBox name, string correctName)
        {
            bool variable = false;
         
            foreach (var r in RecipesBase.GetAll("RecipesBase"))
            {
                if (r.RecipesName == name.Text && r.RecipesName != correctName)
                {
                    variable = true;
                    MessageBox.Show("Taka nazwa już istnieje w bazie danych");
                    name.Text = "";
                    break;
                }
            }

            return variable;
        }

        /// <summary>
        /// Uncheck text
        /// </summary>
        /// <param name="name"></param>
        public static void UncheckText(TextBox name)
        {
            if (name.SelectedText.Length >= 0)
            {
                name.SelectionStart = 0;
            }
        }

        public static void IsCheckBoxChecked(Control set, int[] PanelLeftOrRight)
        {
            int i = 0;
            foreach (Control p in set.Controls)
            {
                if (i >= PanelLeftOrRight.Length) { break; }
                else
                {
                    if (p is CheckBox && PanelLeftOrRight[i] == 1)
                    {
                        ((CheckBox)p).Checked = true;
                    }
                }
                i++;
            }
        }

        //liczy ile jest checkboxów w panelu
        public static int CheckBoxCountInPanel(Control set)
        {
            int i = 0;
            foreach (Control p in set.Controls)
            {
                if (p is CheckBox)
                {
                    i++;
                }
            }
            return i;
        }

        //Podmienia funkcje prawego przycisku
        public static void ChangeContextMenu(RichTextBox nameRich, ContextMenuStrip copyTool, ContextMenuStrip nameTool)
        {
            if (nameRich.ReadOnly == false)
            {
                nameRich.ContextMenuStrip = copyTool;
            }
            else
            {
                nameRich.ContextMenuStrip = nameTool;
            }
        }

        public static void SetFocusToTheEndOfTheName(RichTextBox name)
        {
            if (name.Text.LastOrDefault() != 32)//
            {
                name.SelectionStart = name.TextLength;
            }
            else
            {
                name.Text = name.Text.TrimEnd();
                name.SelectionStart = name.TextLength;
            }
        }

        //pomocnicza dla wyliczenia numberline
        public static int NumberOfLinesInColumn(string name)
        {
            int number = 0;

            foreach (char item in name)
            {
                if (item == '\n') number++;
            }

            return number;
        }

        //seprator gwaizdkowy - poprawić (tak zeby gwiazdki same sie dodawaly w zaleznosci od szerokosci pola)
        public static void Separator(RichTextBox name, int quantityStar)
        {
            string starInsert = "       *";
            string prefix = "        ";
            int i = name.SelectionStart;

            for (int j = 0; j < quantityStar; j++)
            {
                name.Text = name.Text.Insert(i, prefix + starInsert + "    ");
            }

            name.Text = name.Text.Insert(i, "\n");
            name.SelectionStart = name.TextLength;

            i = name.SelectionStart;
            name.Text = name.Text.Insert(i, "\n " + "    ");
            name.SelectionStart = 4 + i;
        }

        //Przypisuje nazwe pictureBoxa
        public static string StarAndOtherPicture(Control set)
        {
            string pictureName = string.Empty;

            foreach (Control element in set.Controls)
            {
                if (element is PictureBox)
                {
                    pictureName = element.Name;
                    break;
                }
            }

            return pictureName;
        }

        //Czysci Labele i wstawia myślniki
        public static void LabelClearTextInsertDash(Label labelName)
        {
            labelName.Text = Form2.dash;
        }

        //Ukrywa gwiazdki
        public static void StarHide(PictureBox pictureName)
        {
            pictureName.Visible = false;
        }

        // W tytule robi duze litrey
        public static void TitleTextToUpper(TextBox name)
        {
            name.Text = name.Text.ToUpper();
        }

        //pokazuje gwiazdki z ratingu
        public static void ShowStar(int id, PictureBox _name, string variableName)
        {
            var stars = from p in Rating.categoryRating
                        where p.Id == id
                        select p;

            if (variableName != "-")
            {
                foreach (var c in stars)
                {
                    if (c.Id <= int.Parse(variableName))
                    {
                        _name.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Concatenates the elements of an array and returns a string
        /// </summary>
        /// <returns></returns>
        public static string GetStringFromArray(string[] array)
        {
            return string.Join("", array);
        }

        //sprawdza czy checkbox jest zaznaczony (1) czy nie (0)
        public static void IsCheckBoxChecked(CheckBox elementName, int number, int[] snc)
        {
            if (elementName.Checked)
            {
                snc[number - 1] = 1;
            }
            else
            {
                snc[number - 1] = 0;
            }
        }

        public static void DeleteRecipes(int text)
        {
            if (MessageBox.Show("Czy na pewno usunąć Plik? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var s = RecipesBase.GetById(text);
                    RecipesBase.DeleteSingleFile(s.Id);
                    MessageBox.Show("Dokument został usunięty");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas usuwania w funkcji - DeleteRecipes");
                }
            }
        }

        public static void HighlightItem(this ReadOnlyRichTextBox rich)
        {
            rich.BackColor = ColorMy.BackgroundColorHighlighted();
        }

        /// <summary>
        /// Makes an item a lighter color
        /// </summary>
        /// <param name="rich"></param>
        public static void HighlightItem(this RichTextBox rich)
        {
            rich.BackColor = ColorMy.BackgroundColorHighlighted();
        }

        /// <summary>
        /// Makes an item a lighter color
        /// </summary>
        /// <param name="rich"></param>
        public static void HighlightItem(this TextBox rich)
        {
            rich.BackColor = ColorMy.BackgroundColorHighlighted();
        }

        public static void BorderColor(RichTextBox rich, Control set, Panel panPicture)
        {
            if (rich.ReadOnly == false)
            {
                ResetColor(rich, set, panPicture);
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
                            ((PictureBox)item).BackColor = ColorMy.BackgroundColorHighlighted();
                        }
                    }
                }
            }
        }

        public static void ResetColor(RichTextBox rich, Control set, Panel panPicture)
        {
            foreach (Control c in set.Controls)
            {

                if (c is RichTextBox)
                {
                    if (c.Name == rich.Name) continue;
                    else
                    {
                        ((RichTextBox)c).BackColor = ColorMy.CreateBright();
                        ((RichTextBox)c).ForeColor = Color.White;
                    }
                }
                if (c is TextBox)
                {

                    ((TextBox)c).BackColor = ColorMy.CreateBright();
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
                    if (c.Name == Star(panPicture) || c.Name == "btnBullPoint") continue;
                    else ((PictureBox)c).BackColor = ColorMy.CreateBright();
                }

                if (c is RichTextBox)
                {
                    if (c.Name == rich.Name) continue;
                    else
                    {
                        ((RichTextBox)c).BackColor = ColorMy.CreateBright();
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

        //przycina dodatkowe spacje w nazwie i ustawia fokus na koncu 
        public static void SetFocusToTheEndOfTheName(TextBox name)
        {
            if (!string.IsNullOrEmpty(name.Text))
            {
                if (name.Text.Last() != 32)
                {
                    name.SelectionStart = name.TextLength;
                }
                else
                {
                    name.Text = name.Text.TrimEnd();
                    name.SelectionStart = name.TextLength;
                }
            }
        }

        public static void PokazForm(string formName)
        {
            var ListaOkien = Application.OpenForms;

            for (int index = ListaOkien.Count - 1; index >= 0; index--)
            {
                if (ListaOkien[index].Name == formName)
                {
                    ListaOkien[index].Visible = true;
                }
                else
                {
                    ListaOkien[index].Hide();
                }
            }
        }

        /// <summary> 
        /// Creates a new form two
        /// </summary>
        public static void NewFormTwo()
        {
            Form2 NewForm = new Form2();
            NewForm.ShowDialog();
        }
    }
}
