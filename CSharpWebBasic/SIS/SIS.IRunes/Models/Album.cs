using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SIS.IRunes.Models
{
    public class Album
    {
        public Album()
        {
            this.Tracks = new HashSet<Track>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price => Tracks.Sum(p => p.Price) * 0.87M;

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
