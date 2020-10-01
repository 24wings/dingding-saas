using System;
using Microsoft.EntityFrameworkCore;

namespace Ocr
{
    public class DatabaseContext:DbContext
    {

        public DbSet<Group> groups { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySql("Server=localhost;Database=ef;User=root;Password=shadow2016;TreatTinyAsBoolean=true;");
    }
}
