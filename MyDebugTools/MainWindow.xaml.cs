using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CommonLib;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace MyDebugTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyTextshow myTextShow;
        ErrorCode myErrorCode;

        public MainWindow()
        {
            InitializeComponent();
            myTextShow = (MyTextshow)this.FindResource("MyTextshow");
            myErrorCode = ErrorCode.Normal;
        }

        public void OperationCompleteProc(bool bSuc, ErrorCode error)
        {
            string info = "";
            if (!bSuc)
            {
                info = string.Format("Fail: ErrorCode:{0}", Enum.GetName(typeof(ErrorCode), myErrorCode));
            }
            else
            {
                info = "Success";
            }
            System.Windows.MessageBox.Show(info);
        }

        private bool MoveFile(string sourceFile, string targetDir, out ErrorCode errorCode)
        {
            bool bRet = false;
            errorCode = ErrorCode.Normal;
            if (sourceFile == string.Empty || targetDir == string.Empty)
            {
                errorCode = ErrorCode.ErrorPathIsNone;
            }
            else
            {
                // Check file
                if (File.Exists(sourceFile))
                {
                    // Check dir or create dir.
                    if (!Directory.Exists(targetDir))
                    {
                        try
                        {
                            Directory.CreateDirectory(targetDir);
                        }
                        catch (Exception e)
                        {
                            errorCode = ErrorCode.Error_Sys_Directory_CreateDirectory;
                        }
                    }
                    // Move file
                    try
                    {
                        string targetFile = string.Format(@"{0}\{1}", targetDir, FilePath.GetFileName(sourceFile));
                        File.Copy(sourceFile, targetFile, true);
                        bRet = true;
                    }
                    catch (Exception e)
                    {
                        errorCode = ErrorCode.Error_Sys_File_Copy;
                    }
                }
                else
                {
                    errorCode = ErrorCode.ErrorFileNotExists;
                }
            }
            return bRet;
        }

        private bool DeleteFile(string sourceFile, out ErrorCode errorCode)
        {
            bool bRet = false;
            errorCode = ErrorCode.Normal;
            if (sourceFile == string.Empty )
            {
                errorCode = ErrorCode.ErrorPathIsNone;
            }
            else
            {
                try
                {
                    File.Delete(sourceFile);
                    bRet = true;
                }
                catch (Exception e)
                {
                    errorCode = ErrorCode.Error_Sys_File_Delete;
                }
            }
            return bRet;
        }

        private void Btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDlg = new OpenFileDialog();
            //openFileDlg.InitialDirectory = "c:\\";
            //openFileDlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //openFileDlg.FilterIndex = 2;
            //openFileDlg.RestoreDirectory = true;
            //if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    myTextShow.FileName = openFileDlg.FileName;
            //}

            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult result = openFileDlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            myTextShow.FileName = openFileDlg.FileName;
        }

        private void Btn_OpenDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            myTextShow.DirName = m_Dialog.SelectedPath.Trim();
        }

        private void Btn_MoveFile_Click(object sender, RoutedEventArgs e)
        {
            string sourceFile = myTextShow.FileName;
            string targetDir = myTextShow.DirName;

            bool bMove = MoveFile(sourceFile, targetDir, out myErrorCode);

            OperationCompleteProc(bMove, myErrorCode);
        }

        private void Btn_StartLog_Click(object sender, RoutedEventArgs e)
        {
            //string sourceFile = myTextShow.FileName;
            //string sourceFileDir = FilePath.GetFilePath(sourceFile);
            //string targetDir = string.Format(@"{0}\bin\Debug", sourceFileDir);

            string sourceDir = Directory.GetCurrentDirectory();
            string sourceFile = string.Format(@"{0}\NLog.config", sourceDir);
            string targetDir = string.Format(@"{0}\bin\Debug", sourceDir);

            bool bMove = MoveFile(sourceFile, targetDir, out myErrorCode);
            OperationCompleteProc(bMove, myErrorCode);
        }

        private void Btn_StopLog_Click(object sender, RoutedEventArgs e)
        {
            //string sourceFile = myTextShow.FileName;
            //string sourceFileDir = FilePath.GetFilePath(sourceFile);
            //sourceFile = string.Format(@"{0}\bin\Debug\{1}", sourceFileDir, FilePath.GetFileName(sourceFile));

            string sourceDir = Directory.GetCurrentDirectory();
            string sourceFile = string.Format(@"{0}\bin\Debug\NLog.config", sourceDir);

            bool bDel = DeleteFile(sourceFile, out myErrorCode);
            OperationCompleteProc(bDel, myErrorCode);
        }

        private void Btn_DelFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_DelSpeciFile_Click(object sender, RoutedEventArgs e)
        {
            string speciFile = myTextShow.DeleSpecFileName;
            string path = myTextShow.DirName;

            // check null
            //if ()
            //{

            //}
            //else
            //{

            //}

        }
    }

    class MyTextshow : INotifyPropertyChanged
    {
        private string fileName;
        private string dirName;
        private string deleSpecFileName;

        public event PropertyChangedEventHandler PropertyChanged;

        public MyTextshow()
        {
            fileName = "";
            dirName = "";
            deleSpecFileName = "";
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
                    }
                }
            }
        }

        public string DirName
        {
            get { return dirName; }
            set
            {
                if (this.dirName != value)
                {
                    this.dirName = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("DirName"));
                    }
                }
            }
        }
        public string DeleSpecFileName
        {
            get { return deleSpecFileName; }
            set
            {
                if ( this.deleSpecFileName != value)
                {
                    this.deleSpecFileName = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("DeleSpecFileName"));
                    }
                }
            }
        }

    }

    public enum ErrorCode
    {
        // MyDebugTools error
        Normal = 0,
        ErrorPathIsNone,
        ErrorFileNotExists,
        ErrorFileCopy,

        // System error
        Error_Sys_File_Delete,
        Error_Sys_File_Copy,
        Error_Sys_Directory_CreateDirectory,
    }
}
