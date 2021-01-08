using System;
using System.Drawing;
using System.Linq;
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
        string[] bodyTable;
        string[] amount;
        string[] gram;
        string[] ingre;

        public SingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Copies lines of text into an array
        /// </summary>
        /// <param name="amountsOrGramsOrIngredients"></param>
        /// <returns></returns>
        public string[] ReplaceStringToArray(string amountsOrGramsOrIngredients)
        {
            return amountsOrGramsOrIngredients.Split('\n');
        }

        /// <summary>
        /// Returns the number of lines in a string
        /// </summary>
        /// <param name="textName"></param>
        /// <returns></returns>
        public int GetNumberOfLines(string textName)
        {
            string[] pod = textName.Split('\n');
            return pod.Length;
        }

        /// <summary>
        /// Compares the number of lines
        /// </summary>
        /// <param name="amounts"></param>
        /// <param name="grams"></param>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        public int CompareLinesLength(string amounts, string grams, string ingredient)
        {
            int length = 0;
            length = GetNumberOfLines(amounts);

            if (length < GetNumberOfLines(grams))
            {
                length = GetNumberOfLines(grams);
            }
            if (length < GetNumberOfLines(ingredient))
            {
                length = GetNumberOfLines(ingredient);
            }

            return length;
        }

        /// <summary>
        /// Removes unnecessary characters
        /// </summary>
        /// <param name="save"></param>
        /// <param name="read"></param>
        public void RemoveUnnecessaryCharacter(ref string[] save, string[] read)
        {
            if (read.Contains("]["))
            {
                for (int i = 0; i < read.Length; i++)
                {
                    if (read[i].Contains("]["))
                    {
                        save[i] = "";
                    }
                    else
                    {
                        save[i] = read[i];
                    }
                }
            }
            else
            {
                save = read;
            }
        }

        //usuwanie znaku sprcjalengo pustki z opisu
        public string DeleteCharInDescription()
        {
            string newString = string.Empty;

            if (descriptionSing.Contains("]["))
            {
                string[] line = descriptionSing.Split('\n');

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].Contains("]["))
                    {
                        newString += " ";
                    }
                    else
                    {
                        newString += line[i];
                    }
                }
            }
            else
            {
                newString = descriptionSing;
            }

            return newString;
        }

        int comLength;
        public void TextCombine()
        {
            amount = new string[comLength];
            gram = new string[comLength];
            ingre = new string[comLength];

            RemoveUnnecessaryCharacter(ref amount, ReplaceStringToArray(amountsSing));
            RemoveUnnecessaryCharacter(ref gram, ReplaceStringToArray(gramsSing));
            RemoveUnnecessaryCharacter(ref ingre, ReplaceStringToArray(ingredientSing));

            bodyTable = new string[comLength];

            for (int i = 0; i < comLength; i++)
            {
                bodyTable[i] = amount[i] + " " + gram[i] + " " + ingre[i];
            }
        }

        private string[] NewMethod(string nameForm)
        {
            string[] name = ReplaceStringToArray(nameForm);
            Array.Resize(ref name, comLength);
            return name;
        }

        private string BodyEmail()
        {
            string body = string.Empty;

            for (int i = 0; i < bodyTable.Length; i++)
            {
                body += bodyTable[i] + '\n';
            }
            return body;
        }

        private void Send()
        {
            BodyEmail();
            try
            {
                const string email = "culinaryrecipes@wp.pl";
                const string pass = "";
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();

                mail.From = new MailAddress(email);
                mail.To.Add(new MailAddress(txtTo.Text));
                mail.Subject = titleSing;

                if (ChcAddDescription.Checked) mail.Body = BodyEmail() + "\n" + DeleteCharInDescription();
                else mail.Body = BodyEmail();


                client.Host = "smtp.wp.pl";

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(email,pass);

                client.Port = 587;
                client.EnableSsl = true;

                client.Send(mail);

                MessageBox.Show("E mail został wysłany");

                Array.Clear(bodyTable, 0, bodyTable.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Array.Clear(bodyTable, 0, bodyTable.Length);
        }

        private void SendButton()
        {
            if (txtTo.Text != "")
            {
                TextCombine();
                Send();
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
            this.Size = new Size(400, 430);
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
            this.Size = new Size(800, 430);
            pbDecreaseSize.Visible = true;
        }

        private void pbDecreaseSize_Click(object sender, EventArgs e)
        {
            pbDecreaseSize.Visible = false;
            pbIncreaseSize.Visible = true;
            this.Size = new Size(405, 430);
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

                btnAdd.TurnOffTheButton();
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
            btnModify.TurnOffTheButton();

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

                txtTo.Text = string.Empty;
                txtTo.Text = dgGrid.Rows[e.RowIndex].Cells[1].Value.ToString();

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

                        btnDelete.TurnOffTheButton();
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
            btnDeleteAll.TurnOffTheButton();
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
            ButtonMy.TurnOffAllTheButtons(panelGroup);
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
            Form1 open = new Form1();
            this.Hide();
            open.ShowDialog();
        }

        private void btnSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendButton();
        }
    }
}
