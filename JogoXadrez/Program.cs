using System;
using JogoXadrez.Tabuleiro;

namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(3,4);
            Console.WriteLine(p.ToString());
            Console.ReadKey();
        }
    }
}