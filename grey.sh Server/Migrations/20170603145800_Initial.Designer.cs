using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using grey.sh_Server.Contexts;

namespace grey.shServer.Migrations
{
    [DbContext(typeof(PlayerContext))]
    [Migration("20170603145800_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("grey.sh_Server.Controllers.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BattleToken");

                    b.Property<string>("Login");

                    b.Property<string>("Nickname");

                    b.Property<bool>("SearchingForBattle");

                    b.Property<string>("Token");

                    b.Property<int>("Wins");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });
        }
    }
}
