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
    public class LinksInfo
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
        public const int DNEIcon = 5; //does not exist
    }

    public class ListsofFiles
    {
        public List<FileInfoWoha> LeftListofFiles;
        public List<FileInfoWoha> RightListofFiles;
        public List<LinksInfo> linksInfo;
        public List<int> WhatToDo;
        public string FileMask;
        public ListsofFiles()
        {
            LeftListofFiles = new List<FileInfoWoha>(); //лівий
            RightListofFiles = new List<FileInfoWoha>(); //правий
            //WhatToDo = new List<int>();
            linksInfo = new List<LinksInfo>(); // відносини між лівим та правим
        }

        public void FillListsFromPath(string SourcePath, string TargetPath,
                            bool isCheckBoxAsymmetric, bool isCheckBoxByContent,
                            bool isCheckBoxIgnoreDate, bool isCheckBoxWithsubdirs)
        {
            LeftListofFiles.Clear();
            RightListofFiles.Clear();
            //FillSourcePathList(SourcePath, //FolderSynchronizerForm.isWithSubdirsChecked);
            FillSourcePathList(SourcePath, LeftListofFiles, isCheckBoxWithsubdirs);
            FillSourcePathList(TargetPath, RightListofFiles, isCheckBoxWithsubdirs);
            RemoveStartFolder(SourcePath, LeftListofFiles);
            RemoveStartFolder(TargetPath, RightListofFiles);
        }


        private void FillSourcePathList(string SourcePath, List<FileInfoWoha> ListofFiles, bool withSubDirs)
        {
            // Process the list of files found in the directory.

            var fileEntries = Directory.EnumerateFiles(SourcePath, FileMask);
            foreach (string fileName in fileEntries)
            {
                FileInfoWoha fileData = new FileInfoWoha();
                fileData.Name = fileName.Substring(SourcePath.Length + 1);
                fileData.PathInFolder= fileName.Remove(SourcePath.Length + 1, fileName.Length - SourcePath.Length - 1);
                fileData.Path = SourcePath;
                ListofFiles.Add(fileData);
            }
            // Recurse into subdirectories of this directory.
            if (withSubDirs)
            {
                string[] subdirectoryEntries = Directory.GetDirectories(SourcePath);
                foreach (string subdirectory in subdirectoryEntries)
                    FillSourcePathList(subdirectory, ListofFiles, withSubDirs);
            }
        }

        private void RemoveStartFolder(string Path, List<FileInfoWoha> listFIW)
        {            
            foreach (FileInfoWoha FIW in listFIW)
            {
                FIW.Path = Path;
                FIW.PathInFolder = FIW.PathInFolder.Remove(0, Path.Length + 1);
            }
        }


        private void FillTargetPathList(string TargetPath)
        {
            // Process the list of files found in the directory.

            var fileEntries = Directory.EnumerateFiles(TargetPath, FileMask);
            foreach (string fileName in fileEntries)
            {
                FileInfoWoha fileData = new FileInfoWoha();
                fileData.Name = fileName.Substring(TargetPath.Length + 1);
                fileData.PathInFolder = fileName.Remove(TargetPath.Length + 1, fileName.Length - TargetPath.Length - 1);
                fileData.Path = TargetPath;
                RightListofFiles.Add(fileData);
            }
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(TargetPath);
            foreach (string subdirectory in subdirectoryEntries)
                FillTargetPathList(subdirectory);
        }
        
    }
    public partial class Synchronization
    {
        public void WohaAsymetricSynchronize(ListsofFiles listsOfFiles)
        {
            //FileInfoWoha FIW = new FileInfoWoha();
            int leftListIndex = 0;
            listsOfFiles.linksInfo.Clear();
            foreach (FileInfoWoha FIW in listsOfFiles.LeftListofFiles)
            {
                LinksInfo LI = new LinksInfo();
                LI.Relations = LinksInfo.RightIcon;
                if (listsOfFiles.RightListofFiles.Exists(x => (x.Name == FIW.Name)&&(x.PathInFolder == FIW.PathInFolder)))
                {
                    LI.Right = listsOfFiles.RightListofFiles.FindIndex(x => x.Name == FIW.Name);
                    LI.Relations =LinksInfo.EqualIcon;
                }
                LI.Left = leftListIndex;
                listsOfFiles.linksInfo.Add(LI);
                leftListIndex++;
            }
            Console.Beep();
        }
        public void WohaSymetricSynchronize(ListsofFiles listsOfFiles, bool isAsymmetricChecked)
        {
            if (!(isAsymmetricChecked))
            {
                //FileInfoWoha FIW = new FileInfoWoha();
                int leftListIndex = 0;
                //listsOfFiles.linksInfo.Clear();
                foreach (FileInfoWoha FIW in listsOfFiles.RightListofFiles)
                {
                    LinksInfo LI = new LinksInfo();
                    LI.Relations = LinksInfo.RightIcon;
                    if (listsOfFiles.LeftListofFiles.Exists(x => (x.Name == FIW.Name) && (x.PathInFolder == FIW.PathInFolder)))
                    {
                        LI.Left = listsOfFiles.LeftListofFiles.FindIndex(x => x.Name == FIW.Name);
                        LI.Relations = LinksInfo.EqualIcon;
                    }
                    LI.Right = leftListIndex;
                    if (listsOfFiles.linksInfo.FindAll(x => (x.Left == LI.Left) && (x.Right == LI.Right) && (x.Relations == LI.Relations)).Count == 0)
                    {
                        listsOfFiles.linksInfo.Add(LI);
                    }
                    leftListIndex++;
                }
                Console.Beep();
                listsOfFiles.linksInfo = listsOfFiles.linksInfo.Distinct().ToList<LinksInfo>();
            }
        }

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
