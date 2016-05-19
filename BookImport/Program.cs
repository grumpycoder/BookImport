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
                OleDbCommand command = new OleDbCommand("SELECT * from  tblOcr", connection);

                var fi = new FileInfo(connection.DataSource);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Batch_number"]} : {reader["Volume"]} : {reader["Publication_Info"]} : {reader["Publication_Date"]} : {reader["Move_Class"]} : {reader["Filename"]} ");
                    string filename = fi.Directory + (string)reader["filename"];
                    using (var db = new BeholderContext())
                    {
                        var pubContext = new MediaPublishedContext()
                        {
                            MimeTypeId = 7,
                            FileName = reader["Publication_Info"].ToString(),
                            DocumentExtension = ".pdf",
                            ContextText = File.ReadAllBytes(filename)
                        };
                        var pub = new MediaPublished()
                        {
                            MediaTypeId = 4,
                            PublishedTypeId = 4,
                            Name = reader["Publication_Info"].ToString(),
                            DatePublished = Convert.ToDateTime(reader["Publication_Date"].ToString()),
                            MovementClassId = Convert.ToInt32(reader["Move_Class"]),
                            ConfidentialityTypeId = 4,
                            CreatedUserId = 1,
                            ModifiedUserId = 1,
                            MediaPublishedContext = pubContext
                        };
                        db.MediaPublished.Add(pub);
                        db.SaveChanges();
                    }
                    //fullPath = fi.Directory + (string) reader["filename"];
                    Console.WriteLine($"Filepath: {fi.Directory + (string)reader["filename"]}");
                }
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}