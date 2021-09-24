using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day5.Core.Entities;
using Week9Day5.Core.Interfaces;

namespace Week9Day5.Core.BL
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly ISpesaRepository spesaRepo;
        private readonly IDipendenteRepository dipendenteRepo;
        private readonly ISpesaDipendenteRepository spesadipendenteRepo;

        public BusinessLayer(ISpesaRepository spesaRepository, IDipendenteRepository dipendenteRepository, ISpesaDipendenteRepository spesaDipendenteRepository)
        {
            spesaRepo = spesaRepository;
            dipendenteRepo = dipendenteRepository;
            spesadipendenteRepo = spesaDipendenteRepository;
        }

        public void Accedi(Dipendente dipendente)
        {
            List<Spesa> spese = new List<Spesa>();
            List<Dipendente> dipendenti = new List<Dipendente>();
            List<SpesaDipendente> spesaDipendente = new List<SpesaDipendente>();

            try
            {
                spese = spesaRepo.Fetch();
                dipendenti = dipendenteRepo.Fetch();
                spesaDipendente = spesadipendenteRepo.Fetch();
                List<Spesa> speseDipendente = (from s in spese
                                      join sd in spesaDipendente on s.Id equals sd.SpesaId
                                      join d in dipendenti on sd.DipendenteId equals d.Id
                                      where dipendente.Id == sd.DipendenteId
                                      select s).ToList();
                if(speseDipendente != null)
                { foreach (var spesa in speseDipendente)
                    {
                        Evento evento = new Evento();
                        evento.MandaLaNotifica += ScriviSuFile;
                        if(spesa.Approvata !=null)
                        {
                            evento.SeApprovata(spesa);
                           
                        }
                    }
                    Console.WriteLine("\nE' stato generato un riepilogo dei risultati delle spese che sono state visionate.");
                }

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        private void ScriviSuFile(Evento evento, Spesa spesa)
        {
            string path = @"C:\Users\angelica.cortez\source\repos\Week9Day5\RiepilogoSpese.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    if (spesa.Approvata == true)
                    {
                        sw.WriteLine($"Data: {spesa.Data.ToShortDateString()} - Categoria: {(Categoria)spesa.Categoria} - " +
                            $"Spesa: {spesa.ImportoSpesa} Euro - Approvata: SI - Rimborso: {spesa.Rimborso} Euro");
                    }
                    else
                    {
                        sw.WriteLine($"Data: {spesa.Data.ToShortDateString()} - Categoria: {(Categoria)spesa.Categoria} - " +
                            $"Spesa: {spesa.ImportoSpesa} Euro - Approvata: NO");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EseguiApprovazione()
        {
            List<Spesa> spese = new List<Spesa>();
            List<Dipendente> dipendenti = new List<Dipendente>();
            List<SpesaDipendente> spesaDipendente = new List<SpesaDipendente>();

            try
            {
                spese = spesaRepo.FetchSpesaWithoutApproval();

                if(spese.Count()>0)
                {
                    foreach(var spesa in spese)
                    {
                        if(spesa.ImportoSpesa>0 && spesa.ImportoSpesa <= 400)
                        {
                            spesa.Approvata = true;
                            spesa.Approvatore = Approvatore.Manager;

                        }
                        else if(spesa.ImportoSpesa>400 && spesa.ImportoSpesa<= 1000)
                        {
                            spesa.Approvata = true;
                            spesa.Approvatore = Approvatore.OperationManager;
                        }
                        else if(spesa.ImportoSpesa>1000 && spesa.ImportoSpesa <= 2500)
                        {
                            spesa.Approvata = true;
                            spesa.Approvatore = Approvatore.CEO;
                        }
                        else
                        {
                            spesa.Approvata = false;
                            spesa.Rimborso = null;
                            
                        }

                        if (spesa.Approvata == true)
                        {
                            switch ((int)spesa.Categoria)
                            {
                                case 1:
                                    spesa.Rimborso = (double)(spesa.ImportoSpesa * 70 / 100);
                                    break;
                                case 2:
                                    spesa.Rimborso = (double)spesa.ImportoSpesa;
                                    break;
                                case 3:
                                    spesa.Rimborso = (double)(spesa.ImportoSpesa * 50 / 100)+ 50 ;
                                    break;
                                case 4:
                                    spesa.Rimborso = (double)(spesa.ImportoSpesa * 10 / 100);
                                    break;
                            }
                        }

                        spesaRepo.Update(spesa);

                    }

                }



            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public Dipendente GetDipendenteByNome(string nome)
        {
            return dipendenteRepo.GetByNome(nome);
        }
    }
}
