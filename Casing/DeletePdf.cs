using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Casing
{
    public class DeletePdf
    {
        public void DeletePdfFiles(String path)
        {
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pdf"); //Getting Text files

            foreach (FileInfo files in Files)
            {
                try
                {
                    // Check if file exists with its full path    
                    if (File.Exists(files.DirectoryName+"\\"+files.Name))
                    {
                        // If file found, delete it    
                        File.Delete(files.DirectoryName + "\\" + files.Name);
                        Console.WriteLine("File deleted.");
                    }
                    else
                        Console.WriteLine("File not found");
                }
                catch (IOException ioExp)
                {
                    Console.WriteLine(ioExp.Message);
                }
            }            
        }
    }
}