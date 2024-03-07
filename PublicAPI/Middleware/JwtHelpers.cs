using Contracts.Security;
using Domain.Entities.Acquisition;
using Domain.Entities.RoleManagement;
using Domain.Entities.UserManagement;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PublicAPI.Middleware
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetLoginClaims(this UserTokens userAccounts, dynamic userInfoModel)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Sid, userAccounts.Id.ToString()), //
                    new Claim(ClaimTypes.Name, userAccounts.UserName), //userAccounts.UserName
                    new Claim(ClaimTypes.Email, userAccounts.EmailId), //userAccounts.EmailId
                    new Claim(ClaimTypes.MobilePhone, userAccounts.MobileNumber), //Mobile number
                    new Claim(ClaimTypes.NameIdentifier, userAccounts.Id.ToString()), //userAccounts.Id.ToString()
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, IRoleService roleService, int roleId)
        {
            var roleResult = roleService.GetByIdAsync(roleId).Result;

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Sid, userAccounts.Id.ToString()),
                    new Claim(ClaimTypes.Name, userAccounts.UserName),
                    new Claim(ClaimTypes.Email, userAccounts.EmailId),
                    new Claim(ClaimTypes.NameIdentifier, userAccounts.Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };

            if (roleResult?.Data!= null)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleResult?.Data.RoleName));

            }


            return claims;
        }
        public static UserTokens GenLoginTokenkey(UserTokens model, JwtSettings jwtSettings, dynamic userInfoModel)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer,
                                                audience: jwtSettings.ValidAudience,
                                                claims: GetLoginClaims(model, userInfoModel),
                                                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                                                expires: new DateTimeOffset(expireTime).DateTime,
                                                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.UserName = model.UserName;
                UserToken.Id = model.Id;
                UserToken.GuidId = Id;
                UserToken.OrgId = userInfoModel.Data.OrgId;
                UserToken.OrgName = userInfoModel.Data.OrgName;
                UserToken.OrgType = userInfoModel.Data.OrgType;
                UserToken.ParentorgId = userInfoModel.Data.ParentorgId;
                UserToken.ParentOrgName = userInfoModel.Data.ParentOrgName;
                UserToken.Status = userInfoModel.Data.Status;
                UserToken.RoleId = userInfoModel.Data.RoleId;
                UserToken.UserName = userInfoModel.Data.UserName;
                UserToken.FirstName = userInfoModel.Data.FirstName;
                UserToken.MiddleName = userInfoModel.Data.MiddleName;
                UserToken.LastName = userInfoModel.Data.LastName;
                UserToken.EmailId = userInfoModel.Data.EmailId;
                UserToken.MobileNumber = userInfoModel.Data.MobileNumber;
                UserToken.InfiniteCreditLimit = userInfoModel.Data.InfiniteCreditLimit;
                UserToken.CreditLimit = userInfoModel.Data.CreditLimit;
                UserToken.ProductId = userInfoModel.Data.ProductId;
                //UserToken.LastloginDate = userInfoModel.Data.LastloginDate;
                UserToken.UserId = userInfoModel.Data.UserId;
                UserToken.UserName = userInfoModel.Data.UserName;

                //UserToken.o
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings, IRoleService roleService, int roleId)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: GetClaims(model, roleService, roleId), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.UserName = model.UserName;
                UserToken.Id = model.Id;
                UserToken.GuidId = Id;
                UserToken.OrgId = model.OrgId;
                UserToken.Status = model.Status;
                UserToken.RoleId = model.RoleId;
                UserToken.FirstName = model.FirstName;
                UserToken.MiddleName = model.MiddleName;
                UserToken.LastName = model.LastName;
                UserToken.MobileNumber = model.MobileNumber;
                UserToken.InfiniteCreditLimit = model.InfiniteCreditLimit;
                UserToken.UserId = model.UserId;
                return UserToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings, dynamic userInfoModel)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var refreshToken = GenerateRefreshToken();
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer,
                                                audience: jwtSettings.ValidAudience,
                                                claims: GetClaims(model, userInfoModel),
                                                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                                                expires: new DateTimeOffset(expireTime).DateTime,
                                                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.RefreshToken = refreshToken;
                UserToken.UserName = model.UserName;
                UserToken.Id = model.Id;
                UserToken.GuidId = Id;
                UserToken.OrgId = userInfoModel.Data.OrgId;
                UserToken.OrgName = userInfoModel.Data.OrgName;
                UserToken.OrgType = userInfoModel.Data.OrgType;
                UserToken.ParentorgId = userInfoModel.Data.ParentorgId;
                UserToken.ParentOrgName = userInfoModel.Data.ParentOrgName;
                UserToken.Status = userInfoModel.Data.Status;
                UserToken.RoleId = userInfoModel.Data.RoleId;
                UserToken.UserName = userInfoModel.Data.UserName;
                UserToken.FirstName = userInfoModel.Data.FirstName;
                UserToken.MiddleName = userInfoModel.Data.MiddleName;
                UserToken.LastName = userInfoModel.Data.LastName;
                UserToken.EmailId = userInfoModel.Data.EmailId;
                UserToken.MobileNumber = userInfoModel.Data.MobileNumber;
                UserToken.InfiniteCreditLimit = userInfoModel.Data.InfiniteCreditLimit;
                UserToken.CreditLimit = userInfoModel.Data.CreditLimit;
                UserToken.ProductId = userInfoModel.Data.ProductId;
                //UserToken.LastloginDate = userInfoModel.Data.LastloginDate;
                UserToken.UserId = userInfoModel.Data.UserId;
                UserToken.UserName = userInfoModel.Data.UserName;

                //UserToken.o
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, dynamic userInfoModel)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Sid, userAccounts.OrgId.ToString()), //
                    new Claim(ClaimTypes.Name, userAccounts.UserName), //userAccounts.UserName
                    new Claim(ClaimTypes.Email, userAccounts.EmailId), //userAccounts.EmailId
                    new Claim(ClaimTypes.MobilePhone, userAccounts.MobileNumber), //Mobile number
                    new Claim(ClaimTypes.NameIdentifier, userAccounts.Id.ToString()), //userAccounts.Id.ToString()
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
    }
}
