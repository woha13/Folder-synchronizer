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
                //20.07.16 12.26 calling assymetryc synchronize method
                AssymetricSynchronize(ListsOfFiles);
            }
        }

        //20.07.16 12.26 creating assymetricSynchronize method
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
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    string message = e.ToString();
                    string caption = "Error deleting file";
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
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
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    string message = e.ToString();
                    string caption = "Error copying file";
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                }

            }
        }

    }
}
