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
    }
}