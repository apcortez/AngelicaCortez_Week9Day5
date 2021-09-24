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
    public class DipendenteRepository : IDipendenteRepository
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                             "Initial Catalog = AcademyI.EsercitazioneFinale;" +
                                             "Integrated Security = true";
        public List<Dipendente> Fetch()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select * from dbo.Dipendenti";

                    SqlDataReader reader = command.ExecuteReader();

                    List<Dipendente> dipendenti = new List<Dipendente>();

                    while (reader.Read())
                    {
                        Dipendente d = new Dipendente();
                        d.Id = (int)reader["Id"];
                        d.Nome = (string)reader["Nome"];


                        dipendenti.Add(d);
                    }

                    return dipendenti;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dipendente GetByNome(string nome)
        {
            try
            {
                Dipendente d = new Dipendente();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select * from dbo.Dipendenti where Nome = @nome";
                    command.Parameters.AddWithValue("@nome", nome);

                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        d.Id = (int)reader["Id"];
                        d.Nome = (string)reader["Nome"];

                    }
                }
                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Dipendente item)
        {
            throw new NotImplementedException();
        }
    }
}
