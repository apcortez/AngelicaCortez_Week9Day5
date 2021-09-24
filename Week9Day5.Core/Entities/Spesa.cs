using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week9Day5.Core.Entities
{
    public class Spesa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double ImportoSpesa { get; set; }
        public Categoria Categoria { get; set; }
        public string Descrizione { get; set; }

        public Approvatore? Approvatore { get; set; }
        public bool? Approvata { get; set; }
        public double? Rimborso { get; set; }
    }

    public enum Categoria
    {
        Vitto = 1,
        Alloggio = 2,
        Trasferta = 3,
        Altro = 4
    }

    public enum Approvatore
    {
        Manager = 1,
        OperationManager=2,
        CEO = 3

    }
}
