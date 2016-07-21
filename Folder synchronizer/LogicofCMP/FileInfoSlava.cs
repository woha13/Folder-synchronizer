using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


//woha added some changes

//Adding some changes
//
//
// adding more changes

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
        public bool EqualByContent (FileInfoWoha leftFile, FileInfoWoha rightFile)
        {
            bool result = false;
            int LeftFileByte;
            int RightFileByte;

            //checking if files equal by size 

            if (leftFile.Size == rightFile.Size)
            {
                // comparing by content
                try
                {
                    FileStream leftFS = new FileStream(leftFile.Path + leftFile.Name, FileMode.Open);
                    FileStream rightFS = new FileStream(rightFile.Path + rightFile.Name, FileMode.Open);
                
                    // reading and comparing bytes from each file until bytes will be NOT equal or until end of left file
                    do
                    {
                        LeftFileByte = leftFS.ReadByte();
                        RightFileByte = rightFS.ReadByte();
                    }
                    while ((LeftFileByte == RightFileByte) && (LeftFileByte != -1));

                    result = (LeftFileByte - RightFileByte == 0);

                    //closing files
                    leftFS.Close();
                    rightFS.Close();
                }
                
                catch (IOException e)
                {
                    ShowExceptionMessage(e.ToString(), "Error opening file");
                }
            }
            else
            {
                result = false;
            }
            
            
            return result;
        }

        //20.07.16 slava - show exception method created
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

            foreach (LinksInfo LI in Lists.linksInfo)
            {
                
                
                //if file from left folder doesn't exist in right folder
                if (LI.Right == -1)
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
                if (LI.Left == -1)
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
            }
        }
        
    }
}
