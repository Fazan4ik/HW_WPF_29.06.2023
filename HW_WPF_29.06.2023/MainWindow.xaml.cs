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

namespace HW_WPF_29._06._2023
{
    public partial class MainWindow : Window
    {
        // Команды
        public ICommand BoldCommand { get; set; }
        public ICommand ItalicCommand { get; set; }
        public ICommand UnderlineCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand Font15Command { get; set; }
        public ICommand Font30Command { get; set; }
        public ICommand RedColorCommand { get; set; }
        public ICommand GreenColorCommand { get; set; }
        public ICommand BlueColorCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация команд
            BoldCommand = new RelayCommand(BoldExecuted);
            ItalicCommand = new RelayCommand(ItalicExecuted);
            UnderlineCommand = new RelayCommand(UnderlineExecuted);
            ClearCommand = new RelayCommand(ClearExecuted);
            Font15Command = new RelayCommand(Font15Executed);
            Font30Command = new RelayCommand(Font30Executed);
            RedColorCommand = new RelayCommand(RedColorExecuted);
            GreenColorCommand = new RelayCommand(GreenColorExecuted);
            BlueColorCommand = new RelayCommand(BlueColorExecuted);

            // Установка контекста данных
            DataContext = this;
        }

        // Методы выполнения команд

        private void BoldExecuted()
        {
            if (MyRichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                MyRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            else
                MyRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void ItalicExecuted()
        {
            if (MyRichTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                MyRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            else
                MyRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        private void UnderlineExecuted()
        {
            TextDecorationCollection currentTextDecorations = (TextDecorationCollection)MyRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            if (currentTextDecorations != null && currentTextDecorations.Count > 0)
                MyRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            else
                MyRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void ClearExecuted()
        {
            MyRichTextBox.Selection.ClearAllProperties();
        }

        private void Font15Executed()
        {
            MyRichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 15.0);
        }

        private void Font30Executed()
        {
            MyRichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 30.0);
        }

        private void RedColorExecuted()
        {
            MyRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, "Red");
        }

        private void GreenColorExecuted()
        {
            MyRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, "Green");
        }

        private void BlueColorExecuted()
        {
            MyRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, "Blue");
        }
    }
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
