namespace dodawanie_figur1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_dodaj = new System.Windows.Forms.Button();
            this.button_Rysuj = new System.Windows.Forms.Button();
            this.checkedBox_LISTA = new System.Windows.Forms.CheckedListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox_matrycaW = new System.Windows.Forms.TextBox();
            this.textBox_matrycaH = new System.Windows.Forms.TextBox();
            this.button_nowa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_usun = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Figura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Szer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wysok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_rozmiesc = new System.Windows.Forms.Button();
            this.button_unSel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_dodaj
            // 
            this.button_dodaj.Enabled = false;
            this.button_dodaj.Location = new System.Drawing.Point(6, 19);
            this.button_dodaj.Name = "button_dodaj";
            this.button_dodaj.Size = new System.Drawing.Size(75, 23);
            this.button_dodaj.TabIndex = 1;
            this.button_dodaj.Text = "Dodaj";
            this.button_dodaj.UseVisualStyleBackColor = true;
            this.button_dodaj.Click += new System.EventHandler(this.button_dodaj_Click);
            // 
            // button_Rysuj
            // 
            this.button_Rysuj.Enabled = false;
            this.button_Rysuj.Location = new System.Drawing.Point(249, 19);
            this.button_Rysuj.Name = "button_Rysuj";
            this.button_Rysuj.Size = new System.Drawing.Size(75, 23);
            this.button_Rysuj.TabIndex = 2;
            this.button_Rysuj.Text = "Rysuj";
            this.button_Rysuj.UseVisualStyleBackColor = true;
            this.button_Rysuj.Click += new System.EventHandler(this.button_Rysuj_Click);
            // 
            // checkedBox_LISTA
            // 
            this.checkedBox_LISTA.CheckOnClick = true;
            this.checkedBox_LISTA.Enabled = false;
            this.checkedBox_LISTA.FormattingEnabled = true;
            this.checkedBox_LISTA.Items.AddRange(new object[] {
            "Elipsa",
            "Koło",
            "Kwadrat",
            "Niestandardowe",
            "Prostokąt ",
            "Trójkąt"});
            this.checkedBox_LISTA.Location = new System.Drawing.Point(246, 22);
            this.checkedBox_LISTA.Name = "checkedBox_LISTA";
            this.checkedBox_LISTA.Size = new System.Drawing.Size(112, 79);
            this.checkedBox_LISTA.Sorted = true;
            this.checkedBox_LISTA.TabIndex = 3;
            this.checkedBox_LISTA.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 311);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(376, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox_matrycaW
            // 
            this.textBox_matrycaW.Location = new System.Drawing.Point(6, 48);
            this.textBox_matrycaW.Name = "textBox_matrycaW";
            this.textBox_matrycaW.Size = new System.Drawing.Size(100, 20);
            this.textBox_matrycaW.TabIndex = 7;
            this.textBox_matrycaW.Text = "1200";
            // 
            // textBox_matrycaH
            // 
            this.textBox_matrycaH.Location = new System.Drawing.Point(118, 48);
            this.textBox_matrycaH.Name = "textBox_matrycaH";
            this.textBox_matrycaH.Size = new System.Drawing.Size(100, 20);
            this.textBox_matrycaH.TabIndex = 8;
            this.textBox_matrycaH.Text = "800";
            // 
            // button_nowa
            // 
            this.button_nowa.Location = new System.Drawing.Point(6, 19);
            this.button_nowa.Name = "button_nowa";
            this.button_nowa.Size = new System.Drawing.Size(212, 23);
            this.button_nowa.TabIndex = 9;
            this.button_nowa.Text = "Nowa matryca";
            this.button_nowa.UseVisualStyleBackColor = true;
            this.button_nowa.Click += new System.EventHandler(this.button_nowa_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.textBox_matrycaW);
            this.groupBox1.Controls.Add(this.button_nowa);
            this.groupBox1.Controls.Add(this.textBox_matrycaH);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 90);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button_usun);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.button_rozmiesc);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button_Rysuj);
            this.groupBox2.Controls.Add(this.button_dodaj);
            this.groupBox2.Location = new System.Drawing.Point(12, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(391, 337);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // button_usun
            // 
            this.button_usun.Enabled = false;
            this.button_usun.Location = new System.Drawing.Point(87, 19);
            this.button_usun.Name = "button_usun";
            this.button_usun.Size = new System.Drawing.Size(75, 23);
            this.button_usun.TabIndex = 13;
            this.button_usun.Text = "Usuń";
            this.button_usun.UseVisualStyleBackColor = true;
            this.button_usun.Click += new System.EventHandler(this.button_usun_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Figura,
            this.ID,
            this.Pole,
            this.X,
            this.Y,
            this.Szer,
            this.Wysok});
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(6, 48);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(376, 257);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // Figura
            // 
            this.Figura.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Figura.FillWeight = 156.1058F;
            this.Figura.HeaderText = "Figura";
            this.Figura.MinimumWidth = 50;
            this.Figura.Name = "Figura";
            this.Figura.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ID.FillWeight = 106.599F;
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 30;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Pole
            // 
            this.Pole.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Pole.FillWeight = 139.9202F;
            this.Pole.HeaderText = "Pole";
            this.Pole.MinimumWidth = 20;
            this.Pole.Name = "Pole";
            this.Pole.ReadOnly = true;
            // 
            // X
            // 
            this.X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X.FillWeight = 74.34377F;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.FillWeight = 74.34377F;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            // 
            // Szer
            // 
            this.Szer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Szer.FillWeight = 74.34377F;
            this.Szer.HeaderText = "W";
            this.Szer.Name = "Szer";
            this.Szer.ReadOnly = true;
            // 
            // Wysok
            // 
            this.Wysok.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Wysok.FillWeight = 74.34377F;
            this.Wysok.HeaderText = "H";
            this.Wysok.Name = "Wysok";
            this.Wysok.ReadOnly = true;
            // 
            // button_rozmiesc
            // 
            this.button_rozmiesc.Enabled = false;
            this.button_rozmiesc.Location = new System.Drawing.Point(168, 19);
            this.button_rozmiesc.Name = "button_rozmiesc";
            this.button_rozmiesc.Size = new System.Drawing.Size(75, 23);
            this.button_rozmiesc.TabIndex = 7;
            this.button_rozmiesc.Text = "Rozmieść";
            this.button_rozmiesc.UseVisualStyleBackColor = true;
            this.button_rozmiesc.Click += new System.EventHandler(this.button_rozmiesc_Click);
            // 
            // button_unSel
            // 
            this.button_unSel.Location = new System.Drawing.Point(72, 478);
            this.button_unSel.Name = "button_unSel";
            this.button_unSel.Size = new System.Drawing.Size(75, 23);
            this.button_unSel.TabIndex = 14;
            this.button_unSel.Text = "Unselect";
            this.button_unSel.UseVisualStyleBackColor = true;
            this.button_unSel.Click += new System.EventHandler(this.button_unSel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(418, 561);
            this.Controls.Add(this.button_unSel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkedBox_LISTA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "Form1";
            this.Text = "Rozmiesczanie figur v1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button button_dodaj;
      
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button_Rysuj;
        public System.Windows.Forms.CheckedListBox checkedBox_LISTA;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox_matrycaW;
        public System.Windows.Forms.TextBox textBox_matrycaH;
        public System.Windows.Forms.Button button_nowa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button button_rozmiesc;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button button_usun;
        public System.Windows.Forms.DataGridViewTextBoxColumn Figura;
        public System.Windows.Forms.DataGridViewTextBoxColumn ID;
        public System.Windows.Forms.DataGridViewTextBoxColumn Pole;
        public System.Windows.Forms.DataGridViewTextBoxColumn X;
        public System.Windows.Forms.DataGridViewTextBoxColumn Y;
        public System.Windows.Forms.DataGridViewTextBoxColumn Szer;
        public System.Windows.Forms.DataGridViewTextBoxColumn Wysok;
        public System.Windows.Forms.Button button_unSel;
    }
}

