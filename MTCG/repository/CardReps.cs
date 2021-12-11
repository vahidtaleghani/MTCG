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
            this.NpgsqlConn = Database.NpgsqlConn;
            NpgsqlConn.Open();
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
            this.NpgsqlConn.Close();
            return cardlist;
        }
        public bool addCard(String id, String name, float damage, int packageId)
        {
            string query = string.Format("INSERT INTO cards (id, name ,damage, package_id , username , deck) " +
                "VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}')",
                id, name, damage, packageId, null, false);
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
            catch (Exception)
            {
                this.NpgsqlConn.Close();
                return false;
            }
        }

        public int packageId()
        {
            string query = string.Format("Select package_id as package_id from cards GROUP BY package_id order by package_id desc limit 1");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    int packageId = Convert.ToInt32(dataReader["package_id"]);
                    this.NpgsqlConn.Close();
                    return packageId;  
                }
                this.NpgsqlConn.Close();
                return 0;
            }
            catch (Exception)
            {
                this.NpgsqlConn.Close();
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
                {
                    this.NpgsqlConn.Close();
                    return false;
                }
                this.NpgsqlConn.Close();
                return true;
            }
            catch (Exception)
            {
                this.NpgsqlConn.Close();
                return false;
            }
        }

        public List<int> getAllFreePackageId()
        {
            List<int> list = new List<int>();
            string query = string.Format("SELECT package_id from cards where username = '' GROUP BY package_id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    list.Add(Convert.ToInt32(dataReader["package_id"]));
                this.NpgsqlConn.Close();
                return list;
            }
            catch (Exception)
            {
                this.NpgsqlConn.Close();
                return null;
            }
        }
    }
}
