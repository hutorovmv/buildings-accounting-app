using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Repositories.Interfaces;
using BuildingsInfo.EF.Repositories;

namespace BuildingsAccounting.Web.Infrastructure
{
    public sealed class UowCreator
    {
        private static IBuildingsInfoUOW uow;

        public static IBuildingsInfoUOW Uow
        {
            get
            {
                if (uow == null)
                    uow = new BuildingsInfoUOW<ApplicationContext>(new ApplicationContext());

                return uow;
            }
        }
    }
}