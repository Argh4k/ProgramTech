using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProgramTech
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private TextBox[] txt_chars;
        public MainWindow()
        {
            InitializeComponent();
            txt_chars = new TextBox[] { txt_char1, txt_char2, txt_char3, txt_char4, txt_char5, txt_char6, txt_char7, txt_char8 };
            WordController contr = new WordController();
            ScoringHandler sc = new ScoringHandler("ScoringHandler.xml");
            Word.setScoringHandler(sc);
            //contr.downloadDictionary(ProgramTech.Language.EN, "https://raw.githubusercontent.com/dwyl/english-words/master/words.txt");
            contr.downloadDictionary(ProgramTech.Language.EN, "http://www.mieliestronk.com/corncob_lowercase.txt");

        }

        private void button_submit_Click(object sender, RoutedEventArgs e)
        {
            int numbOfResults;
            int.TryParse(txt_result_no.Text, out numbOfResults);
            
            SearchEngine seng = new SearchEngine(numbOfResults);
            List<char> characters = new List<char>();
            foreach(TextBox textBox in txt_chars)
            {
                if(textBox.Text.Length > 0)
                {
                    characters.Add(textBox.Text.ToLower()[0]);
                }
            }

            List<Word> wordList;
            if (!txt_length.Text.Equals(String.Empty))
            {
                int expLenght = int.Parse(txt_length.Text);
                wordList = seng.search(characters, ProgramTech.Language.EN, expLenght);
                wordList = wordList.FindAll(w => (w.Length == expLenght));
            }
            else
            {
                wordList = seng.search(characters, ProgramTech.Language.EN);
            }
            datagrid_words.ItemsSource = wordList;
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

        private void button_clear_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxList = new List<TextBox>(txt_chars);
            textBoxList.Add(txt_result_no);
            textBoxList.Add(txt_length);
            foreach(var txtBox in textBoxList)
            {
                txtBox.Text = String.Empty;
            }
        }
    }
}
