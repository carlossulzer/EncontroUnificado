using System;

namespace Dominio
{
	public class SalaDOM
	{
		private int    _cod_sala;
		private string _descricao;
        private string _andar;
        private string _bloco;

		public SalaDOM()
		{
		}

		public SalaDOM(int codSala)
		{
			_cod_sala = codSala;
		}

        public SalaDOM(int cod_sala, string descricao, string andar, string bloco)
		{
            _cod_sala   = cod_sala;
            _descricao  = descricao;
            _andar = andar;
            _bloco = bloco;
		}

		public int codSala
		{
			get { return _cod_sala; }
			set { _cod_sala = value; }
		}

        public string descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}

        public string andar
        {
            get { return _andar; }
            set { _andar = value; }
        }

        public string bloco
        {
            get { return _bloco; }
            set { _bloco = value; }
        }


    }
}
