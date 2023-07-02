using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Diagnostics;

namespace WorkerServiceForm.Installers
{

    public class ServiceInstaller
    {
        static int _timeoutMilliseconds = 5000;

        public static bool IsRunning(string serviceName)
        {
            try
            {
                var controle = new ServiceController(serviceName);
                return controle.Status == ServiceControllerStatus.StartPending || controle.Status == ServiceControllerStatus.Running;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static void StartService(string serviceName)
        {
            try
            {
                var srcController = new ServiceController(serviceName);
                srcController.Start();
                srcController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(_timeoutMilliseconds));
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"Erro ao iniciar o serviço [{serviceName}].\n{ex.InnerException}", ex);
                throw;
            }
        }

        public static void StopService(string serviceName)
        {
            var srcController = new ServiceController(serviceName);
            srcController.Stop();
            srcController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMilliseconds(_timeoutMilliseconds));
        }

        public static void InstallService(string serviceName, string executablePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                InstallServiceWindows(serviceName, executablePath);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                InstallServiceLinux(serviceName, executablePath);
            }
            else
            {
                throw new NotSupportedException("Unsupported operating system.");
            }
        }

        private static void InstallServiceWindows(string serviceName, string executablePath)
        {
            string arguments = $"create {serviceName} binPath= \"{executablePath}\" start=demand";

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "sc";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Failed to install the service. Output: {output}");
                }
            }
        }

        private static void InstallServiceLinux(string serviceName, string executablePath)
        {
            string serviceUnitFilePath = $"/etc/systemd/system/{serviceName}.service";

            string serviceUnitFileContent = $@"
[Unit]
Description={serviceName}

[Service]
ExecStart={executablePath}
Restart=always

[Install]
WantedBy=multi-user.target
";

            File.WriteAllText(serviceUnitFilePath, serviceUnitFileContent);

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "systemctl";
                process.StartInfo.Arguments = $"daemon-reload";
                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Failed to reload the Systemd configuration.");
                }
            }
        }

        public static void UninstallService(string serviceName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                UninstallServiceWindows(serviceName);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                UninstallServiceLinux(serviceName);
            }
            else
            {
                throw new NotSupportedException("Unsupported operating system.");
            }
        }

        private static void UninstallServiceWindows(string serviceName)
        {
            string arguments = $"delete {serviceName}";

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "sc";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Failed to uninstall the service. Output: {output}");
                }
            }
        }

        private static void UninstallServiceLinux(string serviceName)
        {
            string serviceUnitFilePath = $"/etc/systemd/system/{serviceName}.service";

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "systemctl";
                process.StartInfo.Arguments = $"stop {serviceName}";
                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Failed to stop the service.");
                }
            }

            if (File.Exists(serviceUnitFilePath))
            {
                File.Delete(serviceUnitFilePath);
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "systemctl";
                process.StartInfo.Arguments = $"daemon-reload";
                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Failed to reload the Systemd configuration.");
                }
            }
        }
    }
}