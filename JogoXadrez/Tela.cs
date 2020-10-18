using System;
using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez
{
    public class Tela
    {
            public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");

                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    var peca = tabuleiro.ObterPeca(linha, coluna);

                    if (peca == null)
                        Console.Write("- ");
                    else
                    {
                        ImprimirTabuleiro(peca);
                        Console.Write(" ");                        
                    }
                }

                Console.WriteLine();
            }
            
            Console.WriteLine("  a b c d e f g h");

        }

        public static void ImprimirTabuleiro(Peca peca)
        {
            if (peca.Cor == Cor.Branco)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}