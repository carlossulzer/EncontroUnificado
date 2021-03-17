using System;
using DAO;
using DOM;
//using Sicof.Core.Util.Exception;

namespace SRV
{
	public class SalvarUsuario  // : Command
	{
		UsuarioDOM _usuario;
		UsuarioDAO _userDAO = new UsuarioDAO();

		public SalvarUsuario(UsuarioDOM contextUser)
		{
			_usuario = contextUser;
		}

		public UsuarioDOM Usuario
		{
			get { return _usuario; }
		}

        //internal override void Execute()
        //{
        //    if (_usuario.Login.Trim() == String.Empty)
        //        throw new CampoObrigatorioException(String.Format(MensagensExcecao.MSG_CAMPO_OBRIGATORIO,"Login"));
        //    if (_usuario.CodCargo == 0)
        //        throw new CampoObrigatorioException(String.Format(MensagensExcecao.MSG_CAMPO_OBRIGATORIO,"Cargo"));

			
        //    _userDAO.PersistirUsuario(_usuario);
        //}

	}
}
