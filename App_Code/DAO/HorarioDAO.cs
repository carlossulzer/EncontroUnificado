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
	public class HorarioDAO
	{

        public HorarioDAO()
		{
		}


        public static int IncluirHorario(HorarioDOM horarioNovo, bool autoIncremento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + HorarioDIC.TABLE_HORARIO);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(HorarioDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(StringSuporte.Plic(horarioNovo.horaInicial.Trim())+", "  );
            sqlInsert.Append(StringSuporte.Plic(horarioNovo.horaFinal.Trim()));
            sqlInsert.Append(" ) ");


			if (autoIncremento == true)
			{
                sqlInsert.Append("SELECT @@IDENTITY AS CHAVEINSERIDA");
			}

            return objbanco.IncluirRegistro(sqlInsert.ToString());

        }

        public static bool AlterarHorario(HorarioDOM horarioAltera)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + HorarioDIC.TABLE_HORARIO);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(HorarioDIC.COL_HORAINICIAL + " = " + StringSuporte.Plic(horarioAltera.horaInicial.Trim())+", ");
                sqlUpdate.Append(HorarioDIC.COL_HORAFINAL + " = " + StringSuporte.Plic(horarioAltera.horaFinal.Trim()));
                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(HorarioDIC.COL_COD_HORARIO + " = " + horarioAltera.codHorario.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar o horário."+e.Message.ToString());
            }

        }

        public static string ExcluirHorario(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + HorarioDIC.TABLE_HORARIO);
                sqlDelete.Append(" WHERE " + HorarioDIC.COL_COD_HORARIO + " = " + id.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o horário, ou o horário está sendo usado em algum evento.";
            }
        }

        private string ObterDadosHorario()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+HorarioDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + HorarioDIC.TABLE_HORARIO);

            return sql.ToString();
        }

		public HorarioDOM ObterHorarioPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosHorario());
            sql.Append(" WHERE " + HorarioDIC.COL_COD_HORARIO + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static HorarioDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosHorario = objbanco.MontaDataSet(sql.ToString());
            if (dadosHorario.Count > 0)
            {
                dadosHorario.ReadLine();
                return CriarObjetoHorario(dadosHorario);
            }
			return null;
		}

        public static ResultadoQuery ListarHorario()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();  

            sql.Append("SELECT " + HorarioDIC.COL_COD_HORARIO + ", ");
            sql.Append(HorarioDIC.COL_HORAINICIAL + ", ");
            sql.Append(HorarioDIC.COL_HORAFINAL + " ");
            sql.Append(" FROM " + HorarioDIC.TABLE_HORARIO);
            sql.Append(" ORDER BY " + HorarioDIC.COL_HORAINICIAL+", "+HorarioDIC.COL_HORAFINAL);
     
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static HorarioDOM CriarObjetoHorario(ResultadoQuery dadosHorario)
        {
            return new HorarioDOM(Conversor.ConverterParaInteiro(dadosHorario[HorarioDIC.COL_COD_HORARIO]),
                dadosHorario[HorarioDIC.COL_HORAINICIAL],
                dadosHorario[HorarioDIC.COL_HORAFINAL]);
        }

        public static bool RegistroExiste(string horaInicial, string horaFinal, string operacao, string codCaraterizacao)
        {
            bool existente = true;
            string codigo = "0";

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+HorarioDIC.COL_COD_HORARIO+" FROM " + HorarioDIC.TABLE_HORARIO);
            sqlExiste.Append(" WHERE " + HorarioDIC.COL_HORAINICIAL + " = " + StringSuporte.Plic(horaInicial.ToString().Trim())+" and ");
            sqlExiste.Append(HorarioDIC.COL_HORAFINAL + " = " + StringSuporte.Plic(horaFinal.ToString().Trim()));
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

        public static ResultadoQuery DropDownHorario()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + HorarioDIC.COL_COD_HORARIO + ", ");
            sql.Append(HorarioDIC.COL_HORAINICIAL+"+' às '+"+ HorarioDIC.COL_HORAFINAL+" AS "+HorarioDIC.COL_DESC_CONSULTA);
            sql.Append(" FROM " + HorarioDIC.TABLE_HORARIO);
            sql.Append(" ORDER BY 2" );

            return objbanco.MontaDataSet(sql.ToString());
        }

		
	}
}