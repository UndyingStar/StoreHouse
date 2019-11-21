using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFMaterialsRepository : IMaterialRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Materials> Materials
        {
            get { return context.Materials; }
        }


        public void SaveMaterial(Materials materials)
        {
            if (materials.MaterialID == 0)
            {
                context.Materials.Add(materials);
            }
            else
            {
                Materials dbEntry = context.Materials.Find(materials.MaterialID);
                if (dbEntry != null)
                {
                    dbEntry.Title = materials.Title;
                    dbEntry.Price = materials.Price;
                    dbEntry.Amount = materials.Amount;
                    dbEntry.Type = materials.Type;
                    dbEntry.Unit = materials.Unit;
                    dbEntry.Description = materials.Description;
                }
            }
            context.SaveChanges();
        }

        public void DeleteMaterial(Materials material)
        {
            context.Materials.Attach(material);
            context.Entry(material).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        public void CreateMaterial(Materials material)
        {
            context.Materials.Add(material);
            context.SaveChanges();
        }
    }
}
