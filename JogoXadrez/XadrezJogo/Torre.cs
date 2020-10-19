using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tab.ObterPeca(posicao);
            return peca == null || peca.Cor != this.Cor;
        }

        public override bool[,] MovimentoPossivel()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicaoZerada = new Posicao(0, 0);

            //cima
            posicaoZerada.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != Cor)
                    break;

                posicaoZerada.Linha = posicaoZerada.Linha - 1;
            }
            
            
            //abaixo
            posicaoZerada.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.Linha = posicaoZerada.Linha + 1;
            }
            
            //direita
            posicaoZerada.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.Coluna = posicaoZerada.Coluna + 1;
            }
            
            //esquerda
            posicaoZerada.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);

            while (Tab.PosicaoEValida(posicaoZerada) && PodeMover(posicaoZerada))
            {
                mat[posicaoZerada.Linha, posicaoZerada.Coluna] = true;

                if (Tab.ObterPeca(posicaoZerada) != null && Tab.ObterPeca(posicaoZerada).Cor != this.Cor)
                    break;

                posicaoZerada.Coluna = posicaoZerada.Coluna - 1;
            }

                


            return mat;
        }
    }
}