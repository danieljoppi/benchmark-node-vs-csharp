# C# Application

## .NET required 

  * [aspnet](https://get.asp.net/)
  * DNX for .NET Core: `dnvm upgrade -r coreclr`
  * Windows only: 
    * DNX for the .NET Framerowk: `dnvm upgrade -r clr`
  * Mac or Linux:
    * DNX for Mono: `dnvm upgrade -r mono`
  * Show the DNX versions installed: `dnvm list`
  
  
### Compile and Run

```bash
dnu restore
dnu build
dnx web
```

## Database require

  * [Cassandra](http://cassandra.apache.org/)

### Configure Cassanda

To create the keyspace `csharp`, at the CQL shell prompt, type:
```sql
CREATE KEYSPACE csharp WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 };
```

To use the keyspace we’ve just created, type:
```sql
USE csharp;
```

Create a `address` table within the keyspace `csharp`:
```sql
CREATE TYPE address (number int, street text, city text, state text);
```

Create a `transactions` table within the keyspace `csharp`:
```sql
CREATE TABLE transactions (code text, address frozen<address>, tags list<text>, values set<int>, PRIMARY KEY (code));
```

Create a `tags` table within the keyspace `csharp`:
```sql
CREATE TABLE tags (key text, value int, PRIMARY KEY (key));

INSERT INTO tags JSON '{"value": 11, "key": "legal"}';
INSERT INTO tags JSON '{"value": 11, "key": "opa"}';
INSERT INTO tags JSON '{"value": 15, "key": "joinha"}';
INSERT INTO tags JSON '{"value": 19, "key": "humm"}';
``