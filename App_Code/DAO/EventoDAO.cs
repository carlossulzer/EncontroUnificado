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
	public class EventoDAO
	{

        public EventoDAO()
		{
		}


        public static bool IncluirEvento(EventoDOM eventoNovo, DataSet Banca, DataSet Orientador, DataSet Palestrante, DataSet Integrante, DataSet Calendario)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();


            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + EventoDIC.TABLE_EVENTO);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(EventoDIC.ObterColunasdaTabela("I"));
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
	        sqlInsert.Append(eventoNovo.codCaracterizacao.ToString()+", ");
            sqlInsert.Append(eventoNovo.codRecurso.ToString() + ", ");
            sqlInsert.Append(StringSuporte.Plic(eventoNovo.titulo.Trim().ToUpper()) + ", ");
            sqlInsert.Append(eventoNovo.codTipoEvento.ToString() + ", ");
            sqlInsert.Append(StringSuporte.Plic(eventoNovo.ementa.Trim().ToUpper()) + ", ");
            sqlInsert.Append(eventoNovo.codNucleo.ToString() + ", ");
            sqlInsert.Append(StringSuporte.Plic(eventoNovo.objetivos.Trim().ToUpper()) + ", ");
            sqlInsert.Append(StringSuporte.Plic(eventoNovo.publicoAlvo.Trim().ToUpper()) + ", ");
            sqlInsert.Append(eventoNovo.numVagas.ToString());
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" SELECT @@IDENTITY AS CHAVEINSERIDA");

            int codEvento = objbanco.IncluirRegistro(sqlInsert.ToString());
            bool gravacaoOK = false;

            if (codEvento > 0)
            {
                gravacaoOK = BancaDAO.SalvarBanca(codEvento, Banca);
                if (gravacaoOK)
                {
                    gravacaoOK = OrientadorDAO.SalvarOrientador(codEvento, Orientador);
                    if (gravacaoOK)
                    {
                        gravacaoOK = PalestranteDAO.SalvarPalestrante(codEvento, Palestrante);
                        if (gravacaoOK)
                        {
                            gravacaoOK = IntegranteDAO.SalvarIntegrante(codEvento, Integrante);
                            if (gravacaoOK)
                            {
                                gravacaoOK = CalendarioDAO.SalvarCalendario(codEvento, Calendario);
                            }
                        }    
                    }    
                }    
            }
            else
            {
                gravacaoOK = false;
            }

            return gravacaoOK;
        }

        public static bool AlterarEvento(EventoDOM eventoAltera, DataSet Banca, DataSet Orientador, DataSet Palestrante, DataSet Integrante, DataSet Calendario)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + EventoDIC.TABLE_EVENTO);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(CaracterizacaoDIC.COL_COD_CARACTERIZACAO + " = " + eventoAltera.codCaracterizacao.ToString() + ", ");
                sqlUpdate.Append(RecursoDIC.COL_COD_RECURSO + " = " + eventoAltera.codRecurso.ToString() + ", ");
                sqlUpdate.Append(EventoDIC.COL_TITULO + " = " + StringSuporte.Plic(eventoAltera.titulo.Trim().ToUpper()) + ", ");
                sqlUpdate.Append(TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + eventoAltera.codTipoEvento.ToString() + ", ");
                sqlUpdate.Append(EventoDIC.COL_EMENTA + " = " + StringSuporte.Plic(eventoAltera.ementa.Trim().ToUpper()) + ", ");
                sqlUpdate.Append(NucleoDIC.COL_CODNUCLEO + " = " + eventoAltera.codNucleo.ToString() + ", ");
                sqlUpdate.Append(EventoDIC.COL_OBJETIVOS + " = " + StringSuporte.Plic(eventoAltera.objetivos.Trim().ToUpper()) + ", ");
                sqlUpdate.Append(EventoDIC.COL_PUBLICO_ALVO + " = " + StringSuporte.Plic(eventoAltera.publicoAlvo.Trim().ToUpper()) + ", ");
                sqlUpdate.Append(EventoDIC.COL_NUM_VAGAS + " = " + eventoAltera.numVagas.ToString());

                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(EventoDIC.COL_COD_EVENTO + " = " + eventoAltera.codEvento.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int alterados = objbanco.command.ExecuteNonQuery();

                bool gravacaoOK = false;
                gravacaoOK = BancaDAO.SalvarBanca(eventoAltera.codEvento, Banca);
                if (gravacaoOK)
                {
                    gravacaoOK = OrientadorDAO.SalvarOrientador(eventoAltera.codEvento, Orientador);
                    if (gravacaoOK)
                    {
                        gravacaoOK = PalestranteDAO.SalvarPalestrante(eventoAltera.codEvento, Palestrante);
                        if (gravacaoOK)
                        {
                            gravacaoOK = IntegranteDAO.SalvarIntegrante(eventoAltera.codEvento, Integrante);
                            if (gravacaoOK)
                            {
                                gravacaoOK = CalendarioDAO.SalvarCalendario(eventoAltera.codEvento, Calendario);
                            }
                        }
                    }
                }

                return gravacaoOK;
            }
            catch(Exception e)
            {
                throw new ApplicationException("Erro ao alterar o evento."+e.Message.ToString());
            }

        }

        public static string ExcluirEvento(int id)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();

                sqlDelete.Append(" DELETE FROM " + BancaDIC.TABLE_BANCA + " WHERE " + EventoDIC.COL_COD_EVENTO + " = " + id.ToString());
                sqlDelete.Append("; DELETE FROM " + PalestranteDIC.TABLE_PALESTRANTE + " WHERE " + EventoDIC.COL_COD_EVENTO + " = " + id.ToString());
                sqlDelete.Append("; DELETE FROM " + OrientadorDIC.TABLE_ORIENTADOR + " WHERE " + EventoDIC.COL_COD_EVENTO + " = " + id.ToString());
                sqlDelete.Append("; DELETE FROM " + IntegranteDIC.TABLE_INTEGRANTE + " WHERE " + EventoDIC.COL_COD_EVENTO + " = " + id.ToString());
                sqlDelete.Append("; DELETE FROM " + CalendarioDIC.TABLE_CALENDARIO + " WHERE " + EventoDIC.COL_COD_EVENTO + " = " + id.ToString());
                sqlDelete.Append("; DELETE FROM " + EventoDIC.TABLE_EVENTO + " WHERE " + EventoDIC.COL_COD_EVENTO + " = " + id.ToString());

                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o Evento.";
            }
        }

        private string ObterDadosEvento()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+EventoDIC.ObterColunasdaTabela("S"));
            sql.Append(" FROM " + EventoDIC.TABLE_EVENTO);

            return sql.ToString();
        }

		public EventoDOM ObterEventoPeloId(int id)
		{
            StringBuilder sql = new StringBuilder();
            sql.Append(ObterDadosEvento());
            sql.Append(" WHERE " + EventoDIC.COL_COD_EVENTO + " = " + id.ToString());

			return Materializar(sql.ToString());
		}

		private static EventoDOM Materializar(string sql)
		{
            clsObjetosBanco objbanco = new clsObjetosBanco();
            ResultadoQuery dadosEvento = objbanco.MontaDataSet(sql.ToString());
            if (dadosEvento.Count > 0)
            {
                dadosEvento.ReadLine();
                return CriarObjetoEvento(dadosEvento);
            }
			return null;
		}

        public static ResultadoQuery ListarEvento(bool todosReg)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();  

            sql.Append("SELECT " + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(EventoDIC.COL_TITULO+ " ");
            sql.Append(" FROM " + EventoDIC.TABLE_EVENTO);
            if (!todosReg)
                sql.Append(" WHERE " + EventoDIC.COL_COD_EVENTO + " > 1 ");
            sql.Append(" ORDER BY " + EventoDIC.COL_TITULO);
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static EventoDOM CriarObjetoEvento(ResultadoQuery dadosEvento)
        {
            return new EventoDOM(
                Conversor.ConverterParaInteiro(dadosEvento[EventoDIC.COL_COD_EVENTO]),
                Conversor.ConverterParaInteiro(dadosEvento[CaracterizacaoDIC.COL_COD_CARACTERIZACAO]),
                Conversor.ConverterParaInteiro(dadosEvento[RecursoDIC.COL_COD_RECURSO]),
                dadosEvento[EventoDIC.COL_TITULO],
                Conversor.ConverterParaInteiro(dadosEvento[TipoEventoDIC.COL_COD_TIPO_EVENTO]),
                dadosEvento[EventoDIC.COL_EMENTA],
                Conversor.ConverterParaInteiro(dadosEvento[NucleoDIC.COL_CODNUCLEO]),
                dadosEvento[EventoDIC.COL_OBJETIVOS],
                dadosEvento[EventoDIC.COL_PUBLICO_ALVO],
                Conversor.ConverterParaInteiro(dadosEvento[EventoDIC.COL_NUM_VAGAS]));
        }

        public static ResultadoQuery BancaDoEvento(int codEvento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT " + BancaDIC.TABLE_BANCA + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(BancaDIC.TABLE_BANCA+"."+ProfessorDIC.COL_COD_PROFESSOR + ", ");
            sql.Append(ProfessorDIC.TABLE_PROFESSOR+"."+ProfessorDIC.COL_NOME);
            sql.Append(" FROM " + BancaDIC.TABLE_BANCA+", "+ProfessorDIC.TABLE_PROFESSOR);
            sql.Append(" WHERE " + BancaDIC.TABLE_BANCA + "." + ProfessorDIC.COL_COD_PROFESSOR + " = ");
            sql.Append(ProfessorDIC.TABLE_PROFESSOR + "." + ProfessorDIC.COL_COD_PROFESSOR +" and " );
            sql.Append(BancaDIC.TABLE_BANCA + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());

            sql.Append(" ORDER BY " + ProfessorDIC.TABLE_PROFESSOR+"."+ProfessorDIC.COL_NOME);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery OrientadorDoEvento(int codEvento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT " + OrientadorDIC.TABLE_ORIENTADOR + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(OrientadorDIC.TABLE_ORIENTADOR + "." + ProfessorDIC.COL_COD_PROFESSOR + ", ");
            sql.Append(ProfessorDIC.TABLE_PROFESSOR + "." + ProfessorDIC.COL_NOME);
            sql.Append(" FROM " + OrientadorDIC.TABLE_ORIENTADOR + ", " + ProfessorDIC.TABLE_PROFESSOR);
            sql.Append(" WHERE " + OrientadorDIC.TABLE_ORIENTADOR + "." + ProfessorDIC.COL_COD_PROFESSOR + " = ");
            sql.Append(ProfessorDIC.TABLE_PROFESSOR + "." + ProfessorDIC.COL_COD_PROFESSOR+ " and ");
            sql.Append(OrientadorDIC.TABLE_ORIENTADOR + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());
            sql.Append(" ORDER BY " + ProfessorDIC.TABLE_PROFESSOR + "." + ProfessorDIC.COL_NOME);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery PalestranteDoEvento(int codEvento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT " + PalestranteDIC.TABLE_PALESTRANTE + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(PalestranteDIC.TABLE_PALESTRANTE + "." + ProfessorDIC.COL_COD_PROFESSOR + ", ");
            sql.Append(ProfessorDIC.TABLE_PROFESSOR + "." + ProfessorDIC.COL_NOME);
            sql.Append(" FROM " + PalestranteDIC.TABLE_PALESTRANTE + ", " + ProfessorDIC.TABLE_PROFESSOR);
            sql.Append(" WHERE " + PalestranteDIC.TABLE_PALESTRANTE + "." + ProfessorDIC.COL_COD_PROFESSOR + " = ");
            sql.Append(ProfessorDIC.TABLE_PROFESSOR + "." + ProfessorDIC.COL_COD_PROFESSOR+ " and ");
            sql.Append(PalestranteDIC.TABLE_PALESTRANTE + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());
            sql.Append(" ORDER BY " + ProfessorDIC.TABLE_PROFESSOR + "." + ProfessorDIC.COL_NOME);
            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery IntegranteDoEvento(int codEvento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT " + IntegranteDIC.TABLE_INTEGRANTE + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(    IntegranteDIC.TABLE_INTEGRANTE + "." + IntegranteDIC.COL_MATRICULA + ", ");
            sql.Append(    IntegranteDIC.TABLE_INTEGRANTE + "." + IntegranteDIC.COL_COD_ENSINO + ", ");
            sql.Append(    AlunoDIC.TABLE_ALUNOG + "." + AlunoDIC.COL_NOME);
            sql.Append(" FROM " + IntegranteDIC.TABLE_INTEGRANTE + ", " + AlunoDIC.TABLE_ALUNOG);
            sql.Append(" WHERE " + IntegranteDIC.TABLE_INTEGRANTE + "." + IntegranteDIC.COL_MATRICULA + " = ");
            sql.Append(    AlunoDIC.TABLE_ALUNOG + "." + AlunoDIC.COL_MATRICULA+" and ");
            sql.Append(    IntegranteDIC.TABLE_INTEGRANTE + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());
            sql.Append(" UNION ");
            sql.Append(" SELECT " + IntegranteDIC.TABLE_INTEGRANTE + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(    IntegranteDIC.TABLE_INTEGRANTE + "." + IntegranteDIC.COL_MATRICULA + ", ");
            sql.Append(    IntegranteDIC.TABLE_INTEGRANTE + "." + IntegranteDIC.COL_COD_ENSINO + ", ");
            sql.Append(    AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_NOME);
            sql.Append(" FROM " + IntegranteDIC.TABLE_INTEGRANTE + ", " + AlunoDIC.TABLE_ALUNOT);
            sql.Append(" WHERE " + IntegranteDIC.TABLE_INTEGRANTE + "." + IntegranteDIC.COL_MATRICULA + " = ");
            sql.Append(    AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_MATRICULA+" and ");
            sql.Append(    IntegranteDIC.TABLE_INTEGRANTE + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());
            sql.Append(" ORDER BY 4");
            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery CalendarioDoEvento(int codEvento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT " + CalendarioDIC.TABLE_CALENDARIO + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_SALA + ", ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_HORARIO + ", ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + ", ");
            sql.Append("'SALA :'+" + SalaDIC.COL_DESCRICAO + "+' - BLOCO:'+" + SalaDIC.COL_BLOCO + "+' - '+" + SalaDIC.COL_ANDAR + "+'º ANDAR' AS " + SalaDIC.COL_DESC_CONSULTA+" , ");
            sql.Append(HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAINICIAL+"+' às '+" + HorarioDIC.TABLE_HORARIO+"."+HorarioDIC.COL_HORAFINAL+" AS "+HorarioDIC.COL_DESC_CONSULTA);
            sql.Append(" FROM " + CalendarioDIC.TABLE_CALENDARIO + ", " + SalaDIC.TABLE_SALA+", "+HorarioDIC.TABLE_HORARIO );
            sql.Append(" WHERE " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_SALA + " = "+ SalaDIC.TABLE_SALA + "." + SalaDIC.COL_COD_SALA+ " and ") ;
            sql.Append( CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_HORARIO +" = "+ HorarioDIC.TABLE_HORARIO+"." + HorarioDIC.COL_COD_HORARIO+ " and ");
            sql.Append( CalendarioDIC.TABLE_CALENDARIO + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());
            sql.Append(" ORDER BY " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery DropDownEvento()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(EventoDIC.COL_TITULO);
            sql.Append(" FROM " + EventoDIC.TABLE_EVENTO);
            sql.Append(" ORDER BY " + EventoDIC.COL_TITULO);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static bool RegistroExiste(string codEvento, string titulo, string operacao)
        {
            bool existente = true;
            string codigo = "0";

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT "+EventoDIC.COL_COD_EVENTO+" FROM " + EventoDIC.TABLE_EVENTO);
            sqlExiste.Append(" WHERE " + EventoDIC.COL_TITULO + " = " + StringSuporte.Plic(titulo.Trim().ToUpper()));
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

        public static ResultadoQuery DropDownEventosInscricao(string dataEvento)
        {
            DateTime data = Convert.ToDateTime(dataEvento);

            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_TITULO);
            sql.Append(" FROM " + InscricaoDIC.TABLE_INSCRICAO + ", " + EventoDIC.TABLE_EVENTO);
            sql.Append(" WHERE " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA + " = " + StringSuporte.Formatar(data));
            sql.Append(" GROUP BY " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_TITULO);
            sql.Append(" ORDER BY " + EventoDIC.COL_TITULO);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery HorarioDoEvento(int codEvento, DateTime dataEvento)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_HORARIO + ", ");
            sql.Append(HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAINICIAL + "+' às '+" + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAFINAL + " AS " + HorarioDIC.COL_DESC_CONSULTA);
            sql.Append(" FROM " + CalendarioDIC.TABLE_CALENDARIO+ ", " + HorarioDIC.TABLE_HORARIO);
            sql.Append(" WHERE " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_HORARIO + " = " + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString()+" and ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + " = " + StringSuporte.Formatar( dataEvento) );
            sql.Append(" ORDER BY " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA);

            return objbanco.MontaDataSet(sql.ToString());
        }



	}
}