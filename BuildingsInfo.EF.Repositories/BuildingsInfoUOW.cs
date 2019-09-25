using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Repositories.Interfaces;

namespace BuildingsInfo.EF.Repositories
{
    public class BuildingsInfoUOW : IBuildingsInfoUOW
    {
        private readonly BuildingsContext _context;

        public BuildingsInfoUOW(BuildingsContext context)
        {
            _context = context;
            BuildingRepository = new BuildingRepository(_context);
            BuildingTypeRepository = new BuildingTypeRepository(_context);
        }

        public IBuildingRepository BuildingRepository { get; private set; }
        public IBuildingTypeRepository BuildingTypeRepository { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
