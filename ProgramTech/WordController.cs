using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace ProgramTech
{
    public class WordController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(WordController));
        public bool addDictionaryFromFile(Language language, string path)
        {
            if (DatabaseController.getInstance().checkTableExists(language.ToString())) return false;
            List<Word> words = new List<Word>();
            if(!File.Exists(path))
            {
                throw new Exceptions.WordControllerFileNotFoundException(path);
            } else
            {
                foreach(string wordstring in File.ReadLines(path))
                {
                    if(Word.isVaild(wordstring))
                    {
                        words.Add(new Word(wordstring));
                    } else
                    {
                        log.Info(String.Format("{0} was not added to database, because it is not forged only from letters", wordstring));
                    }
                }
            }
            return WordService.getInstance().saveList(words, language);
        }

        public bool downloadDictionary(Language language, string url)
        {
            if (DatabaseController.getInstance().checkTableExists(language.ToString())) return false;
            
                List<Word> words = new List<Word>();
            try
            {
                using (var client = new WebClient())
                {
                    using (System.IO.StringReader reader = new System.IO.StringReader(client.DownloadString(new Uri(url))))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (Word.isVaild(line))
                            {
                                words.Add(new Word(line));
                            }
                            else
                            {
                                log.Info(String.Format("{0} was not added to database, because it is not forged only from letters", line));
                            }
                        }

                    }
                }
            } catch (WebException ex)
            {
                throw new Exceptions.WordControllerWebException(url, ex);
            }
                
            return WordService.getInstance().saveList(words, language);
            }
            

        }

        
}

