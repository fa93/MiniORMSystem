using System.Collections;

namespace MyORM
{
    internal class ORM<T>
    {
        internal void Insert<T>(T item)
        {
            var type = item.GetType();
            var properties = type.GetProperties();
            var objectName = type.Name;
            Dictionary<string, object> data = new();

            if (typeof(T).IsClass)
            {
                foreach (var property in properties)
                {
                    if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string)
                        || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(float)
                        || property.PropertyType == typeof(DateTime))
                    {
                        data.Add(property.Name, property?.GetValue(item));
                    }
                    else if (property.PropertyType.IsGenericType || property.PropertyType.IsArray)
                    {
                        if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || property.PropertyType.IsArray)
                        {
                            var collection = property.GetValue(item) as IList;
                            foreach (var obj in collection)
                            {
                                Insert(obj);
                            }
                        }

                    }
                    else
                    {
                        Insert(property.GetValue(item));
                    }
                    //Console.WriteLine($"{property.Name} : {property.GetValue(item)}");
                }
                SqlCommands connection = new();
                connection.ExecuteInsertOperation(data, objectName);
            }

        }

        internal void Update<T>(T item)
        {
            var type = item.GetType();
            var properties = type.GetProperties();
            var objectName = type.Name;
            Dictionary<string, object> data = new();

            if (typeof(T).IsClass)
            {
                foreach (var property in properties)
                {
                    if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string)
                        || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(float)
                        || property.PropertyType == typeof(DateTime))
                    {
                        data.Add(property.Name, property.GetValue(item));
                    }
                    else if (property.PropertyType.IsGenericType || property.PropertyType.IsArray)
                    {
                        if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || property.PropertyType.IsArray)
                        {
                            var collection = property.GetValue(item) as IList;
                            foreach (var obj in collection)
                            {
                                Update(obj);
                            }
                        }

                    }
                    else
                    {
                        Update(property.GetValue(item));
                    }
                    //Console.WriteLine($"{property.Name} : {property.GetValue(item)}");
                }
                SqlCommands connection = new();
                connection.ExecuteUpdateQuery(data, objectName);
            }

        }

        internal void Delete<T>(T item)
        {
            var type = item.GetType();
            var properties = type.GetProperties();
            var objectName = type.Name;

            Dictionary<string, object> data = new();

            if (typeof(T).IsClass)
            {
                foreach (var property in properties)
                {
                    if (property.Name.Contains("Id"))
                    {
                        data.Add(property.Name, property.GetValue(item));
                    }
                    if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string) &&
                        property.PropertyType != typeof(decimal) && property.PropertyType != typeof(float)
                        && property.PropertyType != typeof(DateTime))
                    {
                        if (property.PropertyType.IsGenericType || property.PropertyType.IsArray)
                        {
                            if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || property.PropertyType.IsArray)
                            {
                                var collection = property.GetValue(item) as IList;
                                Delete(collection?[0]);
                            }
                        }
                        else

                            Delete(property.GetValue(item));
                    }
                }
            }
            SqlCommands connection = new();
            connection.ExecuteDeleteQuery(data, objectName);
        }

        internal void Delete(int id)
        {
            var type = typeof(T);
            var objectName = type.Name;
            Dictionary<string, object> data = new();

            data.Add(objectName, id);

        }

        internal void GetById(int id)
        {
            var type = typeof(T);
            var objectName = type.Name;
            Dictionary<string, object> data = new();

            data.Add("Id", id);

            SqlCommands connection = new();
            connection.CreateReadQuery(data, objectName);
        }

        internal void GetAll()
        {
            var data = GetTableName<T>(typeof(T));
            SqlCommands connection = new();
            connection.CreateReadQueryforAll(data);
        }

        private IList<string> GetTableName<T>(Type type)
        {

            // var type = typeof(T);
            var properties = type.GetProperties();
            var objectName = type.Name;
            List<string> data = new();
            if (!data.Contains(objectName))
                data.Add(objectName);
            foreach (var property in properties)
            {

                if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string) &&
                    property.PropertyType != typeof(decimal) && property.PropertyType != typeof(float)
                    && property.PropertyType != typeof(DateTime))
                {
                    if (property.PropertyType.IsGenericType || property.PropertyType.IsArray)
                    {
                        if (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || property.PropertyType.IsArray)
                        {
                            /*var collection = property.GetValue(item) as IList;
                            GetTableName(collection?[0]);*/
                            var getListType = property.PropertyType.GetGenericArguments()[0];
                            var value = GetTableName<T>(getListType);
                            foreach (var v in value)
                            {
                                if (!data.Contains(v))
                                    data.Add(v);
                            }



                        }
                    }
                    else
                    {
                        var value = GetTableName<T>(property.PropertyType);
                        foreach (var v in value)
                        {
                            if (!data.Contains(v))
                                data.Add(v);
                        }
                    }

                }
            }

            return data;

        }
    }
}
