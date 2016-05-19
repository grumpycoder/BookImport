using BookImport.Data;
using BookImport.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImport
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string connString = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\temp\book.1\index.mdb";
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                connection.Open();
                OleDbDataReader reader = null;
                OleDbCommand command = new OleDbCommand("SELECT * from  tblOcr where filename like '%\\SPLCL-BOOK-00204\\2\\2.pdf%'", connection);

                var userId = 1;
                var confidentialityTypeId = 4;

                var fi = new FileInfo(connection.DataSource);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["BatchId"]} : {reader["Volume"]} : {reader["Publication_Info"]} : {reader["Publication_Date"]} : {reader["Move_Class"]} : {reader["Filename"]} ");
                    string filename = fi.Directory + (string)reader["filename"];
                    using (var db = new BeholderContext())
                    {
                        var pubContext = new MediaPublishedContext()
                        {
                            MimeTypeId = 7,
                            FileName = reader["Publication_Info"].ToString(),
                            DocumentExtension = ".pdf",
                            FileStreamID = Guid.NewGuid(),
                            ContextText = File.ReadAllBytes(filename)
                        };
                        var pub = new MediaPublished()
                        {
                            MediaTypeId = confidentialityTypeId,
                            PublishedTypeId = confidentialityTypeId,
                            Name = reader["Publication_Info"].ToString(),
                            DatePublished = Convert.ToDateTime(reader["Publication_Date"].ToString()),
                            DateReceived = Convert.ToDateTime(reader["Publication_Date"].ToString()),
                            MovementClassId = Convert.ToInt32(reader["Move_Class"]),
                            ConfidentialityTypeId = confidentialityTypeId,
                            CreatedUserId = userId,
                            ModifiedUserId = userId,
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                            MediaPublishedContext = pubContext
                        };

                        db.MediaPublished.Add(pub);
                        db.SaveChanges();

                        pubContext.MediaPublishedId = pub.Id;
                        db.SaveChanges();
                    }
                    Console.WriteLine($"Filepath: {fi.Directory + (string)reader["filename"]}");
                }
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}