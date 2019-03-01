using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Cuisine
  {
    private int Id;
    private string Type;

    public Cuisine(string type, int id=0)
    {
      Type = type;
      Id = id;
    }

    public string GetCuisineType()
    {
      return Type;
    }

    public int GetId()
    {
      return Id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisines (type) VALUES (@type);";
      MySqlParameter prmType = new MySqlParameter();
      prmType.ParameterName = "@type";
      prmType.Value = Type;
      cmd.Parameters.Add(prmType);

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newType)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE cuisines SET type=@type WHERE id=@id;";
      MySqlParameter prmType = new MySqlParameter();
      prmType.ParameterName = "@type";
      prmType.Value = newType;
      cmd.Parameters.Add(prmType);
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@id";
      prmId.Value = Id;
      cmd.Parameters.Add(prmId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisines WHERE id=@id;";
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@id";
      prmId.Value = Id;
      cmd.Parameters.Add(prmId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn!=null)
      {
        conn.Dispose();
      }
    }

    public List<Restaurant> GetAllRestaurants()
    {
      List<Restaurant> allRestaurantsWithType = new List<Restaurant>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id=@cuisine_id;";
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@cuisine_id";
      prmId.Value = Id;
      cmd.Parameters.Add(prmId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);
        Restaurant newRestaurant = new Restaurant(name, address, Id, id);
        allRestaurantsWithType.Add(newRestaurant);
      }
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return allRestaurantsWithType;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand(); //as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisines;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if(!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine)otherCuisine;
        bool nameEquality = this.GetCuisineType().Equals(newCuisine.GetCuisineType());
        bool idEquality = this.GetId().Equals(newCuisine.GetId());
        return (nameEquality && idEquality);
      }
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM cuisines;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        Cuisine newCuisine = new Cuisine(rdr.GetString(1), rdr.GetInt32(0));
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return allCuisines;
    }

    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisines WHERE id=@id;";
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@id";
      prmId.Value = id;
      cmd.Parameters.Add(prmId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      rdr.Read();
      Cuisine newCuisine = new Cuisine(rdr.GetString(1), id);
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return newCuisine;
    }

  }
}
