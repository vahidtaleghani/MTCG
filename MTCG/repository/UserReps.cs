using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.repository
{
    public class UserReps
    {

        private NpgsqlConnection NpgsqlConn;
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
                        dataReader["image"].ToString()
                        );
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message); 
                throw;
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
                        dataReader["image"].ToString()
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
            string query = string.Format("INSERT INTO users (username, name ,password, token , bio , image) " +
                "VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}')",
                username, username, password, username + "-mtcgToken", null, null);
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
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                throw;
            }
        }
        
        public bool deleteUser(String username)
        {
            string query = string.Format("DELETE from users where username='{0}'", username);
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
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                throw;
            }   
        }
        /*
        public bool update()
        {

        }
        */
    }
}
