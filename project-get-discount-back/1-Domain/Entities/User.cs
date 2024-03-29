﻿using project_get_discount_back.Common;
using project_get_discount_back.Helpers;
using System.ComponentModel.DataAnnotations;

namespace project_get_discount_back.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Password { get; set; }
        [Required]
        public AccessType Role { get; set; }


        public User(string name, string email, string password, AccessType role)
        {
            Name = name;
            Email = email;
            Password = Encrypting(password);
            Role = role;
        }

        public User(string name, string email, AccessType role, string userCreate)
        {
            Name = name;
            Email = email;
            Role = role;
            CreatedBy = userCreate;
        }

        public void ActivateUser(string userCreate)
        {
            Deleted = false;
            CreatedBy = userCreate;
        }

        public string Encrypting(string valor)
        {
            var encryptedPassword = new Cryptography();
            return encryptedPassword.Encrypt(valor);
        }

        public enum AccessType
        {
            ADMIN,
            USER,
            NULL
        }
    }
}
