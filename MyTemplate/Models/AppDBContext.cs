using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTemplate.Models
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public DbSet<Category> Categories { set; get; }
        public DbSet<Product> Products { set; get; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        //Hàm loại bỏ tiền tố tự sinh AspNet khi bản được tạo ra
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Model.GetEntityTypes(): Lấy về cái entity
            foreach(var entityType in builder.Model.GetEntityTypes())
            {
                // Lấy về table name trong entity
                string tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    //Đổi lại tên table 
                    entityType.SetTableName(tableName.Substring(6)); //bỏ 6 ký tự đầu là "AspNet"
                }
            }
        }
    }
}
