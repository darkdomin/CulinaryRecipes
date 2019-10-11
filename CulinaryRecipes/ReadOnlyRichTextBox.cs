using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public partial class ReadOnlyRichTextBox : RichTextBox
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32.dll")] static extern bool ShowCaret(IntPtr hWnd);
        
        public ReadOnlyRichTextBox()
        {
            this.ReadOnly = true;
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (this.ReadOnly == true) HideCaret(this.Handle);
            else ShowCaret(this.Handle);
        }

    }
}
