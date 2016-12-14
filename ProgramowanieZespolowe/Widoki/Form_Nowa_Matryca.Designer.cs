namespace ProgramowanieZespolowe
{
    partial class Form_Nowa_Matryca
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_matrycaW = new System.Windows.Forms.TextBox();
            this.textBox_matrycaH = new System.Windows.Forms.TextBox();
            this.button_anuluj = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.textBox_matrycaW);
            this.groupBox1.Controls.Add(this.textBox_matrycaH);
            this.groupBox1.Location = new System.Drawing.Point(29, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 76);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
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
            // button_anuluj
            // 
            this.button_anuluj.Location = new System.Drawing.Point(141, 160);
            this.button_anuluj.Name = "button_anuluj";
            this.button_anuluj.Size = new System.Drawing.Size(75, 23);
            this.button_anuluj.TabIndex = 11;
            this.button_anuluj.Text = "Anuluj";
            this.button_anuluj.UseVisualStyleBackColor = true;
            this.button_anuluj.Click += new System.EventHandler(this.button_anuluj_Click);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(60, 160);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 10;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // Form_Nowa_Matryca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProgramowanieZespolowe.Properties.Resources.wood;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button_anuluj);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_ok);
            this.Name = "Form_Nowa_Matryca";
            this.Text = "Form_Nowa_Matryca";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox textBox_matrycaW;
        public System.Windows.Forms.TextBox textBox_matrycaH;
        public System.Windows.Forms.Button button_anuluj;
        public System.Windows.Forms.Button button_ok;
    }
}