using System.Windows.Forms;

namespace CulinaryRecipes.Models
{
    class BulletCharacter
    {
        RichTextBox nameOfRichTextBox;

        public BulletCharacter(RichTextBox nameOfRichTextBox)
        {
            this.nameOfRichTextBox = nameOfRichTextBox;
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the line number
        /// </summary>
        public int NumberLine { get; set; }

        /// <summary>
        /// Gets or sets the number of lines
        /// </summary>
        public int MaxLine { get; set; }

        #endregion Properties

        #region Method

        /// <summary>
        /// główna funkcja 
        /// </summary>
        /// <param name="e"></param>
        public void CreateBullet(KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (nameOfRichTextBox.SelectionBullet)
                {
                    BullPoint();
                }
            }
        }

        /// <summary>
        /// Add bullet point to new line
        /// </summary>
        private void BullPoint()
        {
            nameOfRichTextBox.SelectionBullet = true;
            SetBulletIndent(32);
            SetBulletRightIndent(35);
        }

        /// <summary>
        /// Sets an Right indent
        /// </summary>
        private void SetBulletRightIndent(int quantity)
        {
            nameOfRichTextBox.SelectionRightIndent = quantity;
        }

        /// <summary>
        /// Sets an internal indent
        /// </summary>
        private int SetBulletIndent(int quantity)
        {
            nameOfRichTextBox.BulletIndent = quantity;
            return quantity;
        }

        /// <summary>
        /// Return the number of the starting line of a paragraph
        /// </summary>
        /// <returns></returns>
        public void ChangeParagraph()
        {
            if (nameOfRichTextBox.SelectionBullet)
            {
                nameOfRichTextBox.SelectionBullet = false;
                SetBulletRightIndent(0);
            }
            else
            {
                BullPoint();
            }
        }

        #endregion Method
    }
}