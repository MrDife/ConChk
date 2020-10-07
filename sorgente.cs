using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;

namespace ChkCon2
{
    class Program
    {
        static Ping pinger = new Ping();
        static DateTime now;
        static PingReply esito;
        static string finalRes;
        static void provaPing(string s)
        {
            esito = pinger.Send(s, 1000);
            if (esito.Status != IPStatus.Success)
            {
                now = DateTime.Now;
                finalRes += ("\n" + $"{s} - {now.ToString("MM/dd/yyyy HH:mm:ss")}");
            }
        }
        static void Main(string[] args)
        {
            string[] servers = { "8.8.8.8", "1.1.1.1", "8.8.4.4", "4.2.2.2" };
            bool flag = true;
            string dove = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            StreamWriter file = new StreamWriter(dove + @"\output.txt");
            Console.WriteLine("Controllo iniziato\nTroverai l'output sul desktop\nPremi qualsiasi tasto per uscire");

            while (flag = !Console.KeyAvailable)
            {
                foreach (string s in servers)
                {
                    if (flag = !Console.KeyAvailable)
                    {
                        Thread.Sleep(2000);
                        provaPing(s);
                    }
                    else break;
                }
            }

            file.Flush();
            file.WriteLine("Non sono stato in grado di raggiungere :");
            file.WriteLine(finalRes);
            file.Close();

        }
    }
}