using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Repositories.Interfaces;
using System.Linq;
using System.Data.Entity;

namespace BuildingsInfo.EF.Repositories
{
    public class BuildingsInfoUOW<Context> : IBuildingsInfoUOW where Context : DbContext
    {
        private readonly Context _context;

        public BuildingsInfoUOW(Context context)
        {
            _context = context;
            BuildingRepository = new BuildingRepository<Context>(_context);
            BuildingTypeRepository = new BuildingTypeRepository<Context>(_context);
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
