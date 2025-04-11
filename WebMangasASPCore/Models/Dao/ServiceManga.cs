using System.Data;
using System.Security.Cryptography.X509Certificates;
using Mysqlx.Expr;
using WebMangasAspCore.Models.Metier;
using WebMangasAspCore.Models.Persistance;
using WebMangasASPCore.Models.MesExceptions;

namespace WebMangasAspCore.Models.Dao
{
	public class ServiceManga
	{
		public static DataTable GetTousLesManga()
		{
			DataTable mesMangas;
			Serreurs er = new Serreurs("Erreur sur lecture des Mangas.", "Manga.getMangas()");

			try
			{
				String mysql = "Select id_manga, lib_genre, nom_dessinateur, nom_scenariste, titre, prix, couverture ";
				mysql += " from Manga join genre on manga.id_genre = genre.id_genre ";
				mysql += " join dessinateur on manga.id_dessinateur = dessinateur.id_dessinateur";
				mysql += " join scenariste on manga.id_scenariste = scenariste.id_scenariste ";

				mesMangas = DBInterface.Lecture(mysql, er);

				return mesMangas;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}
        /// <summary>
        /// Fonction qui retourne un manga
        /// </summary>
        /// <returns></returns>
        public static Manga GetUnManga(string id)
        {
            DataTable dt;
            Manga unManga = null;
            Serreurs er = new Serreurs("Erreur sur lecture des Mangas", "ServiceManga.getUnManga()");

            try
            {
                string mysql = "SELECT id_manga, id_genre, id_dessinateur, id_scenariste, titre, prix, couverture ";
                mysql += "FROM Manga ";
                mysql += "WHERE id_manga = " + id;

                dt = DBInterface.Lecture(mysql, er);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    unManga = new Manga
                    {
                        Id_manga = int.Parse(dataRow[0].ToString()),
                        Id_genre = int.Parse(dataRow[1].ToString()),
                        Id_dessinateur = int.Parse(dataRow[2].ToString()),
                        Id_scenariste = int.Parse(dataRow[3].ToString()),
                        Titre = dataRow[4].ToString(),
                        Prix = double.Parse(dataRow[5].ToString()),
                        Couverture = dataRow[6].ToString()
                    };

                    return unManga;
                }
                else
                {
                    return null;
                }
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
         
        }
        public static void UpdateManga(Manga unM)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un manga.", "ServiceManga.update()");
            String requete = "UPDATE Manga SET " +
                "id_scenariste = " + unM.Id_scenariste +
                ",id_dessinateur = " + unM.Id_dessinateur +
                ",id_genre = " + unM.Id_genre +
                ",titre = '" + unM.Titre + "'" +
				", prix = " + unM.Prix.ToString().Replace(',', '.') +
                ",couverture = '" + unM.Couverture + "'" +
                " WHERE id_manga =" + unM.Id_manga;
            ;

            try
            {
                DBInterface.Execute_Transaction(requete);

            } 
            catch (MonException erreur) 
            {
                throw erreur;
            }
        }
    }
}
