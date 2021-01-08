using CulinaryRecipes.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    class AutoCompleter
    {
        RichTextBox namoOfRichTextBox;
        RichTextBox previousRichTextBox;
        static string[] sortedUnits;

        public AutoCompleter(RichTextBox namoOfTextBox, RichTextBox previousRichTextBox)
        {
            this.namoOfRichTextBox = namoOfTextBox;
            this.previousRichTextBox = previousRichTextBox;
            sortedUnits = new string[SetTheLengthOfTheArray()];
            EnumerateAllUnits();
        }

        /// <summary>
        /// Set the length of the array based on the number of enum units
        /// </summary>
        /// <returns></returns>
        public static int SetTheLengthOfTheArray()
        {
            int i = 0;
            foreach (Units word in
                (Units[])Enum.GetValues(typeof(Units)))
            {
                i++;
            }
            return i;
        }

        /// <summary>
        /// Rewrite to the unit array with enum
        /// </summary>
        public static void EnumerateAllUnits()
        {
            int i = 0;
            foreach (Units word in
                (Units[])Enum.GetValues(typeof(Units)))
            {
                sortedUnits[i] += word;
                i++;
            }
        }

        /// <summary>
        /// Gets or Sets the line number
        /// </summary>
        public int NumberLine { get; set; }

        public void CreateAutoComplete(KeyEventArgs e)
        {
            int start = namoOfRichTextBox.SelectionStart;
            string[] tabRichText = AddingSingleLetters(e);

            if (!string.IsNullOrEmpty(namoOfRichTextBox.Text))
            {
                string firstLetter = tabRichText[NumberLine];

                string[] groupOfWordsFiltered = SortFindAllWithGroup();
                groupOfWordsFiltered = SortByLenght(groupOfWordsFiltered);
                if (groupOfWordsFiltered.Length > 0)
                {
                    firstLetter = WordVariation(previousRichTextBox, groupOfWordsFiltered);
                }

                tabRichText[NumberLine] = firstLetter;
                int lenghtOfTheSelection = GetTextToNumberLineLength(NumberLine, tabRichText) - start;
                ArrayToRichTextBox(tabRichText);
                namoOfRichTextBox.SelectionStart = start;
                SelectedText(lenghtOfTheSelection);
            }
        }

        /// <summary>
        /// Sets the text selection
        /// </summary>
        /// <param name="zaznaczenie"></param>
        private void SelectedText(int zaznaczenie)
        {
            namoOfRichTextBox.Select(namoOfRichTextBox.SelectionStart, zaznaczenie);
        }

        /// <summary>
        /// Assigning an array to the main Richtextbox
        /// </summary>
        /// <param name="tabRichText"></param>
        private void ArrayToRichTextBox(string[] tabRichText)
        {
            namoOfRichTextBox.Lines = tabRichText;
        }

        /// <summary>
        /// Gets the text length of the given number of lines
        /// </summary>
        /// <param name="numberLine"></param>
        /// <returns></returns>
        public int GetTextToNumberLineLength(int numberLine, string[] tab)
        {
            int numberOfCharacters = 0;

            for (int i = 0; i <= numberLine; i++)
            {
                numberOfCharacters += tab[i].Length;
                numberOfCharacters++;
            }

            numberOfCharacters--;
            if (numberOfCharacters < 0) numberOfCharacters = 0;

            return numberOfCharacters;
        }

        /// <summary>
        /// dodaje pojedyncze litery do tablicy i zwraca tablicę
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public string[] AddingSingleLetters(KeyEventArgs e)
        {
            string[] tab = namoOfRichTextBox.Lines;
            return tab;
        }

        /// <summary>
        /// Sortuje i zwraca tabllicę jednej grupy wyrazów 
        /// </summary>
        /// <param name="pasujaceSlowa"></param>
        /// <returns></returns>
        public string[] SortFindAllWithGroup()
        {
            return Array.FindAll(sortedUnits, s => s.StartsWith(namoOfRichTextBox.Lines[NumberLine]));
        }

        /// <summary>
        /// Sorts by length
        /// </summary>
        /// <param name="pasujaceSlowa"></param>
        /// <returns></returns>
        public string[] SortByLenght(string[] tab)
        {
            var result = from word in tab
                         orderby word.Length
                         select word;
            return result.ToArray();
        }

        /// <summary>
        /// wybiera z tablicy pasujące słowa
        /// </summary>
        /// <param name="tablica"></param>
        /// <param name="zwroconyString"></param>
        /// <returns></returns>
        public string SearchingForMatchingWords(string[] tab)
        {
            string first = Array.Find(sortedUnits, p => p.StartsWith(namoOfRichTextBox.Lines[NumberLine]));
            return first;
        }

        /// <summary>
        /// Selects the word that has been correctly changed
        /// </summary>
        /// <param name="previousRichTextBox"></param>
        /// <param name="tab"></param>
        /// <returns></returns>
        public string WordVariation(RichTextBox previousRichTextBox, string[] tab)
        {
            double result = 0;
            if (!string.IsNullOrEmpty(previousRichTextBox.Text))
            {
                result = ConvertUnits.ConverterToDecimal(previousRichTextBox.Lines[NumberLine]);
            }

            string[] tablessThanOne = { Units.opakowania.ToString(), Units.pęczka.ToString(), Units.plastra.ToString(), Units.płata.ToString(), Units.ząbka.ToString() };
            string[] tabEqualToOne = { Units.opakowanie.ToString(), Units.listek.ToString(), Units.pęczek.ToString(), Units.plaster.ToString(), Units.płat.ToString(), Units.ząbek.ToString() };
            string[] tabGraterThanFive = { Units.garści.ToString(), Units.listków.ToString(), Units.pęczków.ToString(), Units.ząbków.ToString() };
            string forReplacement = "";
            string newWord = namoOfRichTextBox.Lines[NumberLine];
            string wordTemp = string.Empty;
            double myTryParse;

            foreach (var word in tab)
            {
                if (string.IsNullOrWhiteSpace(previousRichTextBox.Text) || string.IsNullOrWhiteSpace(previousRichTextBox.Lines[NumberLine]) && newWord == "s" || newWord == "S")
                {
                    wordTemp = "szczypta";
                }
                else
                {
                    if (string.IsNullOrEmpty(previousRichTextBox.Text)) wordTemp = word;
                    else if (result < 1)
                    {
                        if (newWord.Length <= 1 && word.Length <= 3)
                        {
                            wordTemp = word;
                            break;
                        }
                        if (WordException(tab, tablessThanOne, ref forReplacement))
                        {
                            wordTemp = forReplacement;
                            break;
                        }
                        if (word.EndsWith("i"))
                        {
                            wordTemp = word;
                            break;
                        }
                        else
                        {
                            wordTemp = word;
                        }
                    }
                    else if (double.TryParse(previousRichTextBox.Lines[NumberLine], out myTryParse) && myTryParse == 1)
                    {
                        if (newWord.Length <= 1 && word.Length <= 3)
                        {
                            wordTemp = word;
                            break;
                        }
                        if (WordException(tab, tabEqualToOne, ref forReplacement))
                        {
                            wordTemp = forReplacement;
                            break;
                        }
                        else if (word.EndsWith("a") || word.EndsWith("ć"))
                        {
                            wordTemp = word;
                            break;
                        }
                        else
                        {
                            wordTemp = word;
                        }
                    }
                    else if (double.TryParse(previousRichTextBox.Lines[NumberLine], out myTryParse) && myTryParse > 1 && myTryParse < 5)
                    {
                        if (newWord.Length <= 1 && word.Length <= 3)
                        {
                            wordTemp = word;
                            break;
                        }
                        if (word.EndsWith("i") || word.EndsWith("y") || word.EndsWith("nia") || word.EndsWith("na"))
                        {
                            wordTemp = word;
                            break;
                        }
                        else
                        {
                            wordTemp = word;
                        }

                    }
                    else if (double.TryParse(previousRichTextBox.Lines[NumberLine], out myTryParse) && myTryParse >= 5 && myTryParse < 22)
                    {
                        if (newWord.Length <= 1 && word.Length <= 3)
                        {
                            wordTemp = word;
                            break;
                        }

                        if (WordException(tab, tabGraterThanFive, ref forReplacement))
                        {
                            wordTemp = forReplacement;
                            break;
                        }

                        if (word.EndsWith("k") || word.EndsWith("w") || word.EndsWith("ń"))
                        {
                            wordTemp = word;
                            break;
                        }
                        else
                        {
                            wordTemp = word;
                        }
                    }
                    else
                    {
                        wordTemp = word;
                    }
                }
            }
            return wordTemp;
        }

        /// <summary>
        /// Returns true or false for a word that ends with an unusual ending
        /// </summary>
        /// <param name="mainTab"></param>
        /// <param name="tabWithException"></param>
        /// <param name="forReplacement"></param>
        /// <returns></returns>
        private bool WordException(string[] mainTab, string[] tabWithException, ref string forReplacement)
        {
            foreach (var foundWord in mainTab)
            {
                if (tabWithException.Contains(foundWord))
                {
                    forReplacement = foundWord;
                    return true;
                }
            }
            return false;
        }

    }

    public enum Units
    {
        dag,
        gałązek, gałązka, gałązki,
        garści, garście, garść,
        kg,
        kostek, kostka, kostki,
        kromka, kromek, kromki,
        listki, listek, listków,
        łyżka, łyżek, łyżki,
        łyżeczka, łyżeczek, łyżeczki,
        ml,
        opakowanie, opakowania, opakowań,
        plaster, plastra, plastry, plastrów,
        pęczek, pęczka, pęczki, pęczków,
        płat, płata, płaty, płatów,
        puszka, puszki, puszek,
        szt,
        szczypta, szczypty, szczypt,
        szklanka, szklanki, szklanek,
        ząbka, ząbki, ząbków, ząbek,
        ziarna, ziaren,
    }
}
