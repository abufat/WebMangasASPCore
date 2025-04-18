using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMangasASPCore.Models.MesExceptions;
using WebMangasASPCore.Models.Metier;
using WebMangasASPCore.Models.Utilitaires;


namespace WebMangasASPCore.Controllers
{
	public class ConnexionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Controle()
		{

			try
			{
				// on récupère les données du formulaire
				string login = Request.Form["login"];
				string mdp = Request.Form["pwd"];
				try
				{
					ServiceUtilisateur unService = new ServiceUtilisateur();
					Utilisateur unUtilisateur = unService.getUtilisateur(login);
					if (unUtilisateur != null)
					{
						try
						{
							Byte[] selmdp = MonMotPassHash.GenerateSalt();
							Byte[] mdpByte = MonMotPassHash.PasswordHashe("secret", selmdp);
							String mdpS = MonMotPassHash.BytesToString(mdpByte);
							String saltS = MonMotPassHash.BytesToString(selmdp);
							String sel = unUtilisateur.Salt;
							// on récupère le sel
							Byte[] salt = MonMotPassHash.transformeEnBytes(unUtilisateur.Salt);
							// on génère le mot de passe
							Byte[] tempo = MonMotPassHash.transformeEnBytes(unUtilisateur.MotPasse);
							if (MonMotPassHash.VerifyPassword(salt, mdp, tempo) == false)
							{
								ModelState.AddModelError("Erreur", "Erreur lors du contrôle du mot de passe pour : " + login);
								return RedirectToAction("Index", "Connexion");
							}
							//
							HttpContext.Session.SetString("login", unUtilisateur.NomUtil);
                            HttpContext.Session.SetString("role", unUtilisateur.Role);

							return RedirectToAction("Index", "Home");


                        }
						catch (Exception e)
						{
							ModelState.AddModelError("Erreur", "Erreur lors du contrôle : " + e.Message);
							return RedirectToAction("Index", "Connexion");
						}
					}
					else
					{
						ModelState.AddModelError("Erreur", "Erreur login erroné : " + login);
						return RedirectToAction("Index", "Connexion");
					}
				}
				catch (MonException e)
				{
					ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
					return RedirectToAction("Index", "Connexion");
				}
				return RedirectToAction("Index", "Home");
			}
			catch (MonException e)
			{
				ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
				return RedirectToAction("Index", "Connexion");
			}
		}
        public IActionResult Session()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
