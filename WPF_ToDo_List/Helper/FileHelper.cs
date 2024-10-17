using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_List.Helper
{
    internal class FileHelper
    {
        string myFile = @"C:\Users\rahulsharma\Desktop\Tutorials\ToDo List\Docs\mytask.txt";

        public string myFileData {  get; set; } 

        public FileHelper()
        {
            LoadFile();
        }

        public string getFileData()
        {
            return myFileData;
        }

        public void LoadFile()
        {
            try
            {
                if (File.Exists(myFile))
                {
                    myFileData = File.ReadAllText(myFile);
                }
                else
                {
                    MessageBox.Show("File Not Found." + myFile);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void SaveFile()
        {
            try
            {
                File.WriteAllText(myFile, myFileData);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void AddData(string msg)
        {
            try
            {
                myFileData = myFileData + Environment.NewLine + msg;
                SaveFile();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
