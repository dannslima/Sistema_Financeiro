using Domain.Interfaces.IDespesa;
using Entities.Entidades;
using Infra.Configuracao;
using Infra.Repositorio.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio
{
    public class RepositorioDespesa : RepositoryGenerics<Despesa>, InterfaceDespesa
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositorioDespesa()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<Despesa>> ListarDespesasUsuario(string emailUsuario)
        {
            using(var banco = new ContextBase(_optionsBuilder))
            {
                return await
                    (from s in banco.sistemaFinanceiro
                     join c in banco.Categoria on s.Id equals c.IdSistema
                     join us in banco.UsuarioSistemaFinanceiro on s.Id equals us.IdSistema
                     join d in banco.Despesa on c.Id equals d.IdCategoria
                     where us.EmailUsuario.Equals(emailUsuario) && us.SistemaAtual
                     select c).AsNoTracking().ToListAsync();
            }
        }

        public Task<IList<Despesa>> ListarDespesasUsuarioNaoPagasMesesAnterior(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
