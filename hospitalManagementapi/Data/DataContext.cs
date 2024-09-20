using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hospitalManagementapi.Models;





namespace hospitalManagementapi.Data
{
    public class DataContext : DbContext
    {
        // added for the data context
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // add the models and access of 
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }





    }
}
