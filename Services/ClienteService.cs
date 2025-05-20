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
        private async Task<bool> Insertar(Clientes cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Clientes.Add(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }
        private async Task<bool> Modificar(Clientes cliente)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            var local = _context.Clientes.Local
                .FirstOrDefault(c => c.ClienteId == cliente.ClienteId);
            _context.Entry(cliente).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Guardar(Clientes cliente)
        {
            if (!await Existe(cliente.ClienteId))
                return await Insertar(cliente);
            else
                return await Modificar(cliente);
        }
        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var Clientes = await contexto.Clientes
                .Where(c => c.ClienteId == id).ExecuteDeleteAsync();
            return Clientes > 0;
        }
        public async Task<Clientes?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Clientes.AsNoTracking().
                FirstOrDefaultAsync(c => c.ClienteId == id);
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
            return await contexto.Clientes.AsNoTracking()
                .Where(criterio)
                .ToListAsync();

        }
    }
}
