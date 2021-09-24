using System;
using Week9Day5.AdoRepository;
using Week9Day5.Core.BL;

namespace Week9Day5
{
    class Program
    {
        private static readonly IBusinessLayer bl = new BusinessLayer(new SpesaRepository(), new DipendenteRepository(), new SpesaDipendenteRepository());

        static void Main(string[] args)
        {
            bl.EseguiApprovazione();
        }
    }
}
