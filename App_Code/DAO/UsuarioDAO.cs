using System;
using System.Collections;
using System.Data;
using System.Text;
using DIC;
using DOM;
using Banco;
using Util;

namespace DAO
{
	internal class UsuarioDAO
	{
		//Persistencia _persistencia = new Persistencia();

		public UsuarioDAO()
		{
			//_persistencia.TabelaPersistencia = UsuarioDIC.TABLE_USUARIO;
		}

        //public void PersistirUsuario(Usuario usuario)
        //{
        //    _persistencia.AdicionarItemPersistencia(UsuarioDIC.COL_LOGIN,usuario.Login);
        //    _persistencia.AdicionarItemPersistencia(UsuarioDIC.COL_SENHA, usuario.Senha);
        //    _persistencia.AdicionarItemPersistencia(UsuarioDIC.COL_NOMECOMPLETO,usuario.NomeCompleto, usuario.NomeCompleto == String.Empty);
        //    _persistencia.AdicionarItemPersistencia(UsuarioDIC.COL_EMAIL,usuario.Email, usuario.Email == String.Empty);
        //    _persistencia.AdicionarItemPersistencia(UsuarioDIC.COL_BLOQUEADO,usuario.Bloqueado);
        //    _persistencia.AdicionarItemPersistencia(UsuarioDIC.COL_OBRIGARTROCADESENHA,usuario.ObrigarTrocaDeSenha);
        //    usuario.IdUsuario = _persistencia.PersistirChave(UsuarioDIC.COL_IDUSUARIO, usuario.IdUsuario);
        //    //_persistencia.Persistir(UsuarioDIC.COL_IDUSUARIO, usuario.IdUsuario);
        //}
		
		public string ExcluirUsuario(int id)
		{
             try
             {
                 clsObjetosBanco objbanco = new clsObjetosBanco();
                 StringBuilder sqlDelete = new StringBuilder();
                 sqlDelete.Append("DELETE FROM " + UsuarioDIC.TABLE_USUARIO);
                 sqlDelete.Append(" WHERE " + UsuarioDIC.COL_IDUSUARIO + " = " + id.ToString());
                 objbanco.CriarObjetosBanco(sqlDelete.ToString());
                 objbanco.command.ExecuteNonQuery();
                 return string.Empty;
             }
             catch
             {
                 return "Erro ao excluir o usuário.";
             }

		}

		public UsuarioDOM ObterUsuarioPeloLogin(string login)
		{
            StringBuilder sql = new StringBuilder();
			sql.Append(ObterCabecalhoSqlParaUsuarios());
			sql.Append(" WHERE " + UsuarioDIC.COL_LOGIN + " = " + StringSuporte.Plic(login));
			return Materializar(sql.ToString());
		}

		public UsuarioDOM ObterUsuarioPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterCabecalhoSqlParaUsuarios());
			sql.Append(" WHERE " + UsuarioDIC.COL_IDUSUARIO + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static UsuarioDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosUsuario = objbanco.MontaDataSet(sql.ToString());
            if (dadosUsuario.Count > 0)
            {
                dadosUsuario.ReadLine();
                return CriarObjetoUsuario(dadosUsuario);
            }
            return null;
		}

 		public IList ObterListaDeUsuarios(string login)
		{
			ResultadoQuery dadosDoUsuario = ObterBDQueryResult(login);

			IList list = new ArrayList();
			while (dadosDoUsuario.ReadLine())
			{
				list.Add( CriarObjetoUsuario(dadosDoUsuario) );
			}

			return list;
		}

		public DataSet ObterDadosDeUsuarios(string login)
		{
			return ObterBDQueryResult(login).InternalDataSet;
		}

		private ResultadoQuery ObterBDQueryResult(string login)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

			sql.Append(ObterCabecalhoSqlParaUsuarios());
			if (login.Length > 0)
				sql.Append(" WHERE " + UsuarioDIC.COL_LOGIN + " LIKE " + StringSuporte.Plic(login + "%"));
	
			sql.Append(" ORDER BY " + UsuarioDIC.COL_LOGIN);
            return objbanco.MontaDataSet(sql.ToString());
		}

		private static UsuarioDOM CriarObjetoUsuario(ResultadoQuery dadosDoUsuario)
		{
			return new UsuarioDOM(
				Conversor.ConverterParaInteiro(dadosDoUsuario[UsuarioDIC.COL_IDUSUARIO]),
				dadosDoUsuario[UsuarioDIC.COL_LOGIN],
				dadosDoUsuario[UsuarioDIC.COL_SENHA],
				dadosDoUsuario[UsuarioDIC.COL_NOMECOMPLETO],
				dadosDoUsuario[UsuarioDIC.COL_EMAIL],
				Conversor.ConverterParaBoolean(dadosDoUsuario[UsuarioDIC.COL_BLOQUEADO]),
				Conversor.ConverterParaBoolean(dadosDoUsuario[UsuarioDIC.COL_OBRIGARTROCADESENHA])
				);
		}

		private string ObterCabecalhoSqlParaUsuarios()
		{
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT ");
			sql.Append(UsuarioDIC.GetAllColumnsForSelect());
            sql.Append(" FROM " + UsuarioDIC.TABLE_USUARIO);

			return sql.ToString();
		}
	}
}