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
            string query = string.Format("SELECT * from cards where id=@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("SELECT * from cards where username=@username");
            List<Card> cardlist = new List<Card>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
        public bool addCard(String id, String name, String element_type , String card_type ,double damage, int packageId)
        {
            string query = string.Format("INSERT INTO cards (id, name ,element_type ,card_type ,damage, package_id, username) " +
                "VALUES (@id,@name, @element_type,@card_type,@damage,@package_id, @username)");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("name", name);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("element_type", element_type);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("card_type", card_type);
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("damage", damage);
                command.Parameters[4].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Double;
                command.Parameters.AddWithValue("package_id", packageId);
                command.Parameters[5].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                command.Parameters.AddWithValue("username", "");
                command.Parameters[6].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                int dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                    return false;
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
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
            string query = string.Format("UPDATE cards SET username=@username WHERE package_id=@package_id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("package_id", packageId);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
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
            string query = string.Format("SELECT * from cards where username=@username and deck = true");
            List<Card> cardlist = new List<Card>();
            try
            {

                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("UPDATE cards SET deck=true WHERE username=@username and id=@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
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
        public bool ControlCardByUsername(String id, string username)
        {
            string query = string.Format("SELECT * from cards WHERE username=@username and id=@id and deck =@deck");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("deck", false);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
            string query = string.Format("SELECT * from cards WHERE username=@username and id=@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            string query = string.Format("UPDATE cards SET deck=@deck WHERE username=@username and id=@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("deck", false);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
            string query = string.Format("UPDATE cards SET deck=@deck , username=@username where id=@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("deck", false);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
            string query = string.Format("DELETE from cards where username=@username");
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
    }
}
