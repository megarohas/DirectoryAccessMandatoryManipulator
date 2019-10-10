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
    class Explorer
    {
        static FileStream  UsrMenu(List<char> Access,ConsoleKeyInfo K, ref FileStream FS, ref List<FileStream> Fls, ref List<string> AllFilesPathes, ref List<string> AllStrings)
        {
            if(Access[0] =='1' && Access[1] == '1' && Access[2] == '1')
            { 
            Console.Clear();
            Console.WriteLine("1.   Открыть");
            Console.WriteLine("2.   Открыть для изменения");
            Console.WriteLine("3.   Удалить");
            Console.WriteLine("Esc. Выход");
            Console.WriteLine();
            bool check = true;
                while (check)
                {
                    ConsoleKeyInfo KK;
                    KK = Console.ReadKey(true);
                    if (KK.KeyChar == '1')
                    {
                        Console.Clear();
                        Read(KK,ref FS);
                        Console.Clear();
                        Console.WriteLine("1.   Открыть");
                        Console.WriteLine("2.   Открыть для изменения");
                        Console.WriteLine("3.   Удалить");
                        Console.WriteLine("Esc. Выход");
                        Console.WriteLine();
                    }
                    if (KK.KeyChar == '2')
                    {
                        Console.Clear();
                        Editor(K, ref FS);
                        Console.Clear();
                        Console.WriteLine("1.   Открыть");
                        Console.WriteLine("2.   Открыть для изменения");
                        Console.WriteLine("3.   Удалить");
                        Console.WriteLine("Esc. Выход");
                        Console.WriteLine();
                    }
                    if (KK.KeyChar == '3')
                    {
                        Console.Clear();
                        Deleter(FS, ref Fls, ref AllFilesPathes, ref AllStrings);
                        Console.Clear();
                        check = false;
                    }
                    if (KK.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        check = false;
                    }
                }
            }
            if (Access[0] == '1' && Access[1] == '1' && Access[2] == '0')
            {
                Console.Clear();
                Console.WriteLine("1.   Открыть");
                Console.WriteLine("2.   Открыть для изменения");   
                Console.WriteLine("Esc. Выход");
                Console.WriteLine();
                bool check = true;
                while (check)
                {
                    ConsoleKeyInfo KK;
                    KK = Console.ReadKey(true);
                    if (KK.KeyChar == '1')
                    {
                        Console.Clear();
                        Read(K, ref FS);
                        Console.Clear();
                        Console.WriteLine("1.   Открыть");
                        Console.WriteLine("2.   Открыть для изменения");
                        Console.WriteLine("Esc. Выход");
                        Console.WriteLine();
                    }
                    if (KK.KeyChar == '2')
                    {
                        Console.Clear();
                        Editor(K, ref FS);
                        Console.Clear();
                        Console.WriteLine("1.   Открыть");
                        Console.WriteLine("2.   Открыть для изменения");
                        Console.WriteLine("Esc. Выход");
                        Console.WriteLine();
                    }
                    if (KK.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        check = false;
                    }
                }
            }

            if (Access[0] == '1' && Access[1] == '0' && Access[2] == '0')
            {
                Console.Clear();
                Console.WriteLine("1.   Открыть");              
                Console.WriteLine("Esc. Выход");
                Console.WriteLine();
                bool check = true;
                while (check)
                {
                    ConsoleKeyInfo KK;
                    KK = Console.ReadKey(true);
                    if (KK.KeyChar == '1')
                    {
                        Console.Clear();
                        Read(K, ref FS);
                        Console.Clear();
                        Console.WriteLine("1.   Открыть");
                        Console.WriteLine("Esc. Выход");
                        Console.WriteLine();
                    }
                    if (KK.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        check = false;
                    }
                }
            }
                return FS;
        }
        static List<char>  AccessTyper(FileStream FS, string role, List<string> Opport)
        {
            string o = "";
            for (long i = 0; i < FS.Length; i++)
                o += Convert.ToChar(FS.ReadByte());
            o = o.Substring(Convert.ToInt32(FS.Length - 5), 5);
            FS.Seek(0, SeekOrigin.Begin);
            List<char> Out = new List<char> { '0','0','0'};
            if (Opport[1].Contains("r") && (o[2].ToString() == "r")) Out[0] = '1';
            if (Opport[1].Contains("c") && (o[3].ToString() == "c")) Out[1] = '1';
            if (Opport[1].Contains("d") && (o[4].ToString() == "d")) Out[2] = '1';
            return Out;
        }
        static bool        AccessDeny(FileStream FS, string role, List<string> Opport)
        {
            string o = "";
            for (long i = 0; i < FS.Length; i++)
                o += Convert.ToChar(FS.ReadByte());
            o = o.Substring(Convert.ToInt32(FS.Length - 5), 5);
            int a = Convert.ToInt32(o[1].ToString());
            int b = Convert.ToInt32(Opport[0]);
            if (Convert.ToInt32(o[1].ToString()) > Convert.ToInt32(Opport[0])) return true;
            if (!Opport[2].Contains(o[0].ToString())) return true;
            FS.Seek(0, SeekOrigin.Begin);
            return false;
        }
        static void        Read(ConsoleKeyInfo K, ref FileStream SR)
        {
            Console.Clear();
            Console.WriteLine("   Esc для закрытия");
            Console.WriteLine();
            char ch = ' ';
            string o = "";
            for (long i = 0; i < SR.Length-5; i++)
                o += Convert.ToChar(SR.ReadByte());
            SR.Seek(0, SeekOrigin.Begin);
            if (SR.Name.Substring(SR.Name.Length - 6) == "matrix") { Console.WriteLine(o.Substring(3)); }
            else
                Console.WriteLine(o);
            ConsoleKeyInfo Ke = K;
            while (Ke.Key != ConsoleKey.Escape) { Ke = Console.ReadKey(true); }
        }
        static string      Trimmer(string a)
        {
            if (a == Program.PATH)
                return a;
            if (a != "")
                while (a[a.Length - 1] != '\\')
                    a = a.Substring(0, a.Length - 1);
            a = a.Substring(0, a.Length - 1);
            return a;
        }
        static bool        Checker(string a, string b)
        {
            if (a == Trimmer(b))
                return true;
            else
                return false;
        }
        static bool        RullChecker(string str)
        {
            if(str[0] == 'p'|| str[0] == 'e'|| str[0] == 's'|| str[0] == 'a'||str[0] == 'j')
                if (str[1] == '1' || str[1] == '2' || str[1] == '3' || str[1] == '4' || str[1] == '5')
                    return true;
            return false;
        }
        static void        Ruller(ref FileStream FS)
        {
            string str = "";
            do
            {
                Console.Clear();
                Console.Write("   Введите тематику и уровень конфиденциальности: ");
                str = Console.ReadLine();
            }
            while (!RullChecker(str));
            if (str[1] == '1') str += "rcd";
            if (str[1] == '2') str += "rcd";
            if (str[1] == '3') str += "rc0";
            if (str[1] == '4') str += "rc0";
            if (str[1] == '5') str += "r00";
            byte[] strBytes = new byte[5];
            strBytes[0] = Convert.ToByte(str[0]);
            strBytes[1] = Convert.ToByte(str[1]);
            strBytes[2] = Convert.ToByte(str[2]);
            strBytes[3] = Convert.ToByte(str[3]);
            strBytes[4] = Convert.ToByte(str[4]);
            int fsl = Convert.ToInt32(FS.Length);
            if (!Program.RulledFiles.Contains(FS.Name))
                fsl += 5;
            FS.Close();
            byte[]  file = File.ReadAllBytes(FS.Name);
            if (!Program.RulledFiles.Contains(FS.Name))
                Array.Resize(ref file, file.Length + 5);
            for (int i = fsl-5; i < fsl; i++)
                file[i] = Convert.ToByte(str[i - (fsl - 5)]);
            File.WriteAllBytes(FS.Name,file);
            FS = File.Open(FS.Name, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            Program.RulledFiles.Add(FS.Name);
        }
        static void        WriteList(List<char> a)
        {
            for (int i = 0; i < a.Count; i++)
                Console.Write(a[i]);
        }
        static void        Editor(ConsoleKeyInfo K, ref FileStream SR)
        {
            string o = "";
            string ending = "";
            for (long i = 0; i < SR.Length - 5; i++)
                o += Convert.ToChar(SR.ReadByte());
            for (long i = SR.Length -5; i < SR.Length; i++)
                ending += Convert.ToChar(SR.ReadByte());
            SR.Seek(0, SeekOrigin.Begin);
            ConsoleKeyInfo Ke = K;




            List<char> Text = new List<char> { };
            Text.Add('|');
            for (int i = 0; i < o.Length; i++)
                Text.Add(o[i]);



            int CursorCount = 0;
            Console.Clear();
            Console.WriteLine("   Esc для закрытия");
            Console.WriteLine();
            WriteList(Text);
            while (Ke.Key != ConsoleKey.Escape)
            {
                ConsoleKeyInfo KeBuf = Ke;
                Ke = Console.ReadKey(true);
                if (Ke.Key == ConsoleKey.RightArrow && CursorCount <= Text.Count-2)
                {
                    CursorCount++;
                    int d = CursorCount;
                    int dd = d - 1;
                    int ddd = d + 1;
                    Text.RemoveAt(dd);
                    Text.Insert(CursorCount, '|');  
                }
                else
                if (Ke.Key == ConsoleKey.LeftArrow && CursorCount >= 1) 
                {
                    CursorCount--;
                    int d = CursorCount;
                    int dd = d - 1;
                    int ddd = d + 1;
                    Text.RemoveAt(ddd);
                    Text.Insert(CursorCount, '|');

                }
                else
                if (Ke.Key == ConsoleKey.Backspace && CursorCount >= 1 && Ke.Key != ConsoleKey.LeftArrow && Ke.Key != ConsoleKey.RightArrow)
                {
                    int d = CursorCount - 1;
                    Text.RemoveAt(d);
                    CursorCount = d;                   
                }
                else
                if(Ke.Key != ConsoleKey.Escape && Ke.Key != ConsoleKey.LeftArrow && Ke.Key != ConsoleKey.RightArrow)
                {
                    Text.Insert(CursorCount, Ke.KeyChar);
                    CursorCount++;
                }
                Console.Clear();
                Console.WriteLine("   Esc для закрытия");
                Console.WriteLine();
                WriteList(Text);
            }
            string Out = "";
            for (int i = 0; i <Text.Count; i++)
                if (i != CursorCount)
                    Out += Text[i];
            Out += ending;
            byte[] file = new byte[Out.Length];
            for (int i = 0; i < Out.Length; i++)
                file[i] = Convert.ToByte(Out[i]);
            SR.Close();
            File.WriteAllBytes(SR.Name, file);
            SR = File.Open(SR.Name, FileMode.Open, FileAccess.ReadWrite, FileShare.None); 
        }
        static void        Deleter(FileStream SR,ref List<FileStream> Fls, ref List<string> AllFilesPathes, ref List<string> AllStrings)
        {
            SR.Close();
            File.Delete(SR.Name);
            string name = SR.Name;
            Fls.RemoveAll(x => x.Name == name);
            Program.FLS.RemoveAll(x => x.Name == name);
            AllFilesPathes.RemoveAll(x => x == name);
            AllStrings.RemoveAll(x => x == name);
            Program.RulledFiles.RemoveAll(x => x == name);
        }
        public static void Journey(string pth, string role, List<string> Opport)
        {
            Console.CursorVisible = false;
            string[] AllFilesPathes2 = Directory.GetFiles(pth);
            string[] AllDirPathes2 = Directory.GetDirectories(pth);
            List<string> AllFilesPathes = AllFilesPathes2.ToList();
            List<string> AllDirPathes = AllDirPathes2.ToList();
            List<string> AllStrings = new List<string> { };
            foreach (var item in AllFilesPathes)
            {
                if(role == "su")
                    AllStrings.Add(item);
                else
                if (Program.RulledFiles.Contains(item))
                    AllStrings.Add(item);
            }
            foreach (var item in AllDirPathes)
            {
                AllStrings.Add(item);
            }
            AllStrings.Add(Trimmer(pth));
            AllStrings.Add(Program.PATH);
            List<FileStream> Fls = new List<FileStream> { };
            foreach (var item in Program.FLS)
            {
                if (Checker(pth, item.Name) && AllStrings.Contains(item.Name) && File.Exists(item.Name))
                    if (role == "su")
                        Fls.Add(item);
                    else 
                    if (!AccessDeny(item, role, Opport) && File.Exists(item.Name))
                        Fls.Add(item);
                    else AllStrings.RemoveAll(x => x == item.Name);
            }
            int counter = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("   Текущая директория: " + pth + "  Пользователь: " + role + "  '0' Для выхода из программы");
                Console.WriteLine();
                Console.WriteLine();
                for (int i = 0; i < AllStrings.Count; i++)
                {
                    if (i == counter)
                    {
                        if (i == AllStrings.Count - 2)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine(" >> Предыдущая директория");
                        }
                        else
                       if (i == AllStrings.Count - 1)
                            Console.WriteLine(" >> Домашняя директория");
                        else
                            Console.WriteLine(" >>  " + AllStrings[i]);
                    }
                    else
                    {
                        if (i == AllStrings.Count - 2)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("   Предыдущая директория");
                        }
                        else
                        if (i == AllStrings.Count - 1)
                            Console.WriteLine("   Домашняя директория");
                        else
                            Console.WriteLine(AllStrings[i]);
                    }
                }
                Console.SetCursorPosition(0, 0);
                ConsoleKeyInfo K = new ConsoleKeyInfo();
                K = Console.ReadKey(true);
                Thread.Sleep(50);
                if (K.Key == ConsoleKey.DownArrow) { if (counter < AllStrings.Count - 1) counter++; }
                if (K.Key == ConsoleKey.UpArrow) { if (counter > 0) counter--; }
                if (K.Key == ConsoleKey.Escape) { return; }
                if (K.KeyChar == '0') { throw new Exception(); }
                if (K.Key == ConsoleKey.Enter)
                {
                    if (Directory.Exists(AllStrings[counter]))
                        Explorer.Journey(AllStrings[counter], role, Opport);
                    else
                    {
                        FileStream FSS = Fls[counter];
                        if (role == "su")
                            Explorer.Ruller( ref FSS);
                        else
                            Explorer.UsrMenu(AccessTyper(FSS,role,Opport),K, ref FSS,ref Fls,ref AllDirPathes,ref AllStrings);
                        for (int i = 0; i < Fls.Count; i++)
                        {
                            if (Fls[i].Name == FSS.Name)
                                Fls[i] = FSS;
                        }
                        for (int i = 0; i < Program.FLS.Count; i++)
                        {
                            if (Program.FLS[i].Name == FSS.Name)
                                Program.FLS[i] = FSS;
                        }
                    }
                }
                else { }
            }
        }
    }
}
