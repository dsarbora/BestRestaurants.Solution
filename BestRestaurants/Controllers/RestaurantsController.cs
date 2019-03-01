using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
  public class RestaurantsController : Controller
  {
    [HttpGet("/cuisines/{id}/restaurants/new")]
    public ActionResult New(int id)
    {
      Cuisine searchedCuisine = Cuisine.Find(id);
      return View(searchedCuisine);
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/{id}")]
    public ActionResult Show(int cuisineId, int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine searchedCuisine = Cuisine.Find(cuisineId);
      Restaurant searchedRestaurant = Restaurant.Find(id);
      model.Add("type", searchedCuisine);
      model.Add("restaurant", searchedRestaurant);
      return View(model);
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/{id}/edit")]
    public ActionResult Edit(int id, int cuisineId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine searchedCuisine = Cuisine.Find(cuisineId);
      Restaurant searchedRestaurant = Restaurant.Find(id);
      model.Add("type", searchedCuisine);
      model.Add("restaurant", searchedRestaurant);
      return View(model);
    }

    [HttpPost("/cuisines/{cuisineId}/restaurants/{id}")]
    public ActionResult Update(int id, int cuisineId, string name, string address)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Restaurant searchedRestaurant = Restaurant.Find(id);
      searchedRestaurant.Edit(name, address);
      Cuisine searchedCuisine = Cuisine.Find(cuisineId);
      model.Add("type", searchedCuisine);
      model.Add("restaurant", searchedRestaurant);
      return View("Show", model);
    }

  }

}
