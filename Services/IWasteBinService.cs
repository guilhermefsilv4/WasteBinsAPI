using WasteBinsAPI.Models;

namespace WasteBinsAPI.Services;

public interface IWasteBinService
{
    IEnumerable<WasteBinModel> GetAll();
    WasteBinModel? GetById(int id);
    IEnumerable<WasteBinModel> GetAllReference(int lastReference, int size);
    void Add(WasteBinModel wasteBin);
    void Update(WasteBinModel wasteBin);
    void Delete(int id);
}