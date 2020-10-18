using System;
using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tabuleiro = new Tabuleiro(8,8);
            Tela.ImprimirTabuleiro(tabuleiro);
            
            Console.ReadKey();
        }
    }
}