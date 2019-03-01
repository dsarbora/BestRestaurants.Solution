using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    private int Id;
    private string Name;
    private string Address;
    private int CuisineId;

    public Restaurant (string name, string address, int cuisineId, int id = 0)
    {
      Name = name;
      Address = address;
      CuisineId = cuisineId;
      Id = id;
    }

    public string GetName()
    {
      return Name;
    }

    public string GetAddress()
    {
      return Address;
    }

    public int GetId()
    {
      return Id;
    }

    public int GetCuisineId()
    {
      return CuisineId;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurants (name, address, cuisine_id) VALUES (@name, @address, @cuisine_id);";
      MySqlParameter prmName = new MySqlParameter();
      prmName.ParameterName = "@name";
      prmName.Value = Name;
      cmd.Parameters.Add(prmName);
      MySqlParameter prmAddress = new MySqlParameter();
      prmAddress.ParameterName = "@address";
      prmAddress.Value = Address;
      cmd.Parameters.Add(prmAddress);
      MySqlParameter prmCuisineId = new MySqlParameter();
      prmCuisineId.ParameterName = "@cuisine_id";
      prmCuisineId.Value = CuisineId;
      cmd.Parameters.Add(prmCuisineId);
      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }

    }

    public void Edit(string name, string address)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE restaurants SET name=@name, address=@address WHERE id=@id;";
      MySqlParameter prmName = new MySqlParameter();
      prmName.ParameterName = "@name";
      prmName.Value = name;
      cmd.Parameters.Add(prmName);
      MySqlParameter prmAddress = new MySqlParameter();
      prmAddress.ParameterName = "@address";
      prmAddress.Value = address;
      cmd.Parameters.Add(prmAddress);
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@id";
      prmId.Value = Id;
      cmd.Parameters.Add(prmId);
      cmd.ExecuteNonQuery();

      Name = name;
      Address = address;
      //CuisineId = cuisineId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText= @"DELETE FROM restaurants WHERE id = @id;";
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

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant)otherRestaurant;
        bool nameEquality = this.GetName().Equals(newRestaurant.GetName());
        bool addressEquality = this.GetAddress().Equals(newRestaurant.GetAddress());
        bool idEquality = this.GetId().Equals(newRestaurant.GetId());
        bool cuisineIdEquality = this.GetCuisineId().Equals(newRestaurant.GetCuisineId());
        return (nameEquality && addressEquality && idEquality && cuisineIdEquality);
      }
    }

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE id=@id;";
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@id";
      prmId.Value = id;
      cmd.Parameters.Add(prmId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string name = "";
      string address = "";
      int cuisineId = 0;
      while(rdr.Read())
      {
        name=rdr.GetString(1);
        address = rdr.GetString(2);
        cuisineId = rdr.GetInt32(3);
      }
      Restaurant newRestaurant = new Restaurant(name, address, cuisineId, id);
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }
  }
}
