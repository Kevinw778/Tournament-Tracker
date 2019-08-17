using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataSource Source { get; private set; }

        public static void InitializeSources(DatabaseType db)
        {
            switch (db)
            {
                case DatabaseType.Sql:
                    SqlConnector sql = new SqlConnector();
                    Source = sql;
                    break;
                case DatabaseType.TextFile:
                    TextConnector text = new TextConnector();
                    Source = text;
                    break;
                default:
                    break;
            }
            //if (db == DatabaseType.Sql)
            //{
            //    SqlConnector sql = new SqlConnector();
            //    Source = sql;
            //}

            //else if (connectionType == "text")
            //{
            //    TextConnector text = new TextConnector();
            //    Source = text;
            //}
        }

        public static string ConnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
