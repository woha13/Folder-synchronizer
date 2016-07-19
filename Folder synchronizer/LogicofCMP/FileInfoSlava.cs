using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/// <summary>
/// SAMALET
/// </summary>
namespace LogicofCMP
{
    public class Synchronization
    {

        public void Synchronize (ListsofFiles ListsOfFiles, bool isAsymmetricChecked, bool isByContentChecked)
          {
            if (isAsymmetricChecked)
            {
                FileInfoWoha TargetListFile = ListsOfFiles.RightListofFiles.First();

                foreach (FileInfoWoha FIW in ListsOfFiles.LeftListofFiles)
                {
                    
                    //!!!Path field returns not only path but also filename

                    //string sourceFile = System.IO.Path.Combine(FIW.Path, FIW.Name);
                    //string destFile = System.IO.Path.Combine(TargetListFile.Path, FIW.Name);

                    try
                    {
                       // System.IO.File.Copy(sourceFile, destFile, true);
                    }
                    catch (IOException copyError)
                    {

                    }

                }


            }
        }

    }
}
