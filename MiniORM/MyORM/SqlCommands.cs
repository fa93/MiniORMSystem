using System.Data.SqlClient;
using System.Text;

namespace MyORM
{
    internal class SqlCommands
    {
        DefaultConnection defaultConnection = new();
        
        public void GetConnectionNonQuery(string sql)
        {
            var connectionString = defaultConnection.ConnectionString;
            using SqlConnection Connection = new(connectionString);
            using SqlCommand Command = Connection.CreateCommand();
            Command.CommandText = sql;
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();

            Command.ExecuteNonQuery();
        }

        public IList<IDictionary<string, object>> GetConnectionQuery(string sql)
        {
            var connectionString = defaultConnection.ConnectionString;
            using SqlConnection Connection = new(connectionString);
            using SqlCommand Command = Connection.CreateCommand();
            Command.CommandText = sql;
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();

            using SqlDataReader reader = Command.ExecuteReader();

            List<IDictionary<string, object>> data = new List<IDictionary<string, object>>();

            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                foreach (var col in reader.GetColumnSchema())
                {
                    row.Add(col.ColumnName, reader[col.ColumnName]);
                }
                data.Add(row);
            }

            return data;
        }

        public void ExecuteInsertOperation(IDictionary<string, object> dataSet, string tableName)
        {
            //var connectionString = GetConnection();
            var sql = new StringBuilder();
            if (dataSet != null && tableName != null)
            {
                sql = sql.Append("Insert into " + tableName + "(");
                var sqlValue = new StringBuilder();
                sqlValue.Append("Values(");
                var length = dataSet.Count;
                var i = 1;
                foreach (var data in dataSet)
                {
                    if (i != length)
                    {
                        sql = sql.Append(data.Key + ", ");

                        if (data.Value.GetType() == typeof(string) || data.Value.GetType() == typeof(DateTime))
                            sqlValue = sqlValue.Append("'" + data.Value + "', ");
                        else
                            sqlValue = sqlValue.Append(data.Value + ", ");
                    }
                    else
                    {
                        sql = sql.Append(data.Key + ") ");

                        if (data.Value.GetType() == typeof(string) || data.Value.GetType() == typeof(DateTime))
                            sqlValue = sqlValue.Append("'" + data.Value + "')");
                        else
                            sqlValue = sqlValue.Append(data.Value + ")");
                    }


                    i++;
                }
                sql = sql.Append(sqlValue);
                Console.WriteLine(sql.ToString());
                GetConnectionNonQuery(sql.ToString());
            }

        }

        public void ExecuteUpdateQuery(IDictionary<string, object> dataSet, string tableName)
        {
            var deleteQuery = new StringBuilder();
            // var sqlValue = new StringBuilder();
            deleteQuery.Append("Update " + tableName + " Set ");
            var condition = "";

            foreach (var data in dataSet)
            {
                if (data.Key == "Id")
                {
                    condition = " Where " + data.Key + " = " + data.Value;
                }
                else
                {
                    if (data.Value.GetType() == typeof(string) || data.Value.GetType() == typeof(DateTime))
                        deleteQuery = deleteQuery.Append(data.Key + " = '" + data.Value + "', ");
                    else
                        deleteQuery = deleteQuery.Append(data.Key + " = " + data.Value + ", ");
                }

            }

            deleteQuery.Remove(deleteQuery.Length - 2, 1);
            deleteQuery.Append(condition);
            Console.WriteLine(deleteQuery.ToString());
            GetConnectionNonQuery(deleteQuery.ToString());

        }

        public void ExecuteDeleteQuery(IDictionary<string, object> dataSet, string tableName)
        {
            var deleteQuery = new StringBuilder();

            deleteQuery.Append("Delete From " + tableName + " Where ");
            var count = dataSet.Count;
            foreach (var data in dataSet)
            {
                if (count == 2 && data.Key != "Id")
                    deleteQuery.Append(data.Key + " = " + data.Value);
                if (count == 1)
                    deleteQuery.Append(data.Key + " = " + data.Value);
            }


            Console.WriteLine(deleteQuery.ToString());

            GetConnectionNonQuery(deleteQuery.ToString());
        }

        public void CreateReadQuery(IDictionary<string, object> dataSet, string tableName)
        {
            var readQuery = new StringBuilder();
            readQuery.Append("Select * From " + tableName + " Where ");
            foreach (var data in dataSet)
            {
                readQuery.Append(data.Key + " = " + data.Value);
            }

            Console.WriteLine(readQuery.ToString());

            var result = GetConnectionQuery(readQuery.ToString());

            foreach (var key in result[0].Keys)
            {
                Console.Write($"{key} ");
            }
            Console.WriteLine();
            foreach (var row in result)
            {
                foreach (var value in row.Values)
                {
                    Console.Write($"{value} ");
                }
                Console.WriteLine();
            }
        }

        public void CreateReadQueryforAll(IList<string> TableNameList)
        {
            foreach (var tableName in TableNameList)
            {
                var readQuery = "Select * From " + tableName;
                Console.WriteLine($"==>{readQuery}\n");
                var result = GetConnectionQuery(readQuery);
                Console.WriteLine($"{tableName} :\n");
                foreach (var key in result[0].Keys)
                {
                    Console.Write($"{key} ");
                }
                Console.WriteLine();
                foreach (var row in result)
                {
                    foreach (var value in row.Values)
                    {
                        Console.Write($"{value} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
