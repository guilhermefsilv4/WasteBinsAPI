using WasteBinsAPI.Models;

namespace WasteBinsAPI.Data.Repository;

public interface IWasteBinRepository
{
    IEnumerable<WasteBinModel> GetAll();
    WasteBinModel? GetById(int id);
    void Add(WasteBinModel wasteBin);
    void Update(WasteBinModel wasteBin);
    void Delete(WasteBinModel wasteBin);
}