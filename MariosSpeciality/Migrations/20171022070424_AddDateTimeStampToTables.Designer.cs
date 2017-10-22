using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MariosSpeciality.Models;

namespace MariosSpeciality.Migrations
{
    [DbContext(typeof(MariosDbContext))]
    [Migration("20171022070424_AddDateTimeStampToTables")]
    partial class AddDateTimeStampToTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("MariosSpeciality.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<string>("CountryOfOrigin")
                        .IsRequired();

                    b.Property<DateTime>("DatePosted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("ProductImg");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MariosSpeciality.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<byte[]>("AuthorImg");

                    b.Property<string>("ContentBody")
                        .IsRequired();

                    b.Property<DateTime>("PostedDate");

                    b.Property<int>("ProductId");

                    b.Property<int>("Rating");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProductId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MariosSpeciality.Models.Review", b =>
                {
                    b.HasOne("MariosSpeciality.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
