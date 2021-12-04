using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.repository
{
    public class UserDao
    {

        public User getUser(String username)
        {
            NpgsqlConnection NpgsqlConn = Database.NpgsqlConn;
            string query = string.Format("SELECT * from users where username='{0}'", username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, NpgsqlConn);
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
            return null;
        }
        /*
        public List<User> getAll()
        {

        }

        public bool delete(String username)
        {

        }

        public bool update()
        {

        }
        */
    }
}
