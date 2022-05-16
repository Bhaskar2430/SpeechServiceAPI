
using JustSpeak.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustSpeak.Data
{
    public class JustSpeakContext: IdentityDbContext
    {

        public JustSpeakContext()
        { 
        
        }

        public JustSpeakContext(DbContextOptions<JustSpeakContext> options) : base(options)
        { 
        

        }

        public DbSet<EmpLegalNames> EmpLeagalNames { get; set; }


    }
}
