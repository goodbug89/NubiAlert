namespace NubiAlert
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Interval = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_nodata = new System.Windows.Forms.CheckBox();
            this.QuickQueryCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(594, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start!!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.Location = new System.Drawing.Point(12, 11);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.Size = new System.Drawing.Size(569, 445);
            this.richTextBox_log.TabIndex = 1;
            this.richTextBox_log.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Interval :";
            // 
            // textBox_Interval
            // 
            this.textBox_Interval.Location = new System.Drawing.Point(651, 72);
            this.textBox_Interval.Name = "textBox_Interval";
            this.textBox_Interval.Size = new System.Drawing.Size(64, 21);
            this.textBox_Interval.TabIndex = 3;
            this.textBox_Interval.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(715, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Seconds";
            // 
            // checkBox_nodata
            // 
            this.checkBox_nodata.AutoSize = true;
            this.checkBox_nodata.Location = new System.Drawing.Point(597, 110);
            this.checkBox_nodata.Name = "checkBox_nodata";
            this.checkBox_nodata.Size = new System.Drawing.Size(119, 16);
            this.checkBox_nodata.TabIndex = 5;
            this.checkBox_nodata.Text = "Display No Data!";
            this.checkBox_nodata.UseVisualStyleBackColor = true;
            // 
            // QuickQueryCheck
            // 
            this.QuickQueryCheck.AutoSize = true;
            this.QuickQueryCheck.Checked = true;
            this.QuickQueryCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.QuickQueryCheck.Location = new System.Drawing.Point(597, 147);
            this.QuickQueryCheck.Name = "QuickQueryCheck";
            this.QuickQueryCheck.Size = new System.Drawing.Size(90, 16);
            this.QuickQueryCheck.TabIndex = 6;
            this.QuickQueryCheck.Text = "QuickQuery";
            this.QuickQueryCheck.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 465);
            this.Controls.Add(this.QuickQueryCheck);
            this.Controls.Add(this.checkBox_nodata);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Interval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_log);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "NubiAlert - 20181102";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Interval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_nodata;
        private System.Windows.Forms.CheckBox QuickQueryCheck;
    }
}

