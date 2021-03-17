using System;

namespace Dominio
{
	public class InscricaoDOM
	{
		private int _matricula;
        private int _cod_ensino;
        private int _cod_evento;
        private int _cod_horario;
        private DateTime _data;
        private Boolean _presente;

		public InscricaoDOM()
		{
		}

        public InscricaoDOM(int matricula, int cod_ensino, int cod_evento, DateTime data, int codHorario, Boolean presente)
		{
            _matricula = matricula;
            _cod_ensino = cod_ensino;
            _cod_evento = cod_evento;
            _data = data;
            _cod_horario = codHorario;
            _presente = presente;
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

        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }

		public int codHorario
		{
			get { return _cod_horario; }
			set { _cod_horario = value; }
		}

        public Boolean presente
        {
            get { return _presente; }
            set { _presente = value; }
        }

    }
}
