using Npgsql;
using System;
using System.Collections.Generic;


namespace MTCG.repository
{
    public class UserReps
    {
        private NpgsqlConnection NpgsqlConn;
        private String username;
   
        public UserReps()
        {
            this.NpgsqlConn = Database.NpgsqlConn;
            NpgsqlConn.Open();
        }

        public User getUser(String username)
        {
            string query = string.Format("SELECT * from users where username='{0}'", username);
            try
            {

                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return new User(
                        dataReader["username"].ToString(),
                        dataReader["name"].ToString(),
                        dataReader["password"].ToString(),
                        dataReader["token"].ToString(),
                        dataReader["bio"].ToString(),
                        dataReader["image"].ToString(),
                        Convert.ToInt32(dataReader["elo"]),
                        Convert.ToInt32(dataReader["coin"])
                        );
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message); 
            }
            this.NpgsqlConn.Close();
            return null;
        }
        
        public List<User> getAllUsers()
        {
            string query = string.Format("SELECT * from users");
            List<User> userlist = new List<User>();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    userlist.Add(new User(
                        dataReader["username"].ToString(),
                        dataReader["name"].ToString(),
                        dataReader["password"].ToString(),
                        dataReader["token"].ToString(),
                        dataReader["bio"].ToString(),
                        dataReader["image"].ToString(),
                        Convert.ToInt32(dataReader["elo"]),
                        Convert.ToInt32(dataReader["coin"])
                        ));

                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                throw;
            }
            this.NpgsqlConn.Close();
            return userlist;
        }

        public bool addUser(String username,String password)
        {
            string query = string.Format("INSERT INTO users (username, name ,password, token , bio , image, elo , coin) " +
                "VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}','{6}','{7}')",
                username, username, password, username + "-mtcgToken", null, null, 100 , 20);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                int dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                {
                    this.NpgsqlConn.Close();
                    return false;
                }
                this.NpgsqlConn.Close();
                return true;
            }
            catch (Exception)
            {
                this.NpgsqlConn.Close();
                return false;
            }
        }
        
        public bool deleteUser(String username)
        {
            string query = string.Format("DELETE from users where username='{0}'", username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                int dataReader = command.ExecuteNonQuery();

                if (dataReader != 0)
                {
                    this.NpgsqlConn.Close();
                    return true;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            this.NpgsqlConn.Close();
            return false;
        }
        
        public String getToken(String username, String password)
        {
            User user = getUser(username);
            this.NpgsqlConn.Close();
            if (user == null)
                return null;
            if (password != user.password)
                return "Password is Wrong";
            return user.token;

        }

        public String getUsernameByToken(String token)
        {
            string query = string.Format("SELECT * from users where token='{0}'", token);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    this.username = dataReader["username"].ToString();
                this.NpgsqlConn.Close();
                return this.username;
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            this.NpgsqlConn.Close();
            return null;
        }
      
        public bool updateUser(String username,String name, String bio, String image)
        {
            string query = string.Format("UPDATE users SET name='{0}' ,bio='{1}' , image='{2}' WHERE username='{3}'",name, bio, image, username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                int dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                {
                    this.NpgsqlConn.Close();
                    return false;
                }
                this.NpgsqlConn.Close();
                return true;
            }
            catch (Exception)
            {
                this.NpgsqlConn.Close();
                return false;
            }
        }
        
    }
}
