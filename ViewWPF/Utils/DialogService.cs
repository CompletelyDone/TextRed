using Microsoft.Win32;
using System.Windows;
using ViewModel.Interfaces;

namespace ViewWPF.Utils
{
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; } = null!;
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message)) MessageBox.Show("Unknown error");
            else MessageBox.Show(message);
        }
    }
}
