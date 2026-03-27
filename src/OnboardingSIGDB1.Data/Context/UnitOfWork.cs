using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        // SaveChangesAsync retorna o número de linhas afetadas
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}