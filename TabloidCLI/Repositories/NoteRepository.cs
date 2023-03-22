using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI
{
    internal class NoteRepository : DatabaseConnector
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT n.Id,
                                            n.Title
                                            n.Content,
                                            n.CreateDateTime,
                                            n.PostId,
                                            p.URL,
                                            p.Title AS PostTitle
                                            p.PublishDateTime,
                                            p.AuthorId,
                                            p.BlogId,
                                            a.FirstName,
                                            a.LastName,
                                            a.Bio,
                                            b.Title AS BlogTitle,
                                            b.URL AS BlogUrl
                                          FROM note n
                                            LEFT JOIN post p on p.Id = n.PostId
                                            LEFT JOIN author a on p.AuthorId = a.Id
                                            LEFT JOIN blog b on p.BlogId = b.id";


                    List<Note> notes = new();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Note Note = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            Post = new Post()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                                Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                                PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublishDateTime")),
                                Author = new Author()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Bio = reader.GetString(reader.GetOrdinal("Bio")),
                                },
                                Blog = new Blog()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                    Title = reader.GetString(reader.GetOrdinal("BlogTitle")),
                                    Url = reader.GetString(reader.GetOrdinal("BlogUrl")),
                                }

                            }
                        };
                        notes.Add(Note);
                    }

                    reader.Close();

                    return notes;
                }
            }
        }
        public List<Note> GetByPost(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT n.Id,
                                            n.Title
                                            n.Content,
                                            n.CreateDateTime,
                                            n.PostId,
                                            p.URL,
                                            p.Title AS PostTitle
                                            p.PublishDateTime,
                                            p.AuthorId,
                                            p.BlogId,
                                            a.FirstName,
                                            a.LastName,
                                            a.Bio,
                                            b.Title AS BlogTitle,
                                            b.URL AS BlogUrl
                                          FROM note n
                                            LEFT JOIN post p on p.Id = n.PostId
                                            LEFT JOIN author a on p.AuthorId = a.Id
                                            LEFT JOIN blog b on p.BlogId = b.id
                                            WHERE n.PostId = @postId";
                    cmd.Parameters.AddWithValue("@postId", id);

                    List<Note> notes = new();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Note Note = new()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            Post = new Post()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                                Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                                PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublishDateTime")),
                                Author = new Author()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Bio = reader.GetString(reader.GetOrdinal("Bio")),
                                },
                                Blog = new Blog()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                    Title = reader.GetString(reader.GetOrdinal("BlogTitle")),
                                    Url = reader.GetString(reader.GetOrdinal("BlogUrl")),
                                }

                            }
                        };
                        notes.Add(Note);
                    }

                    reader.Close();

                    return notes;
                }
            }
        }
        public void Insert(Note note, Post post)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (Title, Content, CreateDateTime, PostId)
                                                             VALUES (@title, @content, @createDateTime, @postId)";
                    cmd.Parameters.AddWithValue("@title", note.Title);
                    cmd.Parameters.AddWithValue("@content", note.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@postId", post.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
