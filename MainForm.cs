using System;
using System.Drawing; // Icon 클래스를 사용하기 위해 추가
using System.Linq;
using System.Reflection; // Assembly 클래스를 사용하기 위해 추가
using System.Windows.Forms;

namespace SimpleScheduler
{
    public partial class MainForm : Form
    {
        private readonly SchedulerService _schedulerService;

        public MainForm(SchedulerService schedulerService)
        {
            InitializeComponent();
            _schedulerService = schedulerService;
            _schedulerService.Logged += OnLogMessage;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // ▼▼▼ [수정] 코드로 아이콘을 로드하는 메서드 호출 ▼▼▼
            LoadIconFromResources();

            RefreshJobList();
            UpdateUIState();
        }

        // ▼▼▼ [추가] 포함된 리소스에서 아이콘을 안전하게 로드하는 메서드 ▼▼▼
        private void LoadIconFromResources()
        {
            try
            {
                // 현재 실행 중인 프로그램(Assembly)에서 리소스를 찾습니다.
                // 기본 네임스페이스.폴더명.파일명 형식으로 리소스 이름을 지정합니다.
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream("SimpleScheduler.Resources.SimpleScheduler.ico"))
                {
                    if (stream != null)
                    {
                        var appIcon = new Icon(stream);
                        this.Icon = appIcon;
                        this.notifyIcon1.Icon = appIcon;
                    }
                }
            }
            catch (Exception ex)
            {
                // 아이콘 로드 실패 시 로그를 남길 수 있습니다.
                Console.WriteLine("아이콘 로드 실패: " + ex.Message);
            }
        }

        private void RefreshJobList()
        {
            this.lvwJobs.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.lvwJobs_ItemChecked);

            Guid? selectedJobId = null;
            if (lvwJobs.SelectedItems.Count > 0)
            {
                selectedJobId = ((Job)lvwJobs.SelectedItems[0].Tag).Id;
            }

            lvwJobs.Items.Clear();
            var jobs = _schedulerService.GetJobs();
            foreach (var job in jobs.OrderBy(j => j.Name))
            {
                var item = new ListViewItem(job.Name);
                item.Checked = job.IsEnabled;
                item.SubItems.Add(job.IsEnabled ? "활성" : "비활성");

                string nextRunText = job.IsEnabled ? job.NextRunTime.ToString("yyyy-MM-dd HH:mm:ss") : "N/A";
                item.SubItems.Add(nextRunText);

                item.SubItems.Add(job.TargetPath);
                item.Tag = job;
                lvwJobs.Items.Add(item);

                if (job.Id == selectedJobId)
                {
                    item.Selected = true;
                }
            }

            this.lvwJobs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwJobs_ItemChecked);
        }

        private void UpdateUIState()
        {
            bool isSchedulerRunning = _schedulerService.IsRunning;

            btnAdd.Enabled = !isSchedulerRunning;
            btnEdit.Enabled = !isSchedulerRunning && lvwJobs.SelectedItems.Count > 0;
            btnDelete.Enabled = !isSchedulerRunning && lvwJobs.SelectedItems.Count > 0;
            lvwJobs.Enabled = !isSchedulerRunning;

            bool anyJobChecked = lvwJobs.Items.Cast<ListViewItem>().Any(item => item.Checked);
            btnStartScheduler.Enabled = !isSchedulerRunning && anyJobChecked;

            btnStopScheduler.Enabled = isSchedulerRunning;

            btnExitApp.Enabled = !isSchedulerRunning;
        }

        private void OnLogMessage(string message)
        {
            if (rtbLogs.InvokeRequired)
            {
                rtbLogs.Invoke(new Action(() => OnLogMessage(message)));
                return;
            }
            rtbLogs.AppendText(message + Environment.NewLine);
            rtbLogs.ScrollToCaret();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new JobEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _schedulerService.AddOrUpdateJob(form.CurrentJob);
                    RefreshJobList();
                    UpdateUIState();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvwJobs.SelectedItems.Count > 0)
            {
                var selectedJob = (Job)lvwJobs.SelectedItems[0].Tag;
                using (var form = new JobEditForm(selectedJob))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _schedulerService.AddOrUpdateJob(form.CurrentJob);
                        RefreshJobList();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwJobs.SelectedItems.Count > 0)
            {
                var selectedJob = (Job)lvwJobs.SelectedItems[0].Tag;
                if (MessageBox.Show($"'{selectedJob.Name}' 작업을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _schedulerService.DeleteJob(selectedJob.Id);
                    RefreshJobList();
                    UpdateUIState();
                }
            }
        }

        private void lvwJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        private void lvwJobs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var job = (Job)e.Item.Tag;
            if (job != null)
            {
                _schedulerService.UpdateJobEnabledStatus(job.Id, e.Item.Checked);
                e.Item.SubItems[1].Text = e.Item.Checked ? "활성" : "비활성";
                UpdateUIState();
            }
        }

        private void btnStartScheduler_Click(object sender, EventArgs e)
        {
            _schedulerService.Start();
            UpdateUIState();
            RefreshJobList();
        }

        private void btnStopScheduler_Click(object sender, EventArgs e)
        {
            _schedulerService.Stop();
            UpdateUIState();
        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("프로그램을 완전히 종료하시겠습니까?", "종료 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                notifyIcon1.Visible = false;
                Application.Exit();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                notifyIcon1.ShowBalloonTip(1000, "Simple Scheduler", "스케줄러가 백그라운드에서 실행 중입니다.", ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ctxMenuShow_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ctxMenuExit_Click(object sender, EventArgs e)
        {
            _schedulerService.Stop();
            notifyIcon1.Visible = false;
            Application.Exit();
        }
    }
}
