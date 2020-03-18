﻿//@QnSBaseCode
//MdStart

namespace QuickNSmart.AspMvc.Modules.Session
{
    partial interface ISessionWrapper
    {
        #region Translator
        string Translate(string text);
        string Translate(string text, string defaultValue);
        #endregion Translator

        #region General
        bool HasValue(string key);
        void Remove(string key);
        #endregion General

        #region Object-Access
        void SetValue(string key, object value);
        object GetValue(string key);
        #endregion Object-Access

        #region Int-Access
        void SetIntValue(string key, int value);
        int GetIntValue(string key);
        #endregion Int-Access

        #region String-Access
        void SetStringValue(string key, string value);
        string GetStringValue(string key);
        #endregion String-Access

        #region Properties
        string ReturnUrl { get; set; }
        string Hint { get; set; }
        string Error { get; set; }
        #endregion Properties

        #region Authentication
        Models.Persistence.Account.LoginSession LoginSession { get; set; }
        string SessionToken { get; }
        #endregion Authentication
    }
}
//MdEnd