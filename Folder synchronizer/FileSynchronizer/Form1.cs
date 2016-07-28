using FileSynchronizer.Properties;
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
using System.IO;
//using LogicofCMP;

namespace FolderSynchronizer
{
    public partial class FolderSynchronizerForm : Form
    {
        public FolderSynchronizerForm()
        {
            InitializeComponent();

            FolderPathLeftDialog.SelectedPath = @"C:\";
            FolderPathRightDialog.SelectedPath = @"C:\";
            textBoxFolderPathLeft.Text = @"C:\";
            textBoxFolderPathRight.Text = @"C:\";

            //FolderPathLeftDialog.SelectedPath = @"d:\Slava\1";
            //FolderPathRightDialog.SelectedPath = @"d:\Slava\2";
            //textBoxFolderPathLeft.Text = @"d:\Slava\1";
            //textBoxFolderPathRight.Text = @"d:\Slava\2";
            textBoxFileMask.Text = @"*.*";

            checkBoxAsymmetric.Checked = false; //Asymmetric checkbox default state
            checkBoxByContent.Checked = false; //ByContent checkbox default state
            checkBoxIgnoreDate.Checked = false; //IgnoreDate checkbox default state
            checkBoxWithsubdirs.Checked = false; //WithSubdirs checkbox default state


        }
	public ImageList ImageList1 = new ImageList();
       
        int CurrentPosition = 0;

        FolderBrowserDialog FolderPathLeftDialog = new FolderBrowserDialog(); //creating dialog window instance for left file path
        FolderBrowserDialog FolderPathRightDialog = new FolderBrowserDialog(); //creating dialog window instance for right file path
        ListsofFiles listsOfFiles = new ListsofFiles();
	    ImageList imageList = new ImageList();

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

        private void FilllistsOfFiles(ListsofFiles listsOfFiles)
        {
            listsOfFiles.FileMask = textBoxFileMask.Text;
            listsOfFiles.FillListsFromPath(textBoxFolderPathLeft.Text, textBoxFolderPathRight.Text,
                checkBoxAsymmetric.Checked, checkBoxByContent.Checked, 
                checkBoxIgnoreDate.Checked, checkBoxWithsubdirs.Checked);
        }

        private void FilllistViews()
        {
            foreach (WohaAllConnected WAC in listsOfFiles.listWohaAllConnected)
            {
                {
                    //заповнюю ліве
                    if (WAC.Left != -1)
                    {
                        listViewLeftListofFiles.Items.Add(WAC.PathInFolderLeft + "\\" + WAC.NameLeft);
                    }
                    else
                    {
                        listViewLeftListofFiles.Items.Add("");
                    }
                    //заповнюю праве
                    if (WAC.Right != -1)
                    {
                        listViewRightListofFiles.Items.Add(WAC.PathInFolderRight + "\\" + WAC.NameRight);
                    }
                    else
                    {
                        listViewRightListofFiles.Items.Add("");
                    }
                    //заповнюю середнє
		    listViewIcons.SmallImageList = imageList;
                    imageList.Images.Add(Resources.right);
                    imageList.Images.Add(Resources.left);
                    imageList.Images.Add(Resources.eqo);
                    imageList.Images.Add(Resources.delete);
                    string strIcon = "error";
                    int IconType = -1;
                    //string strIcon = "error";
                    switch (WAC.Relations)
                    {
                        case (LinksInfo.RightIcon):
                            strIcon = @"=>";
                            IconType = 0;
                            break;
                        case (LinksInfo.LeftIcon):
                            strIcon = @"<=";
                            IconType = 1;
                            break;
                        case (LinksInfo.EqualIcon):
                            strIcon = @"==";
                            IconType = 2;
                            break;
                        case (LinksInfo.NotEqualIcon):
                            strIcon = @"!=";
                            break;
                        case (LinksInfo.DeleteIcon):
                            strIcon = @"xx";
                            IconType = 3;
                            break;
                        case (LinksInfo.NotEqualToLeft):
                            strIcon = @"!=<";
                            break;
                        case (LinksInfo.NotEqualToRight):
                            strIcon = @"!=>";
                            break;
                    }
                    listViewIcons.Items.Add(strIcon, IconType);
                }
            }
            CurrentPosition = 0;
	    for (int ii = 0; ii < listViewIcons.Items.Count; ii++)
            {
                //listViewIcons.Items[ii].ImageIndex=1;
                listViewIcons.Items[ii].Text = "";
            }
        }
        private void FolderSynchronizerForm_Load(object sender, EventArgs e)
        {
            //наповнюються логічні класи LeftListofFiles і RightListofFiles
            
            //знайшов початок щоб намалювати листбокси
            listViewRightListofFiles.View = View.Details;
            listViewRightListofFiles.HeaderStyle = ColumnHeaderStyle.None;
            ColumnHeader h = new ColumnHeader();
            h.Width = listViewRightListofFiles.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            listViewRightListofFiles.Columns.Add(h);

            listViewLeftListofFiles.View = View.Details;
            listViewLeftListofFiles.HeaderStyle = ColumnHeaderStyle.None;
            ColumnHeader h1 = new ColumnHeader();
            h1.Width = listViewLeftListofFiles.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            listViewLeftListofFiles.Columns.Add(h1);

            listViewIcons.View = View.Details;
            listViewIcons.HeaderStyle = ColumnHeaderStyle.None;
            ColumnHeader h2 = new ColumnHeader();
            h2.Width = listViewIcons.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            listViewIcons.Columns.Add(h2);
            listViewIcons.Enabled = false;
            listViewLeftListofFiles.Enabled = false;
            listViewRightListofFiles.Enabled = false;

            //знайшов кінець

            //створююється листи файлів
            FilllistsOfFiles(listsOfFiles);

            //наповнюються listView 
            ClearListViews();
            FilllistViews();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void fillLinksList()
        {
            foreach (LinksInfo LI in listsOfFiles.listLinksInfo)
            {
                listViewIcons.Items.Add(LI.Left.ToString() + LI.Relations.ToString() + LI.Right.ToString());
            }
        }

        private void buttonSyncronize_Click(object sender, EventArgs e)
        {
            Synchronization syncFiles = new Synchronization();
            
            FilllistsOfFiles(listsOfFiles);
            ClearListViews();
            listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
            FilllistViews();
        }
        
        private void buttonSynchronize_Click(object sender, EventArgs e)
        {

            ConfirmationForm confirmation = new ConfirmationForm(listsOfFiles, textBoxFolderPathLeft.Text, textBoxFolderPathRight.Text, 
                                                                checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);

            confirmation.ShowConfirmation();

            FolderSynchronizerForm_Load(this, e);
            ClearListViews();
            listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
            FilllistViews();
            
        }
        
        private void Down(object sender, KeyEventArgs e)
        {
            listViewRightListofFiles.TopItem = listViewLeftListofFiles.TopItem;
        }

        private void Moved(object sender, EventArgs e)
        {
            listViewLeftListofFiles.TopItem = listViewLeftListofFiles.Items[5];
            listViewIcons.TopItem = listViewIcons.Items[5];
        }
        
        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (CurrentPosition <= listViewIcons.Items.Count-listViewIcons.ClientSize.Height/(listViewIcons.Font.Height+3))
            {
                CurrentPosition++;
                listViewLeftListofFiles.TopItem = listViewLeftListofFiles.Items[CurrentPosition];
                listViewIcons.TopItem = listViewIcons.Items[CurrentPosition];
                listViewRightListofFiles.TopItem = listViewRightListofFiles.Items[CurrentPosition];
            }
        }
        
        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (CurrentPosition > 0)
            {
                CurrentPosition--;
                listViewLeftListofFiles.TopItem = listViewLeftListofFiles.Items[CurrentPosition];
                listViewIcons.TopItem = listViewIcons.Items[CurrentPosition];
                listViewRightListofFiles.TopItem = listViewRightListofFiles.Items[CurrentPosition];
            }
        }

