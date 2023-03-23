using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector
    {
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

        //public Journal Get(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"SELECT a.Id AS JournalId,
        //                                       a.FirstName,
        //                                       a.LastName,
        //                                       a.Bio,
        //                                       t.Id AS TagId,
        //                                       t.Name
        //                                  FROM Journal a 
        //                                       LEFT JOIN JournalTag at on a.Id = at.JournalId
        //                                       LEFT JOIN Tag t on t.Id = at.TagId
        //                                 WHERE a.id = @id";

        //            cmd.Parameters.AddWithValue("@id", id);

        //            Journal journal = null;

        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                if (journal == null)
        //                {
        //                    journal = new Journal()
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("JournalId")),
        //                        Title = reader.GetString(reader.GetOrdinal("Title")),
        //                        Content = reader.GetString(reader.GetOrdinal("Content")),
        //                        CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
        //                    };
        //                }

        //                if (!reader.IsDBNull(reader.GetOrdinal("TagId")))
        //                {
        //                    journal.Add(new Tag()
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("TagId")),
        //                        Name = reader.GetString(reader.GetOrdinal("Name")),
        //                    });
        //                }
        //            }

        //            reader.Close();

        //            return journal;
        //        }
        //    }
        //}

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

                    cmd.ExecuteNonQuery();
                }
            }
        }

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
