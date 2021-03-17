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
	public class CaracterizacaoDAO
	{

        public CaracterizacaoDAO()
		{
		}


        public static int IncluirCaracterizacao(CaracterizacaoDOM caracterizacaoNova, bool autoIncremento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + CaracterizacaoDIC.TABLE_CARACTERIZACAO);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(CaracterizacaoDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(StringSuporte.Plic(caracterizacaoNova.descricao.Trim().ToUpper()));
            sqlInsert.Append(" ) ");

			if (autoIncremento == true)
			{
                sqlInsert.Append("SELECT @@IDENTITY AS CHAVEINSERIDA");
			}

            return objbanco.IncluirRegistro(sqlInsert.ToString());

        }

        public static bool AlterarCaracterizacao(CaracterizacaoDOM caracterizacaoAltera)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + CaracterizacaoDIC.TABLE_CARACTERIZACAO);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(CaracterizacaoDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(caracterizacaoAltera.descricao.Trim().ToUpper()) );

                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(CaracterizacaoDIC.COL_COD_CARACTERIZACAO + " = " + caracterizacaoAltera.codCaracterizacao.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar a caracterização."+e.Message.ToString());
            }

        }

        public static string ExcluirCaracterizacao(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + CaracterizacaoDIC.TABLE_CARACTERIZACAO);
                sqlDelete.Append(" WHERE " + CaracterizacaoDIC.COL_COD_CARACTERIZACAO + " = " + id.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir a caracterização, ou a caracterização está sendo usada em algum evento.";
            }
        }

        private string ObterDadosCaracterizacao()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+CaracterizacaoDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + CaracterizacaoDIC.TABLE_CARACTERIZACAO);

            return sql.ToString();
        }

		public CaracterizacaoDOM ObterCaracterizacaoPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();

            sql.Append(ObterDadosCaracterizacao());
            sql.Append(" WHERE " + CaracterizacaoDIC.COL_COD_CARACTERIZACAO + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static CaracterizacaoDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosCaracterizacao = objbanco.MontaDataSet(sql.ToString());
            if (dadosCaracterizacao.Count > 0)
            {
                dadosCaracterizacao.ReadLine();
                return CriarObjetoCaracterizacao(dadosCaracterizacao);
            }
			return null;
		}

        public static ResultadoQuery ListarCaracterizacao(bool todosReg)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();  

            sql.Append("SELECT " + CaracterizacaoDIC.COL_COD_CARACTERIZACAO + ", ");
            sql.Append(CaracterizacaoDIC.COL_DESCRICAO+ " ");
            sql.Append(" FROM " + CaracterizacaoDIC.TABLE_CARACTERIZACAO);
            if (!todosReg)
                sql.Append(" WHERE " + CaracterizacaoDIC.COL_COD_CARACTERIZACAO + " > 1 ");

            sql.Append(" ORDER BY " + CaracterizacaoDIC.COL_DESCRICAO);

     
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static CaracterizacaoDOM CriarObjetoCaracterizacao(ResultadoQuery dadosCaracterizacao)
        {
            return new CaracterizacaoDOM(
                Conversor.ConverterParaInteiro(dadosCaracterizacao[CaracterizacaoDIC.COL_COD_CARACTERIZACAO]),
                dadosCaracterizacao[CaracterizacaoDIC.COL_DESCRICAO]);
        }

        public static bool RegistroExiste(string descricao, string operacao, string codCaraterizacao)
        {
            bool existente = true;
            string codigo = "0";

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+CaracterizacaoDIC.COL_COD_CARACTERIZACAO+" FROM " + CaracterizacaoDIC.TABLE_CARACTERIZACAO);
            sqlExiste.Append(" WHERE " + CaracterizacaoDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(descricao.ToString().Trim()));
            objbanco.CriarObjetosBanco(sqlExiste.ToString());
            object resultado = objbanco.command.ExecuteScalar();

            if (resultado != null)
                codigo = resultado.ToString();

            if (operacao == "A")
            {
                if (codCaraterizacao == codigo || codigo == "0")
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