using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SQLAccess
/// </summary>
public static class SqlAccess
{
    static string connGDM = ConfigurationManager.ConnectionStrings["ConnectionEvent"].ConnectionString;

    #region Insert
    public static bool RegisterUser(string firstName, string lastName, string username, string password, string email, DateTime bDate, bool alert)
    {
        SqlConnection SqlCon = new SqlConnection(connGDM);
        bool ans = true;
        // Map SQL param names to C# param names
        SqlParameter sql_firstName = new SqlParameter("FirstName", firstName);
        SqlParameter sql_password = new SqlParameter("Password", password);
        SqlParameter sql_LastName = new SqlParameter("LastName", lastName);
        SqlParameter sql_username = new SqlParameter("Username", username);
        SqlParameter sql_email = new SqlParameter("Email", email);
        SqlParameter sql_bDate = new SqlParameter("bDate", bDate);
        SqlParameter sql_alert = new SqlParameter("Alert", alert);

        string query = "INSERT INTO Users (firstName,lastName,email,hasAllerts,username,password,birthDate) VALUES (@FirstName, @LastName, @Email,@Alert,@Username,@Password,@bDate)";
        SqlCommand cmd = new SqlCommand(query, SqlCon);

        // Add Params to the query string
        cmd.Parameters.Add(sql_firstName);
        cmd.Parameters.Add(sql_LastName);
        cmd.Parameters.Add(sql_username);
        cmd.Parameters.Add(sql_email);
        cmd.Parameters.Add(sql_bDate);
        cmd.Parameters.Add(sql_password);
        cmd.Parameters.Add(sql_alert);
        try
        {
            SqlCon.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        {
            ans = false;
        }
        finally { SqlCon.Close(); }
        return ans;
    }
    #endregion

    #region Update
    #endregion

    #region Delete
    #endregion

    #region Select
    public static List<string> GetUserTags(string username)
    {
        List<string> tags = new List<string>();
        SqlParameter sqlTag = new SqlParameter("ID", username);

        using (SqlConnection connection = new SqlConnection(connGDM))
        {
            const string query =
                "SELECT * FROM HashTags,hashTagsOfUsers,Users Where Users.id=hashTagsOfUsers.userId AND HashTags.id=hashTagsOfUsers.tagId AND Users.id=@ID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(sqlTag);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {


                        tags.Add(reader["hashtag"].ToString());

                    }
                }
                connection.Close();
            }
        }

        return tags;

    } 
    #endregion

}