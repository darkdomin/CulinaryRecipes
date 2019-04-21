using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        int time=1 ;
       
        private void timer1_Tick(object sender, EventArgs e)
        {
         
            if (time == 5)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
