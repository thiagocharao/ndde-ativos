
using System;
namespace NDde.Ativos.Cotacoes
{
    /// <summary>
    /// Interface para uma cotação
    /// </summary>
    public interface ICotacaoAtivo
    {

        /// <summary>
        /// Código do Ativo
        /// </summary>
        string Codigo { get; set; }

        /// <summary>
        /// Valor da última cotação
        /// </summary>
        decimal Ultima { get; set; }

        /// <summary>
        /// Quantidade último negócio
        /// </summary>
        decimal Quantidade { get; set; }

        /// <summary>
        /// Valor na Abertura
        /// </summary>
        decimal Abertura { get; set; }

        /// <summary>
        /// Valor no último fechamento
        /// </summary>
        decimal FechamentoAnterior { get; set; }

        /// <summary>
        /// Variação
        /// </summary>
        decimal Variacao { get; set; }

        /// <summary>
        /// Valor de volume financeiro
        /// </summary>
        decimal VolumeFinanceiro { get; set; }

        /// <summary>
        /// Valor mínimo
        /// </summary>
        decimal Minimo { get; set; }

        /// <summary>
        /// Valor máximo
        /// </summary>
        decimal Maximo { get; set; }

        /// <summary>
        /// Número de negócios do ativo
        /// </summary>
        decimal NumeroNegocios { get; set; }

        /// <summary>
        /// Volume projetado
        /// </summary>
        decimal VolumeProjetado { get; set; }

        /// <summary>
        /// Data e hora da última atualização
        /// </summary>
        DateTime DataHora { get; set; }

    }
}
