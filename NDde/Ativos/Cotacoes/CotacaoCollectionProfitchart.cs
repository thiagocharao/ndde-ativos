
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
    public class CotacaoCollectionProfitchart : List<ICotacaoAtivo>, ICotacaoCollection
    {

        #region Propriedades

        /// <summary>
        /// Evento disparado quando um ativo é atualizado
        /// </summary>
        public event EventHandler OnAtivoAtualizado;

        /// <summary>
        /// Client para monitorar valores de cotação
        /// </summary>
        private DdeClient _client = new DdeClient("profitchart", "cot");

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor da coleção
        /// </summary>
        /// <param name="ativos">Ativos a serem inicializados e observados.</param>
        public CotacaoCollectionProfitchart(List<string> ativos)
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
                ativo.Abertura = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "ABE")).GetDecimalValue();
                ativo.Maximo = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "MAX")).GetDecimalValue();
                ativo.Minimo = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "MIN")).GetDecimalValue();
                ativo.NumeroNegocios = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "NEG")).GetDecimalValue();
                ativo.Quantidade = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "QTT")).GetDecimalValue();
                ativo.Ultima = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "ULT")).GetDecimalValue();
                ativo.Variacao = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "VAR")).GetDecimalValue();
                ativo.VolumeFinanceiro = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "VOL")).GetDecimalValue();
                ativo.VolumeProjetado = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "VPJ")).GetDecimalValue();
                ativo.FechamentoAnterior = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "FEC")).GetDecimalValue();
                ativo.DataHora = BuscaValor(_client, string.Format("{0}.{1}", ativo.Codigo, "HOR")).GetDateTimeFromTimeValue();
            }
        }

        /// <summary>
        /// Inicia a monitorar os valores dos ativos
        /// </summary>
        public void StartAdvising()
        {
            _client.Advise += _client_Advise;
            StartAdvisingByClient(_client);

        }

        /// <summary>
        /// Inicia o advise de um client para cada ativo da lista.
        /// </summary>
        /// <param name="client">Client dde</param>
        private void StartAdvisingByClient(DdeClient client)
        {
            foreach (var ativo in this)
            {
                StartAdvising(client, ativo, "ABE");
                StartAdvising(client, ativo, "MAX");
                StartAdvising(client, ativo, "MIN");
                StartAdvising(client, ativo, "NEG");
                StartAdvising(client, ativo, "QTT");
                StartAdvising(client, ativo, "ULT");
                StartAdvising(client, ativo, "VAR");
                StartAdvising(client, ativo, "VOL");
                StartAdvising(client, ativo, "VPJ");
                StartAdvising(client, ativo, "FEC");
                StartAdvising(client, ativo, "HOR");
            }
        }

        /// <summary>
        /// Inicia o advising ignorando se der algum erro.
        /// </summary>
        /// <param name="client">Client</param>
        /// <param name="ativo">Ativo</param>
        /// <param name="campo">Campo para advise</param>
        private void StartAdvising(DdeClient client, ICotacaoAtivo ativo, string campo)
        {
            try
            {
                client.StartAdvise(string.Format("{0}.{1}", ativo.Codigo, campo), 1, true, true, 60000, campo);
            }
            catch { }
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
            ConectaClient(_client);
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
            DisposeClient(_client);
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
        /// Evento disparado quando é há atualização de ativo
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Parametros da atualização</param>
        private void _client_Advise(object sender, DdeAdviseEventArgs e)
        {
            string codigoAtivo = e.Item.Split('.').First();

            switch (e.State.ToString())
            {
                case "ABE":
                    this.First(x => x.Codigo == codigoAtivo).Abertura = e.Text.GetDecimalValue();
                    break;
                case "MAX":
                    this.First(x => x.Codigo == codigoAtivo).Maximo = e.Text.GetDecimalValue();
                    break;
                case "MIN":
                    this.First(x => x.Codigo == codigoAtivo).Minimo = e.Text.GetDecimalValue();
                    break;
                case "NEG":
                    this.First(x => x.Codigo == codigoAtivo).NumeroNegocios = e.Text.GetDecimalValue();
                    break;
                case "QTT":
                    this.First(x => x.Codigo == codigoAtivo).Quantidade = e.Text.GetDecimalValue();
                    break;
                case "ULT":
                    this.First(x => x.Codigo == codigoAtivo).Ultima = e.Text.GetDecimalValue();
                    break;
                case "VAR":
                    this.First(x => x.Codigo == codigoAtivo).Variacao = e.Text.GetDecimalValue();
                    break;
                case "VOL":
                    this.First(x => x.Codigo == codigoAtivo).VolumeFinanceiro = e.Text.GetDecimalValue();
                    break;
                case "VPJ":
                    this.First(x => x.Codigo == codigoAtivo).VolumeProjetado = e.Text.GetDecimalValue();
                    break;
                case "FEC":
                    this.First(x => x.Codigo == codigoAtivo).FechamentoAnterior = e.Text.GetDecimalValue();
                    break;
                case "HOR":
                    this.First(x => x.Codigo == codigoAtivo).DataHora = e.Text.Replace("\0", string.Empty).GetDateTimeFromTimeValue();
                    break;
            }

            OnAtivoAtualizado(this.First(x => x.Codigo == codigoAtivo), null);
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
