using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class Rei : Peca
    {
        public Rei( Tabuleiro tab, Cor cor) : base( cor, tab)
        {
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

        public override bool[,] MovimentoPossivel()
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


            return mat;

        }

    }
}