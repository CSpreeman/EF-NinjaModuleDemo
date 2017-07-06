using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaDomain.Classes
{
    //All of these objects will be instantiated as tables
    public class Ninja
    {
        public Ninja()
        {
            //needs to be instantiated
            EquipmentOwned = new List<NinjaEquipment>();
        }

        //Id will be Primary Key
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ServedInOniwaban { get; set; }
        public Clan Clan { get; set; }
        //Sets Foreign Key to Clan
        public int ClanId { get; set; }
        public List<NinjaEquipment> EquipmentOwned { get; set; } //Label Virtual for lazy loading
        public System.DateTime DateOfBirth { get; set; }
    }

    public class Clan
    {
        public Clan()
        {
            Ninjas = new List<Ninja>();
        }
        //Primary Key
        public int Id { get; set; }
        public string ClanName { get; set; }
        public List<Ninja> Ninjas { get; set; }
    }
    
    public class NinjaEquipment
    {
        //Primary Key
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        [Required]
        public Ninja Ninja { get; set; }
    }
}
