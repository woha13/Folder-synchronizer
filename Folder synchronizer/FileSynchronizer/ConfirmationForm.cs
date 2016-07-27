using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicofCMP;
using System.IO;

namespace FolderSynchronizer
{
    public partial class ConfirmationForm : Form
    {
        public ConfirmationForm(ListsofFiles inputlistsOfFiles, string folderPathLeft, string folderPathRight, 
                                bool isRightChecked, bool isLeftChecked, bool isEqualChecked, bool isNotEqualChecked)
        {
            InitializeComponent();

            listsOfFiles = inputlistsOfFiles;
            this.folderPathLeft = folderPathLeft;
            this.folderPathRight = folderPathRight;

            this.isRightChecked = isRightChecked;
            this.isLeftChecked = isLeftChecked;
            this.isEqualChecked = isEqualChecked;
            this.isNotEqualChecked = isNotEqualChecked;


        }

        ListsofFiles listsOfFiles;
        string folderPathLeft;
        string folderPathRight;
        bool isRightChecked;
        bool isLeftChecked;
        bool isEqualChecked;
        bool isNotEqualChecked;


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        //24.07 22.30 slava - implement confirmation form with all relevant information and functions
        public void ShowConfirmation ()
        {
            //25.07 1.33 slava - changed getting folder paths 
            textBoxLeftToRight.Text = folderPathLeft;
            textBoxRightToLeft.Text = folderPathRight;

            int LeftToRightAmountOfFiles = 0;
            long LeftToRightSizeOfFiles = 0;

            int RightToLeftAmountOfFiles = 0;
            long RightToLeftSizeOfFiles = 0;

            int DeleteFilesAmountOfFiles = 0;
            long DeleteFilesSizeOfFiles = 0;

            //gathering info about files
            foreach (LinksInfo LI in listsOfFiles.listLinksInfo)
            {
                if (isRightChecked)
                {
                    if (LI.Relations == 1 || LI.Relations == 6)
                    {
                        LeftToRightAmountOfFiles++;
                        LeftToRightSizeOfFiles = LeftToRightSizeOfFiles + listsOfFiles.LeftListofFiles.ElementAt(LI.Left).Size;
                    }
                }
               
                if (isLeftChecked)
                {
                    if (LI.Relations == 2 || LI.Relations == 7)
                    {
                        RightToLeftAmountOfFiles++;
                        RightToLeftSizeOfFiles = RightToLeftSizeOfFiles + listsOfFiles.RightListofFiles.ElementAt(LI.Right).Size;

                    }
                }
                
                if(isNotEqualChecked)
                {
                    if (LI.Relations == 5)
                    {
                        DeleteFilesAmountOfFiles++;
                        DeleteFilesSizeOfFiles = DeleteFilesSizeOfFiles + listsOfFiles.RightListofFiles.ElementAt(LI.Right).Size;
                    }
                }

                
            } 

            // activating controls and showing info if needed
            if (LeftToRightAmountOfFiles != 0)
            {
                checkBoxLeftToRight.Checked = true;
                checkBoxLeftToRight.Enabled = true;
                labelLeftToRight.Text = LeftToRightAmountOfFiles + " files. Total size " + LeftToRightSizeOfFiles + " bytes.";
            }
            
            if (RightToLeftAmountOfFiles != 0)
            {
                checkBoxRightToLeft.Checked = true;
                checkBoxRightToLeft.Enabled = true;
                labelRightToLeft.Text = RightToLeftAmountOfFiles + " files. Total size " + RightToLeftSizeOfFiles + " bytes.";
            }

            if (DeleteFilesAmountOfFiles != 0)
            {
                checkBoxRightDeleteFiles.Checked = true;
                checkBoxRightDeleteFiles.Enabled = true;
                labelDeleteFiles.Text = DeleteFilesAmountOfFiles + " files. Total size " + DeleteFilesSizeOfFiles + " bytes.";
            }
            
            ShowDialog();

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            
            // 25.07.16 16.33 slava - confirmation checkbox logic implementation
            //sync.FileHandler(listsOfFiles, folderPathLeft, folderPathRight, checkBoxLeftToRight.Checked, checkBoxRightToLeft.Checked, checkBoxRightDeleteFiles.Checked);
            FileHandler(listsOfFiles, folderPathLeft, folderPathRight); 
            Close();
            
        }

