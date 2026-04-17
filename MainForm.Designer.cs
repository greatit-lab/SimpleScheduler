// MainForm.Designer.cs
namespace SimpleScheduler
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvwJobs = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNextRun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();

            // ▼ [추가] 불러오기, 내보내기 버튼 선언
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();

            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartScheduler = new System.Windows.Forms.Button();
            this.btnStopScheduler = new System.Windows.Forms.Button();
            this.btnExitApp = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwJobs
            // 
            this.lvwJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwJobs.CheckBoxes = true;
            this.lvwJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colStatus,
            this.colNextRun,
            this.colPath});
            this.lvwJobs.FullRowSelect = true;
            this.lvwJobs.HideSelection = false;
            this.lvwJobs.Location = new System.Drawing.Point(12, 25);
            this.lvwJobs.MultiSelect = false;
            this.lvwJobs.Name = "lvwJobs";
            this.lvwJobs.Size = new System.Drawing.Size(760, 200);
            this.lvwJobs.TabIndex = 0;
            this.lvwJobs.UseCompatibleStateImageBehavior = false;
            this.lvwJobs.View = System.Windows.Forms.View.Details;
            this.lvwJobs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwJobs_ItemChecked);
            this.lvwJobs.SelectedIndexChanged += new System.EventHandler(this.lvwJobs_SelectedIndexChanged);
            // 
            // colName
            // 
            this.colName.Text = "작업 이름";
            this.colName.Width = 150;
            // 
            // colStatus
            // 
            this.colStatus.Text = "상태";
            this.colStatus.Width = 80;
            // 
            // colNextRun
            // 
            this.colNextRun.Text = "다음 실행 시간";
            this.colNextRun.Width = 140;
            // 
            // colPath
            // 
            this.colPath.Text = "실행 대상 경로";
            this.colPath.Width = 350;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(12, 231);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(93, 231);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "수정";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(174, 231);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // ▼ [추가] btnImport (목록 불러오기) 설정
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(265, 231);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(95, 23);
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "목록 불러오기";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            // ▼ [추가] btnExport (목록 내보내기) 설정
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(366, 231);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(95, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "목록 내보내기";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // 
            // rtbLogs
            // 
            this.rtbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLogs.Location = new System.Drawing.Point(12, 280);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.Size = new System.Drawing.Size(760, 169);
            this.rtbLogs.TabIndex = 4;
            this.rtbLogs.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "작업 목록";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "실행 로그";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "Simple Scheduler";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuShow,
            this.toolStripSeparator1,
            this.ctxMenuExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 54);
            // 
            // ctxMenuShow
            // 
            this.ctxMenuShow.Name = "ctxMenuShow";
            this.ctxMenuShow.Size = new System.Drawing.Size(122, 22);
            this.ctxMenuShow.Text = "열기";
            this.ctxMenuShow.Click += new System.EventHandler(this.ctxMenuShow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // ctxMenuExit
            // 
            this.ctxMenuExit.Name = "ctxMenuExit";
            this.ctxMenuExit.Size = new System.Drawing.Size(122, 22);
            this.ctxMenuExit.Text = "종료";
            this.ctxMenuExit.Click += new System.EventHandler(this.ctxMenuExit_Click);
            // 
            // btnStartScheduler
            // 
            this.btnStartScheduler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartScheduler.Location = new System.Drawing.Point(492, 231);
            this.btnStartScheduler.Name = "btnStartScheduler";
            this.btnStartScheduler.Size = new System.Drawing.Size(91, 23);
            this.btnStartScheduler.TabIndex = 7;
            this.btnStartScheduler.Text = "스케줄러 시작";
            this.btnStartScheduler.UseVisualStyleBackColor = true;
            this.btnStartScheduler.Click += new System.EventHandler(this.btnStartScheduler_Click);
            // 
            // btnStopScheduler
            // 
            this.btnStopScheduler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopScheduler.Location = new System.Drawing.Point(589, 231);
            this.btnStopScheduler.Name = "btnStopScheduler";
            this.btnStopScheduler.Size = new System.Drawing.Size(88, 23);
            this.btnStopScheduler.TabIndex = 8;
            this.btnStopScheduler.Text = "스케줄러 중단";
            this.btnStopScheduler.UseVisualStyleBackColor = true;
            this.btnStopScheduler.Click += new System.EventHandler(this.btnStopScheduler_Click);
            // 
            // btnExitApp
            // 
            this.btnExitApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExitApp.Location = new System.Drawing.Point(683, 231);
            this.btnExitApp.Name = "btnExitApp";
            this.btnExitApp.Size = new System.Drawing.Size(89, 23);
            this.btnExitApp.TabIndex = 9;
            this.btnExitApp.Text = "프로그램 종료";
            this.btnExitApp.UseVisualStyleBackColor = true;
            this.btnExitApp.Click += new System.EventHandler(this.btnExitApp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnExitApp);
            this.Controls.Add(this.btnStopScheduler);
            this.Controls.Add(this.btnStartScheduler);

            // ▼ [추가] 폼 컨트롤 컬렉션에 등록
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);

            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbLogs);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lvwJobs);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "Simple Scheduler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ListView lvwJobs;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        // ▼ [추가] 불러오기, 내보내기 변수 선언
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colNextRun;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuExit;
        private System.Windows.Forms.Button btnStartScheduler;
        private System.Windows.Forms.Button btnStopScheduler;
        private System.Windows.Forms.Button btnExitApp;
    }
}
