using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SSMSHelper.Common
{
    public class IniHelper
    {
        private readonly string _path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public IniHelper(string path)
        {
            _path = path;
        }

        public void Write(string section, string key, string value)
        {
            if (!System.IO.File.Exists(_path))
            {
                throw new FileNotFoundException($"未找到文件：{_path}");
            }
            WritePrivateProfileString(section, key, value, _path);
        }

        public string Read(string section, string key)
        {
            if (!System.IO.File.Exists(_path))
            {
                throw new FileNotFoundException($"未找到文件：{_path}");
            }
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, _path);
            return temp.ToString();
        }
    }
}
