﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(GetAll());
        }

        PasswordHasher<User> passHasher = new PasswordHasher<User>();
        private List<User> GetAll()
        {
            List<User> users = new List<User>();

            var user = new User()
            {
                Id = "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3",
                FirstName = "Ivon",
                LastName = "Mircheva",
                Email = "ivonmircheva2@gmail.com",
                NormalizedEmail = "IVONMIRCHEVA2@GMAIL.COM",
                UserName = "Ivon06",
                NormalizedUserName = "IVON06",
                City = "Kazanlak",
                Country = "Bulgaria",
                Address = "ul. Stefan Stambolov 20",
                ProfilePictureUrl = "https://res.cloudinary.com/dwocfg6qw/image/upload/v1703607775/FootTrapProject/2150771123_oytfrj.jpg"
            };

            user.PasswordHash = passHasher.HashPassword(user, "123456");
            users.Add(user);


            return users;
        }
    }
}