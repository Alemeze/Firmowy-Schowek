using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmbeddedMvc.Models;




namespace EmbeddedMvc.Repositories
{
    interface SchowekRepository
    {
        SchowekModel Get(int schwId);
        IQueryable<SchowekModel> GetAllActive();
        void Add(SchowekModel schowek);

        void Update(int schwId, SchowekModel schowek);

        void Delete(int schwId);


    }
}
