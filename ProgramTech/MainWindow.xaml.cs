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
            /*
            WordController contr = new WordController();
            ScoringHandler sc = new ScoringHandler("ScoringHandler.xml");
            Word.setScoringHandler(sc);
            contr.addDictionaryFromFile(ProgramTech.Language.EN, "words.txt");
            SearchEngine seng = new SearchEngine(5);
            List<char> characters = new List<char>();
            characters.Add('a');
            characters.Add('l');
            characters.Add('l');
            characters.Add('y');
            characters.Add('k');
            foreach(Word word in seng.search(characters, ProgramTech.Language.EN))
            {
                Console.WriteLine(word.Content + " " + word.Score);
            }
            */
            
            
        }
    }
}
