﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Nop.Core;
using Nop.Data;

namespace Toi.Plugin.Misc.DoItYourself.Data
{
    public class DoItYourselfObjectContext : DbContext, IDbContext
    {
        public DoItYourselfObjectContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #region Implementation of IDbContext

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DoItYourselfObjectContext>(null);
            modelBuilder.Configurations.Add(new DiyGroupMap());
            modelBuilder.Configurations.Add(new DiyProjectMap());
            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"

                
            //Database.SetInitializer(new CreateTablesIfNotExist<ArticlesObjectContext>(null, null));
           
            //string sql = CreateDatabaseInstallationScript();
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            Database.SetInitializer<DoItYourselfObjectContext>(null);
            const string dbScript = "DROP TABLE DiyProject;DROP TABLE DiyGroup;";
            Database.ExecuteSqlCommand(dbScript);
            SaveChanges();
        }
        

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
