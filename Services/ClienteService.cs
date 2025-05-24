using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using RegistroTecnico.DAL;
using System.Linq.Expressions;

namespace RegistroTecnico.Services
{
    public class ClienteService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AnyAsync(c => c.ClienteId == id);
        }

        public async Task<bool> Guardar(Clientes cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            if (cliente.ClienteId == 0)
                contexto.Clientes.Add(cliente);
            else
                contexto.Clientes.Update(cliente);

            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var eliminados = await contexto.Clientes
                .Where(c => c.ClienteId == id).ExecuteDeleteAsync();
            return eliminados > 0;
        }

        public async Task<Clientes?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<Clientes?> BuscarNombres(string nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Nombres == nombre);
        }

        public async Task<Clientes?> BuscarRNC(string rnc)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.RNC == rnc);
        }

        public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
