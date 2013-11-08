namespace BasicCommand
{
    public class User
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string Username { get;set; }
        public string LastName { get;set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", Id, Name, LastName, Username);
        }
    }
}