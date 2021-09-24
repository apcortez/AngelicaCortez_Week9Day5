using System;
using Week9Day5.AdoRepository;
using Week9Day5.Core;
using Week9Day5.Core.BL;
using Week9Day5.Core.Entities;

namespace Week9Day5.UserConsoleApp
{
    class Program
    {
        private static readonly IBusinessLayer bl = new BusinessLayer(new SpesaRepository(), new DipendenteRepository(), new SpesaDipendenteRepository());

        static void Main(string[] args)
        {
            Console.WriteLine("################# BENVENUTO! ################");
            string utente = InserisciUtente();
            Dipendente dipendente = bl.GetDipendenteByNome(utente);

            if (dipendente.Id != 0)
            {
                bl.Accedi(dipendente);

            }
            else 
                Console.WriteLine("Utente non riconosciuto");
        }
        private static string InserisciUtente()
        {
            string nome = String.Empty;
            do
            {
                Console.WriteLine("Nome: ");
                nome = Console.ReadLine();

            } while (String.IsNullOrEmpty(nome));
            return nome;
        }
    }
}
