# benchmark-node-vs-csharp
Node.js vs C# Performance Benchmarks

## Database require

  * [Cassandra](http://cassandra.apache.org/)

### Configure Cassanda

To create the keyspace `benchmark`, at the CQL shell prompt, type:
```sql
CREATE KEYSPACE benchmark WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 };
```

To use the keyspace we’ve just created, type:
```sql
USE benchmark;
```

Create a `address` type within the keyspace `benchmark`:
```sql
CREATE TYPE address (number int, street text, city text, state text);
```

Create a `transactions` table within the keyspace `benchmark`:
```sql
CREATE TABLE transactions (code text, address frozen<address>, tags list<text>, values set<int>, PRIMARY KEY (code));
```

Create a `tags` table within the keyspace `benchmark`:
```sql
CREATE TABLE tags (key text, value int, PRIMARY KEY (key));

INSERT INTO tags JSON '{"value": 11, "key": "legal"}';
INSERT INTO tags JSON '{"value": 12, "key": "opa"}';
INSERT INTO tags JSON '{"value": 15, "key": "joinha"}';
INSERT INTO tags JSON '{"value": 19, "key": "humm"}';
```