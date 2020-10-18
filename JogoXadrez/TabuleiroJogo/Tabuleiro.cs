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

        public Peca ObterPeca(Posicao posicao)
        {
            //return ObterPeca(posicao.Linha, posicao.Coluna);
            return _pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if(PecaExiste(posicao))
                throw  new TabuleiroException("Ja existe uma peça nessa posição");
            
            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public bool PecaExiste(Posicao posicao)
        {
            PosicaoEValida(posicao);
            return ObterPeca(posicao) != null;
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (ObterPeca(posicao) == null)
                return null;
            Peca aux = ObterPeca(posicao);
            aux.Posicao = null;
            _pecas[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }


        public bool PosicaoEValida(Posicao posicao)
        {
            var linhaInvalida = posicao.Linha < 0 || posicao.Linha >= Linhas;
            var colunaInvalida = posicao.Coluna < 0 || posicao.Coluna >= Colunas;
            
            if (linhaInvalida || colunaInvalida) return false;
            return true;
        }

        public void ValidaPosicao(Posicao posicao)
        {
            if(!PosicaoEValida(posicao))
                throw new TabuleiroException("Posição inválida");
        }
    }
}