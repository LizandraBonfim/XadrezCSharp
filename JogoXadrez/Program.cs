using System;
using JogoXadrez.TabuleiroJogo;
using JogoXadrez.XadrezJogo;

namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args) 
        {

            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                
                while (!partida.Terminada)
                {
                    try
                    {

                        Console.Clear();
                        Tela.ImprimirPartida(partida);
                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ParaPosicao();

                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tabuleiro.ObterPeca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tabuleiro, posicoesPossiveis);

                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ParaPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                
                Console.Clear();
                Tela.ImprimirPartida(partida);

            
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}