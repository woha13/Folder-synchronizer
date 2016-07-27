using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LogicofCMP
{
    public class WohaAllConnected
    {
        public int Left;
        public int Right;
        public int Relations;
        public string PathInFolderLeft;
        public string NameLeft;
        public string PathInFolderRight;
        public string NameRight;
        public WohaAllConnected()
        {
            Left = -1;
            Right = -1;
            Relations = 0;
            PathInFolderLeft = ""; //Шлях у папці
            NameLeft = "";
            PathInFolderRight = ""; //Шлях у папці
            NameRight = "";
        }
    }

    public class FileInfoWoha
    {
        public string Path;
        public Int64 Size;
        public DateTime DateCreation;
        public DateTime DateModification;
        public bool IsInSubDir;
        public string PathInFolder;
        public string Name;
        public FileInfoWoha()
        {
            Path = ""; //Шлях до папки
            PathInFolder = ""; //Шлях у папці
            Name = "";
            Size = 0;
            DateCreation = DateTime.MinValue;
            DateModification = DateTime.MinValue;
            IsInSubDir = false;
        }
    }
    public class LinksInfo : IEquatable<LinksInfo>
    {
        public int Left;
        public int Right;
        public int Relations;
        public LinksInfo()
        {
            Left = -1;
            Right = -1;
            Relations = 0;
        }
        //1 - Right
        //2 - Left
        //3 - Equal
        //4 - NotEqual
        //5 - DoesNotExist
        public const int RightIcon = 1;
        public const int LeftIcon = 2;
        public const int EqualIcon = 3;
        public const int NotEqualIcon = 4;
        public const int NotEqualToLeft = 6;   // Лівий новіший
        public const int NotEqualToRight = 7;  // Правий новіший
        public const int DeleteIcon = 5; //File needed to be deleted

        //тре таке створити в асінхронному режимі

        public bool Equals(LinksInfo other)
        {
            if ((Left==other.Left)&&(Right==other.Right)&&(Relations==other.Relations))
                {
                return true;
                }
            return false;
        }
        public override int GetHashCode()
        {
            int hashLeft = Left == null ? 0 : Left.GetHashCode();
            int hashRight = Right == null ? 0 : Right.GetHashCode();
            int hashRelations = Relations == null ? 0 : Relations.GetHashCode();
            return hashLeft  ^ hashRelations ^ hashRight;
        }
    }

    public partial class ListsofFiles
    {
        public List<FileInfoWoha> LeftListofFiles;
        public List<FileInfoWoha> RightListofFiles;
        public List<LinksInfo> listLinksInfo;
        public List<WohaAllConnected> listWohaAllConnected;
        public string FileMask;
        public ListsofFiles()
        {
            LeftListofFiles = new List<FileInfoWoha>(); //лівий
            RightListofFiles = new List<FileInfoWoha>(); //правий
            listLinksInfo = new List<LinksInfo>(); // відносини між лівим та правим
            listWohaAllConnected = new List<WohaAllConnected>();
        }

        public void FillListsFromPath(string SourcePath, string TargetPath,
                            bool isCheckBoxAsymmetric, bool isCheckBoxByContent,
                            bool isCheckBoxIgnoreDate, bool isCheckBoxWithsubdirs)
        {
            LeftListofFiles.Clear();
            RightListofFiles.Clear();
            listLinksInfo.Clear();
            //FillSourcePathList(SourcePath, //FolderSynchronizerForm.isWithSubdirsChecked);
            FillPathList(SourcePath, LeftListofFiles, isCheckBoxWithsubdirs, FileMask);
            FillPathList(TargetPath, RightListofFiles, isCheckBoxWithsubdirs, FileMask);
            RemoveStartFolder(SourcePath, LeftListofFiles);
            RemoveStartFolder(TargetPath, RightListofFiles);
            WohaAsymetricSynchronize(isCheckBoxAsymmetric, isCheckBoxIgnoreDate, isCheckBoxByContent);
            WohaSymetricSynchronize(isCheckBoxAsymmetric, isCheckBoxIgnoreDate, isCheckBoxByContent);
            WohaFillListBoxesNice(LeftListofFiles, RightListofFiles, listLinksInfo);
            //якшо треба асіметрік - вичищаємо все, що копіює наліво
        }
        /// <summary>
        /// WohaFillListBoxesNice - заповнює гарненько лисбокси
        /// </summary>
        /// <param name="LeftListofFiles"></param>  Лівий лист
        /// <param name="RightListofFiles"></param> Правий лист
        /// <param name="listLinksInfo"></param>    Віднисини між ними
        public void WohaFillListBoxesNice(List<FileInfoWoha> LeftListofFiles, List<FileInfoWoha> RightListofFiles, List<LinksInfo> listLinksInfo)
        {
            FileInfoWoha EmptyFile = new FileInfoWoha();
            listWohaAllConnected.Clear(); //все відбувається тут
            foreach (LinksInfo LI in listLinksInfo)
            {
                WohaAllConnected AllConnected = new WohaAllConnected();
                AllConnected.Left = LI.Left;
                if (LI.Left != -1)
                {
                    AllConnected.NameLeft = LeftListofFiles.ElementAt(LI.Left).Name;
                    AllConnected.PathInFolderLeft = LeftListofFiles.ElementAt(LI.Left).PathInFolder;
                }
                AllConnected.Right = LI.Right;
                if (LI.Right != -1)
                {
                    AllConnected.NameRight = RightListofFiles.ElementAt(LI.Right).Name;
                    AllConnected.NameLeft = RightListofFiles.ElementAt(LI.Right).Name;
                    if (LI.Left!=-1)
                    {
                        AllConnected.PathInFolderLeft = LeftListofFiles.ElementAt(LI.Left).PathInFolder;
                    }
                    AllConnected.PathInFolderRight = RightListofFiles.ElementAt(LI.Right).PathInFolder;
                }
                AllConnected.Relations = LI.Relations;
                listWohaAllConnected.Add(AllConnected);
            }
            var new1 = listWohaAllConnected.OrderBy(x => x.PathInFolderLeft+x.NameLeft).ToList();
            listWohaAllConnected = new1;

        }

        public void ShowExceptionMessage(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
        }

        public bool EqualByContent(FileInfoWoha leftFile, FileInfoWoha rightFile)
        {
            bool result = false;
            int LeftFileByte;
            int RightFileByte;

            if (leftFile.Size == rightFile.Size)
            {
                // comparing by content
                try
                {
                    FileStream leftFS = new FileStream(leftFile.Path + leftFile.PathInFolder+  "\\" + leftFile.Name, FileMode.Open);
                    FileStream rightFS = new FileStream(rightFile.Path + rightFile.PathInFolder + "\\" + rightFile.Name, FileMode.Open);
                    // reading and comparing bytes from each file until bytes will be NOT equal or until end of left file
                    do
                    {
                        LeftFileByte = leftFS.ReadByte();
                        RightFileByte = rightFS.ReadByte();
                    }
                    while ((LeftFileByte == RightFileByte) && (LeftFileByte != -1));

                    result = (LeftFileByte - RightFileByte == 0);
                    //closing files
                    leftFS.Close();
                    rightFS.Close();
                }
                catch (IOException e)
                {
                    ShowExceptionMessage(e.ToString(), "Error opening file");
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void FillPathList(string SourcePath, List<FileInfoWoha> ListofFiles, bool withSubDirs, string Mask)
        {
            // Process the list of files found in the directory.
            DirectoryInfo di = new DirectoryInfo(SourcePath);
            try
            {
            foreach (var fi in di.GetFiles(Mask))
            {

                FileInfoWoha fileData = new FileInfoWoha();
                fileData.Name = fi.Name;
                fileData.PathInFolder = fi.Directory.ToString();
                fileData.Path = SourcePath;
                fileData.Size = fi.Length;
                fileData.DateModification = fi.LastWriteTime;
                fileData.DateCreation = fi.CreationTime;
                ListofFiles.Add(fileData);
            }


            // Recurse into subdirectories of this directory.
            if (withSubDirs)
            {
                string[] subdirectoryEntries = Directory.GetDirectories(SourcePath);
                foreach (string subdirectory in subdirectoryEntries)
                    FillPathList(subdirectory, ListofFiles, withSubDirs, Mask);
            }
        }
             catch (System.ArgumentException e)
            {
                ShowExceptionMessage(e.ToString(), "Woha Error opening file");
            }
        }

        private void RemoveStartFolder(string Path, List<FileInfoWoha> listFIW)
        {            
            foreach (FileInfoWoha FIW in listFIW)
            {
                FIW.Path = Path;
                FIW.PathInFolder = FIW.PathInFolder.Remove(0, Path.Length);
            }
        }

        private void ReplaceDeleteforAsimetic(List<LinksInfo> listLinksInfo)
        {
            foreach (LinksInfo LI in listLinksInfo)
            {
                if (LI.Relations==LinksInfo.LeftIcon)
                {
                    LI.Relations = LinksInfo.DeleteIcon;
                }

            }

        }

        public void WohaAsymetricSynchronize(bool isAsymmetricChecked, bool isIgnoreDateChecked, bool isByContentChecked)
        {
            int leftListIndex = 0;
            listLinksInfo.Clear();
            FileInfoWoha FIWRight = new FileInfoWoha();
            foreach (FileInfoWoha FIW in LeftListofFiles)
            {
                LinksInfo LI = new LinksInfo();

                FIWRight=RightListofFiles.Find(x => (x.Name == FIW.Name) 
                                                 && (x.PathInFolder == FIW.PathInFolder));
                //порівнюємо лівий файл
                if (FIWRight != null) //якщо э з таким ім'ям і шляхом зправа, то йдем далі
                {
                    LI.Right = RightListofFiles.FindIndex(x => x.Name == FIW.Name 
                                                            && x.PathInFolder == FIW.PathInFolder); //заповняємо структуру даних файла зправа, 
                                                                                                    //з яким будемо порівнювати
                                                                                                    //схоже тре міняти на Switch
                    if (!(isIgnoreDateChecked)) //якщо ігнорувати дату то копіюємо новіший.
                    {
                        if (FIW.DateModification == FIWRight.DateModification) // інакше якщо тре перевіряти дату, а вона рівна, то файли рівні
                        {
                            LI.Relations = LinksInfo.EqualIcon;
                        }
                        else
                        if (FIWRight.DateModification > FIW.DateModification)  //інакше якшо дата новіша, то
                        {
                            if (isAsymmetricChecked)                                //якщо асиметричне порівняння - то вони рівні, бо не тре зправа наліво 
                            {
                                LI.Relations = LinksInfo.EqualIcon; //можна поставити таку саму 
                                                                    //сіру іконку, як в УС
                            }
                            else                                                    //якщо симетричне порівняння  - то тре з право на ліво закидати
                            {
                                LI.Relations = LinksInfo.LeftIcon;
                                //LI.Relations = LinksInfo.NotEqualToLeft; // лівий новіший
                            }
                        }
                        else
                        if (FIWRight.DateModification < FIW.DateModification)  //якщо ліве новіше, то тре закинути його направо
                        {
                            LI.Relations = LinksInfo.RightIcon;
                            //LI.Relations = LinksInfo.NotEqualToRight; // правий новіший
                        }
                    }
                    else
                    {
                        if (isByContentChecked)
                        {
                            //Копіюємо той, що більший, або зліва направо
                            if(FIW.Size>FIWRight.Size)
                            {
                                LI.Relations = LinksInfo.RightIcon;
                            }
                            else
                            {
                                if (EqualByContent(FIW, FIWRight))
                                {
                                    LI.Relations = LinksInfo.EqualIcon;
                                }
                                else
                                {
                                    LI.Relations = LinksInfo.RightIcon;
                                }
                            }

                        }
                        else
                        {
                            //повертаємо, що вони рівні
                            LI.Relations = LinksInfo.EqualIcon;
                        }
                    }
                }
                else //якщо зправа файла немає, його тре туди зкопіювати
                {
                    LI.Relations = LinksInfo.RightIcon;
                }
                
                LI.Left = leftListIndex;
                listLinksInfo.Add(LI);
                leftListIndex++;
            }
            //Console.Beep();
        }

        public void WohaSymetricSynchronize(bool isAsymmetricChecked, bool isIgnoreDateChecked, bool isByContentChecked)
        {
            int leftListIndex = 0;
            FileInfoWoha FIWLeft = new FileInfoWoha();
            foreach (FileInfoWoha FIW in RightListofFiles)
            {
                LinksInfo LI = new LinksInfo();
                FIWLeft = LeftListofFiles.Find(x => (x.Name == FIW.Name) && (x.PathInFolder == FIW.PathInFolder));

                if (FIWLeft == null)
                {
                    LI.Relations = LinksInfo.LeftIcon;
                    LI.Right = leftListIndex;
                    listLinksInfo.Add(LI);
                }
                leftListIndex++;
            }
            if (isAsymmetricChecked)
            {
                ReplaceDeleteforAsimetic(listLinksInfo);
            }
            //Console.Beep();
        }
    }
    public partial class Synchronization
    {
        public bool CompareBy(FileInfoWoha FIW1, FileInfoWoha FIW2, bool isByContentChecked, 
                                bool isIgnoreDateChecked)
        {
            bool State=false;
            if ((FIW1.Name == FIW2.Name) && (FIW1.Path == FIW2.Path)&&(FIW1.DateModification == FIW2.DateModification))
            {
                State = true;
            }

            if (isIgnoreDateChecked)
            {
                if((FIW1.Name == FIW2.Name) && (FIW1.Path == FIW2.Path))
                {
                    State = true;
                }
            }

            if ((isByContentChecked) && (State))
            {
                if (FIW1.Size == FIW2.Size)
                {
                    State = true;
                }
                else
                {
                    State = false;
                }
            }

            return State;
        }
    }
}
//////////////////////
////////////////////////