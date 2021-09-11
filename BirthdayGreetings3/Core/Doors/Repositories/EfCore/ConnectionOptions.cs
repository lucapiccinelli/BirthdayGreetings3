namespace BirthdayGreetings3.Core.Doors.Repositories.EfCore
{
    public class ConnectionOptions
    {
        public string Host { get; }
        public int Port { get; }
        public string DbName { get; }
        public string User { get; }
        public string Password { get; }

        public ConnectionOptions(string host, int port, string dbName, string user, string password)
        {
            Host = host;
            Port = port;
            DbName = dbName;
            User = user;
            Password = password;
        }

        public string ConnectionString()
        {
            return $"server={Host};port={Port};Database={DbName};Uid={User};Pwd={Password};";
        }
    }
}