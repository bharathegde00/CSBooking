using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CSBooking.Models
{
    public partial class CSBookingContext : DbContext
    {
        public CSBookingContext()
        {
        }

        public CSBookingContext(DbContextOptions<CSBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingDetail> BookingDetails { get; set; } = null!;
        public virtual DbSet<EventsDetail> EventsDetails { get; set; } = null!;
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; } = null!;
        public virtual DbSet<PromoCode> PromoCodes { get; set; } = null!;
        public virtual DbSet<TicketCategory> TicketCategories { get; set; } = null!;
        public virtual DbSet<UserDetail> UserDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=mynewserver07.database.windows.net;Database=CSBooking;User Id=Bharath;Password=Qwertyuiop07@;Trusted_Connection=false;Encrypt=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.ToTable("Booking_Details");

                entity.Property(e => e.BookingId).HasColumnName("Booking_Id");

                entity.Property(e => e.BookingAmount)
                    .HasColumnType("money")
                    .HasColumnName("Booking_Amount");

                entity.Property(e => e.BookingEventId).HasColumnName("Booking_Event_Id");

                entity.Property(e => e.BookingNumberofTickets).HasColumnName("Booking_NumberofTickets");

                entity.Property(e => e.BookingSingleUnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("Booking_Single_UnitPrice");

                entity.Property(e => e.BookingUserId).HasColumnName("Booking_User_Id");

                entity.HasOne(d => d.BookingEvent)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingEventId)
                    .HasConstraintName("FK_Booking_Details");

                entity.HasOne(d => d.BookingUser)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingUserId)
                    .HasConstraintName("FK__Booking_D__Booki__6C190EBB");
            });

            modelBuilder.Entity<EventsDetail>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("Events_Details");

                entity.Property(e => e.EventId).HasColumnName("Event_ID");

                entity.Property(e => e.EventEnddate)
                    .HasColumnType("date")
                    .HasColumnName("Event_Enddate");

                entity.Property(e => e.EventLocation)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Event_Location");

                entity.Property(e => e.EventName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Event_Name");

                entity.Property(e => e.EventStartdate)
                    .HasColumnType("date")
                    .HasColumnName("Event_Startdate");

                entity.Property(e => e.EventTicketCatageory).HasColumnName("Event_Ticket_Catageory");

                entity.Property(e => e.EventType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Event_Type");

                entity.HasOne(d => d.EventTicketCatageoryNavigation)
                    .WithMany(p => p.EventsDetails)
                    .HasForeignKey(d => d.EventTicketCatageory)
                    .HasConstraintName("FK_Events_Details");
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.ToTable("Payment_Details");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Payment_Amount");

                entity.Property(e => e.PaymentEventId).HasColumnName("Payment_Event_Id");

                entity.Property(e => e.PaymentPromoId).HasColumnName("Payment_Promo_Id");

                entity.Property(e => e.PaymentUserId).HasColumnName("Payment_User_Id");

                entity.HasOne(d => d.PaymentEvent)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.PaymentEventId)
                    .HasConstraintName("FK__Payment_D__Payme__72C60C4A");

                entity.HasOne(d => d.PaymentPromo)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.PaymentPromoId)
                    .HasConstraintName("FK__Payment_D__Payme__73BA3083");

                entity.HasOne(d => d.PaymentUser)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.PaymentUserId)
                    .HasConstraintName("FK_Payment_Details");
            });

            modelBuilder.Entity<PromoCode>(entity =>
            {
                entity.HasKey(e => e.PromoId);

                entity.ToTable("Promo_Codes");

                entity.HasIndex(e => e.PromoName, "UQ__Promo_Co__A3DCC227AB33F521")
                    .IsUnique();

                entity.Property(e => e.PromoId).HasColumnName("Promo_Id");

                entity.Property(e => e.PromoName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Promo_Name");

                entity.Property(e => e.PromoOffer).HasColumnName("Promo_Offer");
            });

            modelBuilder.Entity<TicketCategory>(entity =>
            {
                entity.ToTable("Ticket_Category");

                entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategory_Id");

                entity.Property(e => e.TicketCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TicketCategory_Name");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_Details");

                entity.HasIndex(e => e.UserUserName, "UQ__User_Det__1C7BA84D4E83FB6A")
                    .IsUnique();

                entity.HasIndex(e => e.UserEmail, "UQ__User_Det__4C70A05CA7695862")
                    .IsUnique();

                entity.HasIndex(e => e.UserContact, "UQ__User_Det__C84FD24D94D740D8")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Address");

                entity.Property(e => e.UserContact).HasColumnName("User_Contact");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("User_Email");

                entity.Property(e => e.UserGender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("User_Gender");

                entity.Property(e => e.UserUserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("User_UserName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
