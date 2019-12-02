using System;
using Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;

namespace Domain.Entities
{
    public class Cart
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Materials> Materials
        {
            get { return context.Materials; }
        }

        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Materials material, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Material.MaterialID == material.MaterialID)
                .FirstOrDefault();
            Materials dbEntry = context.Materials.Find(material.MaterialID);

            if (line == null)
            {
                lineCollection.Add(new CartLine { Material = material, Quantity = quantity });
                dbEntry.Amount -= 1;
            }
            else
            {
                line.Quantity += quantity;
                dbEntry.Amount -= 1;
            }
            context.SaveChanges();
        }

        public void AddItemDelievery(Materials material, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Material.MaterialID == material.MaterialID)
                .FirstOrDefault();
            Materials dbEntry = context.Materials.Find(material.MaterialID);

            if (line == null)
            {
                lineCollection.Add(new CartLine { Material = material, Quantity = quantity });
                dbEntry.Amount += 1;
            }
            else
            {
                line.Quantity += quantity;
                dbEntry.Amount += 1;
            }
            context.SaveChanges();
        }

        public void RemoveLine(Materials material)
        {
            Materials dbEntry = context.Materials.Find(material.MaterialID);
            int itemQuantity = lineCollection.Find(l => l.Material.MaterialID == material.MaterialID).Quantity;
            lineCollection.RemoveAll(l => l.Material.MaterialID == material.MaterialID);
            dbEntry.Amount += itemQuantity;
            context.SaveChanges();
        }

        public void RemoveLineDelievery(Materials material)
        {
            Materials dbEntry = context.Materials.Find(material.MaterialID);
            int itemQuantity = lineCollection.Find(l => l.Material.MaterialID == material.MaterialID).Quantity;
            lineCollection.RemoveAll(l => l.Material.MaterialID == material.MaterialID);
            dbEntry.Amount -= itemQuantity;
            context.SaveChanges();
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Material.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Materials Material { get; set; }
        public int Quantity { get; set; }
    }
}
