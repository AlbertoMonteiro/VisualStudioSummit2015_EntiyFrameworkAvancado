
namespace VisualStudioSummitDemo.Models
{
    public class Contato
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public Endereco Endereço { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public bool Inativo { get; set; }
        public long TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
