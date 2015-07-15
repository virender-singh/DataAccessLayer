using System.Data;
using System.Data.SqlClient;

public class DataAccessLayer
{
    private static string SqlConnectionString = "Integrated Security=SSPI;Persist Security Info=True;Initial Catalog=DummyDatabase;Data Source=.;Connect Timeout=200;";

    /// <summary>
    /// Replaces every parameter with its value from 2D array
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="parameters">Query parameters</param>
    /// <returns>Query with parameters value to be executed against SQL Server Database</returns>
    private static string SetParametersValue(string query, string[,] parameters)
    {
        for (int i = 0; i < parameters.Length / 2; i++)
        {
            if (!string.IsNullOrEmpty(parameters[i, 0]))
                query = query.Replace(parameters[i, 0], "'" + parameters[i, 1] + "'");
        }
        return query;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public static object ExecuteScalar(string sp, CommandType commandType)
    {
        SqlConnection con = new SqlConnection(SqlConnectionString);
        SqlCommand com = new SqlCommand(sp, con);
        object result = null;
        com.CommandType = commandType;
        try
        {
            con.Open();
            result = com.ExecuteScalar();
            con.Close();
        }
        catch (System.Exception ex)
        {
            //log the exception
        }
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static object ExecuteScalar(string sp, string[,] parameters, CommandType commandType)
    {
        SqlConnection con = new SqlConnection(SqlConnectionString);
        SqlCommand com = new SqlCommand(SetParametersValue(sp, parameters), con);
        object result = null;

        com.CommandType = commandType;
        for (int i = 0; i < parameters.Length / 2; i++)
        {
            com.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
        }
        try
        {
            con.Open();
            result = com.ExecuteScalar();
            con.Close();
        }
        catch (System.Exception ex)
        {
            //log the exception
        }
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public static SqlDataReader ExecuteQuery(string sp, CommandType commandType)
    {
        SqlConnection con = new SqlConnection(SqlConnectionString);
        SqlCommand com = new SqlCommand(sp, con);
        SqlDataReader reader = null;

        com.CommandType = commandType;
        try
        {
            con.Open();
            reader = com.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (System.Exception ex)
        {
            //log the exception
        }
        return reader;
    }
    /// <summary>
    /// FOR SQL
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static SqlDataReader ExecuteQuery(string sp, string[,] parameters, CommandType commandType)
    {
        SqlConnection con = new SqlConnection(SqlConnectionString);
        SqlCommand com = new SqlCommand(SetParametersValue(sp, parameters), con);
        SqlDataReader reader = null;

        com.CommandType = commandType;
        for (int i = 0; i < parameters.Length / 2; i++)
        {
            com.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
        }
        try
        {
            con.Open();
            reader = com.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (System.Exception ex)
        {
            //log the exception
        }
        return reader;
    }
    /// <summary>
    /// FOR SQL
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static void ExecuteNonQuery(string sp, CommandType commandType)
    {
        SqlConnection con = new SqlConnection(SqlConnectionString);
        SqlCommand com = new SqlCommand(sp, con);

        com.CommandType = CommandType.StoredProcedure;
        try
        {
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
        catch (System.Exception ex)
        {
            //log the exception
        }
    }
    /// <summary>
    /// FOR SQL
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static void ExecuteNonQuery(string sp, string[,] parameters, CommandType commandType)
    {
        SqlConnection con = new SqlConnection(SqlConnectionString);
        SqlCommand com = new SqlCommand(SetParametersValue(sp, parameters), con);

        com.CommandType = commandType;
        for (int i = 0; i < parameters.Length / 2; i++)
        {
            com.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
        }
        try
        {
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
        catch (System.Exception ex)
        {
            //log the exception
        }
    }
}