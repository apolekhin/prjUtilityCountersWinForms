namespace Counter
{
    partial class frmCounterValueDetail
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbSelectedCounter = new System.Windows.Forms.ComboBox();
            this.dtSelectedDate = new System.Windows.Forms.DateTimePicker();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lblSelectedCounter = new System.Windows.Forms.Label();
            this.lblSelectedDate = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(44, 114);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(125, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbSelectedCounter
            // 
            this.cbSelectedCounter.FormattingEnabled = true;
            this.cbSelectedCounter.Location = new System.Drawing.Point(109, 12);
            this.cbSelectedCounter.Name = "cbSelectedCounter";
            this.cbSelectedCounter.Size = new System.Drawing.Size(121, 21);
            this.cbSelectedCounter.TabIndex = 2;
            // 
            // dtSelectedDate
            // 
            this.dtSelectedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSelectedDate.Location = new System.Drawing.Point(109, 40);
            this.dtSelectedDate.Name = "dtSelectedDate";
            this.dtSelectedDate.Size = new System.Drawing.Size(121, 20);
            this.dtSelectedDate.TabIndex = 3;
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(109, 64);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(121, 20);
            this.tbValue.TabIndex = 4;
            // 
            // lblSelectedCounter
            // 
            this.lblSelectedCounter.AutoSize = true;
            this.lblSelectedCounter.Location = new System.Drawing.Point(12, 15);
            this.lblSelectedCounter.Name = "lblSelectedCounter";
            this.lblSelectedCounter.Size = new System.Drawing.Size(47, 13);
            this.lblSelectedCounter.TabIndex = 5;
            this.lblSelectedCounter.Text = "Счетчик";
            // 
            // lblSelectedDate
            // 
            this.lblSelectedDate.AutoSize = true;
            this.lblSelectedDate.Location = new System.Drawing.Point(12, 40);
            this.lblSelectedDate.Name = "lblSelectedDate";
            this.lblSelectedDate.Size = new System.Drawing.Size(33, 13);
            this.lblSelectedDate.TabIndex = 6;
            this.lblSelectedDate.Text = "Дата";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(12, 67);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(63, 13);
            this.lblValue.TabIndex = 7;
            this.lblValue.Text = "Показания";
            // 
            // frmCounterValueDetail
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(242, 149);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblSelectedDate);
            this.Controls.Add(this.lblSelectedCounter);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.dtSelectedDate);
            this.Controls.Add(this.cbSelectedCounter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCounterValueDetail";
            this.Text = "frmCounterValueDetail";
            this.Load += new System.EventHandler(this.frmCounterValueDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbSelectedCounter;
        private System.Windows.Forms.DateTimePicker dtSelectedDate;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lblSelectedCounter;
        private System.Windows.Forms.Label lblSelectedDate;
        private System.Windows.Forms.Label lblValue;
    }
}