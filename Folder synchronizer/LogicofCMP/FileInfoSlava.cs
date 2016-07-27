using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

//QQ
//1 How to recieve folder path if there is no file in the list?
//2. Check subfolder path for right files.

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

        //25.07.16 13.38 slava - inserting folderpaths parameters into methos and change implementation
        //25.07.16 16.33 slava - confirmation checkbox logic implementation
        //public void FileHandler(ListsofFiles Lists, string folderPathLeft, string folderPathRight, bool checkboxLeftToRightIsChecked, bool checkboxRightToLeftIsChecked, bool checkboxDeleteFromRightIsChecked)
        //{

        //    FileInfoWoha FIWLeft = null;
        //    FileInfoWoha FIWRight = null;

        //    //25.07.16 13.38 slava - if one of folder is empty by default subfolderas are empty but not null
        //    string pathInFolderLeft = "";
        //    string patInFolderRight = "";

        //    foreach (LinksInfo LI in Lists.listLinksInfo)
        //    {

        //        //25.07.16 13.38 slava - if some list is not empty or Left or Right list does't containf -1 than we create a list instance and getting puth in folder
        //        if (Lists.LeftListofFiles.Count != 0 && LI.Left != -1)
        //        {
        //            FIWLeft = Lists.LeftListofFiles.ElementAt(LI.Left); // getting left file info
        //            pathInFolderLeft = Lists.LeftListofFiles.ElementAt(LI.Left).PathInFolder;
        //        }

        //        if (Lists.RightListofFiles.Count !=0 && LI.Right != -1)
        //        {
        //            FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info
        //            patInFolderRight = Lists.RightListofFiles.ElementAt(LI.Right).PathInFolder;
        //        }


        //        //if file from left folder doesn't exist in right folder

        //        // 25.07.16 16.33 slava - confirmation checkbox logic implementation
        //        if (checkboxLeftToRightIsChecked)
        //        {
        //            if (LI.Relations == 1 || LI.Relations == 6)
        //            {
                        
        //                string sourceFile = Path.Combine(folderPathLeft + pathInFolderLeft, FIWLeft.Name);
        //                string destFile = Path.Combine(folderPathRight + pathInFolderLeft, FIWLeft.Name);

        //                // copying left file to right folder
        //                try
        //                {
        //                    File.Copy(sourceFile, destFile, true);
        //                }
        //                catch (IOException e)
        //                {

        //                    ShowExceptionMessage(e.ToString(), "Error copying file");
        //                }
        //            }

        //        }

        //        // 25.07.16 16.33 slava - confirmation checkbox logic implementation
        //        if (checkboxRightToLeftIsChecked)
        //        {
        //            // if file from right folder doesn't exist in left folder
        //            if (LI.Relations == 2 || LI.Relations == 7)
        //            {
        //                //FIWLeft = Lists.LeftListofFiles.ElementAt(0); // getting left file info
        //                //FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info

        //                string sourceFile = Path.Combine(folderPathRight + patInFolderRight, FIWRight.Name);
        //                string destFile = Path.Combine(folderPathLeft + patInFolderRight, FIWRight.Name);

        //                // copying right file to left folder
        //                try
        //                {
        //                    File.Copy(sourceFile, destFile, true);
        //                }
        //                catch (IOException e)
        //                {

        //                    ShowExceptionMessage(e.ToString(), "Error copying file");
        //                }
        //            }

        //        }

        //        // 25.07.16 16.33 slava - confirmation checkbox logic implementation
        //        if (checkboxDeleteFromRightIsChecked)
        //        {
        //            // 23.07.16 slava - added file deletion section
        //            //If file from right need to be deleted
        //            if (LI.Relations == 5)
        //            {
        //                //FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info

        //                string fileForDeletion = Path.Combine(folderPathRight + patInFolderRight, FIWRight.Name);
        //                try
        //                {
        //                    File.Delete(fileForDeletion);
        //                }
        //                catch (IOException e)
        //                {

        //                    ShowExceptionMessage(e.ToString(), "Error deleting file");
        //                }
        //            }
        //        }
                
        //    }
        //}
        
    }





    public partial class ListsofFiles
    {
        // 27.07.16 slava - implement show buttons invert logic
        public void deleteAllDelete(bool checkboxRightIsChecked, bool CheckboxLeftIsChecked, bool checkBoxEqualIsChecked, bool checkBoxNotEqualIsChecked)
        {
            for (int ii = 0; ii<listWohaAllConnected.Count; ii++)
            {
                int jj = listWohaAllConnected.ElementAt(ii).Relations;
                if ((jj==LinksInfo.DeleteIcon || jj == LinksInfo.NotEqualIcon) && checkBoxNotEqualIsChecked == false)
                {
                    //string kk = listWohaAllConnected.ElementAt(ii).NameLeft; //What for?
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }

                if ((jj == LinksInfo.RightIcon || jj == LinksInfo.NotEqualToRight) && checkboxRightIsChecked == false)
                {
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }

                if ((jj == LinksInfo.LeftIcon || jj == LinksInfo.NotEqualToLeft) && CheckboxLeftIsChecked == false)
                {
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }

                if (jj == LinksInfo.EqualIcon && checkBoxEqualIsChecked == false)
                {
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }
            }

        }
    }
}
