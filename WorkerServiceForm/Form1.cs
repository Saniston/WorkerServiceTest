using WorkerServiceForm.Installers;

namespace WorkerServiceForm
{
    public partial class Form1 : Form
    {
        private readonly string _serviceName;
        private readonly string _executablePath;


        public Form1()
        {
            InitializeComponent();
            _serviceName = "WorkerSample2";
            _executablePath = @"C:\publish\WorkerServiceTest.exe";
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceInstaller.InstallService(_serviceName, _executablePath);
            }
            catch (Exception)
            {

            }
            
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceInstaller.UninstallService(_serviceName);
            }
            catch (Exception)
            {

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                var isRunning = ServiceInstaller.IsRunning(_serviceName);
                if (!isRunning)
                    ServiceInstaller.StartService(_serviceName);
                else
                    ServiceInstaller.StopService(_serviceName);
            }
            catch (Exception)
            {

            }
        }
    }
}