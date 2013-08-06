using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;

namespace BusinessRules
{
    public class CUser
    {
        SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["strConn"].ConnectionString);

        /*c# code for hashing and salting - credit Dino Esposito at http://devproconnections.com/aspnet/aspnet-web-security-protect-user-passwords-hashing-and-salt?page=2 */

        public string hashPassword(string password, string salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public string getRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public bool validatePassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // Consider this function as an internal function where parameters like
            // storedHash and storedSalt are read from the database and then passed.

            var hash = hashPassword(enteredPassword, storedSalt);
            return String.Equals(storedHash, hash);
        }

        public void register(string username, string password, string role)
        {
            //generate unique salt and hashed pw for the user
            string salt = getRandomSalt(12);
            string hashedPassword = hashPassword(password, salt);

            //ado code to interact w/db
            objConn.Open();
            //string strSQL = "INSERT INTO Users (Username, Password, SaltString, Role) VALUES ('" +
            //    username + "','" + hashedPassword + "','" + salt + "','" + role + "');";

            //SqlCommand objCmd = new SqlCommand(strSQL, objConn);

            SqlCommand objCmd = new SqlCommand("spRegister", objConn);
            objCmd.CommandType = System.Data.CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Username", username);
            objCmd.Parameters.AddWithValue("@Password", hashedPassword);
            objCmd.Parameters.AddWithValue("@SaltString", salt);
            objCmd.Parameters.AddWithValue("@Role", role);
            objCmd.ExecuteNonQuery();

            objCmd.Dispose();
            objConn.Close();
        }

        public string login(string username, string password)
        {
            objConn.Open();
            string strSQL = "SELECT * FROM Users WHERE Username = '" + username + "'";
            SqlCommand objCmd = new SqlCommand(strSQL, objConn);

            SqlDataReader objRdr = objCmd.ExecuteReader();
            string role = "";

            //fill the datareader
           // objRdr = objCmd.ExecuteReader();

            while (objRdr.Read())
            {
                if (validatePassword(password, objRdr.GetString(2), objRdr.GetString(3))) {
                    role = objRdr.GetString(4);
                }
            }

            //clean up
            objCmd.Dispose();
            objConn.Close();
            return role;

        }

        public SqlDataReader getUsers()
        {
            objConn.Open();
            string strSQL = "SELECT * FROM Users";
            SqlCommand objCmd = new SqlCommand(strSQL, objConn);

            SqlDataReader objRdr = objCmd.ExecuteReader();
            return objRdr;
        }

        public void deleteUser(int UserID)
        {
            objConn.Open();
            string strSQL = "DELETE FROM Users WHERE UserID = " + UserID.ToString();

            SqlCommand objCmd = new SqlCommand(strSQL, objConn);
            objCmd.ExecuteNonQuery();

            objCmd.Dispose();
            objConn.Close();
        }
    }
}
