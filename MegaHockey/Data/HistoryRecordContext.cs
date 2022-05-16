#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaHockey.Models;

namespace MegaHockey.Data
{
    public class HistoryRecordContext : DbContext
    {
        public HistoryRecordContext (DbContextOptions<HistoryRecordContext> options)
            : base(options)
        {
        }

        public DbSet<MegaHockey.Models.HistoryRecord> HistoryRecord { get; set; }
    }
}
