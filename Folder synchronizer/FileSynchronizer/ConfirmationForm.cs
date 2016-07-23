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
        public ConfirmationForm()
        {
            InitializeComponent();
            
        }

        ListsofFiles listsOfFiles = new ListsofFiles();
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        public void ShowConfirmation (ListsofFiles ListsOfFiles)
        {
            

            ConfirmationForm ConfirmationForm = new ConfirmationForm();


            this.listsOfFiles = ListsOfFiles;
            ConfirmationForm.ShowDialog();

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {


            Synchronization sync = new Synchronization();

            sync.FileHandler(this.listsOfFiles);

        }


    }
}
