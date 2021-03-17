using System;

namespace Dominio
{
	public class EventoDOM
	{
        private int _cod_evento;
        private int _cod_caracterizacao;
        private int _cod_recurso;
		private string _titulo;
        private int _cod_tipo_evento;
        private string _ementa;
        private int _cod_nucleo;
        private string _objetivos;
        private string _publico_alvo;
        private int _num_vagas;

		public EventoDOM()
		{
		}

		public EventoDOM(int codEvento)
		{
			_cod_evento = codEvento;
		}

        public EventoDOM(int cod_evento, int cod_caracterizacao, int cod_recurso, string titulo, int cod_tipo_evento, string ementa, int cod_nucleo, string objetivos, string publico_alvo, int num_vagas)
		{
            _cod_evento         = cod_evento;
            _cod_caracterizacao = cod_caracterizacao;
            _cod_recurso        = cod_recurso;
            _titulo             = titulo;
            _cod_tipo_evento    = cod_tipo_evento;
            _ementa             = ementa;
            _cod_nucleo         = cod_nucleo;
            _objetivos          = objetivos;
            _publico_alvo       = publico_alvo;
            _num_vagas          = num_vagas;
		}

		public int codEvento
		{
			get { return _cod_evento; }
			set { _cod_evento = value; }
		}

        public int codCaracterizacao
		{
			get { return _cod_caracterizacao; }
			set { _cod_caracterizacao = value; }
		}

        public int codRecurso
        {
            get { return _cod_recurso; }
            set { _cod_recurso = value; }
        }

        public string titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public int codTipoEvento
        {
            get { return _cod_tipo_evento; }
            set { _cod_tipo_evento = value; }
        }

        public string ementa
        {
            get { return _ementa; }
            set { _ementa = value; }
        }

        public int codNucleo
        {
            get { return _cod_nucleo; }
            set { _cod_nucleo = value; }
        }

        public string objetivos
        {
            get { return _objetivos; }
            set { _objetivos = value; }
        }

        public string publicoAlvo
        {
            get { return _publico_alvo; }
            set { _publico_alvo = value; }
        }

        public int numVagas
        {
            get { return _num_vagas; }
            set { _num_vagas = value; }
        }


    }
}
