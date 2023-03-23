using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
<<<<<<< HEAD
    public class JournalRepository : DatabaseConnector
    {
=======
    public class JournalRepository : DatabaseConnector, IRepository<Journal>    {
>>>>>>> main
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               Content,
                                               CreateDateTime
                                          FROM Journal";

                    List<Journal> journals = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Journal journal = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        journals.Add(journal);
                    }

                    reader.Close();

                    return journals;
                }
            }
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                                             VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal 
                                                   SET Title = @title,
                                                       Content = @content
                                                   WHERE id = @id";

                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@id", journal.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Journal WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

<<<<<<< HEAD
        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                                             VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    //                }
                    //            }
                    //        }

                    //        public void Update(Journal journal)
                    //        {
                    //            using (SqlConnection conn = Connection)
                    //            {
                    //                conn.Open();
                    //                using (SqlCommand cmd = conn.CreateCommand())
                    //                {
                    //                    cmd.CommandText = @"UPDATE Journal 
                    //                                           SET FirstName = @firstName,
                    //                                               LastName = @lastName,
                    //                                               bio = @bio
                    //                                         WHERE id = @id";

                    //                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    //                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    //                    cmd.Parameters.AddWithValue("@bio", journal.Bio);
                    //                    cmd.Parameters.AddWithValue("@id", journal.Id);

                    //                    cmd.ExecuteNonQuery();
                    //                }
                    //            }
                    //        }

                    //        public void Delete(int id)
                    //        {
                    //            using (SqlConnection conn = Connection)
                    //            {
                    //                conn.Open();
                    //                using (SqlCommand cmd = conn.CreateCommand())
                    //                {
                    //                    cmd.CommandText = @"DELETE FROM Journal WHERE id = @id";
                    //                    cmd.Parameters.AddWithValue("@id", id);

                    //                    cmd.ExecuteNonQuery();
                    //                }
                    //            }
                    //        }
=======
                    cmd.ExecuteNonQuery();
                }
            }
        }
>>>>>>> main

                    //        public void InsertTag(Journal journal, Tag tag)
                    //        {
                    //            using (SqlConnection conn = Connection)
                    //            {
                    //                conn.Open();
                    //                using (SqlCommand cmd = conn.CreateCommand())
                    //                {
                    //                    cmd.CommandText = @"INSERT INTO JournalTag (JournalId, TagId)
                    //                                                       VALUES (@journalId, @tagId)";
                    //                    cmd.Parameters.AddWithValue("@journalId", journal.Id);
                    //                    cmd.Parameters.AddWithValue("@tagId", tag.Id);
                    //                    cmd.ExecuteNonQuery();
                    //                }
                    //            }
                    //        }

                    //        public void DeleteTag(int journalId, int tagId)
                    //        {
                    //            using (SqlConnection conn = Connection)
                    //            {
                    //                conn.Open();
                    //                using (SqlCommand cmd = conn.CreateCommand())
                    //                {
                    //                    cmd.CommandText = @"DELETE FROM JournalTAg 
                    //                                         WHERE JournalId = @journalid AND 
                    //                                               TagId = @tagId";
                    //                    cmd.Parameters.AddWithValue("@journalId", journalId);
                    //                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    //                    cmd.ExecuteNonQuery();
                    //                }
                    //            }
                    //        }
                }
            }
        }
    }
}
