
using NDde.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NDde.Ativos.Cotacoes
{
    /// <summary>
    /// Classe base de uma cotação
    /// </summary>
    public class CotacaoAtivo : ICotacaoAtivo
    {
        #region Campos Privados
        
        /// <summary>
        /// Código do Ativo
        /// </summary>
        private string _codigo;
        
        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="codigoAtivo">Código do Ativo</param>
        public CotacaoAtivo(string codigoAtivo)
        {
            this.Codigo = codigoAtivo.ToUpper();
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Código do Ativo
        /// </summary>
        public string Codigo { get { return _codigo; } set { _codigo = value.ToUpper(); } }
        
        /// <summary>
        /// Valor da última cotação
        /// </summary>
        public decimal Ultima { get; set; }

        /// <summary>
        /// Quantidade último negócio
        /// </summary>
        public decimal Quantidade { get; set; }

        /// <summary>
        /// Valor na Abertura
        /// </summary>
        public decimal Abertura { get; set; }

        /// <summary>
        /// Valor no último fechamento
        /// </summary>
        public decimal FechamentoAnterior { get; set; }

        /// <summary>
        /// Variação
        /// </summary>
        public decimal Variacao { get; set; }

        /// <summary>
        /// Valor de volume financeiro
        /// </summary>
        public decimal VolumeFinanceiro { get; set; }

        /// <summary>
        /// Valor mínimo
        /// </summary>
        public decimal Minimo { get; set; }

        /// <summary>
        /// Valor máximo
        /// </summary>
        public decimal Maximo { get; set; }

        /// <summary>
        /// Número de negócios do ativo
        /// </summary>
        public decimal NumeroNegocios { get; set; }

        /// <summary>
        /// Volume projetado
        /// </summary>
        public decimal VolumeProjetado { get; set; }

        /// <summary>
        /// Data e hora da última atualização
        /// </summary>
        public DateTime DataHora { get; set; }

        #endregion
        
    }
}
