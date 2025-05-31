using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using RegistroTecnico.DAL;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;




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

        var duplicadoNombre = await contexto.Clientes
            .AnyAsync(c => c.ClienteId != cliente.ClienteId &&
                           c.Nombres.ToLower() == cliente.Nombres.ToLower());

        if (duplicadoNombre)
            return false;

        var duplicadoRNC = await contexto.Clientes
            .AnyAsync(c => c.ClienteId != cliente.ClienteId &&
                           c.RNC.ToLower() == cliente.RNC.ToLower());

        if (duplicadoRNC)
            return false;

        if (cliente.ClienteId == 0)
            contexto.Clientes.Add(cliente);
        else
            contexto.Clientes.Update(cliente);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var cliente = await contexto.Clientes.FindAsync(clienteId);
        if (cliente == null)
            return false;

        contexto.Clientes.Remove(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Clientes?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }

    public async Task<Clientes?> BuscarPor(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(criterio);
    }

    public async Task<Clientes?> BuscarNombres(string nombre)
    {
        return await BuscarPor(c => c.Nombres.ToLower() == nombre.ToLower());
    }

    public async Task<Clientes?> BuscarRNC(string rnc)
    {
        return await BuscarPor(c => c.RNC.ToLower() == rnc.ToLower());
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