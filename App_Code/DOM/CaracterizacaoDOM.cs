using System;

namespace Dominio
{
	public class CaracterizacaoDOM
	{
		private int    _cod_caracterizacao;
		private string _descricao;

		public CaracterizacaoDOM()
		{
		}

		public CaracterizacaoDOM(int codCaracterizacao)
		{
			_cod_caracterizacao = codCaracterizacao;
		}

        public CaracterizacaoDOM(int cod_caracterizacao, string descricao)
		{
            _cod_caracterizacao = cod_caracterizacao;
            _descricao          = descricao;
		}

		public int codCaracterizacao
		{
			get { return _cod_caracterizacao; }
			set { _cod_caracterizacao = value; }
		}

        public string descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}

    }
}
