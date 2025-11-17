using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinFlipMVVM.Models
{
    public class Jogo
    {
        public int acertos { get; set; }
        public int erros { get; set; }
        public int sequencia { get; set; }  
        public Jogo()
        {
            acertos = 0;
            erros = 0;
            sequencia = 0;
        }

        public void atualizarDados (bool acertou)
        {
            if (acertou)
            {
                acertos++;
                sequencia++;
            }
            else
            {
                erros++;
                sequencia = 0;
            }
        }
    }
}
