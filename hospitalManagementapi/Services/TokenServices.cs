using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using hospitalManagementapi;
using Microsoft.Extensions.Configuration;
using hospitalManagementapi.Data;
using hospitalManagementapi.Models;

namespace hospitalManagementapi.Services
{
    public class TokenServices
    {

        /// <summary>
        /// The TokenService class is responsible for generating and validating JWT tokens.
        /// It uses configuration settings from appsettings.json to securely sign the tokens.
        /// </summary>

        private readonly IConfiguration _config;


        /// <summary>
        /// Constructor for TokenService.
        /// Initializes the service with configuration values needed for JWT token generation.
        /// </summary>
        /// <param name="config">Configuration settings injected through dependency injection.</param>




        public TokenServices(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generates a JWT (JSON Web Token) for the authenticated user. 
        /// This token will be used for securing API requests, and it includes claims like username and user role.
        /// </summary>
        /// <param name="user">The user object containing the username and role of the authenticated user.</param>
        /// <returns>A JWT token as a string, which will be sent to the client for subsequent API requests.</returns>

        public string CreateToken(User user)
        {

            // Step 1: Define the claims (key-value pairs) that will be included in the token.
            // Claims provide information about the user and their role.
            var claims = new List<Claim>

            {

                // This claim stores the user's username, and is used to identify the user.
                new Claim(JwtRegisteredClaimNames.NameId,user.username),

                // This claim stores the user's role (e.g., Admin, Doctor, Nurse, etc.).
                // The role will be used for role-based authorization to control access to certain API endpoints.
        
                new Claim(ClaimTypes.Role,user.Role)

             };

            // Step 2: Generate a security key based on the secret token key stored in the configuration.
            // This key will be used to sign the token and ensure its integrity.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["EncryptionDecryptionKey"]));

            // Step 3: Create signing credentials using the security key and specify the security algorithm.
            // HmacSha512Signature is a hashing algorithm used for the token signature.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Step 4: Define the token descriptor which includes claims, expiration, and signing credentials.
            // - Subject: Contains the claims for the token (username, role, etc.)
            // - Expires: Sets the expiration date of the token (7 days from creation in this case).
            // - SigningCredentials: Ensures the token is securely signed.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds



            };

            // Step 5: Create the token handler and generate the token using the token descriptor.

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Step 6: Return the generated token as a string.

            return tokenHandler.WriteToken(token);

            //return string

        }



    }
}
