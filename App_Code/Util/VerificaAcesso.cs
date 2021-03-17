using System;
using System.Configuration;
using DOM;
using SRV;
using Banco;


namespace Util
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class VerificaAcesso
	{
		private string _mensagemdeRetorno;
		private bool _trocaSenha;
		private string _usuarioLogado;
		private string _tipodeUsuario;
		private int _foco;

		public VerificaAcesso()
		{
		}

		public bool Verifica_Senha(string login, string senha, string senhaNova, string senhaConf, bool verificarTroca)
		{
			string _usuario_adm = ConfigurationManager.AppSettings["usuario"];
			string _senha_adm   = ConfigurationManager.AppSettings["senha"];

			string _usuarioDigitado = Criptografia.encriptar(login);
			string _senhaDigitada   = Criptografia.encriptar(senha);

			_trocaSenha = false;

			if (_usuario_adm == _usuarioDigitado && _senha_adm == _senhaDigitada)
			{
				UsuarioLogado = login;
				TipodeUsuario = "S";

				return true;
			}
			else
			{

                _mensagemdeRetorno = "Usuário inválido";
                _foco = 1;
                return false;

            }

            //    ObterUsuario obter = new ObterUsuario(login);
            //    //_commandHandler.Run(obter);

            //    clsObjetosBanco objbancoObter = new clsObjetosBanco();
            //    objbancoObter.CriarObjetosBanco(objbancoObter.ToString());
            //    objbancoObter.command.ExecuteNonQuery();
				
            //    UsuarioDOM usuarioRetornado = obter.Usuario;
            //    if (usuarioRetornado == null)
            //    {
            //        _mensagemdeRetorno = "Usuário inválido";
            //        _foco = 1;
            //        return false;
            //    }
            //    else if(usuarioRetornado.Bloqueado == true)
            //    {
            //        _mensagemdeRetorno = "Usuário bloqueado pelo administrador";
            //        _foco = 1;
            //        return false;
            //    }
            //    else if ((usuarioRetornado.ObrigarTrocaDeSenha == true) && (verificarTroca == false))
            //    {
            //        if (senha == usuarioRetornado.Senha)
            //        {
            //            _trocaSenha = true;
            //            _foco = 3;
            //            _mensagemdeRetorno = "Troca de senha obrigatória.";
            //            return false;
            //        }
            //        else
            //        {
            //            _mensagemdeRetorno = "A senha digitada está inválida.";
            //            _foco = 2;
            //            return false;
            //        }
            //    }
            //    else if (senha != usuarioRetornado.Senha)
            //    {
            //        _mensagemdeRetorno = "A senha digitada está inválida.";
            //        _foco = 2;
            //        return false;
            //    }

            //    else if ((usuarioRetornado.Senha == senha) && (verificarTroca == false))
            //    {
            //        UsuarioLogado = login;
            //        TipodeUsuario = "N";

            //        _mensagemdeRetorno = string.Empty; 
            //        return true;
            //    }

            //    else if(senhaNova == String.Empty) 
            //    {
            //        _mensagemdeRetorno = "A nova senha não pode estar em branco.";
            //        _foco = 3;
            //        return false;

            //    }
            //    else if (senhaNova != senhaConf )
            //    {
            //        _mensagemdeRetorno = "As senhas não conferem, favor verificar.";
            //        _foco = 3;
            //        return false;
            //    }
            //    else if (senhaNova == usuarioRetornado.Senha)	
            //    {
            //        _mensagemdeRetorno = "A nova senha é inválida.";
            //        _foco = 3;
            //        return false;
            //    }

            //    else if (verificarTroca == true)
            //    {
            //        usuarioRetornado.Senha = senhaNova;
            //        usuarioRetornado.ObrigarTrocaDeSenha = false;
            //        SalvarUsuario salvar = new SalvarUsuario(usuarioRetornado);
            //        //_commandHandler.Run(salvar);

            //        clsObjetosBanco objbancoSalvar = new clsObjetosBanco();
            //        objbancoSalvar.CriarObjetosBanco(salvar.ToString());
            //        objbancoSalvar.command.ExecuteNonQuery();

            //        UsuarioLogado = login;

            //        _mensagemdeRetorno = "Troca de senha efetuada com sucesso.";
            //        return true;
            //    }

            //    else
            //    {
            //        _mensagemdeRetorno = "Usuário e/ou senha inválidos";
            //        _foco = 1;
            //        return false;
            //    }
            //}
		}

		public string MensagemdeRetorno
		{
			get { return _mensagemdeRetorno; }
			set { _mensagemdeRetorno = value; }
		}

		public bool TrocaSenha
		{
			get { return _trocaSenha; }
			set { _trocaSenha = value; }
		}

		public string UsuarioLogado
		{
			get { return _usuarioLogado; }
			set { _usuarioLogado = value; }
		}

		public string TipodeUsuario
		{
			get { return _tipodeUsuario; }
			set { _tipodeUsuario = value; }
		}

		public int SetarFoco
		{
			get { return _foco; }
			set { _foco = value; }
		}

	}
}



