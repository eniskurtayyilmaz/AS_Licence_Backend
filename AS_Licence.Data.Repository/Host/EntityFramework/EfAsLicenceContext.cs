using System;
using System.Collections.Generic;
using System.Text;
using AS_Licence.Entities.Model.Customer;
using AS_Licence.Entities.Model.CustomerComputerInfo;
using AS_Licence.Entities.Model.Software;
using AS_Licence.Entities.Model.Subscription;
using AS_Licence.Entities.Model.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AS_Licence.Data.Repository.Host.EntityFramework
{
  public class EfAsLicenceContext : DbContext
  {
    /*
    public EfAsLicenceContext()
    {

    }
    */

    public EfAsLicenceContext(DbContextOptions<EfAsLicenceContext> options) : base(options)
    {
      this.SetCommandTimeout(300);
    }

    //TODO: SetCommandTimeout tanımlanmalı, ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
    private void SetCommandTimeout(int timeout)
    {

    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerComputerInfo> CustomerComputerInfos { get; set; }
    public virtual DbSet<Software> Softwares { get; set; }
    public virtual DbSet<Subscription> Subscriptions { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(
          "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AS_LICENCE;Data Source=.");
      
      }
    }
  }
}
