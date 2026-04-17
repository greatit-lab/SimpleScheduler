// JobEditForm.Designer.cs
namespace SimpleScheduler
{
    partial class JobEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.chkUseEndTime = new System.Windows.Forms.CheckBox();
            
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRunType = new System.Windows.Forms.ComboBox();
            
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            
            this.dtpRunTime = new System.Windows.Forms.DateTimePicker();
            this.cmbWeeklyDay = new System.Windows.Forms.ComboBox();
            this.numMonthlyDay = new System.Windows.Forms.NumericUpDown();
            this.lblMonthlyDay = new System.Windows.Forms.Label();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyDay)).BeginInit();
            this.SuspendLayout();
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "작업 이름";
            
            // txtName
            this.txtName.Location = new System.Drawing.Point(100, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 21);
            this.txtName.TabIndex = 0;
            
            // txtTargetPath
            this.txtTargetPath.Location = new System.Drawing.Point(100, 39);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(193, 21);
            this.txtTargetPath.TabIndex = 1;
            
            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "실행 대상";
            
            // btnBrowse
            this.btnBrowse.Location = new System.Drawing.Point(299, 37);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(81, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "찾아보기...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            
            // txtArguments
            this.txtArguments.Location = new System.Drawing.Point(100, 66);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(280, 21);
            this.txtArguments.TabIndex = 3;
            
            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "인수 (옵션)";
            
            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "시작시간";
            
            // dtpStartTime
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(100, 95);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(280, 21);
            this.dtpStartTime.TabIndex = 4;
            
            // dtpEndTime
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(100, 122);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(280, 21);
            this.dtpEndTime.TabIndex = 6;
            
            // chkUseEndTime
            this.chkUseEndTime.AutoSize = true;
            this.chkUseEndTime.Location = new System.Drawing.Point(14, 126);
            this.chkUseEndTime.Name = "chkUseEndTime";
            this.chkUseEndTime.Size = new System.Drawing.Size(76, 16);
            this.chkUseEndTime.TabIndex = 5;
            this.chkUseEndTime.Text = "종료 시간";
            this.chkUseEndTime.UseVisualStyleBackColor = true;
            this.chkUseEndTime.CheckedChanged += new System.EventHandler(this.chkUseEndTime_CheckedChanged);
            
            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "스케줄 유형";
            
            // cmbRunType
            this.cmbRunType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRunType.FormattingEnabled = true;
            this.cmbRunType.Items.AddRange(new object[] { "간격 (분)", "매일", "매주", "매월" });
            this.cmbRunType.Location = new System.Drawing.Point(100, 152);
            this.cmbRunType.Name = "cmbRunType";
            this.cmbRunType.Size = new System.Drawing.Size(85, 20);
            this.cmbRunType.TabIndex = 7;
            this.cmbRunType.SelectedIndexChanged += new System.EventHandler(this.cmbRunType_SelectedIndexChanged);

            // numInterval
            this.numInterval.Location = new System.Drawing.Point(191, 152);
            this.numInterval.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(60, 21);
            this.numInterval.TabIndex = 8;
            this.numInterval.Value = new decimal(new int[] { 1, 0, 0, 0 });
            
            // label6
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "(분)";
            
            // dtpRunTime
            this.dtpRunTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpRunTime.ShowUpDown = true;
            this.dtpRunTime.Location = new System.Drawing.Point(280, 152);
            this.dtpRunTime.Name = "dtpRunTime";
            this.dtpRunTime.Size = new System.Drawing.Size(100, 21);
            this.dtpRunTime.TabIndex = 11;
            
            // cmbWeeklyDay
            this.cmbWeeklyDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeeklyDay.FormattingEnabled = true;
            this.cmbWeeklyDay.Items.AddRange(new object[] { "일요일", "월요일", "화요일", "수요일", "목요일", "금요일", "토요일" });
            this.cmbWeeklyDay.Location = new System.Drawing.Point(191, 152);
            this.cmbWeeklyDay.Name = "cmbWeeklyDay";
            this.cmbWeeklyDay.Size = new System.Drawing.Size(83, 20);
            this.cmbWeeklyDay.TabIndex = 9;
            
            // numMonthlyDay
            this.numMonthlyDay.Location = new System.Drawing.Point(191, 152);
            this.numMonthlyDay.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            this.numMonthlyDay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMonthlyDay.Name = "numMonthlyDay";
            this.numMonthlyDay.Size = new System.Drawing.Size(50, 21);
            this.numMonthlyDay.TabIndex = 10;
            this.numMonthlyDay.Value = new decimal(new int[] { 1, 0, 0, 0 });
            
            // lblMonthlyDay
            this.lblMonthlyDay.AutoSize = true;
            this.lblMonthlyDay.Location = new System.Drawing.Point(245, 155);
            this.lblMonthlyDay.Name = "lblMonthlyDay";
            this.lblMonthlyDay.Size = new System.Drawing.Size(17, 12);
            this.lblMonthlyDay.TabIndex = 14;
            this.lblMonthlyDay.Text = "일";

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(100, 194);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 28);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(195, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // JobEditForm
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(395, 236);
            
            this.Controls.Add(this.dtpRunTime);
            this.Controls.Add(this.cmbWeeklyDay);
            this.Controls.Add(this.numMonthlyDay);
            this.Controls.Add(this.lblMonthlyDay);
            
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numInterval);
            this.Controls.Add(this.cmbRunType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkUseEndTime);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JobEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "작업 편집";
            
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyDay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.CheckBox chkUseEndTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRunType;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpRunTime;
        private System.Windows.Forms.ComboBox cmbWeeklyDay;
        private System.Windows.Forms.NumericUpDown numMonthlyDay;
        private System.Windows.Forms.Label lblMonthlyDay;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
