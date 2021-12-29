using MTCG.data.entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.data.repository
{
    public class TradeReps
    {
        private NpgsqlConnection NpgsqlConn;
        public TradeReps()
        {
            this.NpgsqlConn = new NpgsqlConn().getnpgsqlConn();
        }
        public List<Trade> getAllCardInStore()
        {
            string query = string.Format("SELECT * from store where was_stored = false");
            List<Trade> userlist = new List<Trade>();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    userlist.Add(new Trade(
                        dataReader["username"].ToString(),
                        dataReader["id"].ToString(),
                        dataReader["card_trade_id"].ToString(),
                        dataReader["card_type"].ToString(),
                        Convert.ToDouble(dataReader["min_damage"]),
                        Convert.ToBoolean(dataReader["was_stored"])
                        ));
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return userlist;
        }
        public bool addCardToStore(String username, String id, String card_trade_id, String card_type, double min_damage)
        {
            string query = string.Format("INSERT INTO store (username, id ,card_trade_id ,card_type ,min_damage, was_stored) " +
                "VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}')",
                username, id, card_trade_id, card_type, min_damage, false);
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
        public Trade getTradeById(String id)
        {
            string query = string.Format("select * from store where id='{0}' and was_stored = false", id);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    return new Trade(
                        dataReader["username"].ToString(),
                        dataReader["id"].ToString(),
                        dataReader["card_trade_id"].ToString(),
                        dataReader["card_type"].ToString(),
                        Convert.ToDouble(dataReader["min_damage"]),
                        Convert.ToBoolean(dataReader["was_stored"])
                        );
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return null;
        }
        public bool updateStoredById(String id)
        {
            string query = string.Format("UPDATE store SET was_stored=true WHERE id='{0}'", id);
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
