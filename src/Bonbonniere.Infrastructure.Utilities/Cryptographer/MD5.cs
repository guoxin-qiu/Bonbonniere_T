using System;
using System.Text;
using SysMD5 = System.Security.Cryptography.MD5;

namespace Bonbonniere.Infrastructure.Utilities.Cryptographer
{
    public static class MD5
    {
        public static string Encrypt(string str)
        {
            return BitConverter.ToString(SysMD5.Create().ComputeHash(Encoding.Default.GetBytes(str))).Replace("-", "").ToLower();
        }
    }
}
