using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using RegistroTecnico.DAL;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TecnicoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos.AnyAsync(e => e.TecnicoId == id);
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        var local = contexto.Tecnicos.Local
            .FirstOrDefault(t => t.TecnicoId == tecnico.TecnicoId);

        contexto.Entry(tecnico).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {

        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        var tecnico = await contexto.Tecnicos.FindAsync(id);
        if (tecnico != null)
        {
            contexto.Tecnicos.Remove(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos.FindAsync(id);
    }

    public async Task<Tecnicos?> BuscarNombres(string nombre)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .FirstOrDefaultAsync(t => t.Nombres.Trim().ToLower() == nombre.Trim().ToLower());
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> filtro)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos.AsNoTracking()
            .Where(filtro)
            .ToListAsync();
    }
}
