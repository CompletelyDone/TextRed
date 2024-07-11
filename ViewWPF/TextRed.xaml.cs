using System.Windows;
using ViewModel;
using ViewWPF.Utils;

namespace ViewWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TextRed : Window
    {
        public TextRed()
        {
            InitializeComponent();

            DialogService dialogService = new DialogService();

            this.DataContext = new TextRedVM(dialogService);
        }
    }
}