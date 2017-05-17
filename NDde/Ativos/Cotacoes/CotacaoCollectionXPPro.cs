
using NDde.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDde.Ativos.Util;
namespace NDde.Ativos.Cotacoes
{
    /// <summary>
    /// Classe que representa uma coleção de Cotações de N Tipos
    /// </summary>
    public class CotacaoCollectionXPPro : List<ICotacaoAtivo>, ICotacaoCollection
    {

        #region Propriedades

        /// <summary>
        /// Evento disparado quando um ativo é atualizado
        /// </summary>
        public event EventHandler OnAtivoAtualizado;

        /// <summary>
        /// Client para monitorar valores de Abertura
        /// </summary>
        private DdeClient _c_Abt = new DdeClient("Trade", "Abt");

        /// <summary>
        /// Client para monitorar valores de Máximo
        /// </summary>
        private DdeClient _c_Max = new DdeClient("Trade", "Max");

        /// <summary>
        /// Client para monitorar valores de Mínimo
        /// </summary>
        private DdeClient _c_Min = new DdeClient("Trade", "Min");

        /// <summary>
        /// Client para monitorar valores de Negocios
        /// </summary>
        private DdeClient _c_Neg = new DdeClient("Trade", "Neg");

        /// <summary>
        /// Client para monitorar valores de Quantidade
        /// </summary>
        private DdeClient _c_Qtd = new DdeClient("Trade", "Qtd");

        /// <summary>
        /// Client para monitorar valores de Últimas Cotações
        /// </summary>
        private DdeClient _c_Ult = new DdeClient("Trade", "Ult");

        /// <summary>
        /// Client para monitorar valores de Variações
        /// </summary>
        private DdeClient _c_Osc = new DdeClient("Trade", "Osc");

        /// <summary>
        /// Client para monitorar valores de Volume
        /// </summary>
        private DdeClient _c_Vol = new DdeClient("Trade", "Vol");

        /// <summary>
        /// Client para monitorar valores de Volume Projetado
        /// </summary>
        private DdeClient _c_VPJ = new DdeClient("Trade", "VPJ");

        /// <summary>
        /// Client para monitorar valores de Fechamento Anterior
        /// </summary>
        private DdeClient _c_FchAnt = new DdeClient("Trade", "FchAnt");

