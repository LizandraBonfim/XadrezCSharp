
namespace JogoXadrez.TabuleiroJogo
{
    public class Peca
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
        
    }
}