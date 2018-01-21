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

namespace ProgramTech
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WordController contr = new WordController();
            ScoringHandler sc = new ScoringHandler("ScoringHandler.xml");
            Word.setScoringHandler(sc);
            contr.addDictionaryFromFile(ProgramTech.Language.EN, "words.txt");
        }

        private void button_submit_Click(object sender, RoutedEventArgs e)
        {
            int numbOfResults;
            int.TryParse(txt_result_no.Text, out numbOfResults);
            TextBox[] txt_chars = new TextBox[] { txt_char1, txt_char2, txt_char3, txt_char4, txt_char5, txt_char6, txt_char7, txt_char8 };
            SearchEngine seng = new SearchEngine(numbOfResults);
            List<char> characters = new List<char>();
            foreach(TextBox textBox in txt_chars)
            {
                if(textBox.Text.Length > 0)
                {
                    characters.Add(textBox.Text.ToLower()[0]);
                }
            }

            datagrid_words.ItemsSource = seng.search(characters, ProgramTech.Language.EN);
            //log4net.LogManager.GetLogger(typeof(WordController)).Info(String.Format("{0} not added to database as it is not consisted only of letters", wordstring));

            WordService.getInstance().Dispose();
        }

        private void txt_char_TextChanged(object sender, EventArgs e)
        {
            TextBox txt_char = (TextBox)sender;
            if (!txt_char.Text.Equals(String.Empty)  && !System.Text.RegularExpressions.Regex.IsMatch(txt_char.Text, "^[a-zA-Z]"))
            {
                MessageBox.Show("This textbox accepts only alphabetical characters");
                txt_char.Text = String.Empty;
            }
        }

        private void txt_number_TextChanged(object sender, EventArgs e)
        {
            TextBox txt_number = (TextBox)sender;
            if (!txt_number.Text.Equals(String.Empty) && !System.Text.RegularExpressions.Regex.IsMatch(txt_number.Text, "^[0-9]+"))
            {
                MessageBox.Show("This textbox accepts only numbers");
                txt_number.Text = String.Empty;
            }
        }
    }
}
