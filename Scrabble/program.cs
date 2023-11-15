using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Solution
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        var words = new string[N];
        for (int i = 0; i < N; i++)
        {
            string W = Console.ReadLine();
            words[i] = W;
        }
        char[] LETTERS = Console.ReadLine().ToCharArray();
        Console.Error.WriteLine(String.Join("", LETTERS));

        var letterScores = new Dictionary<char, int> {
            { 'e', 1 },
            { 'a', 1 },
            { 'i', 1 },
            { 'o', 1 },
            { 'n', 1 },
            { 'r', 1 },
            { 't', 1 },
            { 'l', 1 },
            { 's', 1 },
            { 'u', 1 },
            { 'd', 2 },
            { 'g', 2 },
            { 'b', 3 },
            { 'c', 3 },
            { 'm', 3 },
            { 'p', 3 },
            { 'f', 4 },
            { 'h', 4 },
            { 'v', 4 },
            { 'w', 4 },
            { 'y', 4 },
            { 'k', 5 },
            { 'j', 8 },
            { 'x', 8 },
            { 'q', 10 },
            { 'z', 10 }
        };

        var bestWord = "";
        var bestWordScore = 0;

        foreach (string word in words)
        {
            int score = 0;

            if (ValidateWord(word))
            {
                var usedLetters = new List<char>();

                foreach (char letter in word)
                {
                    usedLetters.Add(letter);
                    score += letterScores[letter];
                }

                if (score > bestWordScore)
                {
                    bestWordScore = score;
                    bestWord = word;
                }

            } 
        }

        bool ValidateWord(string word)
        {
            var usedLetters = new List<char>();
            char[] availableLetters = (char[]) LETTERS.Clone();

            foreach (char letter in word)
            {
                var index = Array.IndexOf(availableLetters, letter);
                if (index != -1) availableLetters = availableLetters.Where((val, idx) => idx != index).ToArray();
                else return false;
            }

            if (word.Length > 8) return false;

            return true;
        }

        Console.WriteLine(bestWord);
    }
}
