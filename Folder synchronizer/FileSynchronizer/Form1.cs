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
	public ImageList ImageList1 = new ImageList();
        public bool isAsymmetricChecked = false; //variable to send Asymmetric checkbox state for Syncronize method
        public bool isByContentChecked = false; //variable to send ByContent checkbox state for Syncronize method
        public bool isIgnoreDateChecked = false;
        public bool isWithSubdirsChecked = false;
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
                checkBoxAsymmetric.Checked, checkBoxByContent.Checked, //поміняти на змінні
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
                    //imageList.Images.Add(Image.FromFile("d:\\slava\\left.ico"));
                    //imageList.Images.Add(Image.FromFile("d:\\slava\\eqo.ico"));
                    //imageList.Images.Add(Image.FromFile("d:\\slava\\delete.ico"));
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
        
        private void checkBoxAsymmetric_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBoxAsymmetric.Checked)
            //{
            //    isAsymmetricChecked = true;
            //} else
            //{
            //    isAsymmetricChecked = false;
            //}
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
            //syncFiles.Synchronize(listsOfFiles, isAsymmetricChecked, isByContentChecked);
            //викликається копіювання файлів - поки закоментив
            
            //перевірити всі шляхи і маски на валідність

            listViewIcons.Items.Clear();

            fillLinksList();

            FolderSynchronizerForm_Load(this, e);


            //26.17.16 slava - applying disply options if any
            ClearListViews();
            //FilllistsOfFiles(listsOfFiles);
            listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
            FilllistViews();
        }


        // 20.07.16 23.27 slava - temprory using for checking equalByContent method 
        // 24.07.16 0.04 slava - temprory using for confirmation window
        private void button1_Click(object sender, EventArgs e)
        {

            ConfirmationForm confirmation = new ConfirmationForm(listsOfFiles, textBoxFolderPathLeft.Text, textBoxFolderPathRight.Text, 
                                                                checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);

            confirmation.ShowConfirmation();

            FolderSynchronizerForm_Load(this, e);
            ClearListViews();
            listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
            FilllistViews();

            //Synchronization sync = new Synchronization();

            //sync.FileHandler(listsofFiles);


            //FolderSynchronizerForm_Load(this, e);


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

        private void Down(object sender, KeyEventArgs e)
        {
            listViewRightListofFiles.TopItem = listViewLeftListofFiles.TopItem;
        }

        
        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            //listViewLeftListofFiles.AutoScrollOffset = vScrollBarForAll.AutoScrollOffset;
            //listViewLeftListofFiles.BindingContext = vScrollBarForAll.BindingContext;
            //vScrollBarForAll.Padding
            //vScrollBarForAll.Value = 100;
            listViewLeftListofFiles.TopItem = listViewLeftListofFiles.Items[5];
            listViewIcons.TopItem = listViewIcons.Items[5];
            //listView.TopItem = listView.Items[idx]
        }

        private void Moved(object sender, EventArgs e)
        {
            listViewLeftListofFiles.TopItem = listViewLeftListofFiles.Items[5];
            listViewIcons.TopItem = listViewIcons.Items[5];
        }

        // 27.07.16 slava - implement show buttons invert logic
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
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles,
                                                    listsOfFiles.RightListofFiles,
                                                    listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
                
            }
        }

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
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles,
                                                    listsOfFiles.RightListofFiles,
                                                    listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
        }

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
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles,
                                                    listsOfFiles.RightListofFiles,
                                                    listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
        }

        private void checkBoxNotEqual_CheckedChanged(object sender, EventArgs e)
        {
            //fillLinksList();
            //знайти, що перезаповнить листи по новій
            if (checkBoxNotEqual.Checked == false)
            {
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
            else
            {
                fillLinksList();
                listsOfFiles.WohaFillListBoxesNice(listsOfFiles.LeftListofFiles,
                                                    listsOfFiles.RightListofFiles,
                                                    listsOfFiles.listLinksInfo);
                listsOfFiles.deleteAllDelete(checkBoxRight.Checked, checkBoxLeft.Checked, checkBoxEqual.Checked, checkBoxNotEqual.Checked);
                ClearListViews();
                FilllistViews();
            }
        }

        //26.07.16 slava - added clearlistviewws method
        private void ClearListViews()
        {
            listViewLeftListofFiles.Items.Clear();
            listViewRightListofFiles.Items.Clear();
            listViewIcons.Items.Clear();
        }

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

        private void textBoxFolderPathLeft_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxFolderPathLeft.Text))
            {
                textBoxFolderPathLeft.BackColor = Color.LightGreen;
                buttonSyncWoha.Enabled = true;
            } else
            {
                textBoxFolderPathLeft.BackColor = Color.Red;
                buttonSyncWoha.Enabled = false;
            }
        }

        private void textBoxFolderPathRight_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxFolderPathRight.Text))
            {
                textBoxFolderPathRight.BackColor = Color.LightGreen;
                buttonSyncWoha.Enabled = true;
            }
            else
            {
                textBoxFolderPathRight.BackColor = Color.Red;
                buttonSyncWoha.Enabled = false;
            }
        }
    }
}
