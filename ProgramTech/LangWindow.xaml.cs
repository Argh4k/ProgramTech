using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
using System.Windows.Shapes;

namespace ProgramTech
{
    /// <summary>
    /// Interaction logic for LangWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class LangWindow : Window
    {
        public string SelectedLang { get; set; }
        public LangWindow()
        {
            InitializeComponent();
        }

        private void button_lang_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_lang_cancel_Click(object sender, RoutedEventArgs e)
        {
            SelectedLang = String.Empty;
            this.Close();
        }

        private void combo_lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            SelectedLang = combo.SelectedItem as string;
        }

        private void combo_lang_Loaded(object sender, RoutedEventArgs e)
        {
            string[] langs = Enum.GetNames(typeof(ProgramTech.Language));
            combo_lang.ItemsSource = langs;
            combo_lang.SelectedIndex = 0;
        }
    }
}
