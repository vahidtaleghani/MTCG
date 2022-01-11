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
            this.NpgsqlConn = new NpgsqlConn().getNpgsqlConn();
        }
        public List<Trade> getAllCardInStore()
        {
            string query = string.Format("SELECT * from store where is_sold = false");
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
                        Convert.ToBoolean(dataReader["is_sold"])
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
            string query = string.Format("INSERT INTO store (username, id ,card_trade_id ,card_type ,min_damage, is_sold) " +
                "VALUES (@username,@id,@card_trade_id,@card_type,@min_damage,@is_sold)");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("card_trade_id", card_trade_id);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("card_type", card_type);
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("min_damage", min_damage);
                command.Parameters[4].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Double;
                command.Parameters.AddWithValue("is_sold", false);
                command.Parameters[5].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
            string query = string.Format("select * from store where id=@id and is_sold = false");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    return new Trade(
                        dataReader["username"].ToString(),
                        dataReader["id"].ToString(),
                        dataReader["card_trade_id"].ToString(),
                        dataReader["card_type"].ToString(),
                        Convert.ToDouble(dataReader["min_damage"]),
                        Convert.ToBoolean(dataReader["is_sold"])
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
            string query = string.Format("UPDATE store SET is_sold=true WHERE id=@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
        public bool deleteCardInStoredById(String id)
        {
            string query = string.Format("Delete from store WHERE id=@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
