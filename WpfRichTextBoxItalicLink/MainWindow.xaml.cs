using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfRichTextBoxItalicLink
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        static MainWindow()
        {
            var hasItalicColor = new SolidColorBrush(Colors.Aqua);
            hasItalicColor.Freeze();
            _hasItalic = hasItalicColor;

            var noItalicColor = new SolidColorBrush(Colors.Crimson);
            noItalicColor.Freeze();
            _noItalic = noItalicColor;
        }

        private static readonly SolidColorBrush _hasItalic;
        private static readonly SolidColorBrush _noItalic;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SolidColorBrush ItalicButtonColor
        {
            get
            {
                var currentSelection = this.RTB.Selection;
                if (!currentSelection.IsEmpty)
                {
                    var fontStyleProperty = currentSelection.GetPropertyValue(FontStyleProperty);
                    if (fontStyleProperty is FontStyle fontStyle && fontStyle.Equals(FontStyles.Italic))
                    {
                        return _hasItalic;
                    }
                }

                return _noItalic;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentSelection = this.RTB.Selection;
            if (currentSelection.IsEmpty)
            {
                return;
            }

            EditingCommands.ToggleItalic.Execute(null, this.RTB);
        }

        private void RTB_SelectionChanged(object sender, RoutedEventArgs e) 
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItalicButtonColor)));
    }
}
