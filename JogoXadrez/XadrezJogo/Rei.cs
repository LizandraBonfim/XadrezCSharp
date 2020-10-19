using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei( Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base( cor, tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tab.ObterPeca(posicao);
            return peca == null || peca.Cor != this.Cor;
        }

        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca p = Tab.ObterPeca(posicao);
            return p != null && p.Cor == Cor && p is Torre && p.QtdMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            
            Posicao posicao = new Posicao(0,0);
            
            //cima
            posicao.DefinirValores(Posicao.Linha -1 , Posicao.Coluna);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;

            //nordeste
            posicao.DefinirValores(Posicao.Linha -1 , Posicao.Coluna + 1);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;

            //direita
            posicao.DefinirValores(Posicao.Linha , Posicao.Coluna + 1);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;
            
            //sudeste
            posicao.DefinirValores(Posicao.Linha + 1 , Posicao.Coluna + 1);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;
            
            //em baixo
            posicao.DefinirValores(Posicao.Linha +1  , Posicao.Coluna);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;
            
            //so
            posicao.DefinirValores(Posicao.Linha +1  , Posicao.Coluna - 1);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;
            
            //em baixo
            posicao.DefinirValores(Posicao.Linha  , Posicao.Coluna -1);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;
            
            //no
            posicao.DefinirValores(Posicao.Linha -1  , Posicao.Coluna -1);
            if (Tab.PosicaoEValida(posicao) && PodeMover(posicao))
                mat[posicao.Linha, posicao.Coluna] = true;

            if (QtdMovimentos == 0 && !partida.Xeque)
            {
                //Jogada especial roque pequeno
                Posicao posicaoT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha,Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha,Posicao.Coluna + 2);

                    if (Tab.ObterPeca(p1) != null && Tab.ObterPeca(p2) != null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
                
                
                //Jogada especial roque grande
                Posicao posicaoT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha,Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha,Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha,Posicao.Coluna - 3);

                    if (Tab.ObterPeca(p1) != null && Tab.ObterPeca(p2) != null && Tab.ObterPeca(p3) != null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return mat;

        }

    }
}