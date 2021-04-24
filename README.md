![alt text](https://github.com/scyllanet/scyllanet/blob/main/scylla_net.jpg?raw=true)

# Scylla.Net

Is a fork of the DataStax C# Driver for Apache Cassandra, developed by DataStax.

Scylla.Net is the open source .NET data provider for ScyllaDB.

## Sample Basic

```csharp
var cluster = Cluster
    .Builder()
    .AddContactPoints("host1", "host2")
    .Build();
    
var connection = cluster.Connect("your_keyspace");
var rows = connection.Execute("SELECT id, description FROM your_table");

foreach (var row in rows)
{
  var id = row.GetValue<int>("id");
  var description = row.GetValue<string>("description");
}
```
