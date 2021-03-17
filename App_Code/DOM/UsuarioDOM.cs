namespace DOM
{
	public class UsuarioDOM
	{
		private int _idUsuario;
		private string _login;
		private string _senha;
		private string _nomeCompleto;
		private string _email;
		private bool _bloqueado = false;
		private bool _obrigarTrocadeSenha = false;
		private int _codcargo=0;

		public UsuarioDOM()
		{
		}

		public UsuarioDOM(string login)
		{
			_login = login;
		}

		public UsuarioDOM(int idUsuario, string login, string senha, string nomeCompleto, string email, bool bloqueado, bool obrigarTrocadeSenha)
		{
			_idUsuario = idUsuario;
			_login = login;
			_senha = senha;
			_nomeCompleto = nomeCompleto;
			_email = email;
			_bloqueado = bloqueado;
			_obrigarTrocadeSenha = obrigarTrocadeSenha;
		}

		public int IdUsuario
		{
			get { return _idUsuario; }
			set { _idUsuario = value; }
		}

		public string Login
		{
			get { return _login; }
			set { _login = value; }
		}

		public string Senha
		{
			get { return _senha; }
			set { _senha = value; }
		}

		public string NomeCompleto
		{
			get { return _nomeCompleto; }
			set { _nomeCompleto = value; }
		}

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		public bool Bloqueado
		{
			get { return _bloqueado; }
			set { _bloqueado = value; }
		}

		public bool ObrigarTrocaDeSenha
		{
			get { return _obrigarTrocadeSenha; }
			set { _obrigarTrocadeSenha = value; }
		}

		public override string ToString()
		{
			return _login;
		}
	}
}
