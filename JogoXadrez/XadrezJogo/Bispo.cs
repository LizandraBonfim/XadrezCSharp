using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "B";
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

            //NO
            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna -1 );

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != Cor)
                    break;

                posicaoZerada.DefinirValores(posicaoZerada.Linha - 1, posicaoZerada.Coluna -1 );

                // posicaoZerada.Linha = posicaoZerada.Linha - 1;
            }
            
            
            //NE
            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(posicaoZerada.Linha - 1, posicaoZerada.Coluna + 1);

                // posicaoZerada.Linha = posicaoZerada.Linha + 1;
            }
            
            //SU
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(posicaoZerada.Linha + 1, posicaoZerada.Coluna + 1);

                // posicaoZerada.Coluna = posicaoZerada.Coluna + 1;
            }
            
            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(posicaoZerada.Linha + 1, posicaoZerada.Coluna - 1);

                // posicaoZerada.Coluna = posicaoZerada.Coluna - 1;
            }
            
            return mat;
        }
    }
}