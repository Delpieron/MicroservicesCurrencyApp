using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.Models
{ 
    public class User
    { 
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency{ get; set; }

    }
}
