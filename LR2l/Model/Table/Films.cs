using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2.Model
{
    class Films
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual ICollection<Genres> GenresList { get; set; } = new List<Genres>();
        public virtual ICollection<Actors> ActorsList { get; set; } = new List<Actors>();
        public virtual float Rating { get; set; }
        public virtual bool Viewed { get; set; }

    }
}