        //Show options Right button imlementation
        private void checkBoxRight_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRight.Checked == false)
            {
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
            else
            {
                fillLinksList();
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles, listsOfFiles.RightListofFiles, listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
        }

        //Show options Left button imlementation
        private void checkBoxLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLeft.Checked == false)
            {
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
            else
            {
                fillLinksList();
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles, listsOfFiles.RightListofFiles, listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
        }

        //Show options Equal button imlementation
        private void checkBoxEqual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEqual.Checked == false)
            {
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
            else
            {
                fillLinksList();
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles, listsOfFiles.RightListofFiles, listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
        }

        //Show options NOTEqual button imlementation
        private void checkBoxNotEqual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNotEqual.Checked == false)
            {
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
            else
            {
                fillLinksList();
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles, listsOfFiles.RightListofFiles, listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
        }

        //clearlistviews method
        private void ClearListViews()
        {
            listViewLeftListofFiles.Items.Clear();
            listViewRightListofFiles.Items.Clear();
            listViewIcons.Items.Clear();
        }

        //Show options Dupicates button imlementation
        private void checkBoxDuplicates_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDuplicates.Checked)
            {
                checkBoxEqual.Checked = true;
                checkBoxNotEqual.Checked = true;
            } else
            {
                checkBoxEqual.Checked = false;
                checkBoxNotEqual.Checked = false;
            }
        }

        //Show options Singles button imlementation
        private void checkBoxSingles_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSingles.Checked)
            {
                checkBoxRight.Checked = true;
                checkBoxLeft.Checked = true;
            } else
            {
                checkBoxRight.Checked = false;
                checkBoxLeft.Checked = false;
            }
        }

        //Left folderpath validation
        private void textBoxFolderPathLeft_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxFolderPathLeft.Text))
            {
                textBoxFolderPathLeft.BackColor = Color.LightGreen;
                buttonSyncWoha.Enabled = true;
                buttonSynchronize.Enabled = true;
            } else
            {
                textBoxFolderPathLeft.BackColor = Color.Red;
                buttonSyncWoha.Enabled = false;
                buttonSynchronize.Enabled = false;
            }
        }

        //Right folderpath validation
        private void textBoxFolderPathRight_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxFolderPathRight.Text))
            {
                textBoxFolderPathRight.BackColor = Color.LightGreen;
                buttonSyncWoha.Enabled = true;
                buttonSynchronize.Enabled = true;
            }
            else
            {
                textBoxFolderPathRight.BackColor = Color.Red;
                buttonSyncWoha.Enabled = false;
                buttonSynchronize.Enabled = false;
            }
        }
    }
}