        /// <summary>
        /// Client para monitorar valores de Hora
        /// </summary>
        private DdeClient _c_Hor = new DdeClient("Trade", "Hor");

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor da coleção
        /// </summary>
        /// <param name="ativos">Ativos a serem inicializados e observados.</param>
        public CotacaoCollectionXPPro(List<string> ativos)
        {
            AddRange(ativos);

            ConectaClients();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Cria objetos de cotações para todos os tipos suportados e adiciona-os na lista
        /// </summary>
        /// <param name="ativos">Lista de códigos dos ativos</param>
        private void AddRange(List<string> ativos)
        {
            foreach (var ativo in ativos)
            {
                try
                {
                    if (this.Any(x => x.Codigo == ativo)) continue;
                    Add(new CotacaoAtivo(ativo));
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Atualiza os valores dos objetos da coleção
        /// </summary>
        public void AtualizaValores()
        {
            foreach (var ativo in this)
            {
                ativo.Abertura = BuscaValor(_c_Abt, ativo.Codigo).GetDecimalValue();
                ativo.Maximo = BuscaValor(_c_Max, ativo.Codigo).GetDecimalValue();
                ativo.Minimo = BuscaValor(_c_Min, ativo.Codigo).GetDecimalValue();
                ativo.NumeroNegocios = BuscaValor(_c_Neg, ativo.Codigo).GetDecimalValue();
                ativo.Quantidade = BuscaValor(_c_Qtd, ativo.Codigo).GetDecimalValue();
                ativo.Ultima = BuscaValor(_c_Ult, ativo.Codigo).GetDecimalValue();
                ativo.Variacao = BuscaValor(_c_Osc, ativo.Codigo).GetDecimalValue();
                ativo.VolumeFinanceiro = BuscaValor(_c_Vol, ativo.Codigo).GetDecimalValue();
                ativo.VolumeProjetado = BuscaValor(_c_VPJ, ativo.Codigo).GetDecimalValue();
                ativo.FechamentoAnterior = BuscaValor(_c_FchAnt, ativo.Codigo).GetDecimalValue();
                ativo.DataHora = BuscaValor(_c_Hor, ativo.Codigo).GetDateTimeFromTimeValue();
            }
        }

        /// <summary>
        /// Inicia a monitorar os valores dos ativos
        /// </summary>
        public void StartAdvising()
        {
            _c_Abt.Advise += _c_Abt_Advise;
            _c_Max.Advise += _c_Max_Advise;
            _c_Min.Advise += _c_Min_Advise;
            _c_Neg.Advise += _c_Neg_Advise;
            _c_Qtd.Advise += _c_Qtd_Advise;
            _c_Ult.Advise += _c_Ult_Advise;
            _c_Osc.Advise += _c_Osc_Advise;
            _c_Vol.Advise += _c_Vol_Advise;
            _c_VPJ.Advise += _c_VPJ_Advise;
            _c_FchAnt.Advise += _c_FchAnt_Advise;
            _c_Hor.Advise += _c_Hor_Advise;

            StartAdvisingByClient(_c_Abt);
            StartAdvisingByClient(_c_Max);
            StartAdvisingByClient(_c_Min);
            StartAdvisingByClient(_c_Neg);
            StartAdvisingByClient(_c_Qtd);
            StartAdvisingByClient(_c_Ult);
            StartAdvisingByClient(_c_Osc);
            StartAdvisingByClient(_c_Vol);
            StartAdvisingByClient(_c_VPJ);
            StartAdvisingByClient(_c_FchAnt);
            StartAdvisingByClient(_c_Hor);

        }

        /// <summary>
        /// Inicia o advise de um client para cada ativo da lista.
        /// </summary>
        /// <param name="client">Client dde</param>
        private void StartAdvisingByClient(DdeClient client)
        {
            foreach (var ativo in this)
            {
                client.StartAdvise(ativo.Codigo, 1, true, 60000);
            }
        }

        /// <summary>
        /// Busca valor via client.
        /// </summary>
        /// <param name="client">Client para buscar o valor</param>
        /// <param name="item">item a ser buscado</param>
        /// <returns>Retorna o valor encontrado.</returns>
        private string BuscaValor(DdeClient client, string item)
        {
            try
            {
                return client.Request(item.ToUpper(), 60000);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Conecta todos os clients
        /// </summary>
        private void ConectaClients()
        {
            ConectaClient(_c_Abt);
            ConectaClient(_c_Max);
            ConectaClient(_c_Min);
            ConectaClient(_c_Neg);
            ConectaClient(_c_Qtd);
            ConectaClient(_c_Ult);
            ConectaClient(_c_Osc);
            ConectaClient(_c_Vol);
            ConectaClient(_c_VPJ);
            ConectaClient(_c_FchAnt);
            ConectaClient(_c_Hor);
        }

        /// <summary>
        /// Conecta o client
        /// </summary>
        /// <param name="client">Client para conexão dde</param>
        private void ConectaClient(DdeClient client)
        {
            if (!client.IsConnected)
                client.Connect();
        }

        /// <summary>
        /// Finaliza o objeto fechando conexões e destruindo advisers
        /// </summary>
        public void Dispose()
        {
            DisposeClient(_c_Abt);
            DisposeClient(_c_Max);
            DisposeClient(_c_Min);
            DisposeClient(_c_Neg);
            DisposeClient(_c_Qtd);
            DisposeClient(_c_Ult);
            DisposeClient(_c_Osc);
            DisposeClient(_c_Vol);
            DisposeClient(_c_VPJ);
            DisposeClient(_c_FchAnt);
            DisposeClient(_c_Hor);
        }

        /// <summary>
        /// Desconecta um client
        /// </summary>
        /// <param name="client">Client</param>
        private void DisposeClient(DdeClient client)
        {
            try
            {
                client.Dispose();
            }
            catch { }
            finally
            {
                client = null;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para valor de Abertura
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Abt_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).Abertura = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para valor Máximo
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Max_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).Maximo = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para valor Mínimo
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Min_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).Minimo = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para Volume Negociado
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Neg_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).NumeroNegocios = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para Quantidade
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Qtd_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).Quantidade = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para Última Cotação
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Ult_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).Ultima = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para Variação
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Osc_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).Variacao = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para Volume Financeiro
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Vol_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).VolumeFinanceiro = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para 
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_VPJ_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).VolumeProjetado = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para Fechamento Anterior
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_FchAnt_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).FechamentoAnterior = e.Text.GetDecimalValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Evento disparado quando é há atualização de ativo para Hora
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _c_Hor_Advise(object sender, DdeAdviseEventArgs e)
        {
            this.First(x => x.Codigo == e.Item).DataHora = e.Text.Replace("h", "").GetDateTimeFromTimeValue();
            DisparaEventoOnAtivoAtualizado(e.Item);
        }

        /// <summary>
        /// Dispara o evento avisando que um ativo foi atualizado
        /// </summary>
        /// <param name="codigoAtivo">Código do ativo atualizado</param>
        private void DisparaEventoOnAtivoAtualizado(string codigoAtivo)
        {
            OnAtivoAtualizado(this.First(x => x.Codigo == codigoAtivo), null);
        }

        #endregion

    }
}
