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
        public Peca VulneravelEnPassat { get; private set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassat = null;
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

            //Jogada especial roque pequeno

            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tabuleiro.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, destinoT);
            }

            //Jogada especial roque grande

            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tabuleiro.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, destinoT);
            }

            //Jogada especial en passant

            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (peca.Cor == Cor.Branco)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }

                    pecaCapturada = tabuleiro.RetirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }


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
            
            Peca p = tabuleiro.ObterPeca(destino);
            //JogadaEspecial 
            if (p is Peao)
            {
                if ((p.Cor == Cor.Branco && destino.Linha == 0) || (p.Cor == Cor.Preto && destino.Linha == 7))
                {
                    p = tabuleiro.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tabuleiro, p.Cor);
                    tabuleiro.ColocarPeca(dama,destino);
                    pecas.Add(dama);

                }
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


            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassat = p;
            }
            else
            {
                VulneravelEnPassat = null;
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

            //Jogada especial roque pequeno

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tabuleiro.RetirarPeca(destinoT);
                T.IncrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, origemT);
            }

            //Jogada especial roque grande

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tabuleiro.RetirarPeca(destinoT);
                T.IncrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, origemT);
            }

            //#jogadaEspecial in passant;

            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassat)
                {
                    Peca peao = tabuleiro.RetirarPeca(destino);
                    Posicao postP;

                    if (p.Cor == Cor.Branco)
                        postP = new Posicao(3, destino.Coluna);
                    else
                        postP = new Posicao(4, destino.Coluna);
                    
                    tabuleiro.ColocarPeca(peao, postP);
                }
            }
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
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

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
            ColocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branco));

            ColocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branco, this));


            ColocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Preto));

            ColocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preto, this));
        }
    }
}