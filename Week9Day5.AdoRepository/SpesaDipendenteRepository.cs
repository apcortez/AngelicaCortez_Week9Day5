using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day5.Core.Entities;
using Week9Day5.Core.Interfaces;

namespace Week9Day5.AdoRepository
{
    public class SpesaDipendenteRepository : ISpesaDipendenteRepository
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                             "Initial Catalog = AcademyI.EsercitazioneFinale;" +
                                             "Integrated Security = true";
        public List<SpesaDipendente> Fetch()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select s.Id as spesaId, d.Id as dipendenteId from dbo.Spese s join dbo.Dipendenti d on s.Dipendente = d.Id";

                    SqlDataReader reader = command.ExecuteReader();

                    List<SpesaDipendente> speseDipendenti = new List<SpesaDipendente>();

                    while (reader.Read())
                    {
                        SpesaDipendente sd = new SpesaDipendente();
                        sd.SpesaId= (int)reader["spesaId"];
                        sd.DipendenteId = (int)reader["dipendenteId"];


                        speseDipendenti.Add(sd);
                    }

                    return speseDipendenti;
                }
            }catch(Exception ex)
            {
                throw ex;
            } 
        }

        public void Update(SpesaDipendente item)
        {
            throw new NotImplementedException();
        }
    }
}
