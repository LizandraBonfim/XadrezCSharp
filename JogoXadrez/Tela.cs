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
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    var peca = tabuleiro.ObterPeca(linha, coluna);

                    if (peca == null)
                        Console.Write("- ");
                    else
                        Console.Write(peca + " ");
                }

                Console.WriteLine();
            }
        }
    }
}