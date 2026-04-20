using System;
using System.Data;
using System.Windows.Forms;

namespace Ettermek
{
    public partial class Form1 : Form
    {
        private DalMunkasok dataAccess;
        private MunkasDTO kivalasztottMunkas;
        private decimal aktualisRegiErtek;
        private int _roleId;
        private string _userName;

        public Form1(int roleId, string userName)
        {
            InitializeComponent();
            _roleId = roleId;
            _userName = userName;
            this.Text = $"Éttermek Kezelése - Bejelentkezve: {_userName} (Jog: {GetRoleName(_roleId)})";
            dataAccess = new DalMunkasok();
        }

        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1: return "Admin";
                case 2: return "User";
                case 3: return "Guest";
                default: return "Ismeretlen";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string errorMessage = "";
            bool connectionSuccess = dataAccess.TestConnection(ref errorMessage);

            if (!connectionSuccess)
            {
                MessageBox.Show($"Kapcsolodasi hiba: {errorMessage}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ApplyPermissions();
            LoadOrszagok();
            textBoxNevFilter.Clear();
            LoadMunkasokData();

            // FONTOS: Alapból jelenjen meg a keresés!
            keresésToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        private void ApplyPermissions()
        {
            if (_roleId == 2) // User
            {
                if (törlésToolStripMenuItem != null)
                {
                    törlésToolStripMenuItem.Enabled = false;
                    törlésToolStripMenuItem.Visible = false;
                }
            }
            if (_roleId == 3) // Guest
            {
                if (törlésToolStripMenuItem != null)
                {
                    törlésToolStripMenuItem.Enabled = false;
                    törlésToolStripMenuItem.Visible = false;
                }
                if (módosításToolStripMenuItem != null)
                {
                    módosításToolStripMenuItem.Enabled = false;
                    módosításToolStripMenuItem.Visible = false;
                }
            }
        }

        private void kijelentkezésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Biztosan ki szeretne jelentkezni?", "Kijelentkezés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        private void keresésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelKereses.Visible = true;
            panelTorles.Visible = false;
            panelModositas.Visible = false;
            LoadMunkasokData();
        }

        private void törlésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_roleId != 1)
            {
                MessageBox.Show("Nincs jogosultsága ehhez a funkcióhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            panelKereses.Visible = false;
            panelTorles.Visible = true;
            panelModositas.Visible = false;
            LoadEttermekCombo();
        }

        private void módosításToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_roleId == 3)
            {
                MessageBox.Show("Nincs jogosultsága ehhez a funkcióhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            panelKereses.Visible = false;
            panelTorles.Visible = false;
            panelModositas.Visible = true;
            LoadMunkasokModositas();
        }

        private void LoadOrszagok()
        {
            string errorMessage = "";
            DataSet ds = dataAccess.GetOrszagokData(ref errorMessage);

            if (errorMessage != "OK" || ds == null || ds.Tables.Count == 0)
            {
                // Ha hiba van, a ComboBox üres marad, de a program nem száll el.
                return;
            }

            DataTable t = ds.Tables[0].Copy();
            DataRow allRow = t.NewRow();
            allRow["OrszagID"] = 0;
            allRow["OrszagNev"] = "Osszes";
            t.Rows.InsertAt(allRow, 0);

            comboBoxOrszag.SelectedIndexChanged -= comboBoxOrszag_SelectedIndexChanged;
            comboBoxOrszag.DisplayMember = "OrszagNev";
            comboBoxOrszag.ValueMember = "OrszagID";
            comboBoxOrszag.DataSource = t;

            // JAVÍTÁS: Csak akkor állítjuk be, ha van elem a listában
            if (comboBoxOrszag.Items.Count > 0)
            {
                comboBoxOrszag.SelectedIndex = 0;
            }

            comboBoxOrszag.SelectedIndexChanged += comboBoxOrszag_SelectedIndexChanged;
        }

        private void LoadMunkasokData()
        {
            try
            {
                int? orszagId = null;
                if (comboBoxOrszag.SelectedValue != null)
                {
                    if (int.TryParse(comboBoxOrszag.SelectedValue.ToString(), out int val) && val != 0)
                        orszagId = val;
                }

                string nevFilter = textBoxNevFilter.Text?.Trim();
                string errorMessage = "";
                DataSet ds = dataAccess.GetFilteredMunkasok(orszagId, nevFilter, ref errorMessage);

                if (errorMessage != "OK") return;
                if (ds == null || ds.Tables.Count == 0)
                {
                    dataGridViewMunkasok.DataSource = null;
                    return;
                }

                dataGridViewMunkasok.DataSource = ds.Tables[0];

                if (dataGridViewMunkasok.Columns.Count > 0)
                {
                    // Oszlop formázások...
                    if (dataGridViewMunkasok.Columns.Contains("Fizetes"))
                    {
                        dataGridViewMunkasok.Columns["Fizetes"].DefaultCellStyle.Format = "N2";
                        dataGridViewMunkasok.Columns["Fizetes"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    dataGridViewMunkasok.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Váratlan hiba: {ex.Message}", "Hiba");
            }
        }

        private void comboBoxOrszag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOrszag.DataSource == null) return;
            LoadMunkasokData();
        }

        private void textBoxNevFilter_TextChanged(object sender, EventArgs e)
        {
            LoadMunkasokData();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (comboBoxOrszag.Items.Count > 0)
                comboBoxOrszag.SelectedIndex = 0;
            textBoxNevFilter.Clear();
        }

        private void LoadEttermekCombo()
        {
            string errorMessage = "";
            DataSet ds = dataAccess.GetEttermekData(ref errorMessage);
            if (errorMessage != "OK" || ds == null || ds.Tables.Count == 0) return;

            comboBoxEttermek.DisplayMember = "Nev";
            comboBoxEttermek.ValueMember = "EtteremID";
            comboBoxEttermek.DataSource = ds.Tables[0];
        }

        private void btnEtteremTorol_Click(object sender, EventArgs e)
        {
            if (comboBoxEttermek.SelectedValue == null) return;
            int etteremID = Convert.ToInt32(comboBoxEttermek.SelectedValue);
            string etteremNev = comboBoxEttermek.Text;

            if (MessageBox.Show($"Biztosan törölni szeretné a(z) '{etteremNev}' éttermet?", "Törlés megerősítése", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string errorMessage = "";
                int affectedRows = dataAccess.DeleteEtterem(etteremID, ref errorMessage);

                if (errorMessage == "OK" && affectedRows > 0)
                {
                    MessageBox.Show("Sikeres törlés!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEttermekCombo();
                }
                else
                {
                    MessageBox.Show($"Hiba a törlés során: {errorMessage}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadMunkasokModositas()
        {
            string errorMessage = "";
            DataSet ds = dataAccess.GetAllMunkasok(ref errorMessage);
            if (errorMessage != "OK" || ds == null) return;
            dataGridViewModositas.DataSource = ds.Tables[0];
            dataGridViewModositas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dataGridViewModositas_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewModositas.SelectedRows.Count > 0)
            {
                int munkasID = Convert.ToInt32(dataGridViewModositas.SelectedRows[0].Cells["MunkasID"].Value);
                string errorMessage = "";
                kivalasztottMunkas = dataAccess.GetMunkasByID(munkasID, ref errorMessage);

                if (errorMessage == "OK" && kivalasztottMunkas != null)
                {
                    textBoxUjFizetes.Text = kivalasztottMunkas.Fizetes.ToString("F2");
                    aktualisRegiErtek = kivalasztottMunkas.Fizetes;
                    panelKonkurencia.Visible = false;
                }
            }
        }

        private void btnFizetesMentes_Click(object sender, EventArgs e)
        {
            if (kivalasztottMunkas == null || !decimal.TryParse(textBoxUjFizetes.Text, out decimal ujFizetes)) return;

            string errorMessage = "";
            MunkasDTO aktualisMunkas = dataAccess.GetMunkasByID(kivalasztottMunkas.MunkasID, ref errorMessage);

            if (!ByteArrayEquals(kivalasztottMunkas.RowVersion, aktualisMunkas.RowVersion))
            {
                KezelKonkurencia(ujFizetes, aktualisMunkas);
            }
            else
            {
                MentFizetes(kivalasztottMunkas.MunkasID, ujFizetes, kivalasztottMunkas.RowVersion);
            }
        }

        private bool ByteArrayEquals(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length) return false;
            for (int i = 0; i < a1.Length; i++) if (a1[i] != a2[i]) return false;
            return true;
        }

        private void KezelKonkurencia(decimal ujFizetes, MunkasDTO aktualisMunkas)
        {
            panelKonkurencia.Visible = true;
            lblRegiErtek.Text = $"Régi érték: {kivalasztottMunkas.Fizetes:N2}";
            lblUjErtek.Text = $"Saját értéked: {ujFizetes:N2}";
            lblAktualisErtek.Text = $"Aktuális érték: {aktualisMunkas.Fizetes:N2}";
            rbUjErtek.Checked = true;
            btnKonkurenciaMentes.Tag = new { UjFizetes = ujFizetes, AktualisMunkas = aktualisMunkas };
        }

        private void btnKonkurenciaMentes_Click(object sender, EventArgs e)
        {
            if (btnKonkurenciaMentes.Tag == null) return;
            dynamic adatok = btnKonkurenciaMentes.Tag;
            decimal valasztottFizetes = rbRegiErtek.Checked ? kivalasztottMunkas.Fizetes :
                                      rbUjErtek.Checked ? adatok.UjFizetes :
                                      adatok.AktualisMunkas.Fizetes;

            MentFizetes(kivalasztottMunkas.MunkasID, valasztottFizetes, adatok.AktualisMunkas.RowVersion);
            panelKonkurencia.Visible = false;
        }

        private void MentFizetes(int munkasID, decimal ujFizetes, byte[] rowVersion)
        {
            string errorMessage = "";
            int affectedRows = dataAccess.UpdateMunkasFizetes(munkasID, ujFizetes, rowVersion, ref errorMessage);

            if (errorMessage == "OK" && affectedRows > 0)
            {
                MessageBox.Show("Fizetés sikeresen módosítva!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMunkasokModositas();
            }
            else
            {
                MessageBox.Show("A módosítás sikertelen (lehet, hogy valaki már módosította).", "Konkurencia Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                    dataAccess = null;
                }
            }
            catch { }

            // Fontos: A base hívás legyen a legvégén!
            base.OnFormClosed(e);
        }
    }
}