        // 25.07.16 16.33 slava - confirmation checkbox logic implementation
        private void checkBoxLeftToRight_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        // 25.07.16 16.33 slava - confirmation checkbox logic implementation
        private void checkBoxRightToLeft_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        //25.07.16 16.33 slava - confirmation checkbox logic implementation
        private void checkBoxRightDeleteFiles_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        public void FileHandler(ListsofFiles Lists, string folderPathLeft, string folderPathRight) //, bool checkboxLeftToRightIsChecked, bool checkboxRightToLeftIsChecked, bool checkboxDeleteFromRightIsChecked
        {

            Synchronization sync = new Synchronization();
            FileInfoWoha FIWLeft = null;
            FileInfoWoha FIWRight = null;

            //25.07.16 13.38 slava - if one of folder is empty by default subfolderas are empty but not null
            string pathInFolderLeft = "";
            string patInFolderRight = "";

            foreach (LinksInfo LI in Lists.listLinksInfo)
            {

                //25.07.16 13.38 slava - if some list is not empty or Left or Right list does't containf -1 than we create a list instance and getting puth in folder
                if (Lists.LeftListofFiles.Count != 0 && LI.Left != -1 )
                {
                    FIWLeft = Lists.LeftListofFiles.ElementAt(LI.Left); // getting left file info
                    pathInFolderLeft = Lists.LeftListofFiles.ElementAt(LI.Left).PathInFolder;
                }

                if (Lists.RightListofFiles.Count != 0 && LI.Right != -1)
                {
                    FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info
                    patInFolderRight = Lists.RightListofFiles.ElementAt(LI.Right).PathInFolder;
                }


                //if file from left folder doesn't exist in right folder

                // 25.07.16 16.33 slava - confirmation checkbox logic implementation
                if (checkBoxLeftToRight.Checked) //checkboxLeftToRightIsChecked
                {
                    if (LI.Relations == 1 || LI.Relations == 6)
                    {

                        string sourceFile = Path.Combine(folderPathLeft + pathInFolderLeft, FIWLeft.Name);
                        string destFile = Path.Combine(folderPathRight + pathInFolderLeft, FIWLeft.Name);

                        // copying left file to right folder
                        try
                        {
                            File.Copy(sourceFile, destFile, true);
                        }
                        catch (IOException e)
                        {

                            sync.ShowExceptionMessage(e.ToString(), "Error copying file");
                        }
                    }

                }

                // 25.07.16 16.33 slava - confirmation checkbox logic implementation
                if (checkBoxRightToLeft.Checked) //checkboxRightToLeftIsChecked
                {
                    // if file from right folder doesn't exist in left folder
                    if (LI.Relations == 2 || LI.Relations == 7)
                    {
                        //FIWLeft = Lists.LeftListofFiles.ElementAt(0); // getting left file info
                        //FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info

                        string sourceFile = Path.Combine(folderPathRight + patInFolderRight, FIWRight.Name);
                        string destFile = Path.Combine(folderPathLeft + patInFolderRight, FIWRight.Name);

                        // copying right file to left folder
                        try
                        {
                            File.Copy(sourceFile, destFile, true);
                        }
                        catch (IOException e)
                        {

                            sync.ShowExceptionMessage(e.ToString(), "Error copying file");
                        }
                    }

                }

                // 25.07.16 16.33 slava - confirmation checkbox logic implementation
                if (checkBoxRightDeleteFiles.Checked) //checkboxDeleteFromRightIsChecked
                {
                    // 23.07.16 slava - added file deletion section
                    //If file from right need to be deleted
                    if (LI.Relations == 5)
                    {
                        //FIWRight = Lists.RightListofFiles.ElementAt(LI.Right); //getting right file info

                        string fileForDeletion = Path.Combine(folderPathRight + patInFolderRight, FIWRight.Name);
                        try
                        {
                            File.Delete(fileForDeletion);
                        }
                        catch (IOException e)
                        {

                            sync.ShowExceptionMessage(e.ToString(), "Error deleting file");
                        }
                    }
                }

            }
        }
    }
}
