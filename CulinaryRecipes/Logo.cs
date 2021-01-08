using System;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public partial class Logo : Form
    {
        public string titleLogo;
        public string ingredientLogo;
        public string amountsLogo;
        public string gramsLogo;
        public string descriptionLogo;

        public Logo()
        {
            InitializeComponent();
        }
        int time = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time == 4)
            {
                timer1.Stop();

                SingForm show = new SingForm();
                show.titleSing = titleLogo;
                show.ingredientSing = ingredientLogo;
                show.amountsSing = amountsLogo;
                show.gramsSing = gramsLogo;
                show.descriptionSing = descriptionLogo;
                this.Hide();
                show.ShowDialog();
            }
            else
            {
                time++;
            }
        }
    }
}
