using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.Irepository
{
    public interface ICoverTypeRepository : Irepository<CoverType>
    {
        void Update(CoverType obj);
    }
    
}
