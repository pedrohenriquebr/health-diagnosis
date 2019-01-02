using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Util
{
    class Data
    {
        public static bool nascimento(DateTime nasc)
        {
            try
            {
                int dias = (int)(DateTime.Now - nasc).TotalDays;
                if (dias > 4700 && dias < 40000)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool saida(DateTime entrada, DateTime saida)
        {
            try
            {
                if ((saida - entrada).TotalDays < 0 || (DateTime.Now - saida).TotalDays < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool consulta(DateTime consulta)
        {
            try
            {
                int dias = (int) (DateTime.Now - consulta).TotalDays;
                if (dias <= 15 && dias >= -15)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}