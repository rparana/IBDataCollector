using IB.DC.Model;
using IB.DC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace IB.DC.Data
{
       public class Contexto : DbContext
    {
        // Entities to map
        public DbSet<Space> Spaces { get; set; }

        public Contexto()
        {
            Database.CreateIfNotExists();
        }
    }
}
