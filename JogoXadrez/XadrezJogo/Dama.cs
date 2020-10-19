using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class Dama : Peca
    {
        public Dama(Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "D";
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

            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            }


            //direita
            posicaoZerada.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            }

            //em cima
            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            }

            //em abaixo
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            }

            //No
            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            }

            //NE
            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            }

            //SE
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            }

            //SO
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            }

            return mat;
        }
    }
}