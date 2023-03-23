using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    public class TagRepository : DatabaseConnector, IRepository<Tag>
    {
        public TagRepository(string connectionString) : base(connectionString) { }

        public List<Tag> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, Name FROM Tag";
                    List<Tag> tags = new List<Tag>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tag tag = new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                        tags.Add(tag);
                    }

                    reader.Close();

                    return tags;
                }
            }
        }

        public Tag Get(int id)
        {
            //using (SqlConnection conn = Connection)
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = @"SELECT t.Name

            //                            FROM Tag t
            //                                 JOIN PostTag pt ON t.Id = pt.TagId                                             
            //                                 JOIN AuthorTag at ON t.Id = at.TagId                                             
            //                                 JOIN BlogTag bt ON t.Id = bt.TagId                                             
            //                            WHERE t.id = @id"
            //        ;



            //        cmd.Parameters.AddWithValue("@id", id);

            //        Tag tag = null;

            //        SqlDataReader reader = cmd.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            tag ??= new Tag()
            //            {
            //                Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
            //                Name = reader.GetString(reader.GetOrdinal("FirstName")),
            //            };
            //        }

            //        reader.Close();

            //        return tag;
            //    }
            //}
            throw new NotImplementedException();
        }
        public void Insert(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Tag (Name)
                                                     VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", tag.Name);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public SearchResults<Author> SearchAuthors(string tagName)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT a.id,
                                       a.FirstName,
                                       a.LastName,
                                       a.Bio
                                  FROM Author a
                                       LEFT JOIN AuthorTag at on a.Id = at.AuthorId
                                       LEFT JOIN Tag t on t.Id = at.TagId
                                 WHERE t.Name LIKE @name";
                    cmd.Parameters.AddWithValue("@name", $"%{tagName}%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    SearchResults<Author> results = new SearchResults<Author>();
                    while (reader.Read())
                    {
                        Author author = new Author()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Bio = reader.GetString(reader.GetOrdinal("Bio")),
                        };
                        results.Add(author);
                    }

                    reader.Close();

                    return results;
                }
            }
        }
    }

}





