using System;

namespace Dominio
{
	public class NucleoDOM
	{
		private int    _cod_nucleo;
		private string _descricao;

		public NucleoDOM()
		{
		}

		public NucleoDOM(int codNucleo)
		{
			_cod_nucleo = codNucleo;
		}

        public NucleoDOM(int cod_nucleo, string descricao)
		{
            _cod_nucleo = cod_nucleo;
            _descricao  = descricao;
		}

		public int codNucleo
		{
			get { return _cod_nucleo; }
			set { _cod_nucleo = value; }
		}

        public string descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}	
	
    }
}
