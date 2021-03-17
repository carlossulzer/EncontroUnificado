using System;
using System.Collections;
using System.Data;
using Util;
using Dominio;
using Banco;
using System.Text;
using DIC;

namespace DAO
{
	public class RecursoDAO
	{

        public RecursoDAO()
		{
		}


        public static int IncluirRecurso(RecursoDOM recursoNovo, bool autoIncremento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + RecursoDIC.TABLE_RECURSOS);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(RecursoDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(StringSuporte.Plic(recursoNovo.descricao.Trim().ToUpper()));
            sqlInsert.Append(" ) ");


			if (autoIncremento == true)
			{
                sqlInsert.Append("SELECT @@IDENTITY AS CHAVEINSERIDA");
			}

            return objbanco.IncluirRegistro(sqlInsert.ToString());

        }

        public static bool AlterarRecurso(RecursoDOM recursoAltera)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + RecursoDIC.TABLE_RECURSOS);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(RecursoDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(recursoAltera.descricao.Trim().ToUpper()) );

                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(RecursoDIC.COL_COD_RECURSO + " = " + recursoAltera.codRecurso.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar o recurso."+e.Message.ToString());
            }
        }

        public static string ExcluirRecurso(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + RecursoDIC.TABLE_RECURSOS);
                sqlDelete.Append(" WHERE " + RecursoDIC.COL_COD_RECURSO + " = " + id.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o recurso, ou o recurso está sendo usado em algum evento.";
            }
        }


        private string ObterDadosRecurso()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+RecursoDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + RecursoDIC.TABLE_RECURSOS);

            return sql.ToString();
        }

		public RecursoDOM ObterRecursoPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosRecurso());
            sql.Append(" WHERE " + RecursoDIC.COL_COD_RECURSO + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static RecursoDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosRecurso = objbanco.MontaDataSet(sql.ToString());
            if (dadosRecurso.Count > 0)
            {
                dadosRecurso.ReadLine();
                return CriarObjetoRecurso(dadosRecurso);
            }
			return null;
		}

        public static ResultadoQuery ListarRecurso(bool todosReg)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();  

            sql.Append("SELECT " + RecursoDIC.COL_COD_RECURSO + ", ");
            sql.Append(RecursoDIC.COL_DESCRICAO+ " ");
            sql.Append(" FROM " + RecursoDIC.TABLE_RECURSOS);
            if (! todosReg)
                sql.Append(" WHERE " + RecursoDIC.COL_COD_RECURSO+" > 1 ");

            sql.Append(" ORDER BY " + RecursoDIC.COL_DESCRICAO);

     
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static RecursoDOM CriarObjetoRecurso(ResultadoQuery dadosRecurso)
        {
            return new RecursoDOM(
                Conversor.ConverterParaInteiro(dadosRecurso[RecursoDIC.COL_COD_RECURSO]),
                dadosRecurso[RecursoDIC.COL_DESCRICAO]);
        }

        public static bool RegistroExiste(string descricao, string operacao, string codRecurso)
        {
            bool existente = true;
            string codigo = "0";

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+RecursoDIC.COL_COD_RECURSO+" FROM " + RecursoDIC.TABLE_RECURSOS);
            sqlExiste.Append(" WHERE " + RecursoDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(descricao.ToString().Trim()));
            objbanco.CriarObjetosBanco(sqlExiste.ToString());
            object resultado = objbanco.command.ExecuteScalar();

            if (resultado != null)
                codigo = resultado.ToString();

            if (operacao == "A")
            {
                if (codRecurso == codigo || codigo == "0")
                {
                    existente = false;
                }
                else
                {
                    existente = true;
                }
            }
            else if (operacao == "I")
            {
                if (codigo != "0")
                    existente = true;
                else
                    existente = false;
            }
            return existente;


        }

		
	}
}