// JobEditForm.cs
using System;
using System.IO;
using System.Windows.Forms;

namespace SimpleScheduler
{
    public partial class JobEditForm : Form
    {
        public Job CurrentJob { get; private set; }

        public JobEditForm(Job job = null)
        {
            InitializeComponent();
            CurrentJob = job ?? new Job();
            if (job == null) CurrentJob.IsEnabled = true;
            LoadJobData();
        }

        private void LoadJobData()
        {
            txtName.Text = CurrentJob.Name;
            txtTargetPath.Text = CurrentJob.TargetPath;
            txtArguments.Text = CurrentJob.Arguments;
            dtpStartTime.Value = CurrentJob.StartTime;
            chkUseEndTime.Checked = CurrentJob.EndTime.HasValue;
            dtpEndTime.Value = CurrentJob.EndTime ?? DateTime.Now.AddDays(1);

            // 데이터 바인딩
            cmbRunType.SelectedIndex = (int)CurrentJob.RunType;
            numInterval.Value = CurrentJob.IntervalMinutes;
            dtpRunTime.Value = CurrentJob.DailyRunTime;
            cmbWeeklyDay.SelectedIndex = (int)CurrentJob.WeeklyRunDay; // 0: 일요일 ~ 6: 토요일
            numMonthlyDay.Value = CurrentJob.MonthlyRunDay;

            ToggleEndTimePicker(chkUseEndTime.Checked);
            UpdateScheduleUI();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "실행 파일 (*.exe;*.bat;*.ps1)|*.exe;*.bat;*.ps1|모든 파일 (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtTargetPath.Text = ofd.FileName;
                }
            }
        }

        private void chkUseEndTime_CheckedChanged(object sender, EventArgs e)
        {
            ToggleEndTimePicker(chkUseEndTime.Checked);
        }

        private void cmbRunType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateScheduleUI();
        }

        private void ToggleEndTimePicker(bool isEnabled)
        {
            dtpEndTime.Enabled = isEnabled;
            if (isEnabled)
            {
                dtpEndTime.Format = DateTimePickerFormat.Custom;
                dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            }
            else
            {
                dtpEndTime.Format = DateTimePickerFormat.Custom;
                dtpEndTime.CustomFormat = " ";
            }
        }

        private void UpdateScheduleUI()
        {
            var type = (ScheduleType)cmbRunType.SelectedIndex;

            // 간격
            numInterval.Visible = type == ScheduleType.Interval;
            label6.Visible = type == ScheduleType.Interval;

            // 매일, 매주, 매월의 지정 시간
            dtpRunTime.Visible = type != ScheduleType.Interval;

            // 매주 (요일 선택)
            cmbWeeklyDay.Visible = type == ScheduleType.Weekly;

            // 매월 (일자 선택)
            numMonthlyDay.Visible = type == ScheduleType.Monthly;
            lblMonthlyDay.Visible = type == ScheduleType.Monthly;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtTargetPath.Text))
            {
                MessageBox.Show("작업 이름과 실행 대상은 필수 항목입니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txtTargetPath.Text))
            {
                MessageBox.Show("실행 대상 파일이 존재하지 않습니다.", "파일 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CurrentJob.Name = txtName.Text;
            CurrentJob.TargetPath = txtTargetPath.Text;
            CurrentJob.Arguments = txtArguments.Text;
            CurrentJob.StartTime = dtpStartTime.Value;
            CurrentJob.EndTime = chkUseEndTime.Checked ? (DateTime?)dtpEndTime.Value : null;

            CurrentJob.RunType = (ScheduleType)cmbRunType.SelectedIndex;
            CurrentJob.IntervalMinutes = (int)numInterval.Value;
            CurrentJob.DailyRunTime = dtpRunTime.Value;
            CurrentJob.WeeklyRunDay = (DayOfWeek)cmbWeeklyDay.SelectedIndex;
            CurrentJob.MonthlyRunDay = (int)numMonthlyDay.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
