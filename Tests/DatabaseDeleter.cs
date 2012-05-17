using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;

namespace Tests
{
   public class DatabaseDeleter
   {
      private readonly ISessionFactory _builder;
      private static readonly string[] IgnoredTables = new[] { "sysdiagrams", "usd_AppliedDatabaseScript", "hibernate_sequence" };
      private static string[] _tablesToDelete;
      private static string _deleteSql;
      private static readonly object LockObj = new object();
      private static bool _initialized;

      public DatabaseDeleter(ISessionFactory builder)
      {
         _builder = builder;

         BuildDeleteTables();
      }

      // ReSharper disable ClassNeverInstantiated.Local
      // ReSharper disable UnusedAutoPropertyAccessor.Local
      private class Relationship
      {
         public string PrimaryKeyTable { get; private set; }
         public string ForeignKeyTable { get; private set; }
      }
      // ReSharper restore UnusedAutoPropertyAccessor.Local
      // ReSharper restore ClassNeverInstantiated.Local

      public virtual void DeleteAllData()
      {
         var session = _builder.OpenSession();

         using (var command = session.Connection.CreateCommand())
         {
            command.CommandText = _deleteSql;
            command.ExecuteNonQuery();
         }
      }

      public static string[] GetTables()
      {
         return _tablesToDelete;
      }

      private void BuildDeleteTables()
      {
         if (!_initialized)
         {
            lock (LockObj)
            {
               if (!_initialized)
               {
                  var session = _builder.OpenSession();

                  var allTables = GetAllTables(session);

                  var allRelationships = GetRelationships(session);

                  _tablesToDelete = BuildTableList(allTables, allRelationships);

                  _deleteSql = BuildTableSql(_tablesToDelete);

                  _initialized = true;
               }
            }
         }
      }

      private static string BuildTableSql(IEnumerable<string> tablesToDelete)
      {
         return tablesToDelete.Aggregate("", (current, tableName) => current + String.Format("delete from [{0}];", (object) tableName));
      }

      private static string[] BuildTableList(ICollection<string> allTables, ICollection<Relationship> allRelationships)
      {
         var tablesToDelete = new List<string>();

         while (allTables.Any())
         {
            var leafTables = allTables.Except(allRelationships.Select(rel => rel.PrimaryKeyTable)).ToArray();

            tablesToDelete.AddRange(leafTables);

            foreach (var leafTable in leafTables)
            {
               var table = leafTable;
               allTables.Remove(leafTable);
               var relToRemove = allRelationships.Where(rel => rel.ForeignKeyTable == table).ToArray();
               foreach (var rel in relToRemove)
               {
                  allRelationships.Remove(rel);
               }
            }
         }

         return tablesToDelete.ToArray();
      }

      private static IList<Relationship> GetRelationships(ISession session)
      {
         var otherquery = session.CreateSQLQuery(@"select
	so_pk.name as PrimaryKeyTable
,   so_fk.name as ForeignKeyTable
from
	sysforeignkeys sfk
	  inner join sysobjects so_pk on sfk.rkeyid = so_pk.id
	  inner join sysobjects so_fk on sfk.fkeyid = so_fk.id
order by
	so_pk.name
,   so_fk.name");

         return otherquery.SetResultTransformer(Transformers.AliasToBean<Relationship>()).List<Relationship>();
      }

      private static IList<string> GetAllTables(ISession session)
      {
         var query = session.CreateSQLQuery("select name from sys.tables");

         return query.List<string>().Except(IgnoredTables).ToList();
      }
   }
}