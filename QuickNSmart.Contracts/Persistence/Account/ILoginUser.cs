namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface ILoginUser : IIdentifiable, ICopyable<ILoginUser>
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        byte[] Avatar { get; set; }
        string AvatarMimeType { get; set; }
        State State { get; set; }
    }
}
