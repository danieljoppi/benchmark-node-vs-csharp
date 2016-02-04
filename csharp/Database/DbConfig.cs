using System;
using System.Linq;
using Cassandra;

namespace csharp.Database {
   public class DbConfig {
       ISession session;
       
       public DbConfig() {
           // Connect to the demo keyspace on our cluster running at 127.0.0.1
           Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
           session = cluster.Connect("benchmark");
       }
    
       public void insertTransaction(dynamic data) {
           // generate UUID
           data.code = Guid.NewGuid();
           
           string sql = "insert into transactions JSON '"+data.ToString()+"'";
           //Console.Write(sql);
            
           session.Execute(sql);
       }
        
       public int findTag(string key) {
           string sql = "select value from tags where key = '"+key+"'";
           //Console.Write(sql);
           Row result = session.Execute(sql).First();
           
           string svalue = result["value"].ToString();
           int value1 = 0;
           if (int.TryParse(svalue, out value1)) {
               return value1;
           } else {
               return -1;
           }
       }
   }
}