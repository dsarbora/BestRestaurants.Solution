using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
  public class CuisinesController : Controller
  {
    [HttpGet("/cuisines")]
    public ActionResult Index()
    {
      return View(Cuisine.GetAll());
    }

    [HttpGet("/cuisines/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/cuisines")]
    public ActionResult Create(string type)
    {
      Cuisine newCuisine = new Cuisine(type);
      newCuisine.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/cuisines/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine searchedCuisine = Cuisine.Find(id);
      model["type"] = searchedCuisine;
      model["restaurants"] = searchedCuisine.GetAllRestaurants();
      return View(model);
    }

    [HttpGet("/cuisines/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Cuisine searchedCuisine = Cuisine.Find(id);
      return View(searchedCuisine);
    }

    [HttpPost("/cuisines/{id}")]
    public ActionResult Update(int id, string type)
    {
      Cuisine searchedCuisine = Cuisine.Find(id);
      searchedCuisine.Edit(type);
      return RedirectToAction("Show", id);
    }

    [HttpPost("/cuisines/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Cuisine.Find(id).Delete();
      return RedirectToAction("Index");
    }

    [HttpPost("/cuisines/{id}/restaurants")]
      public ActionResult Create(int id, string name, string address)
      {
        Cuisine searchedCuisine = Cuisine.Find(id);
        Restaurant newRestaurant = new Restaurant(name, address, id);
        newRestaurant.Save();
        return RedirectToAction("Show", id);
      }
  }
}
