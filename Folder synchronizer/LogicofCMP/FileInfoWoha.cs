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
        public string Name;
        public Int64 Size;
        public DateTime DateCreation;
        public DateTime DateMoficication;
        public FileInfoWoha()
        {
            Path = "";
            Name = "";
            Size = 0;
            DateCreation = DateTime.MinValue;
            DateMoficication = DateTime.MinValue;
        }
    }
    public class LinksInfo
    {
        public int Left;
        public int Right;
        public int Relations;
        public LinksInfo()
            {
            Left=0;
            Right=0;
            Relations=0;
            }
    }

    public class ListsofFiles
    {
        public List<FileInfoWoha> LeftListofFiles;
        public List<FileInfoWoha> RightListofFiles;
        public List<LinksInfo> linksInfo;
        public List<int> WhatToDo;
        public string FileMask;
        //1 - Right
        //2 - Left
        //3 - Equal
        //4 - NotEqual
        //5 - DoesNotExist
        const int RightIcon = 1;
        const int LeftIcon = 2;
        const int EqualIcon = 3;
        const int NotEqualIcon = 4;
        const int DNEIcon = 5; //does not exist
        public ListsofFiles()
        {
            LeftListofFiles = new List<FileInfoWoha>(); //лівий
            RightListofFiles = new List<FileInfoWoha>(); //правий
            //WhatToDo = new List<int>();
            linksInfo = new List<LinksInfo>(); // відносини між лівим та правим
        }

        public void FillListsFromPath(string SourcePath, string TargetPath)
        {
            LeftListofFiles.Clear();
            RightListofFiles.Clear();
            FillSourcePathList(SourcePath);
            FillTargetPathList(TargetPath);
        }

        //private void FillSourcePathList(string SourcePath)
        //{
        //    // Process the list of files found in the directory.
        //    FileInfoWoha fileData = new FileInfoWoha();
            
        //    string[] fileEntries = Directory.GetFiles(SourcePath);
        //    foreach (string fileName in fileEntries)
        //    {
        //        //ProcessFile(fileName);
        //        fileData.Name = fileName;
        //        LeftListofFiles.Add(fileData);
        //    }
                

        //    // Recurse into subdirectories of this directory.
        //    string[] subdirectoryEntries = Directory.GetDirectories(SourcePath);
        //    foreach (string subdirectory in subdirectoryEntries)
        //        FillSourcePathList(subdirectory);
        //}

        private void FillSourcePathList(string SourcePath)
        {
            // Process the list of files found in the directory.

            //LeftListofFiles.Clear();
            var fileEntries = Directory.EnumerateFiles(SourcePath, FileMask);
            foreach (string fileName in fileEntries)
            {
                FileInfoWoha fileData = new FileInfoWoha();
                fileData.Name = fileName.Substring(SourcePath.Length+1);
                fileData.Path = fileName.Remove(SourcePath.Length + 1, fileName.Length- SourcePath.Length-1);
                LeftListofFiles.Add(fileData);
            }
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(SourcePath);
            foreach (string subdirectory in subdirectoryEntries)
                FillSourcePathList(subdirectory);
        }

        private void FillTargetPathList(string TargetPath)
        {
            // Process the list of files found in the directory.

            //RightListofFiles.Clear();
            var fileEntries = Directory.EnumerateFiles(TargetPath, FileMask);
            foreach (string fileName in fileEntries)
            {
                FileInfoWoha fileData = new FileInfoWoha();
                fileData.Name = fileName.Substring(TargetPath.Length + 1);
                fileData.Path = fileName.Remove(TargetPath.Length + 1, fileName.Length - TargetPath.Length - 1);
                RightListofFiles.Add(fileData);
            }
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(TargetPath);
            foreach (string subdirectory in subdirectoryEntries)
                FillTargetPathList(subdirectory);
        }
    }

}
