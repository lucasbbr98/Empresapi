﻿using System;

namespace Utilities
{
    public static class Formatter
    {
        public static string CNPJ(string cnpj)
        {
            try
            {
                return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            catch
            {
                return cnpj;
            }
        }
    }
}
