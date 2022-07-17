using EmbeddedMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmbeddedMvc.Repositories
{
    public class ISchowekRepository : SchowekRepository
    {
        public void Add(SchowekModel schowek)
        {
            throw new NotImplementedException();
        }

      

        public SchowekModel Get(int schwId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SchowekModel> GetAllActive()
        {
            throw new NotImplementedException();
        }

        public void Update(int schwId, SchowekModel schowek)
        {
            throw new NotImplementedException();
           
        }
        public void Delete(int schwId)
        {
            throw new NotImplementedException();
        }
    }
}