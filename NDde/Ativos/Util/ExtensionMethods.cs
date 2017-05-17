using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDde.Ativos.Util
{
    /// <summary>
    /// Classe de métodos de extensão para auxílio
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Busca um valor decimal na string
        /// </summary>
        /// <param name="valor">Valor a ser convertido</param>
        /// <returns>Retorna o valor convertido ou 0.</returns>
        public static decimal GetDecimalValue(this string valor)
        {
            decimal valorConvertido = 0;

            if (Decimal.TryParse(valor, out valorConvertido))
            {
                return valorConvertido;
            }

            return 0;
        }

        /// <summary>
        /// Transforma em maiúsculo o primeiro caracter de uma string 
        /// </summary>
        /// <param name="input">String a ser modificada</param>
        /// <returns>Retorna a string modificada</returns>
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return string.Empty;
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        /// <summary>
        /// Busca um valor DateTime em uma string
        /// </summary>
        /// <param name="time">Valor a ser convertido</param>
        /// <returns>Retorna o valor convertido ou 01/01/0001.</returns>
        public static DateTime GetDateTimeFromTimeValue(this string time)
        {
            DateTime valorConvertido;

            if (DateTime.TryParse(string.Format("{0} {1}", DateTime.Now.Date.ToString("dd/MM/yyyy"), time.Trim()), new CultureInfo("pt-BR"), DateTimeStyles.None, out valorConvertido))
            {
                return valorConvertido;
            }

            return new DateTime();
        }

    }
}
