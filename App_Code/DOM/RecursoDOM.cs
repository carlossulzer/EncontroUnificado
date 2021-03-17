using System;

namespace Dominio
{
	public class RecursoDOM
	{
		private int    _cod_recurso;
		private string _descricao;

		public RecursoDOM()
		{
		}

		public RecursoDOM(int codRecurso)
		{
			_cod_recurso = codRecurso;
		}

        public RecursoDOM(int cod_recurso, string descricao)
		{
            _cod_recurso = cod_recurso;
            _descricao   = descricao;
		}

		public int codRecurso
		{
			get { return _cod_recurso; }
			set { _cod_recurso = value; }
		}

        public string descricao
		{
			get { return _descricao; }
			set { _descricao = value; }
		}

    }
}
