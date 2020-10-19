
namespace JogoXadrez.TabuleiroJogo
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca( Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            this.Tab = tab;
            
        }

        public void IncrementarQtdMovimentos()
        {
            QtdMovimentos++;
        }
        
        public void DecrementarQtdMovimentos()
        {
            QtdMovimentos--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int linhas = 0; linhas < Tab.Linhas; linhas++)
            {
                for (int colunas = 0; colunas < Tab.Colunas; colunas++)
                {
                    if (mat[linhas, colunas])
                        return true;
                }
            }

            return false;
        }

        public bool MovimentoPossivel(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }
        public abstract bool[,] MovimentosPossiveis();
    }
}