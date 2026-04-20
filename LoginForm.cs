using System;
using System.Windows.Forms;

namespace Ettermek
{
    public partial class LoginForm : Form
    {
        private DalUser dalUser;

        // ÚJ: Publikus tulajdonságok a kijelentkezéshez
        public int LoggedInRoleId { get; private set; }
        public string LoggedInUserName { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            dalUser = new DalUser();
            LoadRoles();
        }

        // Szerepkörök betöltése
        private void LoadRoles()
        {
            cmbRole.Items.Add(new RoleItem { Id = 1, Name = "Admin" });
            cmbRole.Items.Add(new RoleItem { Id = 2, Name = "User" });
            cmbRole.Items.Add(new RoleItem { Id = 3, Name = "Guest" });
            cmbRole.DisplayMember = "Name";
            cmbRole.ValueMember = "Id";
            cmbRole.SelectedIndex = 1; // Alapértelmezetten User
        }

        // BEJELENTKEZÉS (MÓDOSÍTOTT LOGIKA)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginUsername.Text) || string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                MessageBox.Show("Kérlek töltsd ki az összes mezőt!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int? roleId = dalUser.ValidateUser(txtLoginUsername.Text, txtLoginPassword.Text);

                if (roleId.HasValue)
                {
                    // Adatok mentése
                    LoggedInRoleId = roleId.Value;
                    LoggedInUserName = txtLoginUsername.Text;

                    MessageBox.Show("Sikeres bejelentkezés!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Siker jelzése és ablak bezárása (a Program.cs indítja majd a Form1-et)
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // REGISZTRÁCIÓ (VÁLTOZATLAN)
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegUsername.Text) ||
                string.IsNullOrWhiteSpace(txtRegPassword.Text) ||
                string.IsNullOrWhiteSpace(txtRegPasswordConfirm.Text))
            {
                MessageBox.Show("Kérlek töltsd ki az összes mezőt!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtRegPassword.Text != txtRegPasswordConfirm.Text)
            {
                MessageBox.Show("A két jelszó nem egyezik!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtRegPassword.Text.Length < 6)
            {
                MessageBox.Show("A jelszónak legalább 6 karakter hosszúnak kell lennie!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                RoleItem selectedRole = (RoleItem)cmbRole.SelectedItem;
                dalUser.RegisterUser(txtRegUsername.Text, txtRegPassword.Text, selectedRole.Id);

                MessageBox.Show("Sikeres regisztráció! Most már bejelentkezhetsz.", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtRegUsername.Clear();
                txtRegPassword.Clear();
                txtRegPasswordConfirm.Clear();
                tabControl1.SelectedTab = tabLogin;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class RoleItem { public int Id { get; set; } public string Name { get; set; } }
    }
}
