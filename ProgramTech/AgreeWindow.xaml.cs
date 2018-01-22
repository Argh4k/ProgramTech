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
using System.Windows.Shapes;

namespace ProgramTech
{
    /// <summary>
    /// Interaction logic for AgreeWindow.xaml
    /// </summary>
    public partial class AgreeWindow : Window
    {
        public Boolean Agreement;
        public AgreeWindow()
        {
            InitializeComponent();
        }

        private void button_lang_ok_Click(object sender, RoutedEventArgs e)
        {
            Agreement = true;
            this.Close();
        }

        private void button_lang_cancel_Click(object sender, RoutedEventArgs e)
        {
            Agreement = false;
            this.Close();
        }
    }
}
