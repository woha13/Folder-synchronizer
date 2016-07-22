using LogicofCMP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using LogicofCMP;

namespace FolderSynchronizer
{
    public partial class FolderSynchronizerForm : Form
    {
        public FolderSynchronizerForm()
        {
            InitializeComponent();

            FolderPathLeftDialog.SelectedPath = @"d:\Slava\1";
            FolderPathRightDialog.SelectedPath = @"d:\Slava\2";
            textBoxFolderPathLeft.Text = @"d:\Slava\1";
            textBoxFolderPathRight.Text = @"d:\Slava\2";
            textBoxFileMask.Text = @"*.*";

            checkBoxAsymmetric.Checked = false; //Asymmetric checkbox default state
            checkBoxByContent.Checked = false; //ByContent checkbox default state
            checkBoxIgnoreDate.Checked = false;
            checkBoxWithsubdirs.Checked = false;
        }

        public bool isAsymmetricChecked = false; //variable to send Asymmetric checkbox state for Syncronize method
        public bool isByContentChecked = false; //variable to send ByContent checkbox state for Syncronize method
        public bool isIgnoreDateChecked = false;
        public bool isWithSubdirsChecked = false;

        FolderBrowserDialog FolderPathLeftDialog = new FolderBrowserDialog(); //creating dialog window instance for left file path
        FolderBrowserDialog FolderPathRightDialog = new FolderBrowserDialog(); //creating dialog window instance for right file path
        ListsofFiles listsofFiles = new ListsofFiles();


        private void buttonFolderPathLeft_Click(object sender, EventArgs e)
        {
            
            FolderPathLeftDialog.Description = "Select folder for synchronizaton";

            // checking if user enter folder path manually then in folder browser dialog window this folder will be selected
            if (textBoxFolderPathLeft.Text != null)
            {
                FolderPathLeftDialog.SelectedPath = textBoxFolderPathLeft.Text;
            }

            
            if (FolderPathLeftDialog.ShowDialog() == DialogResult.OK) //checking if dialog window closed by clicking OK button
            {
                textBoxFolderPathLeft.Text = FolderPathLeftDialog.SelectedPath; //sending selected path into texbox
            }
        }

        private void buttonFolderPathRight_Click(object sender, EventArgs e)
        {
            FolderPathRightDialog.Description = "Select folder for synchronizaton";

            if (textBoxFolderPathRight.Text != null)
            {
                FolderPathRightDialog.SelectedPath = textBoxFolderPathRight.Text;
            }


            if (FolderPathRightDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFolderPathRight.Text = FolderPathRightDialog.SelectedPath;
            }
        }

        private void FolderSynchronizerForm_Load(object sender, EventArgs e)
        {
            //наповнюються логічні класи LeftListofFiles і RightListofFiles

            listsofFiles.FileMask = textBoxFileMask.Text;
            listsofFiles.FillListsFromPath(textBoxFolderPathLeft.Text, textBoxFolderPathRight.Text,
                checkBoxAsymmetric.Checked, checkBoxByContent.Checked, //поміняти на змінні
                checkBoxIgnoreDate.Checked, checkBoxWithsubdirs.Checked);
            
            //наповнюються listView 
            listViewLeftListofFiles.Items.Clear();
            foreach (FileInfoWoha FIW in listsofFiles.LeftListofFiles)
            {
                listViewLeftListofFiles.Items.Add(listViewLeftListofFiles.Items.Count.ToString()+
                    " - "+ FIW.Path+'~'+FIW.PathInFolder+
                    '~' + FIW.Name+ " size:" + FIW.Size.ToString());
            }

            listViewRightListofFiles.Items.Clear();
            foreach (FileInfoWoha FIW in listsofFiles.RightListofFiles)
            {
                listViewRightListofFiles.Items.Add(listViewRightListofFiles.Items.Count.ToString() + 
                    " - "+FIW.Path + '~' + FIW.PathInFolder + 
                    '~' + FIW.Name + " size:" + FIW.Size.ToString());
            }

            listViewIcons.Items.Clear();
            foreach (LinksInfo LI in listsofFiles.listLinksInfo)
            {
                listViewIcons.Items.Add(LI.Left.ToString() + '-' + LI.Relations.ToString() + ' ' + LI.Right.ToString());
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void checkBoxAsymmetric_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAsymmetric.Checked)
            {
                isAsymmetricChecked = true;
            } else
            {
                isAsymmetricChecked = false;
            }
        }

        private void checkBoxByContent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxByContent.Checked)
            {
                isByContentChecked = true;
            } else
            {
                isByContentChecked = false;
            }
        }

        private void buttonSyncronize_Click(object sender, EventArgs e)
        {
            Synchronization syncFiles = new Synchronization();
            syncFiles.Synchronize(listsofFiles, isAsymmetricChecked, isByContentChecked);

            listViewIcons.Items.Clear();
            
            foreach (LinksInfo LI in listsofFiles.listLinksInfo)
            {
                listViewIcons.Items.Add(LI.Left.ToString()+LI.Relations.ToString()+LI.Right.ToString());
            }
            FolderSynchronizerForm_Load(this, e);
        }


        // 20.07.16 23.27 slava - temprory using for checking equalByContent method 
        private void button1_Click(object sender, EventArgs e)
        {


            Synchronization sync = new Synchronization();

            sync.FileHandler(listsofFiles);


            FolderSynchronizerForm_Load(this, e);


            //Synchronization sync = new Synchronization();

            //MessageBoxButtons buttons = MessageBoxButtons.OK;
            //DialogResult result;
            //string message = (sync.EqualByContent(listsofFiles.LeftListofFiles.First(), listsofFiles.RightListofFiles.First())).ToString();
            //string caption = "Files equal:";
            //result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);

            //FolderSynchronizerForm_Load(this, e);
        }

        private void checkBoxWithsubdirs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWithsubdirs.Checked)
            {
                isWithSubdirsChecked = true;
            }
            else
            {
                isWithSubdirsChecked = false;
            }
        }
    }
}
