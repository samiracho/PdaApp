using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FriosillaPda
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            serviceInstaller1.AfterInstall += Service_AfterInstall;
        }

        private void Service_AfterInstall(object sender, InstallEventArgs e)
        {
            new ServiceController(serviceInstaller1.ServiceName).Start();
        }
    }
}
