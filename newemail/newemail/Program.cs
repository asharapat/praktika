using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rebex.Net;
using Rebex.Mail;
using System.Configuration;

namespace newemail
{

    class Program
    {
        public static string downloadDir = ConfigurationManager.AppSettings["DownloadPath"];
        public static string unzipDir = ConfigurationManager.AppSettings["UnzipPath"];        
        public static string websiteDir = ConfigurationManager.AppSettings["Web-Site"];
        public static string pathForBackup = ConfigurationManager.AppSettings["PathForBackup"];
        public static bool debugSetting = Convert.ToBoolean(ConfigurationManager.AppSettings["Backup"]);
        public static string newNuGet;

        public static string currentNuGet = File.ReadAllText(@"current_version.txt"); /* read string from current_version.txt */

        public static List<string> attachments;

        static void client_TransferProgress(object sender, ImapTransferProgressEventArgs e)
        {
            if (e.BytesTotal > 0)
                Console.Write("\r{0:F0}%", 100m * e.BytesTransferred / e.BytesTotal);
        }

        static void Main(string[] args)
        {
            // try { 
            unzipDir = unzipDir.Substring(1);
            downloadDir = downloadDir.Substring(1);
            websiteDir = websiteDir.Substring(1);
            pathForBackup = pathForBackup.Substring(1);

            Console.WriteLine("Before we start, let's check paths quickly");
            Console.WriteLine(unzipDir);
            Console.WriteLine(downloadDir);
            Console.WriteLine(websiteDir);
            Console.WriteLine(pathForBackup);

            using (Imap client = new Imap())
                {
                    // connect and log in   
                    Rebex.Licensing.Key = "==Ao6cV2HIJ+/xrOQTyfD2colAHzfn/7uQQVjmgqnqeYlI==";
                    client.Connect("imap.mail.ru", SslMode.Implicit);
                    // authenticate
                    //  client.Login("alikhan.sharapat@outlook.com", "6ataalimuly");
                    client.Login("eleanor.rigby.66@mail.ru", "qwertymnbvcx");

                    // select working folder
                    client.SelectFolder("Inbox");

                    // get the MessageInfo (without message body and attachments 
                    // but with info whether the message contains attachments)
                    ImapMessageInfo messageInfo = client.GetMessageInfo(client.CurrentFolder.TotalMessageCount, ImapListFields.Envelope | ImapListFields.MessageStructure);
                    Console.WriteLine("Reading mail sub. '{0}' (attachments = {1})...", messageInfo.Subject, messageInfo.HasAttachment);

                    // set the progress event handler
                    client.TransferProgress +=
                        new ImapTransferProgressEventHandler(client_TransferProgress);

                    // get the full message (including attachments)

                    MailMessage message = client.GetMailMessage(client.CurrentFolder.TotalMessageCount);
                    attachments = new List<string>();
                        string attachmentName = string.Empty;
                

                FileStream fs2;
                Console.WriteLine();
                    foreach (Attachment attachment in message.Attachments)
                    {

                    
                    Console.WriteLine(downloadDir + attachment.FileName);
                    fs2 = File.Create(downloadDir + attachment.FileName);

                    fs2.Close();
                    Console.WriteLine("Attachment downloaded to path: " + downloadDir.Substring(1) + attachment.FileName/*Path.GetFullPath(attachment.FileName)*/);
                  
                    //if 
                        attachment.Save(/*attachment.FileName*/downloadDir + attachment.FileName);                  
                        attachments.Add(attachment.FileName);
                            attachmentName = attachment.FileName;
                    }

                
            //    File.Copy(attachmentName, Path.Combine(@down));

                    Console.WriteLine("\nMail read - ({0} attachaments)", message.Attachments.Count);

              

                    newNuGet = attachmentName;

                    if (String.Compare(newNuGet, currentNuGet) > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Current NuGet: " + currentNuGet);
                        Console.WriteLine("Replacing current " + currentNuGet + " with " + newNuGet);

                        UnzipNuGetPackage();

                        BackUp();

                        ReplaceWithNewerVersion();

                        //replace string in .txt file (currentNuGet) with newNuGet
                        using (var fs = new FileStream(@"current_version.txt", FileMode.Truncate))
                        {
                        }

                        File.WriteAllText("current_version.txt", newNuGet);

                        currentNuGet = newNuGet;

                        Console.WriteLine("Current NuGet: " + currentNuGet);

                        Console.WriteLine("Everything is done!");


                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Current " + currentNuGet + " is up-to-date");
                    }
                }

         //   }
        /*    catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
            Console.ReadKey();
        
        }

        private static void UnzipNuGetPackage()
        {
            Console.WriteLine();
            Console.WriteLine("Unpacking downloaded NuGet Package");

            string root = "c:\\";

            
            Console.WriteLine("Directory to unpack: " + unzipDir);

            foreach (var package in attachments)
            {
                NuGet.ZipPackage myPack = new NuGet.ZipPackage(downloadDir + package);
                var content = myPack.GetFiles();
                myPack.ExtractContents(new NuGet.PhysicalFileSystem(root), unzipDir);
                Console.WriteLine("Package Id: " + myPack.Id);
                //Console.WriteLine("Content Files Count: " + content.Count());

            }

        }

        private static void BackUp()
        {

          
            string pathWithVersion = Path.Combine(pathForBackup, currentNuGet);

            Directory.CreateDirectory(pathWithVersion);

            Console.WriteLine();

            Console.WriteLine("Making back-up from " + websiteDir + " to " + /*pathForBackup*/pathWithVersion);

            if (debugSetting)
            {
                Copy(websiteDir, /*pathForBackup*/ pathWithVersion);
            }


        }

        private static void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)), true);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
            }
        }

        private static void ReplaceWithNewerVersion()
        {
            DirectoryInfo di = new DirectoryInfo(websiteDir);
            Console.WriteLine();
            Console.WriteLine("Deleting old version in " + websiteDir);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Console.WriteLine("Copying new version to " + websiteDir + " from " + unzipDir);

            Copy(unzipDir, websiteDir);

        }
    }
}
