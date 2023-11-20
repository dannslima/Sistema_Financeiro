using Domain.Interfaces.Generics;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infra.Repositorio.Generics
{
    public class RepositoryGenerics<T> : InterfaceGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _OptionBuilder;

        public RepositoryGenerics()
        {
            _OptionBuilder = new DbContextOptions<ContextBase>();
        }
        public async Task Add(T Object)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(T Object)
        {
            using (var data = new ContextBase(_OptionBuilder))
            {
                data.Set<T>().Remove(Object);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_OptionBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_OptionBuilder))
            {
                return await data.Set<T>().ToListAsync();
            }
        }

        public async Task Update(T Object)
        {
           using (var data = new ContextBase(_OptionBuilder))
           {
               data.Set<T>().Update(Object);
               await data.SaveChangesAsync();
           }
        }

        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle? _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }
    }
}
