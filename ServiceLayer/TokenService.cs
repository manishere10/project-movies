using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Movies.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class TokenService : ITokenBuilderService
    {
        private readonly ILogger<TokenService> _logger;

        private readonly IConfiguration _configuration;

        public TokenService(ILogger<TokenService> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                if ((email.Equals("dev@gmail.com") || email.Equals("dev@yahoo.com")) &&
                    password.Equals("123"))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error occured in TokenService.Authenticate - {0}", ex.Message);
                throw;
            }
        }
        public async Task<string> GetToken(string email)
        {
            try
            {
                var _tokenHandler = new JwtSecurityTokenHandler();

                var _key = Encoding.ASCII.GetBytes(this._configuration.GetSection("Secret").Value);

                var _securityKey = new SymmetricSecurityKey(_key);

                var _signingKey = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature);

                var _claims = new List<Claim>();

                if (email.Contains("gmail.com"))
                {
                    _claims = new List<Claim>
                                        {
                                            new Claim("EMAILID", email),
                                            new Claim("RO", "ADMIN")
                                        };
                }

                if (email.Contains("yahoo.com"))
                {
                    _claims = new List<Claim>
                                        {
                                            new Claim("EMAILID", email),
                                            new Claim("RO", "BROKER")
                                        };
                }

                var _tokenIssuer = this._configuration.GetSection("Issuer").Value;

                var token = new JwtSecurityToken(
                                    _tokenIssuer,
                                    _tokenIssuer,
                                    _claims,
                                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_key),
                                    SecurityAlgorithms.HmacSha256Signature));

                var handler = new JwtSecurityTokenHandler();

                var jwt = handler.WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error occured in TokenService.GetToken - {0}", ex.Message);
                throw;
            }
        }
    }
}
