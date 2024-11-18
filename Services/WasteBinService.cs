using WasteBinsAPI.Data.Repository;
using WasteBinsAPI.Models;

namespace WasteBinsAPI.Services;

public class WasteBinService : IWasteBinService
{
    private readonly IWasteBinRepository _repository;

    public WasteBinService(IWasteBinRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<WasteBinModel> GetAll() => _repository.GetAll();

    public WasteBinModel? GetById(int id) => _repository.GetById(id);

    public IEnumerable<WasteBinModel> GetAllReference(int lastReference, int size) =>
        _repository.GetAllReference(lastReference, size);

    public void Add(WasteBinModel wasteBin) => _repository.Add(wasteBin);

    public void Update(WasteBinModel wasteBin) => _repository.Update(wasteBin);

    public void Delete(int id)
    {
        var wasteBin = _repository.GetById(id);
        if (wasteBin != null)
        {
            _repository.Delete(wasteBin);
        }
    }
}