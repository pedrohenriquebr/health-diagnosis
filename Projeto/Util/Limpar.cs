using System.Windows.Forms;

namespace Projeto
{
    class Limpar
    {
        public static void limpar(Form form)
        {
            foreach (Control controle in form.Controls)
            {
                if (controle is TextBox)
                {
                    TextBox txt = (controle) as TextBox;
                    txt.Clear();
                }
                if (controle is ComboBox)
                {
                    ComboBox txt = (controle) as ComboBox;
                    txt.SelectedIndex = -1;
                }
                if (controle is ListBox)
                {
                    ListBox txt = (controle) as ListBox;
                    txt.Items.Clear();
                }
                if (controle is NumericUpDown)
                {
                    NumericUpDown txt = (controle) as NumericUpDown;
                    txt.Value = 0;
                }
                if (controle is MaskedTextBox)
                {
                    MaskedTextBox txt = (controle) as MaskedTextBox;
                    txt.ResetText();
                }
                if (controle is TabControl)
                {
                    TabControl txt = (controle) as TabControl;
                    for (int x=0;x<txt.TabPages.Count;x++)
                    {
                        foreach (Control crl in txt.TabPages[x].Controls)
                        {
                            if (crl is TextBox)
                            {
                                TextBox tot = (crl) as TextBox;
                                tot.Clear();
                            }
                            if (crl is ComboBox)
                            {
                                ComboBox tot = (crl) as ComboBox;
                                tot.SelectedIndex = -1;
                            }
                            if (crl is ListBox)
                            {
                                ListBox tot = (crl) as ListBox;
                                tot.Items.Clear();
                            }
                            if (crl is NumericUpDown)
                            {
                                NumericUpDown tot = (crl) as NumericUpDown;
                                tot.Value = 0;
                            }
                            if (!(crl is null) && crl is MaskedTextBox)
                            {
                                MaskedTextBox tot = (crl) as MaskedTextBox;
                                tot.ResetText();
                            }
                        }
                    }
                }
            }
        }
    }
}