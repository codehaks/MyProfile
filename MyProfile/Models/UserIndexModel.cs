using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Models
{
    public class UserIndexModel
    {
        public IEnumerable<User> Users { get; set; }
        public GenderType GenderType { get; set; }
        public string Term { get; set; }
        public SortType SortType { get; set; }
        public OrderType OrderType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
