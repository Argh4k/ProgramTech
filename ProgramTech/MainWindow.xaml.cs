using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics.CodeAnalysis;

namespace ProgramTech
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        private ProgramTech.Language SelectedLang { get; set; }
        private WordController contr;
        private TextBox[] txt_chars;
        public MainWindow()
        {
            InitializeComponent();
            txt_chars = new TextBox[] { txt_char1, txt_char2, txt_char3, txt_char4, txt_char5, txt_char6, txt_char7, txt_char8 };
            contr = new WordController();
            ScoringHandler sc = new ScoringHandler("ScoringHandler.xml");
            Word.setScoringHandler(sc);
            //contr.downloadDictionary(ProgramTech.Language.EN, "https://raw.githubusercontent.com/dwyl/english-words/master/words.txt");
            contr.downloadDictionary(ProgramTech.Language.EN, "http://www.mieliestronk.com/corncob_lowercase.txt");
            //contr.addDictionaryFromFile(ProgramTech.Language.EN, "words.txt");

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
                wordList = seng.search(characters, SelectedLang, expLenght);
                wordList = wordList.FindAll(w => (w.Length == expLenght));
            }
            else
            {
                wordList = seng.search(characters, ProgramTech.Language.EN);
            }
            datagrid_words.ItemsSource = wordList;

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
            datagrid_words.ItemsSource = new List<Word>();
        }

        private void button_dictionary_Click(object sender, EventArgs e)
        {
            LangWindow langWind = new LangWindow();
            langWind.ShowDialog();

            string selectedLang = langWind.SelectedLang;

            if (selectedLang.Equals(String.Empty))
            {
                return;
            }

            if (DatabaseController.getInstance().checkTableExists(selectedLang))
            {
                AgreeWindow agreeWindow = new AgreeWindow();
                agreeWindow.ShowDialog();

                if (!agreeWindow.Agreement)
                {
                    return;
                } else
                {
                    DatabaseController.getInstance().removeTable(selectedLang);
                }
            }
            var senderButton = sender as Button;
            switch(senderButton.Name)
            {
                case "button_file":
                    addDictionaryFromFile(selectedLang);
                    break;
                case "button_url":
                    addDictionaryFromUrl(selectedLang);
                    break;
            }
        }

        private void addDictionaryFromFile(string selectedLang)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    contr.addDictionaryFromFile((ProgramTech.Language)Enum.Parse(typeof(ProgramTech.Language), selectedLang), fileName);
                    MessageBox.Show("Dictionary added!");
                }
            }
        }

        private void addDictionaryFromUrl(string selectedLang)
        {
            UrlWindow urlWindow = new UrlWindow();
            urlWindow.ShowDialog();

            string writtenUrl = urlWindow.url;
            ProgramTech.Language lang = (ProgramTech.Language)Enum.Parse(typeof(ProgramTech.Language), selectedLang);

            if (!writtenUrl.Equals(String.Empty))
            {
                contr.downloadDictionary(lang, writtenUrl);
                MessageBox.Show("Dictionary added!");
            }
        }

        private void combo_lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            string lang = combo.SelectedItem as string;
            SelectedLang = (ProgramTech.Language)Enum.Parse(typeof(ProgramTech.Language), lang);
        }

        private void combo_lang_Loaded(object sender, RoutedEventArgs e)
        {
            string[] langs = Enum.GetNames(typeof(ProgramTech.Language));
            List<string> itemSources = new List<string>();
            foreach (string lang in langs)
            {
                if(DatabaseController.getInstance().checkTableExists(lang))
                {
                    itemSources.Add(lang);
                }
            }
            combo_lang.ItemsSource = itemSources;
            combo_lang.SelectedIndex = 0;
        }
    }
}
