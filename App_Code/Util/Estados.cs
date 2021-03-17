using System;
using System.Data;

namespace Util
{
	/// <summary>
	/// Summary description for clsEstado.
	/// </summary>
	public class Estados
	{
		public Estados()
		{
		}

		public static DataSet CarregaEstados()
		{

			DataTable dtEstados = new DataTable("estados");

			DataColumn dcColuna;  // colunas
			DataRow drLinha;      // linhas

			// cria a coluna UF do Tipo String
			dcColuna = new DataColumn();
			dcColuna.DataType = System.Type.GetType("System.String");
			dcColuna.ColumnName = "UF";
			dcColuna.ReadOnly = false;
			dcColuna.Unique = true;
			dtEstados.Columns.Add(dcColuna);

			// cria a coluna Descricao do Tipo String
			dcColuna = new DataColumn();
			dcColuna.DataType = System.Type.GetType("System.String");
			dcColuna.ColumnName = "Descricao";
			dcColuna.ReadOnly = false;
			dcColuna.Unique = false;
			dtEstados.Columns.Add(dcColuna);
			
         //inclui dados na tabela
			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "AC";
			drLinha["Descricao"] = "ACRE";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "AL";
			drLinha["Descricao"] = "ALAGOAS";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "AM";   
			drLinha["Descricao"] = "AMAZONAS";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "AP";    
			drLinha["Descricao"] = "AMAPÁ";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "BA";    
			drLinha["Descricao"] = "BAHIA";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "CE";    
			drLinha["Descricao"] = "CEARA";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "DF"; 
			drLinha["Descricao"] = "DISTRITO FEDERAL";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "ES";
			drLinha["Descricao"] = "ESPIRITO SANTO";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "GO";    
			drLinha["Descricao"] = "GOIAS";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "MA";    
			drLinha["Descricao"] = "MARANHÃO";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "MT";    
			drLinha["Descricao"] = "MATO GROSSO";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "MS";    
			drLinha["Descricao"] = "MATO GROSSO DO SUL";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "MG";    
			drLinha["Descricao"] = "MINAS GERAIS";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "PA";    
			drLinha["Descricao"] = "PARÁ";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "PB";    
			drLinha["Descricao"] = "PARAÍBA";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "PE";    
			drLinha["Descricao"] = "PERNAMBUCO";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "PI";    
			drLinha["Descricao"] = "PIAUI";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "PR";    
			drLinha["Descricao"] = "PARANÁ";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "RJ";    
			drLinha["Descricao"] = "RIO DE JANEIRO";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "RN";    
			drLinha["Descricao"] = "RIO GRANDE DO NORTE";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "RO";    
			drLinha["Descricao"] = "RONDÔNIA";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "RR";    
			drLinha["Descricao"] = "RORAIMA";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "RS";    
			drLinha["Descricao"] = "RIO GRANDE DO SUL";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "SC";    
			drLinha["Descricao"] = "SANTA CATARINA";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "SE";    
			drLinha["Descricao"] = "SERGIPE";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "SP";    
			drLinha["Descricao"] = "SÃO PAULO";
			dtEstados.Rows.Add(drLinha);

			drLinha = dtEstados.NewRow();
			drLinha["UF"] = "TO";    
			drLinha["Descricao"] = "TOCANTINS";
			dtEstados.Rows.Add(drLinha);

			// inclui a tabela no dataset

			DataSet dsEstados = new DataSet();
			dsEstados.Tables.Add(dtEstados);
			
			return dsEstados;
		}
	}
}
