using System;

namespace Util
{
	public class Conversor
	{
		public static int ConverterParaInteiro(object value)
		{
			if ((value != null)&&((string)value != string.Empty))
    			if ((value != null)&&(value.ToString() != string.Empty))
	    			return Convert.ToInt32(value);
		    	else
			    	return 0;
            else
                return 0;
		}

		public static decimal ConverterParaDecimal(object value)
		{
			if ((value != null)&&(value.ToString() != string.Empty))
				return Convert.ToDecimal(value);
			else
				return 0;
		}

		public static DateTime ConverterParaDateTime(object value)
		{
			if ((value != null) && (value.ToString() != string.Empty))
				return Convert.ToDateTime(value);
			else
				return DateTime.MinValue;
		}

		public static bool ConverterParaBoolean(object value)
		{
			if (value == null)
				return false;
			
			if (value.ToString().ToLower() == Boolean.FalseString.ToLower())
				return false;

			if (value.ToString().ToLower() == Boolean.TrueString.ToLower())
				return true;

			if (value.ToString() == "0")
				return false;

			if (value.ToString() == "1")
				return true;

			return false;
		}
	}
}
