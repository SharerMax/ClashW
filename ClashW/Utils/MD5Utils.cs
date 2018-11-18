using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Utils
{
    public sealed class MD5Utils
    {
        private MD5Utils()
        {

        }

        public static string ComputeFileMD5(string filePath)
        {
            if(!File.Exists(filePath))
            {
                return string.Empty;
            }
            using (var md5 = MD5.Create())
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var currentMD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToUpperInvariant();
                    return currentMD5;
                }
            }
        }
    }
}
