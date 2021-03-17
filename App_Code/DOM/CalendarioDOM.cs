using System;

namespace Dominio
{
	public class CalendarioDOM
	{
		private int _cod_sala;
        private int _cod_horario;
        private int _cod_evento;
        private DateTime _data;

		public CalendarioDOM()
		{
		}

        public CalendarioDOM(int cod_sala, int cod_horario, int cod_evento, DateTime data)
		{
            _cod_sala = cod_sala;
            _cod_horario = cod_horario;
            _cod_evento = cod_evento;
            _data = data;
		}

        public int codSala
        {
            get { return _cod_sala; }
            set { _cod_sala = value; }
        }

        public int codHorario
        {
            get { return _cod_horario; }
            set { _cod_horario = value; }
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

    }
}
