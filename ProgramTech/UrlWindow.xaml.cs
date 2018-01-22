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
    /// Interaction logic for UrlWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class UrlWindow : Window
    {
        public string url { get; set; }
        public UrlWindow()
        {
            InitializeComponent();
        }

        private void button_lang_ok_Click(object sender, RoutedEventArgs e)
        {
            url = txt_url_dict.Text;
            this.Close();
        }

        private void button_lang_cancel_Click(object sender, RoutedEventArgs e)
        {
            url = String.Empty;
            this.Close();
        }
    }
}
