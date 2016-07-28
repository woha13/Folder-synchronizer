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

        //method for showing confirmation form with all relevant information and functions
        public void ShowConfirmation ()
        {
           
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
                if (isRightChecked) //check for ShowOptions
                {
                    if (LI.Relations == 1 || LI.Relations == 6) //check for file relations
                    {
                        LeftToRightAmountOfFiles++;
                        LeftToRightSizeOfFiles = LeftToRightSizeOfFiles + listsOfFiles.LeftListofFiles.ElementAt(LI.Left).Size;
                    }
                }
               
                if (isLeftChecked) //check for ShowOptions
                {
                    if (LI.Relations == 2 || LI.Relations == 7) //check for file relations
                    {
                        RightToLeftAmountOfFiles++;
                        RightToLeftSizeOfFiles = RightToLeftSizeOfFiles + listsOfFiles.RightListofFiles.ElementAt(LI.Right).Size;
                    }
                }
                
                if(isNotEqualChecked) //check for ShowOptions
                {
                    if (LI.Relations == 5) //check for file relations
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
            // calling method for handling files
            FileHandler(listsOfFiles, folderPathLeft, folderPathRight); 
            Close();    
        }
        
        public void FileHandler(ListsofFiles Lists, string folderPathLeft, string folderPathRight) 
        {
            FileInfoWoha FIWLeft = null;
            FileInfoWoha FIWRight = null;

            //if one of folder is empty by default subfolderas are empty but not null
            string pathInFolderLeft = "";
            string patInFolderRight = "";

            foreach (LinksInfo LI in Lists.listLinksInfo)
            {

                //if some list is not empty or Left or Right list does't contain -1 than we asighn a list instance and getting puth in folder
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
                if (checkBoxLeftToRight.Checked) 
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
                            ShowExceptionMessage(e.ToString(), "Error copying file");
                        }
                    }
                }

                if (checkBoxRightToLeft.Checked) 
                {
                    if (LI.Relations == 2 || LI.Relations == 7)
                    {
                        string sourceFile = Path.Combine(folderPathRight + patInFolderRight, FIWRight.Name);
                        string destFile = Path.Combine(folderPathLeft + patInFolderRight, FIWRight.Name);

                        // copying right file to left folder
                        try
                        {
                            File.Copy(sourceFile, destFile, true);
                        }
                        catch (IOException e)
                        {
                            ShowExceptionMessage(e.ToString(), "Error copying file");
                        }
                    }

                }
                    if (checkBoxRightDeleteFiles.Checked) 
                {
                    //If file from right need to be deleted
                    if (LI.Relations == 5)
                    {
                        string fileForDeletion = Path.Combine(folderPathRight + patInFolderRight, FIWRight.Name);
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
        public void ShowExceptionMessage(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
        }
    }
}
