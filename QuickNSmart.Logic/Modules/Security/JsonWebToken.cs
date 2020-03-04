﻿//@QnSBaseCode
//MdStart

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommonBase.Extensions;

namespace QuickNSmart.Logic.Modules.Security
{
    internal partial class JsonWebToken
    {
        static JsonWebToken()
        {
            ClassConstructing();
            if (Secret.IsNullOrEmpty())
            {
                Secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";
            }
            if (Key.IsNullOrEmpty())
            {
                Key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            }
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        internal static string Secret { get; set; }
        internal static string Key { get; set; }
        internal static string Issuer { get; set; } = nameof(QuickNSmart);
        internal static string Audience { get; set; } = nameof(Logic);

        internal static string GenerateToken(IEnumerable<Claim> claimsParam)
        {
            claimsParam.CheckArgument(nameof(claimsParam));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: Issuer,
                audience: Audience,
                claims: claimsParam,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30));

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(secToken);
        }
        public static bool CheckJsonWebToken(string token)
        {
            SecurityToken validatedToken;

            return CheckToken(token, out validatedToken);
        }
        internal static bool CheckToken(string token, out SecurityToken validatedToken)
        {
            var result = false;
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
        internal static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)) // The same key as the one that generate the token
            };
        }
    }
}
//MdEnd