using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using UnicornClicker.UWP.Models;

namespace UnicornClicker.UWP.Migrations
{
    [DbContext(typeof(UnicornClickerContext))]
    [Migration("20151229232002_InitialSchema")]
    partial class InitialSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("UnicornClicker.UWP.Models.GameScore", b =>
                {
                    b.Property<int>("GameScoreId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Clicks");

                    b.Property<double>("ClicksPerSecond");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("Played");

                    b.HasKey("GameScoreId");
                });
        }
    }
}
