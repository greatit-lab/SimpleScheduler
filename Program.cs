using System;
using System.Linq;
using System.Windows.Forms;

namespace SimpleScheduler
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ▼▼▼ [수정] SchedulerService 인스턴스를 먼저 생성 ▼▼▼
            var schedulerService = new SchedulerService();

            if (schedulerService.GetJobs().Count == 0)
            {
                MessageBox.Show("등록된 작업이 없습니다. 첫 번째 작업을 추가합니다.", "환영합니다", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (var form = new JobEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        schedulerService.AddOrUpdateJob(form.CurrentJob);
                        // ▼▼▼ [수정] 생성된 서비스를 MainForm에 전달 ▼▼▼
                        Application.Run(new MainForm(schedulerService));
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                // ▼▼▼ [수정] 생성된 서비스를 MainForm에 전달 ▼▼▼
                Application.Run(new MainForm(schedulerService));
            }
        }
    }
}
