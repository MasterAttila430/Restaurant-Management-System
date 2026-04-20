namespace Ettermek
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.keresésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.törlésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.módosításToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kijelentkezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelKereses = new System.Windows.Forms.Panel();
            this.dataGridViewMunkasok = new System.Windows.Forms.DataGridView();
            this.buttonReset = new System.Windows.Forms.Button();
            this.textBoxNevFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxOrszag = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTorles = new System.Windows.Forms.Panel();
            this.btnEtteremTorol = new System.Windows.Forms.Button();
            this.comboBoxEttermek = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelModositas = new System.Windows.Forms.Panel();
            this.panelKonkurencia = new System.Windows.Forms.Panel();
            this.btnKonkurenciaMentes = new System.Windows.Forms.Button();
            this.rbAktualisErtek = new System.Windows.Forms.RadioButton();
            this.rbUjErtek = new System.Windows.Forms.RadioButton();
            this.rbRegiErtek = new System.Windows.Forms.RadioButton();
            this.lblAktualisErtek = new System.Windows.Forms.Label();
            this.lblUjErtek = new System.Windows.Forms.Label();
            this.lblRegiErtek = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnFizetesMentes = new System.Windows.Forms.Button();
            this.textBoxUjFizetes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewModositas = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.panelKereses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMunkasok)).BeginInit();
            this.panelTorles.SuspendLayout();
            this.panelModositas.SuspendLayout();
            this.panelKonkurencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModositas)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keresésToolStripMenuItem,
            this.törlésToolStripMenuItem,
            this.módosításToolStripMenuItem,
            this.kijelentkezésToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // keresésToolStripMenuItem
            // 
            this.keresésToolStripMenuItem.Name = "keresésToolStripMenuItem";
            this.keresésToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.keresésToolStripMenuItem.Text = "Keresés";
            this.keresésToolStripMenuItem.Click += new System.EventHandler(this.keresésToolStripMenuItem_Click);
            // 
            // törlésToolStripMenuItem
            // 
            this.törlésToolStripMenuItem.Name = "törlésToolStripMenuItem";
            this.törlésToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.törlésToolStripMenuItem.Text = "Törlés";
            this.törlésToolStripMenuItem.Click += new System.EventHandler(this.törlésToolStripMenuItem_Click);
            // 
            // módosításToolStripMenuItem
            // 
            this.módosításToolStripMenuItem.Name = "módosításToolStripMenuItem";
            this.módosításToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.módosításToolStripMenuItem.Text = "Módosítás";
            this.módosításToolStripMenuItem.Click += new System.EventHandler(this.módosításToolStripMenuItem_Click);
            // 
            // kijelentkezésToolStripMenuItem
            // 
            this.kijelentkezésToolStripMenuItem.Name = "kijelentkezésToolStripMenuItem";
            this.kijelentkezésToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.kijelentkezésToolStripMenuItem.Text = "Kijelentkezés";
            this.kijelentkezésToolStripMenuItem.Click += new System.EventHandler(this.kijelentkezésToolStripMenuItem_Click);
            // 
            // panelKereses
            // 
            this.panelKereses.Controls.Add(this.dataGridViewMunkasok);
            this.panelKereses.Controls.Add(this.buttonReset);
            this.panelKereses.Controls.Add(this.textBoxNevFilter);
            this.panelKereses.Controls.Add(this.label2);
            this.panelKereses.Controls.Add(this.comboBoxOrszag);
            this.panelKereses.Controls.Add(this.label1);
            this.panelKereses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKereses.Location = new System.Drawing.Point(0, 24);
            this.panelKereses.Name = "panelKereses";
            this.panelKereses.Size = new System.Drawing.Size(800, 426);
            this.panelKereses.TabIndex = 1;
            // 
            // dataGridViewMunkasok
            // 
            this.dataGridViewMunkasok.AllowUserToAddRows = false;
            this.dataGridViewMunkasok.AllowUserToDeleteRows = false;
            this.dataGridViewMunkasok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMunkasok.Location = new System.Drawing.Point(12, 68);
            this.dataGridViewMunkasok.Name = "dataGridViewMunkasok";
            this.dataGridViewMunkasok.ReadOnly = true;
            this.dataGridViewMunkasok.Size = new System.Drawing.Size(776, 346);
            this.dataGridViewMunkasok.TabIndex = 4;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(336, 27);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // textBoxNevFilter
            // 
            this.textBoxNevFilter.Location = new System.Drawing.Point(195, 27);
            this.textBoxNevFilter.Name = "textBoxNevFilter";
            this.textBoxNevFilter.Size = new System.Drawing.Size(121, 20);
            this.textBoxNevFilter.TabIndex = 2;
            this.textBoxNevFilter.TextChanged += new System.EventHandler(this.textBoxNevFilter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Név szűrés";
            // 
            // comboBoxOrszag
            // 
            this.comboBoxOrszag.FormattingEnabled = true;
            this.comboBoxOrszag.Location = new System.Drawing.Point(15, 27);
            this.comboBoxOrszag.Name = "comboBoxOrszag";
            this.comboBoxOrszag.Size = new System.Drawing.Size(154, 21);
            this.comboBoxOrszag.TabIndex = 1;
            this.comboBoxOrszag.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrszag_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ország";
            // 
            // panelTorles
            // 
            this.panelTorles.Controls.Add(this.btnEtteremTorol);
            this.panelTorles.Controls.Add(this.comboBoxEttermek);
            this.panelTorles.Controls.Add(this.label3);
            this.panelTorles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTorles.Location = new System.Drawing.Point(0, 24);
            this.panelTorles.Name = "panelTorles";
            this.panelTorles.Size = new System.Drawing.Size(800, 426);
            this.panelTorles.TabIndex = 5;
            this.panelTorles.Visible = false;
            // 
            // btnEtteremTorol
            // 
            this.btnEtteremTorol.Location = new System.Drawing.Point(265, 25);
            this.btnEtteremTorol.Name = "btnEtteremTorol";
            this.btnEtteremTorol.Size = new System.Drawing.Size(75, 23);
            this.btnEtteremTorol.TabIndex = 2;
            this.btnEtteremTorol.Text = "Törlés";
            this.btnEtteremTorol.UseVisualStyleBackColor = true;
            this.btnEtteremTorol.Click += new System.EventHandler(this.btnEtteremTorol_Click);
            // 
            // comboBoxEttermek
            // 
            this.comboBoxEttermek.FormattingEnabled = true;
            this.comboBoxEttermek.Location = new System.Drawing.Point(15, 27);
            this.comboBoxEttermek.Name = "comboBoxEttermek";
            this.comboBoxEttermek.Size = new System.Drawing.Size(225, 21);
            this.comboBoxEttermek.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Étterem választása";
            // 
            // panelModositas
            // 
            this.panelModositas.Controls.Add(this.panelKonkurencia);
            this.panelModositas.Controls.Add(this.btnFizetesMentes);
            this.panelModositas.Controls.Add(this.textBoxUjFizetes);
            this.panelModositas.Controls.Add(this.label6);
            this.panelModositas.Controls.Add(this.label5);
            this.panelModositas.Controls.Add(this.dataGridViewModositas);
            this.panelModositas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModositas.Location = new System.Drawing.Point(0, 24);
            this.panelModositas.Name = "panelModositas";
            this.panelModositas.Size = new System.Drawing.Size(800, 426);
            this.panelModositas.TabIndex = 6;
            this.panelModositas.Visible = false;
            // 
            // panelKonkurencia
            // 
            this.panelKonkurencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKonkurencia.Controls.Add(this.btnKonkurenciaMentes);
            this.panelKonkurencia.Controls.Add(this.rbAktualisErtek);
            this.panelKonkurencia.Controls.Add(this.rbUjErtek);
            this.panelKonkurencia.Controls.Add(this.rbRegiErtek);
            this.panelKonkurencia.Controls.Add(this.lblAktualisErtek);
            this.panelKonkurencia.Controls.Add(this.lblUjErtek);
            this.panelKonkurencia.Controls.Add(this.lblRegiErtek);
            this.panelKonkurencia.Controls.Add(this.label7);
            this.panelKonkurencia.Location = new System.Drawing.Point(221, 108);
            this.panelKonkurencia.Name = "panelKonkurencia";
            this.panelKonkurencia.Size = new System.Drawing.Size(356, 216);
            this.panelKonkurencia.TabIndex = 5;
            this.panelKonkurencia.Visible = false;
            // 
            // btnKonkurenciaMentes
            // 
            this.btnKonkurenciaMentes.Location = new System.Drawing.Point(128, 176);
            this.btnKonkurenciaMentes.Name = "btnKonkurenciaMentes";
            this.btnKonkurenciaMentes.Size = new System.Drawing.Size(97, 23);
            this.btnKonkurenciaMentes.TabIndex = 7;
            this.btnKonkurenciaMentes.Text = "Mentés";
            this.btnKonkurenciaMentes.UseVisualStyleBackColor = true;
            this.btnKonkurenciaMentes.Click += new System.EventHandler(this.btnKonkurenciaMentes_Click);
            // 
            // rbAktualisErtek
            // 
            this.rbAktualisErtek.AutoSize = true;
            this.rbAktualisErtek.Location = new System.Drawing.Point(17, 142);
            this.rbAktualisErtek.Name = "rbAktualisErtek";
            this.rbAktualisErtek.Size = new System.Drawing.Size(85, 17);
            this.rbAktualisErtek.TabIndex = 6;
            this.rbAktualisErtek.TabStop = true;
            this.rbAktualisErtek.Text = "radioButton3";
            this.rbAktualisErtek.UseVisualStyleBackColor = true;
            // 
            // rbUjErtek
            // 
            this.rbUjErtek.AutoSize = true;
            this.rbUjErtek.Location = new System.Drawing.Point(17, 102);
            this.rbUjErtek.Name = "rbUjErtek";
            this.rbUjErtek.Size = new System.Drawing.Size(85, 17);
            this.rbUjErtek.TabIndex = 5;
            this.rbUjErtek.TabStop = true;
            this.rbUjErtek.Text = "radioButton2";
            this.rbUjErtek.UseVisualStyleBackColor = true;
            // 
            // rbRegiErtek
            // 
            this.rbRegiErtek.AutoSize = true;
            this.rbRegiErtek.Location = new System.Drawing.Point(17, 62);
            this.rbRegiErtek.Name = "rbRegiErtek";
            this.rbRegiErtek.Size = new System.Drawing.Size(85, 17);
            this.rbRegiErtek.TabIndex = 4;
            this.rbRegiErtek.TabStop = true;
            this.rbRegiErtek.Text = "radioButton1";
            this.rbRegiErtek.UseVisualStyleBackColor = true;
            // 
            // lblAktualisErtek
            // 
            this.lblAktualisErtek.AutoSize = true;
            this.lblAktualisErtek.Location = new System.Drawing.Point(14, 126);
            this.lblAktualisErtek.Name = "lblAktualisErtek";
            this.lblAktualisErtek.Size = new System.Drawing.Size(35, 13);
            this.lblAktualisErtek.TabIndex = 3;
            this.lblAktualisErtek.Text = "label8";
            // 
            // lblUjErtek
            // 
            this.lblUjErtek.AutoSize = true;
            this.lblUjErtek.Location = new System.Drawing.Point(14, 86);
            this.lblUjErtek.Name = "lblUjErtek";
            this.lblUjErtek.Size = new System.Drawing.Size(35, 13);
            this.lblUjErtek.TabIndex = 2;
            this.lblUjErtek.Text = "label8";
            // 
            // lblRegiErtek
            // 
            this.lblRegiErtek.AutoSize = true;
            this.lblRegiErtek.Location = new System.Drawing.Point(14, 46);
            this.lblRegiErtek.Name = "lblRegiErtek";
            this.lblRegiErtek.Size = new System.Drawing.Size(35, 13);
            this.lblRegiErtek.TabIndex = 1;
            this.lblRegiErtek.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(14, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(329, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "KONKURENCIA KONFLIKTUS DETEKTÁLVA!";
            // 
            // btnFizetesMentes
            // 
            this.btnFizetesMentes.Location = new System.Drawing.Point(235, 29);
            this.btnFizetesMentes.Name = "btnFizetesMentes";
            this.btnFizetesMentes.Size = new System.Drawing.Size(75, 23);
            this.btnFizetesMentes.TabIndex = 4;
            this.btnFizetesMentes.Text = "Mentés";
            this.btnFizetesMentes.UseVisualStyleBackColor = true;
            this.btnFizetesMentes.Click += new System.EventHandler(this.btnFizetesMentes_Click);
            // 
            // textBoxUjFizetes
            // 
            this.textBoxUjFizetes.Location = new System.Drawing.Point(108, 31);
            this.textBoxUjFizetes.Name = "textBoxUjFizetes";
            this.textBoxUjFizetes.Size = new System.Drawing.Size(100, 20);
            this.textBoxUjFizetes.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(105, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Új fizetés";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Válassz sort!";
            // 
            // dataGridViewModositas
            // 
            this.dataGridViewModositas.AllowUserToAddRows = false;
            this.dataGridViewModositas.AllowUserToDeleteRows = false;
            this.dataGridViewModositas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModositas.Location = new System.Drawing.Point(15, 68);
            this.dataGridViewModositas.Name = "dataGridViewModositas";
            this.dataGridViewModositas.ReadOnly = true;
            this.dataGridViewModositas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewModositas.Size = new System.Drawing.Size(773, 346);
            this.dataGridViewModositas.TabIndex = 0;
            this.dataGridViewModositas.SelectionChanged += new System.EventHandler(this.dataGridViewModositas_SelectionChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelKereses);
            this.Controls.Add(this.panelModositas);
            this.Controls.Add(this.panelTorles);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Éttermek Kezelése";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelKereses.ResumeLayout(false);
            this.panelKereses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMunkasok)).EndInit();
            this.panelTorles.ResumeLayout(false);
            this.panelTorles.PerformLayout();
            this.panelModositas.ResumeLayout(false);
            this.panelModositas.PerformLayout();
            this.panelKonkurencia.ResumeLayout(false);
            this.panelKonkurencia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModositas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem keresésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem törlésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem módosításToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kijelentkezésToolStripMenuItem;
        private System.Windows.Forms.Panel panelKereses;
        private System.Windows.Forms.DataGridView dataGridViewMunkasok;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TextBox textBoxNevFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxOrszag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTorles;
        private System.Windows.Forms.Button btnEtteremTorol;
        private System.Windows.Forms.ComboBox comboBoxEttermek;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelModositas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewModositas;
        private System.Windows.Forms.Button btnFizetesMentes;
        private System.Windows.Forms.TextBox textBoxUjFizetes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelKonkurencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRegiErtek;
        private System.Windows.Forms.Label lblAktualisErtek;
        private System.Windows.Forms.Label lblUjErtek;
        private System.Windows.Forms.RadioButton rbAktualisErtek;
        private System.Windows.Forms.RadioButton rbUjErtek;
        private System.Windows.Forms.RadioButton rbRegiErtek;
        private System.Windows.Forms.Button btnKonkurenciaMentes;
    }
}
