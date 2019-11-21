using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IMaterialRepository
    {
        IEnumerable<Materials> Materials { get; }
        void SaveMaterial(Materials material);
        void DeleteMaterial(Materials material);
        void CreateMaterial(Materials material);
    }
}
