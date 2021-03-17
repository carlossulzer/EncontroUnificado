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
	public class ProfessorDAO
	{

        public ProfessorDAO()
		{
		}


        public static int IncluirProfessor(ProfessorDOM professorNovo, bool autoIncremento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + ProfessorDIC.TABLE_PROFESSOR);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(ProfessorDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(professorNovo.matricula.ToString()+ ", ");
            sqlInsert.Append(StringSuporte.Plic(professorNovo.nome.Trim().ToUpper())+", ");
            sqlInsert.Append(StringSuporte.Plic(professorNovo.telefone1.Trim()) + ", ");
            sqlInsert.Append(StringSuporte.Plic(professorNovo.telefone2.Trim()) + ", ");
            sqlInsert.Append(StringSuporte.Plic(professorNovo.email.Trim().ToLower()));
            sqlInsert.Append(" ) ");


			if (autoIncremento == true)
			{
                sqlInsert.Append("SELECT @@IDENTITY AS CHAVEINSERIDA");
			}

            return objbanco.IncluirRegistro(sqlInsert.ToString());

        }

        public static bool AlterarProfessor(ProfessorDOM professorAltera)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + ProfessorDIC.TABLE_PROFESSOR);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(ProfessorDIC.COL_MATRICULA + " = " + professorAltera.matricula.ToString()+", ");
                sqlUpdate.Append(ProfessorDIC.COL_NOME + " = " + StringSuporte.Plic(professorAltera.nome.Trim().ToUpper()) + ", ");
                sqlUpdate.Append(ProfessorDIC.COL_TELEFONE1 + " = " + StringSuporte.Plic(professorAltera.telefone1.Trim()) + ", ");
                sqlUpdate.Append(ProfessorDIC.COL_TELEFONE2 + " = " + StringSuporte.Plic(professorAltera.telefone2.Trim()) + ", ");
                sqlUpdate.Append(ProfessorDIC.COL_EMAIL + " = " + StringSuporte.Plic(professorAltera.email.Trim().ToLower()));

                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(ProfessorDIC.COL_COD_PROFESSOR + " = " + professorAltera.codProfessor.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar o professor."+e.Message.ToString());
            }

        }

        public static string ExcluirProfessor(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + ProfessorDIC.TABLE_PROFESSOR);
                sqlDelete.Append(" WHERE " + ProfessorDIC.COL_COD_PROFESSOR + " = " + id.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o professor, ou o professor está sendo usado em algum evento.";
            }
        }

        private string ObterDadosProfessor()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+ProfessorDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + ProfessorDIC.TABLE_PROFESSOR);

            return sql.ToString();
        }

		public ProfessorDOM ObterProfessorPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosProfessor());
            sql.Append(" WHERE " + ProfessorDIC.COL_COD_PROFESSOR + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static ProfessorDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosProfessor = objbanco.MontaDataSet(sql.ToString());
            if (dadosProfessor.Count > 0)
            {
                dadosProfessor.ReadLine();
                return CriarObjetoProfessor(dadosProfessor);
            }
			return null;
		}

        public static ResultadoQuery ListarProfessor()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();  

            sql.Append("SELECT " + ProfessorDIC.COL_COD_PROFESSOR + ", ");
            sql.Append(ProfessorDIC.COL_NOME+ ", ");
            sql.Append(ProfessorDIC.COL_MATRICULA + ", ");
            sql.Append(ProfessorDIC.COL_EMAIL + " ");
            sql.Append(" FROM " + ProfessorDIC.TABLE_PROFESSOR);
            sql.Append(" ORDER BY "+ProfessorDIC.COL_NOME);
     
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static ProfessorDOM CriarObjetoProfessor(ResultadoQuery dadosProfessor)
        {
            return new ProfessorDOM(
                Conversor.ConverterParaInteiro(dadosProfessor[ProfessorDIC.COL_COD_PROFESSOR]),
                Conversor.ConverterParaInteiro(dadosProfessor[ProfessorDIC.COL_MATRICULA]),
                dadosProfessor[ProfessorDIC.COL_NOME],
                dadosProfessor[ProfessorDIC.COL_TELEFONE1],
                dadosProfessor[ProfessorDIC.COL_TELEFONE2],
                dadosProfessor[ProfessorDIC.COL_EMAIL]);
        }

        public static bool RegistroExiste(string matricula, string nome, string operacao, string codProfessor)
        {
            bool existente = true;
            string codigo = "0";

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+ProfessorDIC.COL_COD_PROFESSOR+" FROM " + ProfessorDIC.TABLE_PROFESSOR);
            sqlExiste.Append(" WHERE " + ProfessorDIC.COL_MATRICULA + " = " + matricula.Trim());
            objbanco.CriarObjetosBanco(sqlExiste.ToString());

            object resultado = objbanco.command.ExecuteScalar();

            if (resultado != null)
                codigo = resultado.ToString();

            if (operacao == "A")
            {
                if (codProfessor == codigo || codigo == "0")
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

        public static ResultadoQuery DropDownProfessor()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + ProfessorDIC.COL_COD_PROFESSOR + ", ");
            sql.Append(ProfessorDIC.COL_NOME );
            sql.Append(" FROM " + ProfessorDIC.TABLE_PROFESSOR);
            sql.Append(" ORDER BY " + ProfessorDIC.COL_NOME);

            return objbanco.MontaDataSet(sql.ToString());
        }
		
	}
}