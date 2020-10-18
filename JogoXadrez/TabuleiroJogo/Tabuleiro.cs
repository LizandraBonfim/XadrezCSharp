namespace JogoXadrez.TabuleiroJogo
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            this._pecas = new Peca[linhas, colunas];
        }

        public Peca ObterPeca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }
    }
}