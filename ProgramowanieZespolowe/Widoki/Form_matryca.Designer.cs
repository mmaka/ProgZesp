namespace dodawanie_figur1
{
    partial class Form_matryca
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_matryca));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_drukuj = new System.Windows.Forms.Button();
            this.button_zapiszOb = new System.Windows.Forms.Button();
            this.richTextBox_Dymek = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 237);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button_drukuj
            // 
            this.button_drukuj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_drukuj.Enabled = false;
            this.button_drukuj.Location = new System.Drawing.Point(12, 259);
            this.button_drukuj.Name = "button_drukuj";
            this.button_drukuj.Size = new System.Drawing.Size(75, 23);
            this.button_drukuj.TabIndex = 2;
            this.button_drukuj.Text = "Drukuj";
            this.button_drukuj.UseVisualStyleBackColor = true;
            this.button_drukuj.Click += new System.EventHandler(this.button_drukuj_Click);
            // 
            // button_zapiszOb
            // 
            this.button_zapiszOb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_zapiszOb.Location = new System.Drawing.Point(93, 259);
            this.button_zapiszOb.Name = "button_zapiszOb";
            this.button_zapiszOb.Size = new System.Drawing.Size(99, 23);
            this.button_zapiszOb.TabIndex = 3;
            this.button_zapiszOb.Text = "Zapisz Obraz";
            this.button_zapiszOb.UseVisualStyleBackColor = true;
            this.button_zapiszOb.Click += new System.EventHandler(this.button_zapisz_Click);
            // 
            // richTextBox_Dymek
            // 
            this.richTextBox_Dymek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox_Dymek.DetectUrls = false;
            this.richTextBox_Dymek.HideSelection = false;
            this.richTextBox_Dymek.Location = new System.Drawing.Point(198, 259);
            this.richTextBox_Dymek.Multiline = false;
            this.richTextBox_Dymek.Name = "richTextBox_Dymek";
            this.richTextBox_Dymek.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox_Dymek.Size = new System.Drawing.Size(621, 23);
            this.richTextBox_Dymek.TabIndex = 4;
            this.richTextBox_Dymek.Text = "";
            // 
            // Form_matryca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(827, 290);
            this.Controls.Add(this.richTextBox_Dymek);
            this.Controls.Add(this.button_zapiszOb);
            this.Controls.Add(this.button_drukuj);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(439, 0);
            this.Name = "Form_matryca";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Form_matryca";
            this.Load += new System.EventHandler(this.Form_matryca_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

       public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button button_drukuj;
        public System.Windows.Forms.Button button_zapiszOb;
        public System.Windows.Forms.RichTextBox richTextBox_Dymek;
    }
}