using BuildingsInfo.EF.DataContext;
using BuildingsInfo.EF.Repositories.Interfaces;
using BuildingsInfo.EF.Repositories;
using BuildingsAccounting.Web.Models;

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
                {
                    uow = new BuildingsInfoUOW<ApplicationContext>(new ApplicationContext());
                }

                return uow;
            }
        }
    }
}