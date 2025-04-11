namespace WebMangasASPCore.Models.Metier
{
	public class Utilisateur
	{
		private int numUtil;
		private string nomUtil;
		private string prenomUtil;
		private string motPasse;
		private string salt;
		private string role;

		public int NumUtil { get => numUtil; set => numUtil = value; }
		public string NomUtil { get => nomUtil; set => nomUtil = value; }
		public string PrenomUtil { get => prenomUtil; set => prenomUtil = value; }
		public string MotPasse { get => motPasse; set => motPasse = value; }
		public string Role { get => role; set => role = value; }
		public string Salt { get => salt; set => salt = value; }

		public Utilisateur(int num, string nom, string prenom, string MP, string role)
		{
			this.numUtil = num;
			this.nomUtil = nom;
			this.prenomUtil = prenom;
			this.motPasse = MP;
			this.role = role;
		}

		public Utilisateur()
		{

		}
	}
}

