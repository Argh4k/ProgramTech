using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramTech
{
    public class SearchEngine
    {
        readonly int resultLength;
        public SearchEngine(int _length)
        {
            resultLength = _length;
        }

        public List<Word> search(List<char> characters, Language language)
        {
            ISet<char> charSet = new HashSet<char>(characters);
            var returnLists = new List<Task<List<Word>>>();
            foreach(char c in charSet)
            {
                returnLists.Add(Task.Run (() => { return excludeNotFitting(searchSingleCharacter(c, language, characters.Count), characters); }));
            }
            Task.WaitAll(returnLists.ToArray());
            List<Word> finalList = new List<Word>();
            foreach (var task in returnLists)
            {
                finalList.AddRange(task.Result);    
            }
            return finalList.OrderByDescending(word => word.Score).Take(resultLength).ToList();
        }

        public List<Word> search(List<char> characters, Language language, int maxLenght)
        {
            ISet<char> charSet = new HashSet<char>(characters);
            var returnLists = new List<Task<List<Word>>>();
            foreach (char c in charSet)
            {
                returnLists.Add(Task.Run(() => { return excludeNotFitting(searchSingleCharacter(c, language, maxLenght), characters); }));
            }
            Console.WriteLine(returnLists.Count);
            Task.WaitAll(returnLists.ToArray());
            List<Word> finalList = new List<Word>();
            foreach (var task in returnLists)
            {
                finalList.AddRange(task.Result);
            }
            return finalList.OrderByDescending(word => word.Score).Take(resultLength).ToList();
        }


        private List<Word> searchSingleCharacter(char ch, Language language, int maxLength)
        {
            List<Word> words;
            words = WordService.getInstance().getByFirstCharacter(language, ch, maxLength, true);
            return words;
        }

        private List<Word> searchSingleCharacterAndLength(char ch, Language language, int length)
        {
            List<Word> words;
            words = WordService.getInstance().getByFirstCharacterAndLength(language, ch, length, true);
            return words;
        }

        private List<Word> excludeNotFitting(List<Word> words, List<Char> characters)
        {
            int count = 0;
            List<Word> toReturn = new List<Word>();
            foreach (var word in words)
            {
                if(canCreateWord(word.Content, characters))
                {
                    toReturn.Add(word);
                    count++;
                }
                if(count == 10)
                {
                    return toReturn;
                }
            }
            return toReturn;
        }

        private bool canCreateWord(string word, List<Char> characters)
        {
            //I think there is better way to do this
            List<char> local = new List<char>(characters);
            foreach(char c in word)
            {
                if(!local.Remove(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}