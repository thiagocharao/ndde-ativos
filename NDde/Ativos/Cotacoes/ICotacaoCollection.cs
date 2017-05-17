using System;
namespace NDde.Ativos.Cotacoes
{
    /// <summary>
    /// Interface de implementação de uma coleção de ativos
    /// </summary>
    public interface ICotacaoCollection
    {
        /// <summary>
        /// Deve implementar um método que Atualize todos os valores da coleção
        /// </summary>
        void AtualizaValores();
        
        /// <summary>
        /// Deve descruir todas as conexões do objeto
        /// </summary>
        void Dispose();
        
        /// <summary>
        /// Evento que alerta quando um ativo é atualizado
        /// </summary>
        event EventHandler OnAtivoAtualizado;

        /// <summary>
        /// Deve iniciar os "Listeners" que avisam quando um valor de um ativo foi atualizado
        /// </summary>
        void StartAdvising();
    }
}
