using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tab.ObterPeca(posicao);
            return peca == null || peca.Cor != this.Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicaoZerada = new Posicao(0, 0);

            //cima

            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

            //abaixo
            posicaoZerada.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

            //direita
            posicaoZerada.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;
            
            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;
            
            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;
            
            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha + 2, Posicao.Coluna -1 );
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;
    
            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

            return mat;
        }
    }
}