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
            numInterval.Value = CurrentJob.IntervalMinutes;

            // 폼 로드 시, 체크박스 상태에 따라 종료 시간 컨트롤의 표시 여부/활성화 상태를 설정
            ToggleEndTimePicker(chkUseEndTime.Checked);
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

        // ▼▼▼ [추가] 종료 시간 컨트롤의 상태를 변경하는 헬퍼 메서드 ▼▼▼
        private void ToggleEndTimePicker(bool isEnabled)
        {
            dtpEndTime.Enabled = isEnabled;
            if (isEnabled)
            {
                // 활성화되면 날짜/시간 포맷을 다시 보여줌
                dtpEndTime.Format = DateTimePickerFormat.Custom;
                dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            }
            else
            {
                // 비활성화되면 포맷을 공백으로 변경하여 값을 숨김
                dtpEndTime.Format = DateTimePickerFormat.Custom;
                dtpEndTime.CustomFormat = " ";
            }
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
            CurrentJob.IntervalMinutes = (int)numInterval.Value;

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
