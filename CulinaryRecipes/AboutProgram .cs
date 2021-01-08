using System;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Function.UncheckText(txt);
            if (txt.SelectedText.Length >= 0) txt.SelectionStart = 0;
        }
    }
}
