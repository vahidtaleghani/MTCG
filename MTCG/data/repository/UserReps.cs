using MTCG.data;
using Npgsql;
using System;
using System.Collections.Generic;


namespace MTCG.repository
{
    public class UserReps
    {
        private NpgsqlConnection NpgsqlConn;
        private String username;
        private int coin;
        public UserReps()
        {
            this.NpgsqlConn = new NpgsqlConn().getnpgsqlConn();
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
                        Convert.ToInt32(dataReader["coin"])
                        );
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message); 
            }
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
                        Convert.ToInt32(dataReader["coin"])
                        ));
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return userlist;
        }
        public bool addUser(String username,String password)
        {
            string query = string.Format("INSERT INTO users (username, name ,password, token , bio , image) " +
                "VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}')",
                username, username, password, username + "-mtcgToken", null, null);

            string statquery = string.Format("INSERT INTO stats (username) " +"VALUES ('{0}')",username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlCommand statCommand = new NpgsqlCommand(statquery, this.NpgsqlConn);
                int dataReader = command.ExecuteNonQuery();
                int statDataReader = statCommand.ExecuteNonQuery();
                if (dataReader == 0 || statDataReader ==0)
                    return false;
                return true;
            }
            catch (Exception)
            {
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
                    return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return false;
        }
        public String getToken(String username, String password)
        {
            User user = getUser(username);
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
                return this.username;
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
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
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool updateCoinsUser(String username, int coins)
        {
            string query = string.Format("UPDATE users SET coin='{0}' WHERE username='{1}'", coins, username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                int dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int getCoinsByUsername(String username)
        {
            string query = string.Format("SELECT coin from users where username='{0}'", username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    this.coin = Convert.ToInt32(dataReader["coin"]);
                return this.coin;
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return -1;
        }
    }
}
