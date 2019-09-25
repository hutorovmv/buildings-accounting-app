using Common.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories.Interfaces
{
    public interface IBuildingsInfoUOW : IUnitOfWork
    {
        IBuildingRepository BuildingRepository { get; }
        IBuildingTypeRepository BuildingTypeRepository { get; }
    }
}
