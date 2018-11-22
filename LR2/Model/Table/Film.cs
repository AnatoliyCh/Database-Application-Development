using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2.Model.Table
{
    class Film
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual ICollection<Genre> GenresList { get; set; } = new List<Genre>();
        public virtual ICollection<Actor> ActorsList { get; set; } = new List<Actor>();
        public virtual float Rating { get; set; }
        public virtual bool Viewed { get; set; }
    }
}
