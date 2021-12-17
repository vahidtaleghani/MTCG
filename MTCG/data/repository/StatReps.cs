using MTCG.data;
using MTCG.repository.entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.repository
{
    public class StatReps
    {
        private NpgsqlConnection NpgsqlConn;
        public StatReps()
        {
            this.NpgsqlConn = new NpgsqlConn().getnpgsqlConn();
        }
        public bool addStat(String username)
        {
            string statquery = string.Format("INSERT INTO stats (username) " + "VALUES ('{0}')", username);
            try
            {
                NpgsqlCommand statCommand = new NpgsqlCommand(statquery, this.NpgsqlConn);
                int statDataReader = statCommand.ExecuteNonQuery();
                if (statDataReader == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Stat getStatsByUsername(String username)
        {
            string query = string.Format("select * from stats where username='{0}'", username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return new Stat(
                        dataReader["username"].ToString(),
                        Convert.ToInt32(dataReader["elo"]),
                        Convert.ToInt32(dataReader["win"]),
                        Convert.ToInt32(dataReader["lose"]),
                        Convert.ToInt32(dataReader["draw"])
                        );
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return null;
        }
    }
}
