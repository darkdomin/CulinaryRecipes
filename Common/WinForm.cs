using System.Windows.Forms;

namespace Common
{
    public class WinForm
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
    }
}
