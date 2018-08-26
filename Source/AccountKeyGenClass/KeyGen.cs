using System;
using System.Linq;

namespace AccountKeyGenClass
{
    public class KeyGen
    {
        private static Random RND = new Random();

        public static string Key(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[RND.Next(s.Length)]).ToArray());
        }
    }
}
