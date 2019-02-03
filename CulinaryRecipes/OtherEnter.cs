using System.Linq;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public class OtherEnter
    {

        //Nowa Linia 
        public static void NewLine(RichTextBox name)
        {
            int i = name.Text.Length;

            name.Focus();
            name.Text = name.Text.Insert(i, "\n ");
            name.SelectionStart = 1 + i;
        }

        //kod dla pierwszej linii podczas wpisywania ilosci itd
        public static void ForTheFirstLine(RichTextBox first, RichTextBox second, KeyEventArgs e)
        {
            int j;
            int i = 0;
            if (first.Lines.Length <= 1 && e.KeyCode == Keys.Enter)
            {
                second.Focus();
                e.Handled = true;
                second.SelectionStart = 1 + i;
            }
            else if (first.Lines.Length > 1 && e.KeyCode == Keys.Enter)
            {
                second.Focus();
                e.Handled = true;
                second.Text = second.Text + "\n ";
                i = second.SelectionStart;
                j = second.TextLength;
                second.SelectionStart = j - 1;
            }
        }

        // wyróenuje ilość linii w 3 textboxach, tak zeby nie było czegos takiego ze jeden ma więcej lub mniej
        public static void AlignTheNumberOfLines(RichTextBox Amounts, RichTextBox Grams, RichTextBox Ingridient)
        {
            if (Ingridient.Lines.Length > Amounts.Lines.Length || Ingridient.Lines.Length > Grams.Lines.Length)
            {
                AlignTheNumberOfLinesCenter(Ingridient, Amounts);
                AlignTheNumberOfLinesCenter(Ingridient, Grams);
            }
            if (Amounts.Lines.Length > Ingridient.Lines.Length || Amounts.Lines.Length > Grams.Lines.Length)
            {
                AlignTheNumberOfLinesCenter(Amounts, Ingridient);
                AlignTheNumberOfLinesCenter(Amounts, Grams);
            }
            if (Grams.Lines.Length > Ingridient.Lines.Length || Grams.Lines.Length > Amounts.Lines.Length)
            {
                AlignTheNumberOfLinesCenter(Grams, Ingridient);
                AlignTheNumberOfLinesCenter(Grams, Amounts);
            }
        }

        //dodawanie linii do textboxa ktory ma mniej linii- funkcja uzupelniająca poprzednia
        public static void AlignTheNumberOfLinesCenter(RichTextBox longName, RichTextBox shortName)
        {
            while (longName.Lines.Length > shortName.Lines.Length)
            {
                int i = shortName.Text.Length;
                shortName.Focus();
                shortName.Text = shortName.Text.Insert(i, "\n "); ;
                shortName.SelectionStart = 1 + i;
            }
        }

        //Enter + dodatkowo nowa linia w pozostalych textboxach
        public void ClassicEnterPlusNewLine(KeyEventArgs e, RichTextBox name, RichTextBox second, RichTextBox third)
        {
            NewLine(second);
            NewLine(third);

            name.Focus();
        }

        //ustaw focus w w najdalszym miejscu (np. gdy wlacza sie modyfikacje)
        public static void SetFocus(RichTextBox name1, RichTextBox name2, RichTextBox name3)
        {
            int i;
            int quantityChar = name1.Lines[name1.Lines.Length - 1].Length;

            if (name1.TextLength > name2.TextLength && name1.TextLength > name3.TextLength)
            {
                i = name1.TextLength;
                if (name1.Text.Last() == ' ') i = i - 2;

                name1.Focus();
                name1.SelectionStart = i;
            }
        }

    }
}
