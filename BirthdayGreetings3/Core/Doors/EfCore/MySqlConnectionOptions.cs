namespace BirthdayGreetings3.Core.Doors.EfCore
{
    public class MySqlConnectionOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }

        public string ConnectionString()
        {
            return $"server={Host};port={Port};Database={DbName};Uid={User};Pwd={Password};";
        }
    }
}