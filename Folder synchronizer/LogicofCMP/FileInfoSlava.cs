using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

//QQ
//1 PathinFolder for right files?

namespace LogicofCMP
{
    //20.07.16 12.37 Woha - add partial to class definition
    public partial class Synchronization
    {

        public void Synchronize (ListsofFiles ListsOfFiles, bool isAsymmetricChecked, bool isByContentChecked)
          {
            if (isAsymmetricChecked)
            {
                //20.07.16 12.26 Slava - calling assymetryc synchronize method
                AssymetricSynchronize(ListsOfFiles);
            }
        }

        //20.07.16 12.26 Slava - creating assymetricSynchronize method
        private void AssymetricSynchronize (ListsofFiles ListsOfFiles)
        {
            FileInfoWoha TargetListFile = ListsOfFiles.RightListofFiles.First();

            //deleting all files from right list

            foreach (FileInfoWoha FIW in ListsOfFiles.RightListofFiles)
            {
                string fileForDeletion = Path.Combine(FIW.Path, FIW.Name);
                try
                {
                    File.Delete(fileForDeletion);
                }
                catch (IOException e)
                {
                    //20.07.16 slava - show exception method used
                    ShowExceptionMessage(e.ToString(), "Error deleting file");
                }

            }

            //copying files from left list to right list
            foreach (FileInfoWoha FIW in ListsOfFiles.LeftListofFiles)
            {
                string sourceFile = Path.Combine(FIW.Path, FIW.Name);
                string destFile = Path.Combine(TargetListFile.Path, FIW.Name);

                try
                {
                    File.Copy(sourceFile, destFile, true);
                }
                catch (IOException e)
                {
                    //20.07.16 slava - show exception method used
                    ShowExceptionMessage(e.ToString(), "Error copying file");
                }

            }
        }

        //20.07.16 22.43 slava - adding equal by content method
        //24.07.16 11.46 Woha - Забрав у свій файл public bool EqualByContent (FileInfoWoha leftFile, FileInfoWoha rightFile)


        //20.07.16 slava - show exception method created
        //24.07.16 11.46 Woha - собі потягнув його в свій клас. Як функцію загальну створити ?

        public void ShowExceptionMessage(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
        }

        public void FileHandler(ListsofFiles Lists)
        {
            
            FileInfoWoha FIWLeft;
            FileInfoWoha FIWRight;

            foreach (LinksInfo LI in Lists.listLinksInfo)
            {
                
                
                //if file from left folder doesn't exist in right folder
                if (LI.Relations == 1)
                {

                    FIWLeft = Lists.LeftListofFiles.ElementAt(LI.Left); // getting left file info
                    FIWRight = Lists.RightListofFiles.ElementAt(0); //getting right file info

                    string sourceFile = Path.Combine(FIWLeft.Path+ FIWLeft.PathInFolder, FIWLeft.Name);
                    string destFile = Path.Combine(FIWRight.Path + FIWRight.PathInFolder, FIWLeft.Name);

                    // copying left file to right folder
                    try
                    {
                        File.Copy(sourceFile, destFile, false);
                    }
                    catch (IOException e)
                    {
                        
                        ShowExceptionMessage(e.ToString(), "Error copying file");
                    }
                }



                // if file from right folder doesn't exist in left folder
                if (LI.Relations == 2)
                {
                    FIWLeft = Lists.LeftListofFiles.ElementAt(0); // getting left file info
                    FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info

                    string sourceFile = Path.Combine(FIWRight.Path + FIWRight.PathInFolder, FIWRight.Name);
                    string destFile = Path.Combine(FIWLeft.Path + FIWLeft.PathInFolder, FIWRight.Name);

                    // copying right file to left folder
                    try
                    {
                        File.Copy(sourceFile, destFile, false);
                    }
                    catch (IOException e)
                    {

                        ShowExceptionMessage(e.ToString(), "Error copying file");
                    }
                }

                //if files equal but left is newer
                if (LI.Relations == 6)
                {

                    FIWLeft = Lists.LeftListofFiles.ElementAt(LI.Left); // getting left file info
                    FIWRight = Lists.RightListofFiles.ElementAt(0); //getting right file info

                    string sourceFile = Path.Combine(FIWLeft.Path + FIWLeft.PathInFolder, FIWLeft.Name);
                    string destFile = Path.Combine(FIWRight.Path + FIWRight.PathInFolder, FIWLeft.Name);

                    // copying left file to right folder
                    try
                    {
                        File.Copy(sourceFile, destFile, false);
                    }
                    catch (IOException e)
                    {

                        ShowExceptionMessage(e.ToString(), "Error copying file");
                    }
                }

                // if files are equal but right file is newer
                if (LI.Relations == 7)
                {
                    FIWLeft = Lists.LeftListofFiles.ElementAt(0); // getting left file info
                    FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info

                    string sourceFile = Path.Combine(FIWRight.Path + FIWRight.PathInFolder, FIWRight.Name);
                    string destFile = Path.Combine(FIWLeft.Path + FIWLeft.PathInFolder, FIWRight.Name);

                    // copying right file to left folder
                    try
                    {
                        File.Copy(sourceFile, destFile, false);
                    }
                    catch (IOException e)
                    {

                        ShowExceptionMessage(e.ToString(), "Error copying file");
                    }
                }

                // 23.07.16 slava - added file deletion section
                //If file from right need to be deleted
                if (LI.Relations == 5)
                {
                    FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info

                    string fileForDeletion = Path.Combine(FIWRight.Path + FIWRight.PathInFolder, FIWRight.Name);
                    try
                    {
                        File.Delete(fileForDeletion);
                    }
                    catch (IOException e)
                    {
                        
                        ShowExceptionMessage(e.ToString(), "Error deleting file");
                    }
                }

            }
        }
        
    }
}
