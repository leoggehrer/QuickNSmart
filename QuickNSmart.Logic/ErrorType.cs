//@QnSBaseCode
//MdStart
namespace QuickNSmart.Logic
{
    public enum ErrorType : int
    {
        InitAppAccess = 1,
        InvalidAccount = InitAppAccess * 2,
        NotLogedIn = InvalidAccount * 2,
        NotAuthorized = NotLogedIn * 2,
        InvalidId = NotLogedIn * 2,
        InvalidPageSize = InvalidId * 2,
        InvalidAuthorizationToken = InvalidPageSize * 2, 
        AuthorizationTimeOut = InvalidAuthorizationToken * 2,
        InvalidEmail = AuthorizationTimeOut * 2,
        InvalidPassword = InvalidEmail * 2,
    }
}
//MdEnd