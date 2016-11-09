namespace Counter
{
    partial class Form3
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSelectCounter = new System.Windows.Forms.ComboBox();
            this.cbSelectDate = new System.Windows.Forms.ComboBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите счетчик";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Выберите дату";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Внесите показания";
            // 
            // cbSelectCounter
            // 
            this.cbSelectCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectCounter.FormattingEnabled = true;
            this.cbSelectCounter.Location = new System.Drawing.Point(136, 12);
            this.cbSelectCounter.Name = "cbSelectCounter";
            this.cbSelectCounter.Size = new System.Drawing.Size(136, 21);
            this.cbSelectCounter.TabIndex = 3;
            this.cbSelectCounter.SelectedIndexChanged += new System.EventHandler(this.cbSelectCounter_SelectedIndexChanged);
            // 
            // cbSelectDate
            // 
            this.cbSelectDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectDate.FormatString = "yyyy-MM-dd";
            this.cbSelectDate.FormattingEnabled = true;
            this.cbSelectDate.Location = new System.Drawing.Point(136, 39);
            this.cbSelectDate.Name = "cbSelectDate";
            this.cbSelectDate.Size = new System.Drawing.Size(136, 21);
            this.cbSelectDate.TabIndex = 4;
            this.cbSelectDate.SelectedIndexChanged += new System.EventHandler(this.cbSelectDate_SelectedIndexChanged);
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(136, 66);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(136, 20);
            this.tbValue.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Внести изменения";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.cbSelectDate);
            this.Controls.Add(this.cbSelectCounter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form3";
            this.Text = "Изменить показания";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSelectCounter;
        private System.Windows.Forms.ComboBox cbSelectDate;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button button1;
    }
}