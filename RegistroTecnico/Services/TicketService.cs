using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using RegistroTecnico.DAL;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TicketService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AnyAsync(t => t.TicketId == id);
    }
    private async Task<bool> Insertar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(ticket);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(Tickets ticket)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        var local = _context.Tickets.Local
            .FirstOrDefault(t => t.TicketId == ticket.TicketId);
        _context.Entry(ticket).State = EntityState.Modified;
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Tickets ticket)
    {
        if (!await Existe(ticket.TicketId))
            return await Insertar(ticket);
        else
            return await Modificar(ticket);
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Clientes = await contexto.Tickets
            .Where(t => t.TicketId == id).ExecuteDeleteAsync();
        return Clientes > 0;
    }
    public async Task<Tickets?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking().
            FirstOrDefaultAsync(c => c.TicketId == id);
    }
    public async Task<Tickets?> BuscarDescripcion(string descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Descripcion == descripcion);
    }

    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}