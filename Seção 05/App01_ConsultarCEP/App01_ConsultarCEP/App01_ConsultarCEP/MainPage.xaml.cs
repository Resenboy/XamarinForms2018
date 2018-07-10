using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs e)
        {
            string cep = CEP.Text.Trim();

            if (IsValidCep(cep))
            {
                try
                {
                    var end = ViaCepServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {

                        RESULTADO.Text = String.Format("Endereço: {0}, {1}, {2}, {3}, {5}", end.logradouro, end.complemento, end.bairoo, end.localidade, end.uf, end.cep);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }

                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                    throw;
                }
            }
        }

        private bool IsValidCep(string cep)
        {
            bool valid = true;
            if (cep.Length != 8)
            {
                DisplayAlert("Atenção", "CEP inválido! Deve conter 8 caracteres", "OK");
                valid = false;
            }

            int NovoCep = 0;
            if (!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("Atenção", "CEP inválido! Deve conter apenas números", "OK");
                valid = false;
            }

            return valid;
        }
    }
}
