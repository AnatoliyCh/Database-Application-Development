using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2.Controller
{
    interface IControll
    {
        void Read();
        void Create();
        void Update();
        void Delete();
        void Search();
    }
}
