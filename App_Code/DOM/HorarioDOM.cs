using System;

namespace Dominio
{
	public class HorarioDOM
	{
		private int    _cod_horario;
		private string _hora_inicial;
		private string _hora_final;

		public HorarioDOM()
		{
		}

		public HorarioDOM(int codHorario)
		{
			_cod_horario = codHorario;
		}

        public HorarioDOM(int cod_horario, string hora_inicial, string hora_final)
		{
            _cod_horario  = cod_horario;
            _hora_inicial = hora_inicial;
            _hora_final   = hora_final;
        }

		public int codHorario
		{
			get { return _cod_horario; }
			set { _cod_horario = value; }
		}

        public string horaInicial
        {
            get { return _hora_inicial; }
            set { _hora_inicial = value; }
        }

        public string horaFinal
        {
            get { return _hora_final; }
            set { _hora_final = value; }
        }
    }
}
