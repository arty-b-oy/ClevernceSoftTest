using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverenceSoftTest
{
    public class StringСonversion
    {
        public StringСonversion() { }
        
        public string ConvertString(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; )
            {
                char symbol = str[i];
                int counter = 0;
                while(i < str.Length && symbol == str[i])
                {
                    counter++;
                    i++;
                }
                result += symbol;
                if (counter > 1)
                    result += counter.ToString();
            }
            return result;
        }

    }
}
