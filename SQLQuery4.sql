SELECT a.id,
                                               a.FirstName,
                                               a.LastName,
                                               a.Bio, t.Name
                                          FROM Author a
                                               LEFT JOIN AuthorTag at on a.Id = at.AuthorId
                                               LEFT JOIN Tag t on t.Id = at.TagId
         
SELECT b.id,
                                               b.Title,
                                               b.Url, t.Name
                                          FROM Blog b
                                               LEFT JOIN BlogTag bt on b.Id = bt.BlogId
               
                                               LEFT JOIN Tag t on t.Id = bt.TagId

SELECT p.id,
                                               p.Title,
                                               p.Url, t.name
                                          FROM Post p
                                               LEFT JOIN PostTag pt on p.Id = pt.PostId
                                               LEFT JOIN Tag t on t.Id = pt.TagId
                                  