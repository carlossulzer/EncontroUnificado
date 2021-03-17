using System.Globalization;
using System;
namespace Util
{
	public class StringSuporte
	{
		public static string Plic(string argumento)
		{
			return "'" + argumento.Replace("'", "''") + "'";
		}

        //public static string FormataDataBanco(string argumento)
        //{
        //    return "Convert(DATETIME," + StringSuporte.Formatar(argumento) + ",20)";

        //}
        
        public static string Completar(string argumento, int tamanho, string preenchimento)
        { 
            int cont = tamanho - argumento.Length;

            for (int i = 1; i <= cont; i++)
            {
                argumento = argumento + preenchimento;
                
            }
            return argumento;
        }


        public static string Formatar(object argumento)
        {
            string tipo = argumento.GetType().ToString();
            string itemFormat = string.Empty;

            switch (tipo)
            {
                case "System.String":
                    {
                        string var = (string)argumento;
                        itemFormat = "'" + var.Replace("'", "''") + "'";
                        return itemFormat;
                    }
                case "System.Boolean":
                    {
                        if (bool.Parse(argumento.ToString()))
                            itemFormat = "1";
                        else
                            itemFormat = "0";

                        return itemFormat;
                    }
                case "System.DateTime":
                    {
                        DateTime data = (DateTime)argumento;

                        itemFormat = "Convert(DATETIME,'" + data.ToString("yyyy-MM-dd hh:mm:ss") + "',20)";
                        return itemFormat;
                    }
                case "System.Text.StringBuilder":
                    {
                        itemFormat = "'" + argumento.ToString().Replace("'", "''") + "'";
                        return itemFormat;
                    }
                case "System.Decimal":
                    {
                        string var = (string)argumento;
                        itemFormat = var.Replace(",", ".");
                        return itemFormat;
                    }
                default:
                    {
                        itemFormat = argumento.ToString();
                        return itemFormat;
                    }

            }
        }

	}





}
