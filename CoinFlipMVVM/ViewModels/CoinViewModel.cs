using CoinFlipMVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoinFlipMVVM.ViewModels
{
    public partial class CoinViewModel : ObservableObject
    {
        public CoinViewModel()
        {
            Application.Current.MainPage.DisplayAlert("Mensagem", "Bem-vindo(a) ao COIN FLIP!!", "Ok");
            FlipCommand = new Command(Flip);
            jogo = new Jogo();
        }

        public Jogo jogo {  get; set; }
        public ICommand FlipCommand { get; set; }

        [ObservableProperty]
        public string _ladoEscolhido = string.Empty;

        [ObservableProperty]
        public string _imagem = string.Empty;

        [ObservableProperty]
        public string _resultado = string.Empty;

        [ObservableProperty]
        public string _acertos = string.Empty;

        [ObservableProperty]
        public string _erros = string.Empty;

        [ObservableProperty]
        public string _sequencia = string.Empty;

        public async void Flip()
        {
            try
            {
                if (string.IsNullOrEmpty(_ladoEscolhido))
                {
                    throw new Exception("Escolha o lado da moeda!");
                }
                string nome = await Application.Current.MainPage.DisplayPromptAsync("Identificação", "Digite seu nome");

                if (string.IsNullOrEmpty(nome))
                {
                    throw new Exception("Escreva seu nome!");
                }
                string diaDaSemana = await Application.Current.MainPage.DisplayActionSheet("Dia da semana", "Cancelar", "Ok",
                    [
                        "Segunda-feira",
                        "Terça-feira",
                        "Quarta-feira",
                        "Quinta-feira",
                        "Sexta-feira",
                        "Sábado",
                        "Domingo"
                    ]
                );

                Coin coin = new Coin();
                _resultado = coin.Jogar(_ladoEscolhido);
                _imagem = $"{coin.Lado}.png";

                _resultado = $"{nome}, hoje é {diaDaSemana}. {_resultado}";

                jogo.atualizarDados(_ladoEscolhido.Equals(coin.Lado) ? true : false);
                _acertos = $"Acertos: {jogo.acertos}";
                _erros = $"Erros: {jogo.erros}";
                _sequencia = $"Sequência: {jogo.sequencia}";

                OnPropertyChanged(nameof(Resultado));
                OnPropertyChanged(nameof(Imagem));
                OnPropertyChanged(nameof(Acertos));
                OnPropertyChanged(nameof(Erros));
                OnPropertyChanged(nameof(Sequencia));

                bool retorno = await Application.Current.MainPage.DisplayAlert("Pergunta", "Deseja reiniciar o jogo?", "Sim", "Não");

                if (retorno)
                {
                    _resultado = string.Empty;
                    _imagem = string.Empty;

                    OnPropertyChanged(nameof(Resultado));
                    OnPropertyChanged(nameof(Imagem));
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }
    }
}
