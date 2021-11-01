using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;

namespace LoginScreen

{



    public class DB
    {
        private NpgsqlConnection connection;
        public void Connection()
        {
            connection = new NpgsqlConnection("SERVER = localhost;Port = 5432; DATABASE = login;   UID = postgres; PWD = 0203");
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Close();
        }

        public NpgsqlDataReader Reader(string query)
        {
            Connection();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            NpgsqlDataReader myReader;
            myReader = command.ExecuteReader();
            return myReader;

        }

        public void ExecuteQuery(string query)
        {
            Connection();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteReader();

        }
       

        public DataSet ListDatas(string table)
        {
            Connection();
            string query = $"Select * from {table}";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }
}


