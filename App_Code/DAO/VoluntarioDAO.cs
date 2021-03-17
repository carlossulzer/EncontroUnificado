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
	public class VoluntarioDAO
	{

        public VoluntarioDAO()
		{
		}


        public static int IncluirVoluntario(VoluntarioDOM voluntarioNovo)
        {
            int reg = 0;
            if (!RegistroExiste(voluntarioNovo.matricula.ToString(), voluntarioNovo.codEnsino.ToString(), voluntarioNovo.codEvento.ToString(), "I" ))
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();

                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("INSERT INTO " + VoluntarioDIC.TABLE_VOLUNTARIO);
                sqlInsert.Append(" ( ");
                sqlInsert.Append(VoluntarioDIC.ObterColunasdaTabela());
                sqlInsert.Append(" ) ");
                sqlInsert.Append(" VALUES ");
                sqlInsert.Append(" ( ");
                sqlInsert.Append(voluntarioNovo.matricula.ToString()+", ");
                sqlInsert.Append(voluntarioNovo.codEnsino.ToString() + ", ");
                sqlInsert.Append(voluntarioNovo.codEvento.ToString()+ ", ");
                sqlInsert.Append(voluntarioNovo.codHorario.ToString() + ", ");
                sqlInsert.Append(voluntarioNovo.codSala.ToString() + ", ");
                sqlInsert.Append(StringSuporte.Formatar(voluntarioNovo.data));
                sqlInsert.Append(" ) ");
                reg = objbanco.IncluirRegistro(sqlInsert.ToString());
            }
            return reg;
        }

        public static bool AlterarVoluntario(VoluntarioDOM voluntarioAltera, string codEvento)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + VoluntarioDIC.TABLE_VOLUNTARIO);
                sqlUpdate.Append(" SET ");
                sqlUpdate.Append(VoluntarioDIC.COL_MATRICULA + " = " + voluntarioAltera.matricula.ToString() + ", ");
                sqlUpdate.Append(VoluntarioDIC.COL_COD_ENSINO + " = " + voluntarioAltera.codEnsino.ToString() + ", ");
                sqlUpdate.Append(EventoDIC.COL_COD_EVENTO + " = " + voluntarioAltera.codEvento.ToString() + ", ");
                sqlUpdate.Append(HorarioDIC.COL_COD_HORARIO + " = " + voluntarioAltera.codHorario.ToString() + ", ");
                sqlUpdate.Append(SalaDIC.COL_COD_SALA + " = " + voluntarioAltera.codSala.ToString() + ", ");
                sqlUpdate.Append(VoluntarioDIC.COL_DATA + " = " + voluntarioAltera.data.ToString() + ", ");

                sqlUpdate.Append(" WHERE " + VoluntarioDIC.COL_MATRICULA + " = " + voluntarioAltera.matricula.ToString() + " and ");
                sqlUpdate.Append(VoluntarioDIC.COL_COD_ENSINO + " = " + voluntarioAltera.codEnsino.ToString() + " and ");
                sqlUpdate.Append(EventoDIC.COL_COD_EVENTO + " = " + codEvento);

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();
                return (alterados > 0);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Erro ao alterar o voluntário." + e.Message.ToString());
            }
        }

        public static string ExcluirVoluntario(VoluntarioDOM voluntarioExcluir)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + VoluntarioDIC.TABLE_VOLUNTARIO);
                sqlDelete.Append(" WHERE " + VoluntarioDIC.COL_MATRICULA + " = " + voluntarioExcluir.matricula.ToString() + " and ");
                sqlDelete.Append(VoluntarioDIC.COL_COD_ENSINO + " = " + voluntarioExcluir.codEnsino.ToString() + " and ");
                sqlDelete.Append(EventoDIC.COL_COD_EVENTO + " = " + voluntarioExcluir.codEvento.ToString());

                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o voluntário.";
            }
        }

        private string ObterDadosVoluntario()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + VoluntarioDIC.ObterColunasdaTabela());
            sql.Append(" FROM " + VoluntarioDIC.TABLE_VOLUNTARIO);

            return sql.ToString();
        }

        public VoluntarioDOM ObterVoluntarioPeloRegistro(int matricula, int cod_ensino, int cod_evento)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosVoluntario());
            sql.Append(" WHERE " + VoluntarioDIC.COL_MATRICULA + " = " + matricula.ToString()+" and ");
            sql.Append(VoluntarioDIC.COL_COD_ENSINO + " = " + cod_ensino.ToString() + " and ");
            sql.Append(EventoDIC.COL_COD_EVENTO + " = " + cod_evento.ToString());

            return Materializar(sql.ToString());
        }

        private static VoluntarioDOM Materializar(string sql)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosVoluntario = objbanco.MontaDataSet(sql.ToString());
            if (dadosVoluntario.Count > 0)
            {
                dadosVoluntario.ReadLine();
                return CriarObjetoVoluntario(dadosVoluntario);
            }
            return null;
        }

        public static ResultadoQuery ListarVoluntario()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT DISTINCT " + VoluntarioDIC.TABLE_VOLUNTARIO+"."+VoluntarioDIC.COL_MATRICULA + ", ");
            sql.Append(    VoluntarioDIC.TABLE_VOLUNTARIO + "." + VoluntarioDIC.COL_COD_ENSINO + ", ");
            sql.Append(    VoluntarioDIC.TABLE_VOLUNTARIO + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(    AlunoDIC.TABLE_ALUNOG+"."+AlunoDIC.COL_NOME);
            sql.Append(" FROM " + VoluntarioDIC.TABLE_VOLUNTARIO + ", " + AlunoDIC.TABLE_ALUNOG );
            sql.Append(" WHERE " + VoluntarioDIC.TABLE_VOLUNTARIO + "." + VoluntarioDIC.COL_MATRICULA + " = " + AlunoDIC.TABLE_ALUNOG+"."+AlunoDIC.COL_MATRICULA+" and ");
            sql.Append(    VoluntarioDIC.TABLE_VOLUNTARIO + "." + VoluntarioDIC.COL_COD_ENSINO + " = 1 ");
            sql.Append(" UNION ");
            sql.Append(" SELECT DISTINCT " + VoluntarioDIC.TABLE_VOLUNTARIO + "." + VoluntarioDIC.COL_MATRICULA + ", ");
            sql.Append(    VoluntarioDIC.TABLE_VOLUNTARIO + "." + VoluntarioDIC.COL_COD_ENSINO + ", ");
            sql.Append(    VoluntarioDIC.TABLE_VOLUNTARIO + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(    AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_NOME );
            sql.Append(" FROM " + VoluntarioDIC.TABLE_VOLUNTARIO + ", " + AlunoDIC.TABLE_ALUNOT );
            sql.Append(" WHERE " + VoluntarioDIC.TABLE_VOLUNTARIO + "." + VoluntarioDIC.COL_MATRICULA + " = " + AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_MATRICULA + " and ");
            sql.Append(     VoluntarioDIC.TABLE_VOLUNTARIO + "." + VoluntarioDIC.COL_COD_ENSINO + " = 2 ");
            sql.Append(" ORDER BY 4");

            return objbanco.MontaDataSet(sql.ToString());
        }

        private static VoluntarioDOM CriarObjetoVoluntario(ResultadoQuery dadosVoluntario)
        {
            return new VoluntarioDOM(Conversor.ConverterParaInteiro(dadosVoluntario[VoluntarioDIC.COL_MATRICULA]),
                Conversor.ConverterParaInteiro(dadosVoluntario[VoluntarioDIC.COL_COD_ENSINO]),
                Conversor.ConverterParaInteiro(dadosVoluntario[EventoDIC.COL_COD_EVENTO]),
                Conversor.ConverterParaInteiro(dadosVoluntario[HorarioDIC.COL_COD_HORARIO]),
                Conversor.ConverterParaInteiro(dadosVoluntario[SalaDIC.COL_COD_SALA]),
                Conversor.ConverterParaDateTime(dadosVoluntario[VoluntarioDIC.COL_DATA]));
        }

        public static bool RegistroExiste(string matricula, string codEnsino, string codEvento, string operacao )
        {
            bool existente = true;
            string _matricula = string.Empty;
            string _codEnsino = string.Empty;
            string _codEvento = string.Empty;

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+VoluntarioDIC.COL_MATRICULA+","+VoluntarioDIC.COL_COD_ENSINO+", "+EventoDIC.COL_COD_EVENTO);
            sqlExiste.Append(" FROM " + VoluntarioDIC.TABLE_VOLUNTARIO);
            sqlExiste.Append(" WHERE " + VoluntarioDIC.COL_MATRICULA + " = " + matricula + " and ");
            sqlExiste.Append(VoluntarioDIC.COL_COD_ENSINO + " = " + codEnsino+" and ");
            sqlExiste.Append(EventoDIC.COL_COD_EVENTO + " = " + codEvento);
            IDataReader resultado = objbanco.MontaDataReader(sqlExiste.ToString());

            while (resultado.Read())
            {
                _matricula  = resultado["matricula"].ToString();
                _codEnsino = resultado["cod_ensino"].ToString();
                _codEvento = resultado["cod_evento"].ToString();
            }

            if (operacao == "A")
            {
                if ((matricula == _matricula && codEnsino == _codEnsino && codEvento == _codEvento) || ((_matricula == string.Empty) && (_codEnsino == string.Empty) && (_codEvento == string.Empty)))
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
                if ((_matricula != string.Empty) && (_codEnsino != string.Empty) && (_codEvento != string.Empty))
                    existente = true;
                else
                    existente = false;
            }
            return existente;
        }
	}
}