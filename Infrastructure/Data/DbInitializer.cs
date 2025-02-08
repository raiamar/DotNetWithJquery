using Microsoft.Data.SqlClient;

public class DbInitializer
{
    public static void Initialize(string connectionString)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string[] scriptFiles = {
                "Infrastructure/Data/Database/01_CreateEmployeeTable.sql",
                // other sql
            };

            foreach (var scriptFile in scriptFiles)
            {
                try
                {
                    var script = File.ReadAllText(scriptFile);

                    using (var command = new SqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing {scriptFile}: {ex.Message}");
                }
            }
        }
    }
}
