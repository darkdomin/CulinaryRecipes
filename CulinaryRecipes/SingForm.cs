using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public partial class SingForm : Form
    {
        public string titleSing;
        public string ingredientSing;
        public string amountsSing;
        public string gramsSing;
        public string descriptionSing;
        int id;
        public SingForm()
        {
            InitializeComponent();
          
        }

        //przepisuje całe słowa z ciągu do tablicy string
        public List<string> ReplaceStringToList(string amountsOrGramsOrIngredients)
        {
            string copy = string.Empty;
            List<string> newList = new List<string>();

            foreach (char item in amountsOrGramsOrIngredients)
            {
                if (item == '\n')
                {
                    newList.Add(copy);
                    copy = string.Empty;
                }
                else
                {
                    copy += item;
                }
            }

            newList.Add(copy);

            return newList;
        }

        //zwraca ilość linii w ciągu
        public int GetNumberOfLines(string tekst)
        {
            string jakis = string.Empty;
            int linia = 1;
            foreach (char item in tekst)
            {
                if (item == '\n') linia++;
            }
            return linia;
        }

        //porównuje ilośćlinii
        public int CompareLinesLength(string amounts, string grams, string ingredient)
        {
            int length = 0;
            if (GetNumberOfLines(amounts) > GetNumberOfLines(grams) && GetNumberOfLines(amounts) > GetNumberOfLines(ingredient)) length = GetNumberOfLines(amounts);
            else if (GetNumberOfLines(grams) > GetNumberOfLines(amounts) && GetNumberOfLines(grams) > GetNumberOfLines(ingredient)) length = GetNumberOfLines(grams);
            else if (GetNumberOfLines(ingredient) > GetNumberOfLines(amounts) && GetNumberOfLines(ingredient) > GetNumberOfLines(grams)) length = GetNumberOfLines(ingredient);
            else length = GetNumberOfLines(amounts);
            return length;
        }

        string host = "";

        //przypisz smtp do zmiennej host
        public void AssignSMTPToHost(TextBox nameFrom)
        {
            if (nameFrom.Text.Contains(Email.wp.ToString()))
            {
                host = "smtp.wp.pl";
            }
            else if (nameFrom.Text.Contains(Email.outlook.ToString()))
            {
                host = "smtp-mail.outlook.com";
            }
            else if (nameFrom.Text.Contains(Email.o2.ToString()))
            {
                host = "poczta.o2.pl";
            }
            else if (nameFrom.Text.Contains(Email.onet.ToString()))
            {
                host = "smtp.poczta.onet.pl";

            }
            //else if (nameFrom.Text.Contains(Email.interia.ToString()))
            //{
            //    host = "poczta.interia.pl";
            //}
            else if (nameFrom.Text.Contains(Email.gmail.ToString()))
            {
                host = "smtp.gmail.com";
            }
            else
            {

                MessageBox.Show("Niestety program w tej chwili obsługuje tylko:\n" +
                    " wp, onet, o2, interia, outlook, gmail ");
                nameFrom.Text = "";
                txtPassword.Text = "";

            }
        }
        string[] bodyTable;
        string body = string.Empty;

        List<string> amount = new List<string>();
        List<string> gram = new List<string>();
        List<string> ingre = new List<string>();

        //funkcja skracająca odczyt w funkcji
        public void CopyList(List<string> save, List<string> read)
        {
            foreach (var item in read)
            {
                if (item != "][") save.Add(item);
                else save.Add("");
            }
        }

        //usuwanie znaku sprcjalengo pustki z opisu
        public string DeleteCharInDescription()
        {
            string newString = string.Empty;
            foreach (char item in descriptionSing)
            {
                if (item == ']') continue;
                else if (item == '[') continue;
                else newString += item;
            }
            return newString;
        }

        int comLength;
        public void sklec()
        {
            CopyList(amount, ReplaceStringToList(amountsSing));
            CopyList(gram, ReplaceStringToList(gramsSing));
            CopyList(ingre, ReplaceStringToList(ingredientSing));

            bodyTable = new string[comLength];

            for (int i = 0; i < comLength; i++)
            {
                bodyTable[i] = amount[i] + " " + gram[i] + " " + ingre[i];
            }
        }

        private void SetBodyEmail()
        {
            foreach (var item in bodyTable)
            {
                body += item + "\n";
            }
        }

        private void Send()
        {
            bool mailSent = false;
            SetBodyEmail();
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();

                mail.From = new MailAddress(txtEmail.Text);
                mail.To.Add(new MailAddress(txtTo.Text));
                mail.Subject = titleSing;

                if (ChcAddDescription.Checked) mail.Body = body + "\n" + DeleteCharInDescription();
                else mail.Body = body;

                
                client.Host = host;
                if (!string.IsNullOrWhiteSpace(host))
                {

                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(txtEmail.Text, txtPassword.Text);

                    client.Port = 587;
                    client.EnableSsl = true;


                    client.Send(mail);

                    MessageBox.Show("E mail został wysłany");

                    Array.Clear(bodyTable, 0, bodyTable.Length);
                    amount.Clear();
                    gram.Clear();
                    ingre.Clear();
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                MessageBox.Show("Hasło lub Email jest nie poprawne!");              
            }
            Array.Clear(bodyTable, 0, bodyTable.Length);
            amount.Clear();
            gram.Clear();
            ingre.Clear();
            body = "";
        }

        private void SendButton()
        {
            if (txtEmail.Text != "" && txtPassword.Text != "" && txtTo.Text != "")
            {
                AssignSMTPToHost(txtEmail);
                if (host != "")
                {
                    sklec();
                    Send();
                }
            }
            if (txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Pola nie mogą być puste");
            }
            if (txtPassword.Text == "")
            {
                errorProvider2.SetError(txtPassword, "Pola nie mogą być puste");
            }
            if (txtTo.Text == "")
            {
                errorProvider3.SetError(txtTo, "Pola nie mogą być puste");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SendButton();
        }

        private void SingForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(405, 508);
            CreateDataGridView();
            comLength = CompareLinesLength(amountsSing, gramsSing, ingredientSing);

            foreach (var item in EmailBase.getAll("EmailBase"))
            {
                dgGrid.Rows.Add(item.Id, item.Email);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pbIncreaseSize.Visible = false;
            this.Size = new Size(800, 508);
            pbDecreaseSize.Visible = true;
        }

        private void pbDecreaseSize_Click(object sender, EventArgs e)
        {
            pbDecreaseSize.Visible = false;
            pbIncreaseSize.Visible = true;
            this.Size = new Size(405, 508);
        }

        bool add = false;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.TurnOnTheButton();
            add = true;

            if (txtAddEmail.Visible == false)
            {
                lblAdd.Visible = true;
                txtAddEmail.Visible = true;
                txtAddEmail.Enabled = true;
                btnDelete.Enabled = false;
                btnModify.Enabled = false;
                btnCancel.Visible = true;
            }
            else
            {

                try
                {
                    AddEmail();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void AddEmail()
        {
            EmailBase modelAdd = new EmailBase();
            modelAdd.Email = txtAddEmail.Text.ToLower();

            if (string.IsNullOrWhiteSpace(txtAddEmail.Text))
            {
                errorProvider1.SetError(txtAddEmail, "Wpisz Email");
            }
            else
            {
                EmailBase.add(modelAdd);
                DisappearModifyElement();
                btnDelete.Enabled = true;
                btnModify.Enabled = true;
                btnCancel.Visible = false;

                btnAdd.TurnOFFTheButton();
                add = false;
            }

            Fill();
        }

        public void Fill()
        {
            dgGrid.Rows.Clear();
            foreach (var item in EmailBase.getAll("EmailBase"))
            {
                dgGrid.Rows.Add(item.Id, item.Email);
            }
        }

        public void CreateDataGridView()
        {
            dgGrid.Rows.ToString().ToUpper();

            dgGrid.Columns.Add("id", "id");
            dgGrid.Columns.Add("email", "Email");

            dgGrid.Columns[0].Visible = false;
        }

        static string chooseEmail = "Wybierz Email";
        static string writeEmail = "Wpisz Email";
        bool modify = false;
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgGrid.Rows.Count > 0)
            {
                btnModify.TurnOnTheButton();
                modify = true;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnCancel.Visible = true;

                if (txtAddEmail.Visible == true && txtAddEmail.Text != "")
                {
                    try
                    {
                        ModifyEmail();
                        modify = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (txtAddEmail.Visible == true && txtAddEmail.Text == "")
                {
                    DisappearModifyElement();
                }
                else
                {
                    txtAddEmail.Visible = true;
                    lblAdd.Visible = true;
                    lblAdd.Text = chooseEmail;
                }
            }
            else
            {
                MessageBox.Show("Brak adresów Email do edycji.");
                Cancel();
            }
        }

        public void ModifyEmail()
        {
            var mod = EmailBase.getById(id);

            mod.Email = txtAddEmail.Text;

            EmailBase.update(mod);
            MessageBox.Show("Modyfikacja przebiegła pomyślnie!!!");
            btnModify.TurnOFFTheButton();

            Fill();

            txtAddEmail.Text = "";
            DisappearModifyElement();
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnCancel.Visible = false;
        }

        private void DisappearModifyElement()
        {
            txtAddEmail.Enabled = false;
            txtAddEmail.Visible = false;
            lblAdd.Visible = false;
            txtAddEmail.Text = "";
        }

        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[0].Value);
            txtAddEmail.Enabled = true;
            if (txtAddEmail.Visible == true)
            {

                txtAddEmail.Text = dgGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            

            }
            else
            {
                btnDelete.Enabled = true;

                if (txtEmail.Text == "")
                {
                    txtEmail.Text = dgGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
                else txtTo.Text = dgGrid.Rows[e.RowIndex].Cells[1].Value.ToString();

                dgGrid.DefaultCellStyle.SelectionBackColor = Color.SlateGray;
                errorProvider1.Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgGrid.Rows.Count > 0)
            {
                btnCancel.Visible = true;
                btnDeleteAll.Visible = true;
                btnDelete.TurnOnTheButton();
                if (id == 0)
                {
                    MessageBox.Show("Wybierz kontakt do usunięcia");
                }
                else if (MessageBox.Show("Czy na pewno usunąć Plik? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        var s = EmailBase.getById(id);
                        EmailBase.del(s.Id);
                        MessageBox.Show("Email został usunięty");

                        btnDelete.TurnOFFTheButton();
                        btnDeleteAll.Visible = false;
                        btnCancel.Visible = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                Fill();
            }
            else
            {
                MessageBox.Show("Brak adresów Email Do skasowania");
                Cancel();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = true;
            btnDeleteAll.TurnOnTheButton();
            EmailBase search = new EmailBase();
            if (MessageBox.Show("Czy na pewno usunąć Bazę danych? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                EmailBase.ClearDb();
                MessageBox.Show("Dokument został usunięty");
                Fill();
                btnCancel.Visible = false;
            }
            btnDeleteAll.TurnOFFTheButton();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                errorProvider1.Clear();
            }
            if (e.KeyCode == Keys.Enter) txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                errorProvider2.Clear();
            }
            if (e.KeyCode == Keys.Enter) txtTo.Focus();
        }

        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                errorProvider3.Clear();
            }
        }

        private void txtAddEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                errorProvider1.Clear();
            }
            if (e.KeyCode == Keys.Enter && add)
            {

                AddEmail();

            }
            if (e.KeyCode == Keys.Enter && modify)
            {
                ModifyEmail();
            }
        }
        private void Cancel()
        {
            btnCancel.Visible = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnModify.Enabled = true;
            lblAdd.Visible = false;
            txtAddEmail.Visible = false;
            txtAddEmail.Text = "";
            ButtonMy.TurnOFFAllTheButtons(panelGroup);
            btnDeleteAll.Visible = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 nowy = new Form1();
            nowy.ShowDialog();
        }

        private void SingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pomocProblemyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5();
            newForm.ShowDialog();
        }

        private void btnSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendButton();
        }
    }
}
