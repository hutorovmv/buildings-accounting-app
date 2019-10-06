using System.Collections.Generic;
using System.Data.Entity;
using BuildingsInfo.EF.Models;
using BuildingsInfo.EF.Interfaces;

namespace BuildingsInfo.EF.DataContext
{
    public class ContextInitializer<Context> : DropCreateDatabaseAlways<Context> where Context : DbContext, IBuildingsContext
    {
        protected override void Seed(Context context)
        {
            context.BuildingTypes.AddRange(new List<BuildingType>
            {
                new BuildingType()
                {
                    Id = 1,
                    Name = "Виробнича будівля",
                    Parent = null,
                    Description = "Призначена для виробництва промислової або сільськогосподарської продукції"
                },
                new BuildingType()
                {
                    Id = 2,
                    Name = "Невиробнича будівля",
                    Parent = null,
                    Description = null
                },
                new BuildingType()
                {
                    Id = 3,
                    Name = "Житловa будівля",
                    ParentId = 2,
                    Description = "Призначена для обслуговування побутових потреб людей"
                },
                new BuildingType()
                {
                    Id = 4,
                    Name = "Громадська будівля",
                    ParentId = 2,
                    Description = null
                },
                new BuildingType()
                {
                    Id = 5,
                    Name = "Промислова будівля",
                    ParentId = 1,
                    Description = "Будівля або споруда, в якій або за допомогою якої випускають готову промислову продукцію чи напівфабрикати"
                },
                new BuildingType()
                {
                    Id = 6,
                    Name = "Сільськогосподарська будівля",
                    ParentId = 1,
                    Description = null
                },
                new BuildingType()
                {
                    Id = 7,
                    Name = "Житлова будівля садибного типу",
                    ParentId = 3,
                    Description = "Складається із житлових та допоміжних приміщень.На присадибних ділянках, розміщуються також господарські будівлі"
                },
                new BuildingType()
                {
                    Id = 8,
                    Name = "Будівля для тимчасового проживання",
                    ParentId = 4,
                    Description = "Туристичні бази, гірські притулки, дитячі та сімейні табори відпочинку, будинки відпочинку та інші"
                }
            });

            context.Buildings.AddRange(new List<Building>
            {
                new Building()
                {
                    Id = 1,
                    BuildingTypeId = 7,
                    Address = "Вінницька область, Липовецький район, с.Козинці, вул.Мазуренка, 27",
                    FloorsNumber = 1,
                    Area = 14.5,
                    Note = "Земельна ділянка не приватизована",
                    Description = "Будинок з господарськими будівлями",
                    Photos = new string[] { "1.jpg", "2.jpg", "3.jpg" }
                },
                new Building()
                {
                    Id = 2,
                    BuildingTypeId = 7,
                    Address = "Вінницька область, м.Могилів-Подільський, вул.Січових Стрільців, 22",
                    FloorsNumber = 2,
                    Area = 121.4,
                    Note = "Документи на право власності на земельну ділянку відсутні",
                    Description = "Будинок з господарськими будівлями і спорудами",
                    Photos = new string[] { "4.jpg", "5.jpg", "6.jpg" }
                },
                new Building()
                {
                    Id = 3,
                    BuildingTypeId = 4,
                    Address = "Полтавська область, Гадяцький район, с.Красна Лука, вул. Дачна, 21а",
                    FloorsNumber = 1,
                    Area = 89.4,
                    Note = "Договір оренди до 31.12.2019",
                    Description = "База відпочинку",
                    Photos = new string[] { "7.jpg" }
                },
                new Building()
                {
                    Id = 4,
                    BuildingTypeId = 4,
                    Address = "Полтавська область, Карлівський район, с - ще Михайлівське, вул.Горького, 5а",
                    FloorsNumber = 1,
                    Area = 166.8,
                    Note = "Загальний стан об’єкта – незадовільний",
                    Description = "Магазин-павільйон",
                    Photos = new string[] { "8.jpg", "9.jpg" }
                }
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
