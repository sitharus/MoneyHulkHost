using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MoneyHulkHost.Models;

namespace MoneyHulkHost.Migrations
{
    [DbContext(typeof(MHContext))]
    [Migration("20160307071435_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("MoneyHulkHost.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<int>("Kind");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Number")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("AccountId");
                });

            modelBuilder.Entity("MoneyHulkHost.Models.AccountLine", b =>
                {
                    b.Property<int>("AccountLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountAccountId")
                        .IsRequired();

                    b.Property<int?>("CategoryCategoryId");

                    b.Property<int?>("ImportedFromImportLineId");

                    b.Property<int?>("StatementImportId");

                    b.Property<decimal>("Value");

                    b.HasKey("AccountLineId");
                });

            modelBuilder.Entity("MoneyHulkHost.Models.Budget", b =>
                {
                    b.Property<int>("BudgetId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("BudgetId");
                });

            modelBuilder.Entity("MoneyHulkHost.Models.Category", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int?>("BudgetBudgetId");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<bool>("IsIncome");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();
                });

            modelBuilder.Entity("MoneyHulkHost.Models.Import", b =>
                {
                    b.Property<int>("ImportId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ImportDateUTC");

                    b.Property<DateTime>("StatementDate");

                    b.HasKey("ImportId");
                });

            modelBuilder.Entity("MoneyHulkHost.Models.ImportLine", b =>
                {
                    b.Property<int>("ImportLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ImportImportId")
                        .IsRequired();

                    b.HasKey("ImportLineId");
                });

            modelBuilder.Entity("MoneyHulkHost.Models.AccountLine", b =>
                {
                    b.HasOne("MoneyHulkHost.Models.Account")
                        .WithMany()
                        .HasForeignKey("AccountAccountId");

                    b.HasOne("MoneyHulkHost.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryCategoryId");

                    b.HasOne("MoneyHulkHost.Models.ImportLine")
                        .WithMany()
                        .HasForeignKey("ImportedFromImportLineId");

                    b.HasOne("MoneyHulkHost.Models.Import")
                        .WithMany()
                        .HasForeignKey("StatementImportId");
                });

            modelBuilder.Entity("MoneyHulkHost.Models.Category", b =>
                {
                    b.HasOne("MoneyHulkHost.Models.Budget")
                        .WithOne()
                        .HasForeignKey("MoneyHulkHost.Models.Category", "BudgetBudgetId");

                    b.HasOne("MoneyHulkHost.Models.Budget")
                        .WithOne()
                        .HasForeignKey("MoneyHulkHost.Models.Category", "CategoryId");
                });

            modelBuilder.Entity("MoneyHulkHost.Models.ImportLine", b =>
                {
                    b.HasOne("MoneyHulkHost.Models.Import")
                        .WithMany()
                        .HasForeignKey("ImportImportId");
                });
        }
    }
}
