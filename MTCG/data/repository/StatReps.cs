using MTCG.data;
using MTCG.data.entity;
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
        public StatsUser getStatsUserByUsername(String username)
        {
            string query = string.Format("SELECT users.username, users.name, users.bio, users.image, users.coin , " +
                "stats.elo, stats.win, stats.lose, stats.draw, sum(stats.win + stats.lose + stats.draw) as sumPlay " +
                "FROM stats INNER JOIN users ON stats.username = users.username " +
                "where users.username='{0}' " +
                "group by users.username, users.name, users.bio, users.image, users.coin, stats.elo, stats.win, stats.lose, stats.draw",
                username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return new StatsUser(
                        dataReader["username"].ToString(),
                        dataReader["name"].ToString(),
                        dataReader["bio"].ToString(),
                        dataReader["image"].ToString(),
                        Convert.ToInt32(dataReader["coin"]),
                        Convert.ToInt32(dataReader["elo"]),
                        Convert.ToInt32(dataReader["win"]),
                        Convert.ToInt32(dataReader["lose"]),
                        Convert.ToInt32(dataReader["draw"]),
                        Convert.ToInt32(dataReader["sumPlay"])
                        );
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return null;
        }
        public List<StatsUser> getStatsAllUsers()
        {
            string query = string.Format("SELECT users.username, users.name, users.bio, users.image, users.coin , " +
                "stats.elo, stats.win, stats.lose, stats.draw, sum(stats.win + stats.lose + stats.draw) as sumPlay " +
                "FROM stats INNER JOIN users ON stats.username = users.username " +
                "where users.username !='admin' " +
                "group by users.username, users.name, users.bio, users.image, users.coin, stats.elo, stats.win, stats.lose, stats.draw " +
                "order by stats.elo DESC");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                List<StatsUser> allUsersStats = new List<StatsUser>();
                while (dataReader.Read())
                    allUsersStats.Add(new StatsUser(
                        dataReader["username"].ToString(),
                        dataReader["name"].ToString(),
                        dataReader["bio"].ToString(),
                        dataReader["image"].ToString(),
                        Convert.ToInt32(dataReader["coin"]),
                        Convert.ToInt32(dataReader["elo"]),
                        Convert.ToInt32(dataReader["win"]),
                        Convert.ToInt32(dataReader["lose"]),
                        Convert.ToInt32(dataReader["draw"]),
                        Convert.ToInt32(dataReader["sumPlay"])
                        ));
                return allUsersStats;
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return null;
        }
        public bool updateStatWinnerByUsernameAfterPlay(String username)
        {
            string query = string.Format("UPDATE stats SET elo=elo+3, win = win+1 where username='{0}'", username);
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
        public bool updateStatLoserByUsernameAfterPlay(String username)
        {
            string query = string.Format("UPDATE stats SET elo=elo-5 , lose = lose+1 where username='{0}'", username);
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
        public bool updateStatDrawByUsernameAfterPlay(String username)
        {
            string query = string.Format("UPDATE stats SET draw=draw+1 where username='{0}'", username);
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
    }
}
