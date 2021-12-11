using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.repository
{
    public class CardReps
    {

        private NpgsqlConnection NpgsqlConn;
        private String username;

        public CardReps()
        {
            this.NpgsqlConn = Database.NpgsqlConn;
            NpgsqlConn.Open();
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
            string query = string.Format("Select MAX(package_id) as package_id from cards GROUP BY package_id limit 1");
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
    }
}
