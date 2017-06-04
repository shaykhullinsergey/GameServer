using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using grey.sh_Server.Contexts;

namespace grey.shServer.Migrations
{
    [DbContext(typeof(PlayerContext))]
    [Migration("20170604191821_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("grey.sh_Server.Controllers.InfoPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Nickname");

                    b.Property<int>("Wins");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });
        }
    }
}
