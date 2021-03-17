using System;

namespace Dominio
{
	public class VoluntarioDOM
	{
		private int _matricula;
        private int _cod_ensino;
        private int _cod_evento;
        private int _cod_horario;
        private int _cod_sala;
        private DateTime _data;

		public VoluntarioDOM()
		{
		}

        public VoluntarioDOM(int matricula, int cod_ensino, int cod_evento, int cod_horario, int cod_sala, DateTime data)
		{
            _matricula = matricula;
            _cod_ensino = cod_ensino;
            _cod_evento = cod_evento;
            _cod_horario = cod_horario;
            _cod_sala = cod_sala;
            _data = data;
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

        public int codHorario
        {
            get { return _cod_horario; }
            set { _cod_horario = value; }
        }

        public int codSala
        {
            get { return _cod_sala; }
            set { _cod_sala = value; }
        }


        public DateTime data
        {
            get { return _data; }
            set { _data = value; }
        }


    }
}
