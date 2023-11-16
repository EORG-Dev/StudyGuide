using System;
using System.Collections.Generic;
using System.Text;

namespace StudyGuide.Services
{
    public static class SRV_Random
    {
        public static string RandomString(int length)
        {
            string result = "";
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                int cc = (int)r.Next(65, 90);
                result += (char)cc;
            }
            return result;
        }
    }
}
