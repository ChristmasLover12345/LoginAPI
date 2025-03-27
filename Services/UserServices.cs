using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LoginAPI.Context;
using LoginAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace LoginAPI.Services
{
    public class UserServices
    {
        

        private readonly DataContext _dataContext;
        public UserServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


         public bool CreateUser(UserDTO newUser)
        {
            bool result = false;

            if (!DoesUserExist(newUser.Email))
            {
                UserModel usetToAdd = new();
                usetToAdd.Email = newUser.Email;

                PasswordDTO hashPasword = HashPassword(newUser.Password);
                usetToAdd.Hash = hashPasword.Hash;
                usetToAdd.Salt = hashPasword.Salt;

                _dataContext.Users.Add(usetToAdd);
                result = _dataContext.SaveChanges() !=0;
            }

            return result;

        }

        private bool DoesUserExist(string email)
        {
            return _dataContext.Users.SingleOrDefault(users => users.Email == email) != null;
        }

         private static PasswordDTO HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(64);

            string salt = Convert.ToBase64String(saltBytes);
            string hash;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256)){
                hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            PasswordDTO hashedPassword = new();
            hashedPassword.Salt = salt;
            hashedPassword.Hash = hash;
            return hashedPassword;

        }

        public string Login(UserDTO user)         
        {
            string result = null;

            UserModel foundUser = GetUserByEmail(user.Email);

            if(foundUser == null)
            {
                return result;
            }

            if(VerifyPassword(user.Password, foundUser.Salt, foundUser.Hash))
            {
                // JWT: JSON web token = a type of token used for authentication or transfering information
                // Bearer Token: A token that grants access to a resource, such as an API. JWT can be used as a bearer token, but there are other types of tokens that can be used as a bearer token.

                // Setting the string that will be encrypted int our JWT
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                
                // Now to encrypt our secret key
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                // Set the options for our token to define properties such as where the token is issued from, where it is allowed to be used, and how long it is valid for
                var tokenOptions = new JwtSecurityToken(
                    // Issuer: where is this token allowed to be generated from
                    issuer: "rideapi-egexbda9bpfgh6c9.westus-01.azurewebsites.net",
                    // audience: where this token is allowed to authenticate.
                    // issuer and audience should be the same since our api os handling both login and authentication
                    audience: "rideapi-egexbda9bpfgh6c9.westus-01.azurewebsites.net",
                    // Claims = additional options for authentication
                    claims: new List<Claim>(),
                    // Sets the token expiration date. in other words, this is what makes our tokens temporary, thus keeping our access to our rescources safe and secure
                    expires: DateTime.Now.AddMinutes(1440),
                    // This attaches our newly encrypted super secret key that was turned into sign on credentials.
                    signingCredentials: signingCredentials
                    
                );

                // Generate our JWT and save the token as a string into a variable
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                result = tokenString;
                // Token Anatomy:
                // wnuifwnuiwfenuiwfenuiwfe.nusfenuioeoionve.wqundsandoawnd
                // Header: wnuifwnuiwfenuiwfenuiwfe
                // Payload: nusfenuioeoionve  This will have information about the token including the expiration
                // Signature: wqundsandoawnd  encrypt and combine the header and payload using secret key

            }
           
           return result;

        }

        private UserModel GetUserByEmail(string email)
        {
            return _dataContext.Users.SingleOrDefault(users => users.Email == email);
        }

         private static bool VerifyPassword(string password, string salt, string hash)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string newHash; 
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                newHash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            return newHash == hash;

        }

          public bool UpdatePassword(UserDTO user)
        {
            bool result = false;

            var foundUser = GetUserByEmail(user.Email);

            if(foundUser == null)
            {
                return result;
            }

            PasswordDTO hashPassword = HashPassword(user.Password);

            foundUser.Hash = hashPassword.Hash;
            foundUser.Salt = hashPassword.Salt;

            _dataContext.Update(foundUser);

            result = _dataContext.SaveChanges() != 0;
            return result;

        }


    }
}