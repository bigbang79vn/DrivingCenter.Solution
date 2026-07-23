namespace DrivingCenter.App.DTO.Account
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }

        public Account(string user, string pass, string displayName)
        {
            this.Username = user.ToString();
            this.Password = pass.ToString();
            this.DisplayName = displayName;
        }

        public override string ToString()
        {
            return Username + "|" + Password + "|" + DisplayName;
        }
    }
}
