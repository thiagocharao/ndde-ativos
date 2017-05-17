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
    public partial class Form1 : Form
    {

        CotacaoCollectionXPPro collection;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            collection = new CotacaoCollectionXPPro(new List<string>() {"PETR4", "ABCB4"
                    ,"ABCP11","ABRE11","AEDU3","AEFI11","AELP3","AFLT3","AFLU3","AFLU5","AGEN11","AGRO3","AHEB3","AHEB5","AHEB6","ALLL3"
                    ,"ALMI11B","ALPA3","ALPA4","ALSC3","AMAR3","AMBV1","AMBV2","AMBV3","AMBV4","AMGN11B","AMIL3","AMZO11B","ANCR11B"
                    ,"AORE3","APTI4","ARMT11B","ARTR3","ARZZ3","ATTB11B","AUTM3","AVON11B","AXPB11B","AZEV3","AZEV4","BAHI3","BALM3","BALM4"
                    ,"BAUH4","BAZA3","BBAS11","BBAS3","BBDC3","BBDC4","BBFI11B","BBRC11","BBRK3","BBVJ11","BCFF11B","BDLL3","BDLL4","BDRX","BEEF3"
                    ,"BEES3","BEES4","BEMA3","BGIP3","BGIP4","BHGR3","BICB3","BICB4","BIOM3","BIOM4","BISA3","BISA9","BMEB3","BMEB4","BMIN3","BMIN4","BMKS3"
                    ,"BMLC11B","BMTO3","BMTO4","BNBR3","BNBR4","BOAC11B","BOBR4","BOEI11B","BOVA","BOVA11","BPAR3","BPAT11","BPHA3","BPNM4","BRAP3","BRAP4"
                    ,"BRAX","BRAX11","BRCP11","BRFS3","BRIN3","BRIV3","BRIV4","BRKM3","BRKM5","BRKM6","BRML3","BRPR3","BRSR3","BRSR5","BRSR6","BSLI3"
                    ,"BSLI4","BTOW3","BTTL3","BTTL4","BUET3","BUET4","BVMF3","CAFE3","CAFE4","CALI3","CALI4","CAMB4","CARD3","CASN3","CASN4","CBEE3"
                    ,"CBMA3","CBMA4","CCHI3","CCHI4","CCPR3","CCRO3","CEBR3","CEBR5","CEBR6","CEDO3","CEDO4","CEEB3","CEEB5","CEED3","CEED4","CEGR3"
                    ,"CELP3","CELP5","CELP6","CELP7","CEPE3","CEPE5","CEPE6","CESP3","CESP5","CESP6","CGAS3","CGAS5","CGRA3","CGRA4","CIEL3","CIQU3"
                    ,"CIQU4","CLAN4","CLSC3","CLSC4","CMGR3","CMGR4","CMIG3","CMIG4","CNES11B","COCE3","COCE5","COCE6","COLG11B","CORR3","CORR4","CPFE3"
                    ,"CPLE3","CPLE5","CPLE6","CPTP3B","CRDE3","CREM3","CRIV3","CRIV4","CRUZ3","CSAB3","CSAB4","CSAN3","CSBC11","CSMG3","CSMO","CSNA3"
                    ,"CSRN3","CSRN5","CSRN6","CTAX3","CTAX4","CTIP3","CTKA3","CTKA4","CTMI3","CTNM3","CTNM4","CTPC3","CTSA3","CTSA4" });


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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            collection.Dispose();
        }

    }
}
