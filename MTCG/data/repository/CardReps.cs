using MTCG.data;
using MTCG.repository.entity;
using Npgsql;
using System;
using System.Collections.Generic;

namespace MTCG.repository
{
    public class CardReps
    {
        private NpgsqlConnection NpgsqlConn;
        public CardReps()
        {
            this.NpgsqlConn = new NpgsqlConn().getNpgsqlConn();
        }
        public Card getCardById(String id)
        {
            string query = string.Format("SELECT * from cards where id='{0}'", id);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return new Card(
                        dataReader["id"].ToString(),
                        dataReader["name"].ToString(),
                        dataReader["element_type"].ToString(),
                        dataReader["card_type"].ToString(),
                        Convert.ToDouble(dataReader["damage"]),
                        Convert.ToInt32(dataReader["package_id"]),
                        dataReader["username"].ToString(),
                        Convert.ToBoolean(dataReader["deck"])
                        );
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return null;
        }
        public List<Card> getAllCardsByUsername(String username)
        {
            string query = string.Format("SELECT * from cards where username='{0}'", username);
            List<Card> cardlist = new List<Card>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    cardlist.Add(new Card(
                        dataReader["id"].ToString(),
                        dataReader["name"].ToString(),
                        dataReader["element_type"].ToString(),
                        dataReader["card_type"].ToString(),
                        Convert.ToDouble(dataReader["damage"]),
                        Convert.ToInt32(dataReader["package_id"]),
                        dataReader["username"].ToString(),
                        Convert.ToBoolean(dataReader["deck"])
                        ));
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return cardlist;
        }
        public bool addCard(String id, String name, String element_type , String card_type ,float damage, int packageId)
        {
            string query = string.Format("INSERT INTO cards (id, name ,element_type ,card_type ,damage, package_id , username , deck) " +
                "VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}','{6}','{7}')",
                id, name,element_type,card_type , damage, packageId, null, false);
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
        public int getLastpackageId()
        {
            string query = string.Format("Select package_id as package_id from cards GROUP BY package_id order by package_id desc limit 1");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return Convert.ToInt32(dataReader["package_id"]);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public bool updateUsernameOfCards(String username, int packageId)
        {
            string query = string.Format("UPDATE cards SET username='{0}' WHERE package_id='{1}'", username, packageId);
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
        public int getFirstFreePackageId()
        {
            string query = string.Format("SELECT package_id from cards where username = '' GROUP BY package_id order by package_id limit 1");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return Convert.ToInt32(dataReader["package_id"]);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public List<Card> getDeckByUsername(String username)
        {
            string query = string.Format("SELECT * from cards where username='{0}' and deck = true", username);
            List<Card> cardlist = new List<Card>();
            try
            {

                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    cardlist.Add(new Card(
                        dataReader["id"].ToString(),
                        dataReader["name"].ToString(),
                        dataReader["element_type"].ToString(),
                        dataReader["card_type"].ToString(),
                        Convert.ToDouble(dataReader["damage"]),
                        Convert.ToInt32(dataReader["package_id"]),
                        dataReader["username"].ToString(),
                        Convert.ToBoolean(dataReader["deck"])
                        ));
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
            return cardlist;
        }
        public bool updateDeckByUsername(String id, string username)
        {
            string query = string.Format("UPDATE cards SET deck=true WHERE username='{0}' and id='{1}'", username, id);
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
        public bool ControlCardByUsername(String id, string username)
        {
            string query = string.Format("SELECT * from cards WHERE username='{0}' and id='{1}' and deck =false", username, id);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read() ==false)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ControlCardBelongeToUsername(String id, string username)
        {
            string query = string.Format("SELECT * from cards WHERE username='{0}' and id='{1}'", username, id);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read() == false)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool updateDeckByUsernameAfterPlay(String id, string username)
        {
            string query = string.Format("UPDATE cards SET deck=false WHERE username='{0}' and id='{1}'", username, id);
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
        public bool updateCardByUsername(String id, string username)
        {
            string query = string.Format("UPDATE cards SET deck=false , username='{0}' where id='{1}'", username, id);
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
        public bool deleteCardsByUsername(String username)
        {
            string query = string.Format("DELETE from cards where username='{0}'", username);
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
    }
}
