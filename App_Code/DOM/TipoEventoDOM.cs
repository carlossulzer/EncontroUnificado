using System;

namespace Dominio
{
	public class TipoEventoDOM
	{
		private int    _cod_tipo_evento;
		private string _descricao;

		public TipoEventoDOM()
		{
		}

		public TipoEventoDOM(int codTipoEvento)
		{
			_cod_tipo_evento = codTipoEvento;
		}

        public TipoEventoDOM(int cod_tipo_evento, string descricao)
		{
            _cod_tipo_evento = cod_tipo_evento;
            _descricao       = descricao;
		}

		public int codTipoEvento
		{
			get { return _cod_tipo_evento; }
			set { _cod_tipo_evento = value; }
		}

        public string descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}

    }
}
