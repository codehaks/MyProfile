using MyProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.ViewModels
{
    public class UserAddModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
    }
}
