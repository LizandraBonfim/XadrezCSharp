namespace JogoXadrez.Tabuleiro
{
    public class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return $"Linhas: {Linha}, Coluna: {Coluna}";
        }
    }
}