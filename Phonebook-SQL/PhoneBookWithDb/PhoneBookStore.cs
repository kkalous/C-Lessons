﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public interface IPhoneBookStore
    {
        Dictionary<string, string> GetContactList();
        Task WriteContactAsync(string name, string number);
        Task DeleteContactAsync(string name, string number);
        Task UpdateContactAsync(string name, string number);
    }

    public class PhoneBookStore : IPhoneBookStore
    {

        private readonly string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public async Task WriteContactAsync(string name, string number)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @$"insert into contacts(contact_name, contact_number) values ('{name}',{number});";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                conn.Close();
            }
        }

        public async Task DeleteContactAsync(string name, string number)
        {
            
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = null;
                if (number == null)
                {
                    query = @$"delete from contacts where contact_name = '{name}';";              ;
                }
                else if (name == null)
                {
                    query = @$"delete from contacts where contact_number = {number};";
                }
                else
                {
                    query = @$"delete from contacts where contact_name = '{name}' and contact_number = {number};";
                }


                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                conn.Close();


            }
        }
       
        public async Task UpdateContactAsync(string name, string number)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @$"update contacts set contact_number = {number} where contact_name = '{name}';";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                conn.Close();
            }
        }

        public  Dictionary<string, string> GetContactList()
        {
            try
            {
                var contactsList = new Dictionary<string, string>();

                //sql connection object
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"select * from dbo.contacts;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //open connection
                    conn.Open();
                    //execute the SQLCommand
                    SqlDataReader dr =  cmd.ExecuteReader();                

                    while (dr.Read())
                    {
                        contactsList.Add(dr.GetString(0), dr.GetDecimal(1).ToString());
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return contactsList;
                }
            }
            catch (Exception ex)
            {
                //display error message
                throw ex;
            }


        }
    }

}

