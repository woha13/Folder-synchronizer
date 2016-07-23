using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogicofCMP
{
    public class FileInfoWoha
    {
        public string Path;
        public string PathInFolder;
        public string Name;
        public Int64 Size;
        public DateTime DateCreation;
        public DateTime DateModification;
        public bool IsInSubDir;
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

    public class ListsofFiles
    {
        public List<FileInfoWoha> LeftListofFiles;
        public List<FileInfoWoha> RightListofFiles;
        public List<LinksInfo> listLinksInfo;
        public List<int> WhatToDo;
        public string FileMask;
        public ListsofFiles()
        {
            LeftListofFiles = new List<FileInfoWoha>(); //лівий
            RightListofFiles = new List<FileInfoWoha>(); //правий
            //WhatToDo = new List<int>();
            listLinksInfo = new List<LinksInfo>(); // відносини між лівим та правим
        }

        public void FillListsFromPath(string SourcePath, string TargetPath,
                            bool isCheckBoxAsymmetric, bool isCheckBoxByContent,
                            bool isCheckBoxIgnoreDate, bool isCheckBoxWithsubdirs)
        {
            LeftListofFiles.Clear();
            RightListofFiles.Clear();
            listLinksInfo.Clear();
            //FillSourcePathList(SourcePath, //FolderSynchronizerForm.isWithSubdirsChecked);
            FillPathList(SourcePath, LeftListofFiles, isCheckBoxWithsubdirs);
            FillPathList(TargetPath, RightListofFiles, isCheckBoxWithsubdirs);
            RemoveStartFolder(SourcePath, LeftListofFiles);
            RemoveStartFolder(TargetPath, RightListofFiles);
            WohaAsymetricSynchronize(isCheckBoxIgnoreDate, isCheckBoxByContent);
            WohaSymetricSynchronize(isCheckBoxAsymmetric, isCheckBoxIgnoreDate, isCheckBoxByContent);
        }


        private void FillPathList(string SourcePath, List<FileInfoWoha> ListofFiles, bool withSubDirs)
        {
            // Process the list of files found in the directory.

            //var fileEntries = Directory.EnumerateFiles(SourcePath, FileMask);
            DirectoryInfo di = new DirectoryInfo(SourcePath);
            foreach (var fi in di.GetFiles())
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
                    FillPathList(subdirectory, ListofFiles, withSubDirs);
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

        public void WohaAsymetricSynchronize(bool isIgnoreDateChecked, bool isByContentChecked)
        {
            //FileInfoWoha FIW = new FileInfoWoha();
            int leftListIndex = 0;
            listLinksInfo.Clear();
            FileInfoWoha FIWRight = new FileInfoWoha();
            foreach (FileInfoWoha FIW in LeftListofFiles)
            {
                LinksInfo LI = new LinksInfo();

                LI.Relations = LinksInfo.RightIcon;

                FIWRight=RightListofFiles.Find(x => (x.Name == FIW.Name) && (x.PathInFolder == FIW.PathInFolder));

                if (FIWRight!=null)
                {
                    if ((isIgnoreDateChecked))
                    {
                        //if (FIWRight.DateModification >= FIW.DateModification)
                        //{
                            LI.Right = RightListofFiles.FindIndex(x => x.Name == FIW.Name && x.PathInFolder == FIW.PathInFolder);
                            LI.Relations = LinksInfo.EqualIcon;
                        //}
                    }
                    else if (FIW.DateModification==FIWRight.DateModification)
                    {
                        LI.Right = RightListofFiles.FindIndex(x => x.Name == FIW.Name 
                                                                   && x.PathInFolder == FIW.PathInFolder);
                        LI.Relations = LinksInfo.EqualIcon;
                    }
                    else if (FIWRight.DateModification >= FIW.DateModification)
                    {
                        LI.Right = RightListofFiles.FindIndex(x => x.Name == FIW.Name && x.PathInFolder == FIW.PathInFolder);
                        LI.Relations = LinksInfo.LeftIcon;
                    }
                }
                LI.Left = leftListIndex;
                listLinksInfo.Add(LI);
                leftListIndex++;
            }
            Console.Beep();
        }

        public void WohaSymetricSynchronize(bool isAsymmetricChecked,bool isIgnoreDateChecked, bool isByContentChecked)
        {
            if (!(isAsymmetricChecked))
            {
                int leftListIndex = 0;
                FileInfoWoha FIWLeft = new FileInfoWoha();
                foreach (FileInfoWoha FIW in RightListofFiles)
                {
                    LinksInfo LI = new LinksInfo();

                    LI.Relations = LinksInfo.LeftIcon;

                    FIWLeft = LeftListofFiles.Find(x => (x.Name == FIW.Name) && (x.PathInFolder == FIW.PathInFolder));

                    if (FIWLeft != null)
                    {
                        if ((isIgnoreDateChecked))
                        {
                                LI.Left = LeftListofFiles.FindIndex(x => x.Name == FIW.Name
                                                                     && x.PathInFolder == FIW.PathInFolder);
                                LI.Relations = LinksInfo.EqualIcon;
                        }
                        else if (FIW.DateModification == FIWLeft.DateModification)
                        {
                            LI.Left = LeftListofFiles.FindIndex(x => x.Name == FIW.Name 
                                                                      && x.PathInFolder == FIW.PathInFolder);
                            LI.Relations = LinksInfo.EqualIcon;
                        }
                        else if (FIWLeft.DateModification > FIW.DateModification)
                            {
                            LI.Left = LeftListofFiles.FindIndex(x => x.Name == FIW.Name
                                                                 && x.PathInFolder == FIW.PathInFolder);
                            LI.Relations = LinksInfo.RightIcon;
                            }
                    }
                    LI.Right= leftListIndex;
                    listLinksInfo.Add(LI);
                    leftListIndex++;
                }
                Console.Beep();
                listLinksInfo = listLinksInfo.Distinct().ToList<LinksInfo>();
            }
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
