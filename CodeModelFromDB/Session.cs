namespace CodeModelFromDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Session")]
    public partial class Session
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Duration { get; set; }

        [Required]
        [StringLength(50)]
        public string Spot { get; set; }

        [Required]
        [StringLength(50)]
        public string WavesCaught { get; set; }
    }
}
