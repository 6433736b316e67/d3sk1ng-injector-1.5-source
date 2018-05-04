namespace DLLInjection.Gui
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class IniFile
    {
        private string Path;
        private string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        public IniFile(string IniPath = null)
        {
            if (IniPath == null)
            {
            }
            this.Path = new FileInfo(this.EXE + ".ini").FullName.ToString();
        }

        public void DeleteKey(string Key, string Section = null)
        {
            if (Section == null)
            {
            }
            this.Write(Key, null, this.EXE);
        }

        public void DeleteSection(string Section = null)
        {
            if (Section == null)
            {
            }
            this.Write(null, null, this.EXE);
        }

        [DllImport("kernel32", CharSet=CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        public bool KeyExists(string Key, string Section = null) => 
            (this.Read(Key, Section).Length > 0);

        public string Read(string Key, string Section = null)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            if (Section == null)
            {
            }
            GetPrivateProfileString(this.EXE, Key, "", retVal, 0xff, this.Path);
            return retVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            if (Section == null)
            {
            }
            WritePrivateProfileString(this.EXE, Key, Value, this.Path);
        }

        [DllImport("kernel32", CharSet=CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
    }
}

