﻿using Microsoft.AspNetCore.Mvc;
using WebMangasASPCore.Models.MesExceptions;
using WebMangasAspCore.Models.Dao;
using WebMangasAspCore.Models.Metier;
using System.Data;


namespace WebMangasASPCore.Controllers
{
	public class MangaController : Controller
	{
		public IActionResult Index()
		{
			System.Data.DataTable mesMangas = null;
			try
			{
				mesMangas = ServiceManga.GetTousLesManga();
					
			}
			catch (MonException e)
			{
				ModelState.AddModelError("Erreur", "Erreur Lors de la partie de la récuparation de mangas :" + e.Message);
			}
			return View(mesMangas);
		}

		public IActionResult Modifier(string id)
		{
			Manga unManga = null;	
			try
			{
				unManga = ServiceManga.GetUnManga(id);
				return View(unManga);
			}
			catch (MonException e)
			{
				return NotFound();
			}
		}

        [HttpPost]
        public IActionResult Modifier(Manga unM)
        {
            try
            {
                ServiceManga.UpdateManga(unM);
                return View();
            }
            catch (MonException e)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Rechercher()
        {
            DataTable dt = ServiceManga.GetTousLesTitres();
            ViewBag.Titres = dt;
            return View();
        }

        [HttpPost]
        public IActionResult Resultats(string titreChoisi, string rechercheLibre)
        {
            DataTable resultats = ServiceManga.RechercherMangas(titreChoisi, rechercheLibre);
            ViewBag.Resultats = resultats;
            ViewBag.Titres = ServiceManga.GetTousLesTitres();
            return View();
        }

    }

}
