using System;
using System.Linq;
using Cassandra;

namespace csharp.Database {
   public class DbConfig {
       ISession session;
       
       public DbConfig() {
           // Connect to the demo keyspace on our cluster running at 127.0.0.1
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            session = cluster.Connect("csharp");
       }
    
        public void insert(dynamic data) {
            string sql = "insert into transactions JSON '"+data.ToString()+"'";
            Console.Write(sql);
            
            session.Execute(sql);
        }   
   }
}