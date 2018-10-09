using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Extensions
{
    public class StringExtensions
    {
        public string Capitalize(string inputString)
        {
            var firstLetter = inputString[0].ToString().ToUpper();
            var allLettersExeptFirst = inputString.Substring(1).ToLower();

            var result = firstLetter + allLettersExeptFirst;

            return result;
        }
    }
}
