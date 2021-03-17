using DIC;
using DOM;
using DAO;

namespace Util
{
	public class ObterUsuario //: Command
	{
		private readonly string _login;
		private UsuarioDOM _user;
		private int _id;

		public ObterUsuario(string login)
		{
			_login = login;
		}
		
		public ObterUsuario(int id)
		{
			_id = id;
		}

		public UsuarioDOM Usuario
		{
			get { return _user; }
		}

		internal void Execute()
		{
			UsuarioDAO userDAO = new UsuarioDAO();
			
			if (_id > 0)
				_user = userDAO.ObterUsuarioPeloId(_id);
			else
				_user = userDAO.ObterUsuarioPeloLogin(_login);
		}

	}
}
