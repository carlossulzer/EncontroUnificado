using System;

namespace Dominio
{
	public class IntegranteDOM
	{
		private int _matricula;
        private int _cod_ensino;
        private int _cod_evento;

		public IntegranteDOM()
		{
		}

        public IntegranteDOM(int matricula, int cod_ensino, int cod_evento)
		{
            _matricula = matricula;
            _cod_ensino = cod_ensino;
            _cod_evento = cod_evento;
		}

        public int matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
        }

        public int codEnsino
        {
            get { return _cod_ensino; }
            set { _cod_ensino = value; }
        }

		public int codEvento
		{
			get { return _cod_evento; }
			set { _cod_evento = value; }
		}
    }
}
