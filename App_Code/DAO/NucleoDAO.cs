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
	public class NucleoDAO
	{

        public NucleoDAO()
		{
		}


        public static int IncluirNucleo(NucleoDOM nucleoNovo, bool autoIncremento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + NucleoDIC.TABLE_NUCLEO);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(NucleoDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(StringSuporte.Plic(nucleoNovo.descricao.Trim().ToUpper()));
            sqlInsert.Append(" ) ");


			if (autoIncremento == true)
			{
                sqlInsert.Append("SELECT @@IDENTITY AS CHAVEINSERIDA");
			}

            return objbanco.IncluirRegistro(sqlInsert.ToString());

        }

        public static bool AlterarNucleo(NucleoDOM nucleoAltera)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + NucleoDIC.TABLE_NUCLEO);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(NucleoDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(nucleoAltera.descricao.Trim().ToUpper()));
                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(NucleoDIC.COL_CODNUCLEO + " = " + nucleoAltera.codNucleo.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar o núcleo."+e.Message.ToString());
            }

        }

        public static string ExcluirNucleo(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + NucleoDIC.TABLE_NUCLEO);
                sqlDelete.Append(" WHERE " + NucleoDIC.COL_CODNUCLEO + " = " + id.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o núcleo, ou o núcleo está sendo usado em algum evento.";
            }
        }

        private string ObterDadosNucleo()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+NucleoDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + NucleoDIC.TABLE_NUCLEO);

            return sql.ToString();
        }

		public NucleoDOM ObterNucleoPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosNucleo());
            sql.Append(" WHERE " + NucleoDIC.COL_CODNUCLEO + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static NucleoDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosNucleo = objbanco.MontaDataSet(sql.ToString());
            if (dadosNucleo.Count > 0)
            {
                dadosNucleo.ReadLine();
                return CriarObjetoNucleo(dadosNucleo);
            }
			return null;
		}

        public static ResultadoQuery ListarNucleo(bool todosReg)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();  

            sql.Append("SELECT " + NucleoDIC.COL_CODNUCLEO + ", ");
            sql.Append(NucleoDIC.COL_DESCRICAO + " ");
            sql.Append(" FROM " + NucleoDIC.TABLE_NUCLEO);
            if (! todosReg)
                sql.Append(" WHERE " + NucleoDIC.COL_CODNUCLEO + " > 1 ");

            sql.Append(" ORDER BY " + NucleoDIC.COL_DESCRICAO);

            return objbanco.MontaDataSet(sql.ToString());
        }

        private static NucleoDOM CriarObjetoNucleo(ResultadoQuery dadosNucleo)
        {
            return new NucleoDOM(Conversor.ConverterParaInteiro(dadosNucleo[NucleoDIC.COL_CODNUCLEO]),
                dadosNucleo[NucleoDIC.COL_DESCRICAO]);
        }

        public static bool RegistroExiste(string descricao, string operacao, string codNucleo)
        {
            bool existente = true;
            string codigo = "0";

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+NucleoDIC.COL_CODNUCLEO+" FROM " + NucleoDIC.TABLE_NUCLEO);
            sqlExiste.Append(" WHERE " + NucleoDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(descricao.ToString().Trim()));
            objbanco.CriarObjetosBanco(sqlExiste.ToString());

            object resultado = objbanco.command.ExecuteScalar();

            if (resultado != null)
                codigo = resultado.ToString();

            if (operacao == "A")
            {
                if (codNucleo == codigo || codigo == "0")
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