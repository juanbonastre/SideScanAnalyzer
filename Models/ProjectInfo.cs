using SideScanAnalyzer.JSON_Models;
using SideScanAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Models
{
    public class ProjectInfo : INotifyPropertyChanged
    {
        public static string IMAGES_RPATH = "images";
        public static string XTF_FILES_RPATH = "xtf_files";
        public static string PROJECT_EXTENSION = ".config.json";

        MainWindowViewModel parent;

        private string _ProjectParentDirectoryPath;
        public string ProjectParentDirectoryPath
        {
            get
            {
                return _ProjectParentDirectoryPath;
            }
            set
            {
                _ProjectParentDirectoryPath = value;
                OnPropertyChanged("ProjectParentDirectoryPath");
            }
        }
        private string _ProjectName;
        public string ProjectName
        {
            get
            {
                return _ProjectName;
            }
            set
            {
                _ProjectName = value;
                OnPropertyChanged("ProjectName");
            }
        }
        private int _ImageSize;
        public int ImageSize
        {
            get
            {
                return _ImageSize;
            }
            set
            {
                _ImageSize = value;
                OnPropertyChanged("ImageSize");
            }
        }
        private DateTimeOffset _CreationTime;
        public DateTimeOffset CreationTime
    {
            get
            {
                return _CreationTime;
            }
            set
            {
                _CreationTime = value;
                OnPropertyChanged("CreationTime");
            }
        }
        private DateTimeOffset _LastModificationTime;
        public DateTimeOffset LastModificationTime
{
            get
            {
                return _LastModificationTime;
            }
            set
            {
                _LastModificationTime = value;
                OnPropertyChanged("LastModificationTime");
            }
        }
        private bool _ProjectLoaded;
        public bool ProjectLoaded
        {
            get
            {
                return _ProjectLoaded;
            }
            set
            {
                _ProjectLoaded = value;
                OnPropertyChanged("ProjectLoaded");
            }
        }
        private bool _IsChanged;
        public bool IsChanged
        {
            get
            {
                return _IsChanged;
            }
            set
            {
                _IsChanged = value;
            }
        }

        /*private XTFFileItemViewModel? _SeletectXTFItem;
        public XTFFileItemViewModel? SeletectXTFItem
        {
            get
            {
                return _SeletectXTFItem;
            }
            set
            {
                _SeletectXTFItem = value;
                OnPropertyChanged("SeletectXTFItem");
            }
        }*/

        private string _SelectedImagePath;
        public string SelectedImagePath
        {
            get
            {
                return _SelectedImagePath;
            }
            set
            {
                _SelectedImagePath = value;
                OnPropertyChanged("SelectedImagePath");
            }
        }

        public ProjectInfo() { }
        public ProjectInfo(MainWindowViewModel parent)
        {
            this.parent = parent;

            this._ProjectParentDirectoryPath = "";
            this._ProjectName = "Proyecto sin guardar";
            this._CreationTime = DateTimeOffset.Now;
            this._LastModificationTime = DateTimeOffset.Now;
            this._ProjectLoaded = false;
            this._IsChanged = true;
        }
        public ProjectInfo(MainWindowViewModel parent, string projectParentDirectoryPath, string projectName)
        {
            this.parent = parent;

            this._ProjectParentDirectoryPath = projectParentDirectoryPath;
            this._ProjectName = projectName;
            this._CreationTime = DateTimeOffset.Now;
            this._LastModificationTime = DateTimeOffset.Now;
            this._ProjectLoaded = false;
            this._IsChanged = true;
        }

        internal string GetFullPath()
        {
            return Path.Join(GetFullDirectoryPath(), GetProjectFullName());
        }

        internal string GetProjectFullName()
        {
            return ProjectName + PROJECT_EXTENSION;
        }

        internal string GetFullDirectoryPath()
        {
            return Path.Join(ProjectParentDirectoryPath, ProjectName);
        }
        internal string GetFullXTFFilesDirPath()
        {
            return Path.Join(GetFullDirectoryPath(), XTF_FILES_RPATH);
        }
        internal string GetFullImagesDirPath()
        {
            return Path.Join(GetFullDirectoryPath(), IMAGES_RPATH);
        }


        public async Task Save()
        {
            this.LastModificationTime = DateTimeOffset.Now;

            string jsonString = JSONProjectInfoConverter.ToJSONString(this);
            File.WriteAllText(GetFullPath(),jsonString);
            IsChanged = false;

            if (parent!=null) parent.SetStatusString("Proyecto guardado");
        }
        
        internal void CheckXTFDirectory()
        {
            if (!Directory.Exists(GetFullXTFFilesDirPath()))
                Directory.CreateDirectory(GetFullXTFFilesDirPath());
        }
        internal void CheckImagesDirectory()
        {
            if (!Directory.Exists(GetFullImagesDirPath()))
                Directory.CreateDirectory(GetFullImagesDirPath());
        }

        public override string ToString()
        {
            string result = "";

            result += "\nNombre del proyecto: " + ProjectName;
            result += "\nFecha de creación: " + CreationTime.ToString();
            result += "\nÚltima modificación: " + LastModificationTime.ToString();

            return result;
        }
        public string ToStringTest()
        {
            string result = "";

            result += "ProjectParentDirectoryPath: " + ProjectParentDirectoryPath;
            result += "\nProjectName: " + ProjectName;
            result += "\nCreationTime: " + CreationTime.ToString();
            result += "\nLastModificationTime: " + LastModificationTime.ToString();
            result += "\nProjectLoaded: " + ProjectLoaded.ToString();
            result += "\nIsChanged: " + IsChanged.ToString();

            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            IsChanged = true;
        }
    }
}
