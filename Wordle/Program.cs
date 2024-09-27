using System.IO.IsolatedStorage;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;

namespace Wordle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string word = InitializeRandomWord();
            string userGuess = "";
            int guesses = 6;
            List<string> guessed = new List<string>();
            Console.WriteLine("--------Wordle--------");
            while (userGuess != word && guesses != 0)
            {
                bool isRightWord = false;
                do
                {
                    userGuess = Console.ReadLine();
                    isRightWord = ValidateWord(userGuess, guessed);
                } while (!isRightWord) ;
                
                CompareGuess(userGuess, word);
                guesses--;
                guessed.Add(userGuess);
                Console.WriteLine($"\n{guesses} guesses left\n");

            }
            if (guesses == 0) Console.WriteLine($"Goodluck next time :) the word was {word}");
            else Console.WriteLine("Well Done!");
            
        }
        private static string InitializeRandomWord()
        {
            int wordCount = 0;
            try
            {
                wordCount = File.ReadLines("wordlist.txt").Count();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            List<string> wordList = new List<string>();
            using (StreamReader readText = new StreamReader("wordlist.txt"))
            {
                for (int i = 0; i < wordCount; i++)
                {
                    wordList.Add(readText.ReadLine());
                }
            }
            Random rand = new Random();
            int index = rand.Next(1, wordList.Count);
            return wordList[index];
        }
        private static void CompareGuess(string guess, string word)
        {
            int[] guessGreen = new int[guess.Length];
            int[] guessOrange = new int[guess.Length];
            for (int i = 0;i < guess.Length; i++)
            {
                if (guess[i] == word[i])
                {
                    guessGreen[i] = guess[i];
                }
            }
            for (int i = 0; i < guess.Length; i++)
            {
                if (!guessGreen.Contains(guess[i]) && word.Contains(guess[i]))
                {
                    guessOrange[i] = guess[i];
                }
            }
            for(int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == guessGreen[i])
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(guess[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (guessOrange.Contains(guess[i]))
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(guess[i]);
                    Console.BackgroundColor= ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(guess[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.Write("\n");
        }
        private static bool ValidateWord(string word, List<string> seen)
        {
            int wordCount = 0;
            try
            {
                wordCount = File.ReadLines("wordlist.txt").Count();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            List<string> wordList = new List<string>();
            using (StreamReader readText = new StreamReader("wordlist.txt"))
            {
                for (int i = 0; i < wordCount; i++)
                {
                    wordList.Add(readText.ReadLine());
                }
            }
            if (word.Length > 5 || word.Length < 5)
            {
                return false;
            }
            else if (!wordList.Contains(word))
            {
                return false;
            }
            else if (seen.Contains(word)) {
            
                return false;
            }
            else {
                return true;
            }
        }
    }
}
