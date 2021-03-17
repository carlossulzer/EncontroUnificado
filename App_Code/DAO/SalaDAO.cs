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
	public class SalaDAO
	{

        public SalaDAO()
		{
		}


        public static int IncluirSala(SalaDOM salaNova, bool autoIncremento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + SalaDIC.TABLE_SALA);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(SalaDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(StringSuporte.Plic(salaNova.descricao.Trim().ToUpper())+", ");
            sqlInsert.Append(StringSuporte.Plic(salaNova.andar.Trim().ToUpper())+", ");
            sqlInsert.Append(StringSuporte.Plic(salaNova.bloco.Trim().ToUpper()));

            sqlInsert.Append(" ) ");


			if (autoIncremento == true)
			{
                sqlInsert.Append("SELECT @@IDENTITY AS CHAVEINSERIDA");
			}

            return objbanco.IncluirRegistro(sqlInsert.ToString());

        }

        public static bool AlterarSala(SalaDOM salaAltera)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + SalaDIC.TABLE_SALA);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(SalaDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(salaAltera.descricao.Trim().ToUpper())+", " );
                sqlUpdate.Append(SalaDIC.COL_ANDAR + " = " + StringSuporte.Plic(salaAltera.andar.Trim().ToUpper()) + ", ");
                sqlUpdate.Append(SalaDIC.COL_BLOCO + " = " + StringSuporte.Plic(salaAltera.bloco.Trim().ToUpper()) );

                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(SalaDIC.COL_COD_SALA + " = " + salaAltera.codSala.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar a sala"+e.Message.ToString());
            }

        }

        public static string ExcluirSala(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + SalaDIC.TABLE_SALA);
                sqlDelete.Append(" WHERE " + SalaDIC.COL_COD_SALA + " = " + id.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir a sala, ou a sala está sendo usada em algum evento.";
            }
        }

        private string ObterDadosSala()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+SalaDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + SalaDIC.TABLE_SALA);

            return sql.ToString();
        }

		public SalaDOM ObterSalaPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosSala());
            sql.Append(" WHERE " + SalaDIC.COL_COD_SALA + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static SalaDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosSala = objbanco.MontaDataSet(sql.ToString());
            if (dadosSala.Count > 0)
            {
                dadosSala.ReadLine();
                return CriarObjetoSala(dadosSala);
            }
			return null;
		}

        public static ResultadoQuery ListarSala()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();  

            sql.Append("SELECT " + SalaDIC.COL_COD_SALA + ", ");
            sql.Append(SalaDIC.COL_DESCRICAO+ ", ");
            sql.Append(SalaDIC.COL_ANDAR + ", ");
            sql.Append(SalaDIC.COL_BLOCO + " ");
            sql.Append(" FROM " + SalaDIC.TABLE_SALA);
            sql.Append(" ORDER BY " + SalaDIC.COL_DESCRICAO+", "+SalaDIC.COL_ANDAR+","+SalaDIC.COL_BLOCO);
     
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static SalaDOM CriarObjetoSala(ResultadoQuery dadosSala)
        {
            return new SalaDOM(
                Conversor.ConverterParaInteiro(dadosSala[SalaDIC.COL_COD_SALA]),
                dadosSala[SalaDIC.COL_DESCRICAO],
                dadosSala[SalaDIC.COL_ANDAR],
                dadosSala[SalaDIC.COL_BLOCO]);
        }

        public static bool RegistroExiste(string sala, string andar, string bloco, string operacao, string codSala)
        {
            bool existente = true;
            string codigo = "0";

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+SalaDIC.COL_COD_SALA+" FROM " + SalaDIC.TABLE_SALA);
            sqlExiste.Append(" WHERE " + SalaDIC.COL_DESCRICAO + " = " + StringSuporte.Plic(sala.ToString().Trim())+" and ");
//            sqlExiste.Append(SalaDIC.COL_ANDAR + " = " + StringSuporte.Plic(andar.ToString().Trim()) + " and ");
            sqlExiste.Append(SalaDIC.COL_BLOCO + " = " + StringSuporte.Plic(bloco.ToString().Trim()) );
            objbanco.CriarObjetosBanco(sqlExiste.ToString());

            object resultado = objbanco.command.ExecuteScalar();

            if (resultado != null)
                codigo = resultado.ToString();

            if (operacao == "A")
            {
                if (codSala == codigo || codigo == "0")
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

        public static ResultadoQuery DropDownSala()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + SalaDIC.COL_COD_SALA + ", ");
            sql.Append("'SALA :'+"+SalaDIC.COL_DESCRICAO+"+' - BLOCO:'+"+SalaDIC.COL_BLOCO+"+' - '+"+SalaDIC.COL_ANDAR+"+'º ANDAR' AS "+SalaDIC.COL_DESC_CONSULTA);
            sql.Append(" FROM " + SalaDIC.TABLE_SALA);
            sql.Append(" ORDER BY 2");
            return objbanco.MontaDataSet(sql.ToString());
        }
		
	}
}