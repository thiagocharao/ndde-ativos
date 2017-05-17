using NDde.Ativos.Cotacoes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDde.Test.Forms
{
    public partial class Form2 : Form
    {

        CotacaoCollectionProfitchart collection;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            collection = new CotacaoCollectionProfitchart(new List<string>() {"AMBV4",
                            "BBAS3","BBDC4","BRAP4","BRFS3","BVMF3","CESP6","CMIG4","CSNA3","CYRE3","DJI","DOLFUT","DOLPT","ELET6","EMBR3",
                            "FIBR3","GFSA3","GGBR4","GOAU4","IBOV","INDFUT","ITSA4","ITUB4","LAME4","LREN3","MRVE3","OGXP3",
                            "OIBR4","PETR4","TIMP3","USIM5","VALE5","VIVT4" });

            collection.OnAtivoAtualizado += collection_OnAtivoAtualizado;

            collection.AtualizaValores();

            InicializaGrid();

            collection.StartAdvising();
        }

        private void InicializaGrid()
        {
            foreach (var item in collection)
            {
                int index = grid.Rows.Add();
                DataGridViewRow row = grid.Rows[index];

                row.Tag = item.Codigo;

                row.Cells["Ativo"].Value = item.Codigo.ToString();
                row.Cells["Ultima"].Value = item.Ultima.ToString();
                row.Cells["Quantidade"].Value = item.Quantidade.ToString();
                row.Cells["Abertura"].Value = item.Abertura.ToString();
                row.Cells["Fechamento"].Value = item.FechamentoAnterior.ToString();
                row.Cells["Variacao"].Value = item.Variacao.ToString();
                row.Cells["VolumeFinanceiro"].Value = item.VolumeFinanceiro.ToString();
                row.Cells["Minimo"].Value = item.Minimo.ToString();
                row.Cells["Maximo"].Value = item.Maximo.ToString();
                row.Cells["NumeroNegocios"].Value = item.NumeroNegocios.ToString();
                row.Cells["VolumeProjetado"].Value = item.VolumeProjetado.ToString();
                row.Cells["DataHora"].Value = item.DataHora.ToString();

            }
        }

        void collection_OnAtivoAtualizado(object sender, EventArgs e)
        {
            AtualizaGrid((ICotacaoAtivo)sender);
        }

        private void AtualizaGrid(ICotacaoAtivo ativoAtualizado)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if ((string)row.Tag == ativoAtualizado.Codigo)
                {
                    row.Selected = true;
                    row.Cells["Ativo"].Value = ativoAtualizado.Codigo.ToString();
                    row.Cells["Ultima"].Value = ativoAtualizado.Ultima.ToString();
                    row.Cells["Quantidade"].Value = ativoAtualizado.Quantidade.ToString();
                    row.Cells["Abertura"].Value = ativoAtualizado.Abertura.ToString();
                    row.Cells["Fechamento"].Value = ativoAtualizado.FechamentoAnterior.ToString();
                    row.Cells["Variacao"].Value = ativoAtualizado.Variacao.ToString();
                    row.Cells["VolumeFinanceiro"].Value = ativoAtualizado.VolumeFinanceiro.ToString();
                    row.Cells["Minimo"].Value = ativoAtualizado.Minimo.ToString();
                    row.Cells["Maximo"].Value = ativoAtualizado.Maximo.ToString();
                    row.Cells["NumeroNegocios"].Value = ativoAtualizado.NumeroNegocios.ToString();
                    row.Cells["VolumeProjetado"].Value = ativoAtualizado.VolumeProjetado.ToString();
                    row.Cells["DataHora"].Value = ativoAtualizado.DataHora.ToString();
                }
            }
        }

    }
}
