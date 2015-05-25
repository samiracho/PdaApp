using System.ServiceProcess;

namespace FriosillaPda
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new FriosillaPdaService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
