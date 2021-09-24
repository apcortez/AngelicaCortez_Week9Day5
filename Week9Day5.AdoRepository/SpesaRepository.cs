using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Week9Day5.Core.Entities;
using Week9Day5.Core.Interfaces;

namespace Week9Day5.AdoRepository
{
    public class SpesaRepository : ISpesaRepository
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                            "Initial Catalog = AcademyI.EsercitazioneFinale;" +
                                            "Integrated Security = true";
        public List<Spesa> Fetch()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select * from dbo.Spese";

                    SqlDataReader reader = command.ExecuteReader();

                    List<Spesa> spese = new List<Spesa>();

                    while (reader.Read())
                    {
                        Spesa spesa = new Spesa();
                        spesa.Id = (int)reader["Id"];
                        spesa.Data = Convert.ToDateTime(reader["Data"]);
                        spesa.ImportoSpesa = (double)reader["Spesa"];
                        spesa.Categoria = (Categoria)reader["Categoria"];
                        spesa.Descrizione = (string)reader["Descrizione"];
                        if (reader["Approvata"] != DBNull.Value)
                        {
                            spesa.Approvata = (bool)reader["Approvata"];
                        }
                        if (reader["Rimborso"] !=DBNull.Value)
                        {
                            spesa.Rimborso = (double)reader["Rimborso"];
                            spesa.Approvatore = (Approvatore)reader["Approvatore"];
                        }

                        spese.Add(spesa);
                    }

                    return spese;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Spesa> FetchSpesaWithoutApproval()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select * from dbo.Spese where Approvata is null";

                    SqlDataReader reader = command.ExecuteReader();

                    List<Spesa> spese = new List<Spesa>();

                    while (reader.Read())
                    {
                        Spesa spesa = new Spesa();
                        spesa.Id = (int)reader["Id"];
                        spesa.Data = Convert.ToDateTime(reader["Data"]); 
                        spesa.ImportoSpesa = (double)reader["Spesa"];
                        spesa.Categoria = (Categoria)reader["Categoria"];
                        spesa.Descrizione = (string)reader["Descrizione"];
                        

                        spese.Add(spesa);
                    }

                    return spese;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public void Update(Spesa spesa)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "update dbo.Spese set Approvata = @approvata, Rimborso = @rimborso, Approvatore = @approvatore where Id = @id";

                    command.Parameters.AddWithValue("@approvata", spesa.Approvata);
                    if (spesa.Rimborso != null)
                    {
                        command.Parameters.AddWithValue("@rimborso", spesa.Rimborso);
                        command.Parameters.AddWithValue("@approvatore", spesa.Approvatore);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@rimborso", DBNull.Value);
                        command.Parameters.AddWithValue("@approvatore", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@id", spesa.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
