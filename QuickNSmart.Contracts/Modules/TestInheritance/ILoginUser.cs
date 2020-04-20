//@QnSIgnore
namespace QuickNSmart.Contracts.Modules.TestInheritance
{
    public interface ILoginUser : ILogin, ICopyable<ILoginUser>
    {
        public string Name { get; set; }
        public Contracts.Modules.Common.State State { get; set; }
    }
}
