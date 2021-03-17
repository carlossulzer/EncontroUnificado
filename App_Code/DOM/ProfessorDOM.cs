using System;

namespace Dominio
{
	public class ProfessorDOM
	{
		private int    _cod_professor;
		private int    _matricula;
        private string _nome;
        private string _telefone1;
        private string _telefone2;
        private string _email;

		public ProfessorDOM()
		{
		}

		public ProfessorDOM(int codProfessor)
		{
            _cod_professor = codProfessor;
		}

        public ProfessorDOM(int cod_professor, int matricula, string nome, string telefone1, string telefone2, string e_mail)
		{
            _cod_professor = cod_professor;
            _matricula = matricula;
            _nome = nome;
            _telefone1 = telefone1;
            _telefone2 = telefone2;
            _email = e_mail;
		}

		public int codProfessor
		{
			get { return _cod_professor; }
			set { _cod_professor = value; }
		}

        public int matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
        }

        public string nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

        public string telefone1
        {
            get { return _telefone1; }
            set { _telefone1 = value; }
        }

        public string telefone2
        {
            get { return _telefone2; }
            set { _telefone2 = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

    }
}
