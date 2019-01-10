using Microsoft.EntityFrameworkCore;
using MyProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Data
{
    public class ProfileDbContext:DbContext
    {
        public ProfileDbContext
            (DbContextOptions<ProfileDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


    }
}
