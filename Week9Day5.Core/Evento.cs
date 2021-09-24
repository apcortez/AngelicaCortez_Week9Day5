using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day5.Core.Entities;

namespace Week9Day5.Core
{
    public class Evento
    {
        public delegate void ScriviSuFile(Evento evento, Spesa spesa);
        public event ScriviSuFile MandaLaNotifica;


        public void SeApprovata(Spesa spesa)
        {
            if (MandaLaNotifica != null)
            {
                MandaLaNotifica(this, spesa);
                
            }
        }

  
    }

}
