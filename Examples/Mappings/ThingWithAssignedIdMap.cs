using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   class ThingWithAssignedIdMap : ClassMap<ThingWithAssignedId>
   {
      public ThingWithAssignedIdMap()
      {
         Table("AssignedIdThings");
         Id(x => x.Age).GeneratedBy.Assigned();
         Map(x => x.Name);
      }
   }
}
