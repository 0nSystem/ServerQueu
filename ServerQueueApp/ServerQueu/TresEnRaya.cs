using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerQueu
{
    public struct TresEnRaya
    {

        public int[,] Board { get; } = new int[3,3];
        public bool FinishGame { get; set; }= false;
        
        public TresEnRaya()
        {

     
        }

    }
}
