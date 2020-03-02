﻿//@QnSBaseCode
//MdStart
namespace QuickNSmart.Logic
{
    public enum ErrorType : int
    {
        InitAppAccess = 1,
        InvalidAccount = InitAppAccess * 2,
        NotLogedIn = InvalidAccount * 2,
        InvalidId = NotLogedIn * 2,
        InvalidPageSize = InvalidId * 2,
        InvalidAuthorizationToken = InvalidPageSize * 2, 
        AuthorizationTimeOut = InvalidAuthorizationToken * 2,
        NotAuthorized = AuthorizationTimeOut * 2,
    }
}
//MdEnd