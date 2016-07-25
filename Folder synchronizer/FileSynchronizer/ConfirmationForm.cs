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

namespace FolderSynchronizer
{
    public partial class ConfirmationForm : Form
    {
        public ConfirmationForm(ListsofFiles inputlistsOfFiles, string s1, string s2)
        {
            InitializeComponent();

            listsOfFiles = inputlistsOfFiles;
        }

        ListsofFiles listsOfFiles;
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        //24.07 22.30 slava - implement confirmation form with all relevant information and functions
        public void ShowConfirmation ()
        {
            
            textBoxLeftToRight.Text = listsOfFiles.LeftListofFiles.ElementAt(0).Path;
            textBoxRightToLeft.Text = listsOfFiles.RightListofFiles.ElementAt(0).Path;

            int LeftToRightAmountOfFiles = 0;
            long LeftToRightSizeOfFiles = 0;

            int RightToLeftAmountOfFiles = 0;
            long RightToLeftSizeOfFiles = 0;

            int DeleteFilesAmountOfFiles = 0;
            long DeleteFilesSizeOfFiles = 0;

            //gathering info about files
            foreach (LinksInfo LI in listsOfFiles.listLinksInfo)
            {
                if (LI.Relations == 1 || LI.Relations == 6)
                {
                    LeftToRightAmountOfFiles++;
                    LeftToRightSizeOfFiles = LeftToRightSizeOfFiles + listsOfFiles.LeftListofFiles.ElementAt(LI.Left).Size;
                }

                if (LI.Relations == 2 || LI.Relations == 7)
                {
                    RightToLeftAmountOfFiles ++;
                    RightToLeftSizeOfFiles = RightToLeftSizeOfFiles + listsOfFiles.RightListofFiles.ElementAt(LI.Right).Size;

                }

                if (LI.Relations == 5)
                {
                    DeleteFilesAmountOfFiles++;
                    DeleteFilesSizeOfFiles = DeleteFilesSizeOfFiles + listsOfFiles.RightListofFiles.ElementAt(LI.Right).Size;
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


            Synchronization sync = new Synchronization();

            sync.FileHandler(listsOfFiles);

            Close();

        }
        
    }
}
