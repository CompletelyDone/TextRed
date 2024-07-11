using Model;
using System.Collections.ObjectModel;
using ViewModel.Base;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class TextRedVM : ViewModelBase
    {
        private readonly IDialogService dialogService;
        public TextRedVM(IDialogService dialogService)
        {
            processProps = new ObservableCollection<TextProcessProps>();
            AddButton = new Command(Add);
            RemoveButton = new Command(Remove);
            ProcessButton = new Command(Process);
            OpenButton = new Command(Open);
            SaveButton = new Command(Save);
            this.dialogService = dialogService;
        }
        private ObservableCollection<TextProcessProps> processProps;
        public ObservableCollection<TextProcessProps> ProcessProps
        {
            get { return processProps; }
            set
            {
                processProps = value;
                OnPropertyChanged();
            }
        }
        public Command AddButton { get; private set; }
        public Command RemoveButton { get; private set; }
        public Command ProcessButton { get; private set; }
        public Command OpenButton { get; private set; }
        public Command SaveButton { get; private set; }
        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }
        private string pathIn = "";
        public string PathIn
        {
            get => pathIn;
            set
            {
                pathIn = value;
                OnPropertyChanged();
            }
        }
        private string pathOut = "";
        public string PathOut
        {
            get => pathOut;
            set
            {
                pathOut = value;
                OnPropertyChanged();
            }
        }
        private int minWordLength = 0;
        public string MinWordLength
        {
            get => minWordLength.ToString();
            set
            {
                Int32.TryParse(value, out minWordLength);
                OnPropertyChanged();
            }
        }
        private bool removePunctuation = false;
        public bool RemovePunctuation
        {
            get => removePunctuation;
            set
            {
                removePunctuation = value;
                OnPropertyChanged();
            }
        }
        private void Add()
        {
            if (!File.Exists(pathIn))
            {
                dialogService.ShowMessage($"Файла по такому пути не существует: {pathIn}");
                return;
            }
            if (string.IsNullOrWhiteSpace(pathOut) || !Path.IsPathRooted(PathOut) || !Directory.Exists(Path.GetDirectoryName(PathOut)))
            {
                dialogService.ShowMessage("Введите корректный путь сохранения");
                return;
            }

            TextProcessProps newProps = new TextProcessProps
            {
                PathIn = pathIn,
                PathOut = pathOut,
                MinWordLength = minWordLength,
                RemovePunctuation = removePunctuation
            };

            ProcessProps.Add(newProps);
            ClearFields();
        }
        private void Remove()
        {
            if (SelectedIndex >= ProcessProps.Count || SelectedIndex < 0) return;
            ProcessProps.RemoveAt(SelectedIndex);
            SelectedIndex = -1;
        }
        private void Open()
        {
            dialogService.OpenFileDialog();
            PathIn = dialogService.FilePath;
        }
        private void Save()
        {
            dialogService.SaveFileDialog();
            PathOut = dialogService.FilePath;
        }
        private async void Process()
        {
            await Task.Run(() => TextProcessor.ProcessFiles(ProcessProps));
            ProcessProps.Clear();
            ClearFields();
        }

        private void ClearFields()
        {
            PathIn = "";
            PathOut = "";
            MinWordLength = "0";
            RemovePunctuation = false;
        }
    }
}
