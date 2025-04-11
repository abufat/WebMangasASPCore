using System.Data;
using WebMangasASPCore.Models.MesExceptions;
using WebMangasASPCore.Models.Metier;
using WebMangasAspCore.Models.Persistance;

public class ServiceUtilisateur
{
	public Utilisateur getUtilisateur(String nom)
	{
		DataTable dt;
		Utilisateur unUtil = null;
		String mysql = "SELECT numUtil, nomUtil, MotPasse, Salt, role FROM utilisateurs " +
					   " where nomUtil = " + "'" + nom + "'";
		Serreurs er = new Serreurs("Erreur sur recherche d'un utilisateur.", "Service.getUtilisateur");

		try
		{
			dt = DBInterface.Lecture(mysql, er);
			if (dt.IsInitialized && dt.Rows.Count > 0)
			{
				unUtil = new Utilisateur();
				// il faut redécouper la liste pour retrouver les lignes
				DataRow dataRow = dt.Rows[0];
				unUtil.NumUtil = Int16.Parse(dataRow[0].ToString());
				unUtil.NomUtil = dataRow[1].ToString();
				unUtil.MotPasse = dataRow[2].ToString();
				unUtil.Salt = dataRow[3].ToString();
				unUtil.Role = dataRow[4].ToString();
			}
			return unUtil;
		}
		catch (MonException e)
		{
			throw e;
		}
		catch (Exception exc)
		{
			throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), exc.Message);
		}
	}
}
	