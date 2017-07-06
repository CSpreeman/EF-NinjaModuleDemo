using NinjaDomain.Classes;
using System.Data.Entity;

namespace NinjaDomain.DataModel
{
    //This is our DBContext, it tells EF what models we are using
    public class NinjaContext : DbContext
    {
        //Use DBSet to reference what objects we will be using to create our tables
        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<NinjaEquipment> Equipment { get; set; }
    }
}
