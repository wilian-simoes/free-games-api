﻿using FreeGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeGames.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<DiscordConfiguration> DiscordConfigurations { get; set; }
    }
}