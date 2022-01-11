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
            this.NpgsqlConn = new NpgsqlConn().getNpgsqlConn();
        }
        public User getUser(String username)
        {
            string query = string.Format("SELECT * from users where username=@username");
            try
            {

                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("INSERT INTO users (username, name ,password, token) " +
                "VALUES (@username,@name, @password,@token)");

            string statquery = string.Format("INSERT INTO stats (username) " +"VALUES (@username)");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("name", username);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("password", password);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("token", username + "-mtcgToken");
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                NpgsqlCommand statCommand = new NpgsqlCommand(statquery, this.NpgsqlConn);
                statCommand.Parameters.AddWithValue("username", username);
                statCommand.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                int dataReader = command.ExecuteNonQuery();
                int statDataReader = statCommand.ExecuteNonQuery();
                if (dataReader == 0 || statDataReader ==0)
                    return false;
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return false;
            }
        }
        public bool deleteUser(String username)
        {
            string query = string.Format("DELETE from users where username=@username");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("SELECT * from users where token=@token");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("token", token);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("UPDATE users SET name=@name ,bio=@bio , image=@image WHERE username=@username");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("name", name);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("bio", bio);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("image", image);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("username", username);
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("UPDATE users SET coin=@coin WHERE username=@username");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("coin", coins);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                command.Parameters.AddWithValue("username", username);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("SELECT coin from users where username=@username", username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
