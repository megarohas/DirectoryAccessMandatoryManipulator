using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;



namespace MBKS_Galkin_03
{
    public class Program
    {
        public static List<string>                     RulledFiles = new List<string> { };
        public static string                           ChekRuller(string name)
        {
            if (name.Substring(name.Length - 6) == "matrix") return "100";
            DateTime DC = File.GetCreationTime(name);
            DateTime DCD = DC.AddDays(1);
            DateTime DCH = DC.AddHours(1);
            if (DCD < DateTime.Now) return "100";
            if (DCD > DateTime.Now && DCH < DateTime.Now) return "110";
            else
                return "111";
        }
        public static void                             Login()
        {
            do
            {
                Console.Clear();
                Console.Write("   Введите имя пользователя: ");
                CurUser = Console.ReadLine();
            }
            while (!USRS.ContainsKey(CurUser));

            string[] AllFilesPathes = new string[0];
            bool check = true;
            while (check)
            {
                try
                {
                    Console.Clear();
                    Console.Write("   Введите путь к директории: ");
                    PATH = Console.ReadLine();
                    AllFilesPathes = Directory.GetFiles(PATH, "*", SearchOption.AllDirectories);
                    check = false;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.Write("Плохая директория!!!");
                    Console.ReadKey(true);
                }
            }

           

            for (int i = 0; i <FLS.Count; i++)
            {
                FLS[i].Close();
            }
            FLS.Clear();
                foreach (var item in AllFilesPathes)
                FLS.Add(File.Open(item, FileMode.Open, FileAccess.ReadWrite, FileShare.None));

        }
        public static List<FileStream>                 FLS = new List<FileStream> { };
        public static string                           CurUser = "";
        public static string                           PATH = ""/*"C:\\Matrix"*/;
        public static Dictionary<string, List<string>> USRS = new Dictionary<string, List<string>> { { "admin" , new List<string> { "5", "rcd", "pesaj" } },
                                                                                                     { "aduser", new List<string>    { "4", "rc", "pes" } },
                                                                                                     { "user"  , new List<string>     { "3", "rc", "pe" } },
                                                                                                     { "guest" , new List<string>       { "1", "r", "p" } },
                                                                                                     { "su"    , new List<string>                   { } } };

        static void Main(string[] args)
        {

           
            try
            {
                while (true)
                {
                    Login();
                    Explorer.Journey(PATH, CurUser, USRS[CurUser]);
                }
            }
            catch (Exception e)
            {
                for (int i = 0; i < RulledFiles.Count; i++)
                {
                    List<FileStream> A = FLS.FindAll(x => x.Name == RulledFiles[i]);
                    for (int iii = 0; iii < A.Count; iii++)
                    {
                        A[iii].Close();
                        byte[] file = File.ReadAllBytes(A[i].Name);
                        Array.Resize(ref file, file.Length - 5);
                        File.WriteAllBytes(A[i].Name, file);
                    }
                }
                return;
            }
        }
    }
}
