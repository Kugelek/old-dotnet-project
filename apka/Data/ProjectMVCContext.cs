using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Data
{
    public class ProjectMVCContext : DbContext
    {
        public ProjectMVCContext(DbContextOptions<ProjectMVCContext> options)
            : base(options)
        {
        }

       
    }
   
}
