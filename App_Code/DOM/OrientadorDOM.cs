using System;

namespace Dominio
{
	public class OrientadorDOM
	{
		private int _cod_evento;
        private int _cod_professor;

		public OrientadorDOM()
		{
		}

        public OrientadorDOM(int cod_evento, int cod_professor)
		{
            _cod_evento = cod_evento;
            _cod_professor = cod_professor;
		}

		public int codEvento
		{
			get { return _cod_evento; }
			set { _cod_evento = value; }
		}

        public int codProfessor
		{
			get { return _cod_professor; }
			set { _cod_professor = value; }
		}

    }
}
