using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;


public class SistemaServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AnyAsync(s => s.SistemasId == id);
    }

    private async Task<bool> Insertar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Sistemas sistema)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        var local = _context.Sistemas.Local
            .FirstOrDefault(s => s.SistemasId == sistema.SistemasId);

        _context.Entry(sistema).State = EntityState.Modified;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Sistemas sistema)
    {
        if (!await Existe(sistema.SistemasId))
            return await Insertar(sistema);
        else
            return await Modificar(sistema);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var sistema = await contexto.Sistemas
            .Where(s => s.SistemasId == id).ExecuteDeleteAsync();
        return sistema > 0;
    }

    public async Task<Sistemas?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking().
            FirstOrDefaultAsync(s => s.SistemasId == id);
    }

    public async Task<Sistemas?> BuscarDescripcion(string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Descripcion == nombre);
    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}