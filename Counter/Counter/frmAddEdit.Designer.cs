namespace Counter
{
    partial class frmAddEdit
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
            this.dtSelectedDate = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.cbSelectedCounter = new System.Windows.Forms.ComboBox();
            this.lblCounterName = new System.Windows.Forms.Label();
            this.lblSelectedDate = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbSelectDate = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dtSelectedDate
            // 
            this.dtSelectedDate.CustomFormat = "yyyy-MM-dd";
            this.dtSelectedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSelectedDate.Location = new System.Drawing.Point(113, 42);
            this.dtSelectedDate.Name = "dtSelectedDate";
            this.dtSelectedDate.Size = new System.Drawing.Size(120, 20);
            this.dtSelectedDate.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(16, 126);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(162, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(113, 78);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(120, 20);
            this.tbValue.TabIndex = 3;
            // 
            // cbSelectedCounter
            // 
            this.cbSelectedCounter.FormattingEnabled = true;
            this.cbSelectedCounter.Location = new System.Drawing.Point(113, 13);
            this.cbSelectedCounter.Name = "cbSelectedCounter";
            this.cbSelectedCounter.Size = new System.Drawing.Size(121, 21);
            this.cbSelectedCounter.TabIndex = 4;
            this.cbSelectedCounter.SelectedIndexChanged += new System.EventHandler(this.cbSelectedCounter_SelectedIndexChanged);
            // 
            // lblCounterName
            // 
            this.lblCounterName.AutoSize = true;
            this.lblCounterName.Location = new System.Drawing.Point(13, 16);
            this.lblCounterName.Name = "lblCounterName";
            this.lblCounterName.Size = new System.Drawing.Size(77, 13);
            this.lblCounterName.TabIndex = 5;
            this.lblCounterName.Text = "Select Counter";
            // 
            // lblSelectedDate
            // 
            this.lblSelectedDate.AutoSize = true;
            this.lblSelectedDate.Location = new System.Drawing.Point(13, 48);
            this.lblSelectedDate.Name = "lblSelectedDate";
            this.lblSelectedDate.Size = new System.Drawing.Size(75, 13);
            this.lblSelectedDate.TabIndex = 6;
            this.lblSelectedDate.Text = "Selected Date";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(13, 81);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(61, 13);
            this.lblValue.TabIndex = 7;
            this.lblValue.Text = "Input Value";
            // 
            // cbSelectDate
            // 
            this.cbSelectDate.FormatString = "yyyy-MM-dd";
            this.cbSelectDate.FormattingEnabled = true;
            this.cbSelectDate.Location = new System.Drawing.Point(112, 45);
            this.cbSelectDate.Name = "cbSelectDate";
            this.cbSelectDate.Size = new System.Drawing.Size(121, 21);
            this.cbSelectDate.TabIndex = 8;
            this.cbSelectDate.SelectedIndexChanged += new System.EventHandler(this.cbSelectDate_SelectedIndexChanged);
            // 
            // frmAddEdit
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(249, 161);
            this.Controls.Add(this.cbSelectDate);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblSelectedDate);
            this.Controls.Add(this.lblCounterName);
            this.Controls.Add(this.cbSelectedCounter);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtSelectedDate);
            this.Name = "frmAddEdit";
            this.Text = "frmAddEdit";
            this.Load += new System.EventHandler(this.frmAddEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtSelectedDate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.ComboBox cbSelectedCounter;
        private System.Windows.Forms.Label lblCounterName;
        private System.Windows.Forms.Label lblSelectedDate;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbSelectDate;
    }
}