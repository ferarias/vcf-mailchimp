using System;
using System.IO;
using vCardLib;
using vCardLib.Deserializers;
using vCardLib.Collections;
using System.Text;
using System.Collections.Generic;
using vCardLib.Models;
using CsvHelper;

namespace VcfProcessor
{
    class Program
    {

        const string allGroup = "all";
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Specify a folder for vcf files!");
                return;
            }

            var folder = args[0];
            if(!Directory.Exists(folder))
            {
                Console.WriteLine($"Folder for vcf files '{folder}' does not exist!");
                return;
            }

            string outFile;
            if(args.Length > 1)
            {
                outFile = args[1];
            }
            else
            {
                outFile = Path.Combine(folder, "contact-groups.csv");
            }

            System.Console.WriteLine($"Starting at '{folder}'...");

            var data = new Dictionary<string, List<string>>();

            // Enumerate all vCard files from folder
            var vcfFiles = Directory.EnumerateFiles(folder, "*.vcf", SearchOption.AllDirectories);
            foreach (var vcfFile in vcfFiles)
            {
                var group = Path.GetFileNameWithoutExtension(vcfFile).ToUpperInvariant();
                var isGeneralGroup = string.Equals(group, allGroup, StringComparison.CurrentCultureIgnoreCase);

                // Read this vCard file and extract all contacts
                using(var sr = new StreamReader(vcfFile, Encoding.UTF8))
                {
                    vCardCollection contacts = Deserializer.FromStreamReader(sr);

                    // Fill groups for each contact
                    foreach(vCard contact in contacts)
                    {
                        foreach (EmailAddress email in contact.EmailAddresses)
                        {
                            if(!data.ContainsKey(email.Email.Address))
                                data.Add(email.Email.Address, new List<string>());

                            if(isGeneralGroup) continue;

                            if(!data[email.Email.Address].Contains(group))
                                data[email.Email.Address].Add(group);
                        }
                    }
                }
            }

            // Build the output csv file
            
            using (var writer = new StreamWriter(outFile))
            using (var csv = new CsvWriter(writer))
            {    
                foreach (var item in data)
                {
                    var groups = FormatGroups(item.Value);
                    csv.WriteRecord(new { Email = item.Key, Groups = groups });
                    csv.NextRecord();
                }
            }
            
            System.Console.WriteLine($"Written at '{outFile}'.");
        }

        private static string FormatGroups(List<string> groups)
        {
            return string.Join(',',groups);
            // var sb = new StringBuilder();
            // var i = 0;
            // sb.Append("[");
            // foreach (var g in groups)
            // {
            //     if(i++ > 0)
            //         sb.Append(',');
            //     sb.Append($"\"{g}\"");
            // }
            // sb.Append("]");
            // return sb.ToString();
        }
    }

}
