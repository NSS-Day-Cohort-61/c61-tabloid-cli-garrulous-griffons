using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class PostRepository : DatabaseConnector, IRepository<Post>
    {
        public PostRepository(string connectionString) : base(connectionString) { }

        public List<PostTag> GetPostTags()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, PostId, TagId  FROM PostTag";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<PostTag> posttags = new List<PostTag>();
                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);
                            int postIdColumnPosition = reader.GetOrdinal("PostId");
                            int postIdValue = reader.GetInt32(postIdColumnPosition);
                            int tagIdColumnPosition = reader.GetOrdinal("TagId");
                            int tagIdValue = reader.GetInt32(tagIdColumnPosition);
                            PostTag newposttag = new PostTag
                            {
                                Id = idValue,
                                PostId = postIdValue,
                                TagId = tagIdValue,
                            };
                            posttags.Add(newposttag);
                        }
                        return posttags;
                    }
                }
            }
        }

        public Post Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.Id AS PostId,
                                               p.Title AS PostTitle,
                                               p.Url as PostUrl,
                                               a.Id AS AuthorId,
                                               a.FirstName AS FirstName,
                                               a.LastName AS LastName,
                                               a.Bio AS Bio,
                                               b.Id AS BlogId,
                                               b.Title AS BlogTitle
                                          FROM Post p 
                                               LEFT JOIN Author a on a.Id = p.AuthorId
                                               LEFT JOIN Blog b on b.Id = p.BlogId
                                         WHERE p.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    Post post = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (post == null)
                        {
                            post = new Post()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                                Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                                Url = reader.GetString(reader.GetOrdinal("PostUrl")),
                            };
                        }
                    }
                    reader.Close();

                    return post;
                }
            }
        }

        public List<Tag> GetByPostTag(int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.Name AS TagName,
                                               t.Id as TagId
                                          FROM PostTag as pt 
                                               LEFT JOIN Post p on p.Id = pt.postId
                                               LEFT JOIN Tag t on t.Id = pt.tagId 
                                         WHERE pt.postId = @postId";
                    cmd.Parameters.AddWithValue("@postId", postId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Tag> posttags = new List<Tag>();
                    while (reader.Read())
                    {
                        Tag pt = new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("TagId")),
                            Name = reader.GetString(reader.GetOrdinal("TagName")),
                        };
                        posttags.Add(pt);
                    }

                    reader.Close();

                    return posttags;
                }
            }
        }



        public void InsertPostTag(Post post, Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PostTag (PostId, TagId )
                                                     VALUES (@postid, @tagid)";
                    cmd.Parameters.AddWithValue("@postid", post.Id);
                    cmd.Parameters.AddWithValue("@tagid", tag.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeletePostTag(int postid, int tagid)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PostTag WHERE PostTag.TagId = @tagid AND PostTag.PostId = @postid";
                    cmd.Parameters.AddWithValue("@tagid", tagid);
                    cmd.Parameters.AddWithValue("@postid", postid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
