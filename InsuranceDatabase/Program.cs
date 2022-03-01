using DbUp;
using System;
using System.Configuration;
using System.Reflection;

namespace InsuranceDatabase
{
    internal class Program
    {
        /// <summary>
        /// https://dbup.readthedocs.io/en/latest/
        /// </summary>
        static int Main(string[] args)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["InsuranceConnectString"];

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(settings.ConnectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
