using System.Text.RegularExpressions;
using JogoXadrez.TabuleiroJogo;

namespace JogoXadrez.XadrezJogo
{
    public class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca p = Tab.ObterPeca(posicao);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao posicao)
        {
            return Tab.ObterPeca(posicao) == null;
        }

      

        public override bool[,] MovimentosPossiveis()
        {
            bool [,] mat = new bool[Tab.Linhas,Tab.Colunas];
            
            Posicao pos = new Posicao(0,0);

            if (Cor == Cor.Branco)
            {
                pos.DefinirValores(Posicao.Linha -1 , Posicao.Coluna);
                if (Tab.PosicaoEValida(pos) && Livre(pos))
                    mat[pos.Linha, pos.Coluna] = true;
                
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if(Tab.PosicaoEValida(pos) && Livre(pos) && QtdMovimentos == 0)
                    mat[pos.Linha, pos.Coluna] = true;
                
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if(Tab.PosicaoEValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;
                
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if(Tab.PosicaoEValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;
            }

            else
            {
                pos.DefinirValores(Posicao.Linha + 1 , Posicao.Coluna);
                if (Tab.PosicaoEValida(pos) && Livre(pos))
                    mat[pos.Linha, pos.Coluna] = true;
                
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if(Tab.PosicaoEValida(pos) && Livre(pos) && QtdMovimentos == 0)
                    mat[pos.Linha, pos.Coluna] = true;
                
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if(Tab.PosicaoEValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;
                
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if(Tab.PosicaoEValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}