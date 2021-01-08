using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public partial class Form3 : Form
    {
        public int idDgGridForm3;
        public int numberOfPortionsForm3;
        public int counterForm3;
        public int portions3;

        public string titleForm3;
        public string gramsForm3;
        public string ingredientForm3;
        public string shortDescriptionForm3;
        public string InstructionForm3;
        public string listOfCuisinesForm3;
        public string difficultLevelForm3;
        public string executionTimeForm3;
        public string AmountsOfFoodForm3;
        public string unlockFieldsForm3 = "1";
        public string photoForm3;
        public string RatingForm3;
        public string clearForm3;
        public string LinkForm23;
        public string correctModyficationName3;

        //zmienne pamięciowe- Anuluj//
        public string title3;
        public string amounts3;
        public string ingrediet3;
        public string shortDes3;
        public string longDes3;
        public string cuisines3;
        public string level3;
        public string time3;
        public string rating3;

        public bool cancel3;
        public bool hideForm3;
        public bool addRecipeForm3;
        public bool addRecipe;
        public bool newForm3;

        public List<CheckBox> SavedCheckBoxForm3 = new List<CheckBox>();

        string add = "add";

        private void TurnOnAndOffTheButton(GroupBox panelName, Button turnOnOff)
        {
            ButtonMy.TurnOffAllTheButtons(panelName);
            ButtonMy.TurnOnTheButton(turnOnOff);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbLevel, btndifficultLevOne);
            difficultLevelForm3 = btndifficultLevOne.Tag.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbLevel, btndifficultLevTwo);
            difficultLevelForm3 = btndifficultLevTwo.Tag.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbLevel, btndifficultLevThree);
            difficultLevelForm3 = btndifficultLevThree.Tag.ToString();
        }

        private void btnTime30_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbTime, btnTime30);
            executionTimeForm3 = btnTime30.Tag.ToString();
        }

        private void btnTime60_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbTime, btnTime60);
            executionTimeForm3 = btnTime60.Tag.ToString();
        }

        private void btnTime90_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbTime, btnTime90);
            executionTimeForm3 = btnTime90.Tag.ToString();
        }

        private void btnTime100_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbTime, btnTime100);
            executionTimeForm3 = btnTime100.Tag.ToString();
        }

        private void btnOneStar_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbRating, btnOneStar);

            var s = from p in Rating.categoryRating
                    where p.Id == 1
                    select p;
            foreach (var c in s)
            {
                RatingForm3 = c.Id.ToString();
            }
        }

        private void btnTwoStar_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbRating, btnTwoStar);

            var s = from p in Rating.categoryRating
                    where p.Id == 2
                    select p;
            foreach (var c in s)
            {
                RatingForm3 = c.Id.ToString();
            }
        }

        private void btnThreeStar_Click(object sender, EventArgs e)
        {
            TurnOnAndOffTheButton(gbRating, btnThreeStar);

            var s = from p in Rating.categoryRating
                    where p.Id == 3
                    select p;
            foreach (var c in s)
            {
                RatingForm3 = c.Id.ToString();
            }
        }

        private void rbAmerican_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbAmerican.Tag.ToString();
        }

        private void rbAsian_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbAsian.Tag.ToString();
        }

        private void rbCeska_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbCeska.Tag.ToString();
        }

        private void rbFrench_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbFrench.Tag.ToString();
        }

        private void rbHiszpanska_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbSpanish.Tag.ToString();
        }

        private void rbGreece_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbGreece.Tag.ToString();
        }

        private void rbIberian_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbPortuguese.Tag.ToString();
        }

        private void rbPolish_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbPolish.Tag.ToString();
        }

        private void rbItalish_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbItalish.Tag.ToString();
        }

        private void rbWegierska_CheckedChanged(object sender, EventArgs e)
        {
            listOfCuisinesForm3 = rbHungarian.Tag.ToString();
        }

        private void DisplayHighlightedButton(string id, Control set)
        {
            foreach (Control c in set.Controls)
            {
                if (c is Button)
                {
                    if (c.Tag.ToString() == id)
                    {
                        ((Button)c).BackColor = Color.White;
                        ((Button)c).ForeColor = Color.Black;
                    }
                }
            }
        }

        private void DisplayPhoto()
        {
            rtxtLinkForm3.Text = photoForm3;
            pbLittlePhoto.ImageLocation = rtxtLinkForm3.Text;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (clearForm3 == add)
            {
                listOfCuisinesForm3 = "";
                RatingForm3 = "";
                executionTimeForm3 = "";
                difficultLevelForm3 = "";
            }
            else
            {
                DisplayHighlightedButton(RatingForm3, gbRating);
                DisplayHighlightedButton(difficultLevelForm3, gbLevel);
                DisplayHighlightedButton(executionTimeForm3, gbTime);
                DisplayPhoto();
                ButtonMy.SelectTypeOfCuisine(listOfCuisinesForm3, gbKitchen);
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void rtxtLinkForm3_TextChanged(object sender, EventArgs e)
        {
            pbLittlePhoto.ImageLocation = rtxtLinkForm3.Text;
        }

        private void btnPhotoForm3_Click(object sender, EventArgs e)
        {
            OpenFileDialog otworz_plik = new OpenFileDialog();

            otworz_plik.Filter = "Pliki graficzne |*.jpg; *.tiff; *.raw;*.txt";
            if (otworz_plik.ShowDialog() == DialogResult.OK)
            {
                pbLittlePhoto.Image = Image.FromFile(otworz_plik.FileName);
                rtxtLinkForm3.Text = otworz_plik.FileName;
            }
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLinkForm3.Undo();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLinkForm3.Cut();
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLinkForm3.Copy();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Form1 shutdown = new Form1();
            shutdown.Show();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLinkForm3.Paste();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLinkForm3.SelectedText = "";
        }

        public string interval = "  ";
        public int[] IdMealForm3 = new int[7];
        public int[] idComponentsForm3 = new int[8];
        internal bool InTheProcessOfAdding;

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 model = new Form2();

            if (idDgGridForm3 == 0) model.clear = add;
            else model.clear = clearForm3;

            model.idDgGridForm2 = idDgGridForm3;

            model.titleForm2 = titleForm3;
            model.numberOfPortionsForm2 = numberOfPortionsForm3;

            model.amountsOfIngredientsForm2 = AmountsOfFoodForm3;
            model.gramsForm2 = gramsForm3;
            model.ingredientForm2 = ingredientForm3;

            model.ShortDescriptionForm2 = shortDescriptionForm3;
            model.instructionForm2 = InstructionForm3;

            photoForm3 = rtxtLinkForm3.Text;
            if (photoForm3 == null || photoForm3 == "") photoForm3 = "-";
            model.linkForm2 = photoForm3;
            model.linkForm22 = LinkForm23;

            if (listOfCuisinesForm3 == null || listOfCuisinesForm3 == "") listOfCuisinesForm3 = "-";
            model.listOfCuisinesForm2 = listOfCuisinesForm3;

            if (RatingForm3 == null || RatingForm3 == "") RatingForm3 = "-";
            model.idRatingForm2 = RatingForm3;

            if (difficultLevelForm3 == null || difficultLevelForm3 == "") difficultLevelForm3 = "-";
            model.difficultLevelForm2 = difficultLevelForm3;

            if (executionTimeForm3 == null || executionTimeForm3 == "") executionTimeForm3 = "-";
            model.executionTimeForm2 = executionTimeForm3;

            model.unlockFieldsForm2 = unlockFieldsForm3;
           
           // p.AddRecipeForm2 = addRecipeForm3;
            model.addRecipe = addRecipe;
            model.InTheProcessOfAdding = InTheProcessOfAdding;
            InTheProcessOfAdding = false;

            model.correctModyficationName = correctModyficationName3;

            #region MealAdd
            model.checkBoxDish[0] = IdMealForm3[0];
            model.checkBoxDish[1] = IdMealForm3[1];
            model.checkBoxDish[2] = IdMealForm3[2];
            model.checkBoxDish[3] = IdMealForm3[3];
            model.checkBoxDish[4] = IdMealForm3[4];
            model.checkBoxDish[5] = IdMealForm3[5];
            model.checkBoxDish[6] = IdMealForm3[6];
            #endregion

            #region ComponentAdd
            model.checkBoxIngredients[0] = idComponentsForm3[0];
            model.checkBoxIngredients[1] = idComponentsForm3[1];
            model.checkBoxIngredients[2] = idComponentsForm3[2];
            model.checkBoxIngredients[3] = idComponentsForm3[3];
            model.checkBoxIngredients[4] = idComponentsForm3[4];
            model.checkBoxIngredients[5] = idComponentsForm3[5];
            model.checkBoxIngredients[6] = idComponentsForm3[6];
            model.checkBoxIngredients[7] = idComponentsForm3[7];
            #endregion

            model.counterForm2 = counterForm3;

            #region Pamięć
            model.title1 = title3;
            model.amounts = amounts3;
            model.ingrediet = ingrediet3;
            model.shortDes = shortDes3;
            model.longDes = longDes3;
            model.cuisines = cuisines3;
            model.level = level3;
            model.time = time3;
            model.rating = rating3;
            model.portions = portions3;
            model.cancel = cancel3;
            #endregion

            model.SavedCheckBoxForm2 = SavedCheckBoxForm3;
            model.newForm = newForm3;

            this.Hide();
            model.ShowDialog();
        }

        public Form3()
        {
            InitializeComponent();
        }

        private void zaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLinkForm3.SelectAll();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            rtxtLinkForm3.Focus();
            rtxtLinkForm3.SelectionStart = 0;
        }

        #region keyDown
        private void ClearLink(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) rtxtLinkForm3.Text = "";
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) rtxtLinkForm3.Text = "";
        }

        private void rtxtLinkForm3_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbAsian_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnThreeStar_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnTwoStar_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnOneStar_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbAmerican_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbCeska_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbFrench_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbSpanish_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbGreece_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbPortuguese_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbPolish_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbItalish_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rbHungarian_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btndifficultLevOne_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btndifficultLevTwo_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btndifficultLevThree_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnTime30_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnTime60_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnTime90_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnTime100_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void btnPhotoForm3_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
            ClearLink(e);
        }

        private void rtxtLinkForm3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(rtxtLinkForm3, "Klawisz 'Esc' - Czyści pole");
        }
        #endregion keyDown

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (InTheProcessOfAdding)
            {
                var result = MessageBox.Show("Jesteś w trakcie dodawania przepisu\nNa pewno przerwać proces?", "Zamykanie programu", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    InTheProcessOfAdding = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
