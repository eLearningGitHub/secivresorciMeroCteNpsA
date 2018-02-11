 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 
 using Microsoft.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using Microsoft.EntityFrameworkCore.Infrastructure;
 using Microsoft.EntityFrameworkCore.Design;

using Microsoft.Extensions.Configuration;

 using ProductCatalogApi.Domain;


namespace ProductCatalogApi.Data
{
     public class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
     {
          /* 
          private IConfiguration config;

          public CatalogContextFactory(IConfiguration config)
          {
          this.config = config;
          }
          */
          public CatalogContext CreateDbContext(string[] args)
          {
          
          var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
                    optionsBuilder.UseSqlServer("Server=localhost,1401;Database=CatalogDb;User Id=sa;Password=MyComplexPassword!234;MultipleActiveResultSets=true");
                    //optionsBuilder.UseSqlServer(this.config["ConnectionString"]);
          return new CatalogContext(optionsBuilder.Options);   
          }
     }    

}