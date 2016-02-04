using System;
using System.Linq;
using Cassandra;
using Newtonsoft.Json.Linq;

namespace csharp.Database {
   public class DbConfig {
       ISession session;
       
       public DbConfig() {
           // Connect to the demo keyspace on our cluster running at 127.0.0.1
           Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
           session = cluster.Connect("benchmark");
       }
    
       public void insertTransaction(dynamic data) {
           JObject json = JObject.FromObject(new {
                // generate UUID
                code = Guid.NewGuid(),
                tags = data.tags,
                values = data.values,
                address = data.address
           });
           
           
           string sql = "insert into transactions JSON '"+json.ToString()+"'";
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