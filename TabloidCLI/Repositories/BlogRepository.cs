﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class BlogRepository : DatabaseConnector
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               Url
                                               
                                          FROM Blog";

                    List<Blog> Blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")),
                            
                        };
                        Blogs.Add(blog);
                    }

                    reader.Close();

                    return Blogs;
                }
            }
        }

        public Blog Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT b.Id AS BlogId,
                                               b.Title,
                                               b.url,
                                               t.Id AS TagId,
                                               t.Name 
                                          FROM Blog b 
                                               LEFT JOIN BlogTag AS bt ON b.Id = bt.BlogId
                                               LEFT JOIN Tag AS t ON bt.TagId = t.Id 
                                         WHERE b.id = @id";               

                    cmd.Parameters.AddWithValue("@id", id);

                    Blog blog = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (blog == null)
                        {
                            blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),

                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("TagId")))
                        {
                            blog.Tags.Add(new Tag()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TagId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            });
                        }
                    }

                    reader.Close();

                    return blog;
                }
            }
        }

        public void Insert(Blog Blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, Url)
                                                             VALUES (@title, @url)";
                    cmd.Parameters.AddWithValue("@title", Blog.Title);
                    //cmd.Parameters.AddWithValue("@id", Blog.Id);
                    cmd.Parameters.AddWithValue("@Url", Blog.Url);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Blog Blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog 
                                                   SET Title = @title,
                                                       Url = @url
                                                   WHERE id = @id";

                    cmd.Parameters.AddWithValue("@title", Blog.Title);
                    cmd.Parameters.AddWithValue("@url", Blog.Url);
                    cmd.Parameters.AddWithValue("@id", Blog.Id);

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
                    cmd.CommandText = @"DELETE FROM Blog WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        //        public void InsertTag(Blog Blog, Tag tag)
        //        {
        //            using (SqlConnection conn = Connection)
        //            {
        //                conn.Open();
        //                using (SqlCommand cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandText = @"INSERT INTO BlogTag (BlogId, TagId)
        //                                                       VALUES (@BlogId, @tagId)";
        //                    cmd.Parameters.AddWithValue("@BlogId", Blog.Id);
        //                    cmd.Parameters.AddWithValue("@tagId", tag.Id);
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }

        //        public void DeleteTag(int BlogId, int tagId)
        //        {
        //            using (SqlConnection conn = Connection)
        //            {
        //                conn.Open();
        //                using (SqlCommand cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandText = @"DELETE FROM BlogTAg 
        //                                         WHERE BlogId = @Blogid AND 
        //                                               TagId = @tagId";
        //                    cmd.Parameters.AddWithValue("@BlogId", BlogId);
        //                    cmd.Parameters.AddWithValue("@tagId", tagId);

        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
    }
}
