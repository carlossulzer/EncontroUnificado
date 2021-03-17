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
	public class TipoEventoDAO
	{
        public TipoEventoDAO()
		{
		}

        public static int IncluirTipoEvento(TipoEventoDOM tipoEventoNovo, bool autoIncremento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + TipoEventoDIC.TABLE_TIPO_EVENTO);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(TipoEventoDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(StringSuporte.Plic(tipoEventoNovo.descricao.Trim().ToUpper()));
            sqlInsert.Append(" ) ");


			if (autoIncremento == true)
			{
                sqlInsert.Append("SELECT @@IDENTITY AS CHAVEINSERIDA");
			}

            return objbanco.IncluirRegistro(sqlInsert.ToString());

        }

        public static bool AlterarTipoEvento(TipoEventoDOM tipoEventoAltera)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + TipoEventoDIC.TABLE_TIPO_EVENTO);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(TipoEventoDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(tipoEventoAltera.descricao.Trim().ToUpper()) );

                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + tipoEventoAltera.codTipoEvento.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar o tipo de evento."+e.Message.ToString());
            }
        }

        public static string ExcluirTipoEvento(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + TipoEventoDIC.TABLE_TIPO_EVENTO);
                sqlDelete.Append(" WHERE " + TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + id.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();

                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o tipo de evento, ou o tipo de evento está sendo usada em algum evento.";
            }
        }

        private string ObterDadosTipoEvento()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+TipoEventoDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + TipoEventoDIC.TABLE_TIPO_EVENTO);

            return sql.ToString();
        }

		public TipoEventoDOM ObterTipoEventoPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosTipoEvento());
            sql.Append(" WHERE " + TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static TipoEventoDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosTipoEvento = objbanco.MontaDataSet(sql.ToString());
            if (dadosTipoEvento.Count > 0)
            {
                dadosTipoEvento.ReadLine();
                return CriarObjetoTipoEvento(dadosTipoEvento);
            }
			return null;
		}

        public static ResultadoQuery ListarTipoEvento(bool todosReg)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT " + TipoEventoDIC.COL_COD_TIPO_EVENTO + ", ");
            sql.Append(TipoEventoDIC.COL_DESCRICAO+ " ");
            sql.Append(" FROM " + TipoEventoDIC.TABLE_TIPO_EVENTO);
            if (!todosReg)
                sql.Append(" WHERE " + TipoEventoDIC.COL_COD_TIPO_EVENTO + " > 1 ");

            sql.Append(" ORDER BY " + TipoEventoDIC.COL_DESCRICAO);
     
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static TipoEventoDOM CriarObjetoTipoEvento(ResultadoQuery dadosTipoEvento)
        {
            return new TipoEventoDOM(
                Conversor.ConverterParaInteiro(dadosTipoEvento[TipoEventoDIC.COL_COD_TIPO_EVENTO]),
                dadosTipoEvento[TipoEventoDIC.COL_DESCRICAO]);
        }

        public static  bool RegistroExiste(string descricao, string operacao, string codEvento)
        {
            bool existente = true;
            string codigo = "0";
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+TipoEventoDIC.COL_COD_TIPO_EVENTO+" FROM " + TipoEventoDIC.TABLE_TIPO_EVENTO);
            sqlExiste.Append(" WHERE " + TipoEventoDIC.COL_DESCRICAO + " = " +StringSuporte.Plic(descricao.ToString().Trim()));
            objbanco.CriarObjetosBanco(sqlExiste.ToString());

            object resultado = objbanco.command.ExecuteScalar();

            if (resultado != null)
                codigo = resultado.ToString();

            if (operacao == "A")
            {
                if (codEvento == codigo || codigo == "0")
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