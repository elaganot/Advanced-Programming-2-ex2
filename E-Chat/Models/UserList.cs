namespace E_Chat.Models
{
    public class UserList
    {
        public int Id { get; set; }
        public static List<User> Users {
            get { return Users; }
            set { Users.Add(new User() { UserName = "eden", Name = "Eden Hamami", Password = "a123456" }); }

        }
    }
}
