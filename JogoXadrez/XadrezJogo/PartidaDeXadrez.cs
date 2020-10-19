using System.Collections.Generic;
using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tabuleiro.RetirarPeca(origem);
            peca.IncrementarQtdMovimentos();

            Peca pecaCapturada = tabuleiro.RetirarPeca(destino);
            tabuleiro.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);

            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se por em xeque.");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (TesteXequeMate(Adversaria(JogadorAtual)))
                Terminada = true;
            
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tabuleiro.ObterPeca(destino);
            p.DecrementarQtdMovimentos();

            if (pecaCapturada != null)
            {
                tabuleiro.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            tabuleiro.ColocarPeca(p, origem);
        }


        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                    aux.Add(x);
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                    aux.Add(x);
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei) return x;
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null) throw new TabuleiroException("Não existe rei");

            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna]) return true;
            }

            return false;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branco) return Cor.Preto;
            else return Cor.Branco;
        }

        public void ValidarPosicaoDeOrigem(Posicao posicao)
        {
            if (tabuleiro.ObterPeca(posicao) == null)
                throw new TabuleiroException("Não existe peça na posição escolhida.");
            if (JogadorAtual != tabuleiro.ObterPeca(posicao).Cor)
                throw new TabuleiroException("A peça escolhida não é sua.");
            if (!tabuleiro.ObterPeca(posicao).ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não existe movimentos possiveis para peça de origem.");
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.ObterPeca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Posicao de destino inválida");
        }


        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int linhas = 0; linhas < tabuleiro.Linhas; linhas++)
                {
                    for (int colunas = 0; colunas < tabuleiro.Colunas; colunas++)
                    {
                        if (mat[linhas, colunas])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(linhas, colunas);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testaXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);

                            if (!testaXeque)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public void MudaJogador()
        {
            if (JogadorAtual == Cor.Branco)
                JogadorAtual = Cor.Preto;
            else
                JogadorAtual = Cor.Branco;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ParaPosicao());
            pecas.Add(peca);
        }

        public void ColocarPecas()
        {
            
            ColocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branco));
            ColocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branco));
            
            ColocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Branco));
            ColocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branco));
            ColocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branco));
            
            
            ColocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Branco));
            ColocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 8, new Dama(tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Branco));
            ColocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Branco));
            
            ColocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preto));
            ColocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preto));
            ColocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preto));
            ColocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preto));
            ColocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preto));
            ColocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preto));
        }
    }
}