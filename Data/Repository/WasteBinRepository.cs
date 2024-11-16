using WasteBinsAPI.Data.Contexts;
using WasteBinsAPI.Models;

namespace WasteBinsAPI.Data.Repository;

public class WasteBinRepository : IWasteBinRepository
{
    private readonly DatabaseContext _context;

    public WasteBinRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<WasteBinModel> GetAll()
    {
        return _context.WasteBins.ToList();
    }

    public WasteBinModel? GetById(int id)
    {
        return _context.WasteBins.Find(id);
    }

    public void Add(WasteBinModel wasteBin)
    {
        _context.WasteBins.Add(wasteBin);
        _context.SaveChanges();
    }

    public void Update(WasteBinModel wasteBin)
    {
        _context.WasteBins.Update(wasteBin);
        _context.SaveChanges();
    }

    public void Delete(WasteBinModel wasteBin)
    {
        _context.WasteBins.Remove(wasteBin);
        _context.SaveChanges();
    }
}