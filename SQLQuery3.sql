SELECT b.id,
                                            b.Title,
                                               b.Url, t.Name
                                          FROM Blog b
                                               LEFT JOIN BlogTag bt on b.Id = bt.BlogId
                                               LEFT JOIN Tag t on t.Id = bt.TagId