using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2.Model.Table
{
    class Films_Actors
    {
        public virtual int Id { get; set; }
        public virtual int Id_Film { get; set; }
        public virtual int Id_Actors { get; set; }
        public virtual int Index_List { get; set; }
    }
}
