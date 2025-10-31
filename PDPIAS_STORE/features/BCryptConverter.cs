using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace PDPIAS_management.Features
{
    public class BCryptConverter
    {
        // Function to encrypt the password using bcrypt
        public static string EncryptPassword(string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(plainPassword));
            }

            // Hash the plain password using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            return hashedPassword;
        }

        public static bool Varify(string plainPassword , string hashedPasswordFromDB)
        {
            bool isMatch = BCrypt.Net.BCrypt.Verify(plainPassword, hashedPasswordFromDB);
            return isMatch;
        }
    }
}