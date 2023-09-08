using Microsoft.AspNetCore.Identity;

namespace Market.Models
{
    public class AppUser : IdentityUser
    {
        //every user has a cart
        public int CartId { get; set; }
        //public virtual Cart Cart { get; set; }
        //public virtual List<Reciept> reciepts { get; set; }
        public virtual List<Reciept>? reciepts { get; set; }
    }
}
