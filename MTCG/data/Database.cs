using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.data;
using Npgsql;

namespace MTCG.repository
{
    //Verbindung aufbauen
    //singleton klasse (kontrollieren die Verbindung nur einmal existiert) ??? 
    public class Database
    {
        private static String USERID = "postgres";
        private static String PASSWORD = "test";
        private static int PORT = 5432;
        private static String HOSTNAME = "localhost";
        private static String DATABASE_NAME = "mtcg";

        public static NpgsqlConnection NpgsqlConn { get; set; }

        private Database()
        {
            connect();
        }
        private static Database _instance;
        // static -> wir gereifen ohne objekt 
        public static Database GetInstance()
        {
            if(_instance == null)
                _instance = new Database();
            return _instance;
        }
        private void connect()
        {
           
            string strConn = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", HOSTNAME, PORT, USERID, PASSWORD, DATABASE_NAME);
            try
            {
                NpgsqlConn = new NpgsqlConnection(strConn);
                Console.WriteLine("Connected to Database");
                
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message); 
            }
            
        }

        //Löscht alle Daten. Die Datenbank ist nach dem Aufrufen dieser Methode leer
        public void truncateUsersTable()
        {
            String query = "truncate table users cascade";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, new NpgsqlConn().getNpgsqlConn());
                NpgsqlDataReader dataReader = command.ExecuteReader();
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
        }
        public void truncateCardsTable()
        {
            String query = "truncate table cards cascade";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, new NpgsqlConn().getNpgsqlConn());
                NpgsqlDataReader dataReader = command.ExecuteReader();
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
            }
        }
    }
}
