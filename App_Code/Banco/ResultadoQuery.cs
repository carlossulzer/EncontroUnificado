using System;
using System.Data;

namespace Banco
{
	public class ResultadoQuery
	{
		private DataSet _internalDataSet = new DataSet();
		private int _currentPosition = 0;
		private DataRow _currentRow;

		public ResultadoQuery(DataSet ds)
		{
			_internalDataSet = ds;
		}

        public ResultadoQuery(DataSet ds, bool forceReadFirstLine)
		{
			_internalDataSet = ds;
			if (forceReadFirstLine)
				ReadLine();
		}

		public DataSet InternalDataSet
		{
			get { return _internalDataSet; }
		}

		public string this[string columnName]
		{
			get
			{
				object columnValue = _currentRow[columnName];
				if (columnValue != DBNull.Value)
					return columnValue.ToString();
				else
					return String.Empty;
			}
		}

		public bool ReadLine()
		{
			bool recordFound = false;
			if (_internalDataSet.Tables[0].Rows.Count - 1 >= _currentPosition)
			{
				_currentRow = _internalDataSet.Tables[0].Rows[_currentPosition];
				recordFound = true;
				_currentPosition++;
			}
			return recordFound;
		}

		public int Count
		{
			get
			{
				if (HasData())
					return _internalDataSet.Tables[0].Rows.Count;
				else
					return 0;
			}
		}

		private bool HasData()
		{
			if (_internalDataSet.Tables[0] == null)
				return false;
			else
				return _internalDataSet.Tables[0].Rows.Count > 0;
		}


	}
}
