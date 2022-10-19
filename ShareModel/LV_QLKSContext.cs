using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShareModel
{
    public partial class LV_QLKSContext : DbContext
    {
        public LV_QLKSContext()
        {
        }

        public LV_QLKSContext(DbContextOptions<LV_QLKSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Businessregistration> Businessregistrations { get; set; } = null!;
        public virtual DbSet<Customerreview> Customerreviews { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Discountdetail> Discountdetails { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Floor> Floors { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<ImageHotel> ImageHotels { get; set; } = null!;
        public virtual DbSet<ImageRoom> ImageRooms { get; set; } = null!;
        public virtual DbSet<Orderroom> Orderrooms { get; set; } = null!;
        public virtual DbSet<Orderroomdetail> Orderroomdetails { get; set; } = null!;
        public virtual DbSet<Pricelistbr> Pricelistbrs { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Typeofaccount> Typeofaccounts { get; set; } = null!;
        public virtual DbSet<Typeofroom> Typeofrooms { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ASUS;Database=LV_QLKS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountUsername)
                    .IsClustered(false);

                entity.ToTable("ACCOUNT");

                entity.HasIndex(e => e.ToaId, "RELATIONSHIP_7_FK");

                entity.Property(e => e.AccountUsername)
                    .HasMaxLength(15)
                    .HasColumnName("ACCOUNT_USERNAME");

                entity.Property(e => e.AccountPassword)
                    .HasMaxLength(50)
                    .HasColumnName("ACCOUNT_PASSWORD");

                entity.Property(e => e.ToaId).HasColumnName("TOA_ID");

                entity.HasOne(d => d.Toa)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ToaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACCOUNT_RELATIONS_TYPEOFAC");
            });

            modelBuilder.Entity<Businessregistration>(entity =>
            {
                entity.HasKey(e => e.BrId)
                    .IsClustered(false);

                entity.ToTable("BUSINESSREGISTRATION");

                entity.HasIndex(e => e.HotelId, "RELATIONSHIP_11_FK");

                entity.HasIndex(e => e.UserPhone, "RELATIONSHIP_12_FK");

                entity.HasIndex(e => e.PricelistbrId, "RELATIONSHIP_13_FK");

                entity.Property(e => e.BrId).HasColumnName("BR_ID");

                entity.Property(e => e.BrDate)
                    .HasColumnType("date")
                    .HasColumnName("BR_DATE");

                entity.Property(e => e.BrStatus).HasColumnName("BR_STATUS");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.PricelistbrId).HasColumnName("PRICELISTBR_ID");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Businessregistrations)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUSINESS_RELATIONS_HOTEL");

                entity.HasOne(d => d.Pricelistbr)
                    .WithMany(p => p.Businessregistrations)
                    .HasForeignKey(d => d.PricelistbrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUSINESS_RELATIONS_PRICELIS");

                entity.HasOne(d => d.UserPhoneNavigation)
                    .WithMany(p => p.Businessregistrations)
                    .HasForeignKey(d => d.UserPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUSINESS_RELATIONS_USER");
            });

            modelBuilder.Entity<Customerreview>(entity =>
            {
                entity.HasKey(e => new { e.RoomId, e.UserPhone });

                entity.ToTable("CUSTOMERREVIEW");

                entity.HasIndex(e => e.UserPhone, "CUSTOMERREVIEW2_FK");

                entity.HasIndex(e => e.RoomId, "CUSTOMERREVIEW_FK");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.Property(e => e.CrComment)
                    .HasMaxLength(200)
                    .HasColumnName("CR_COMMENT");

                entity.Property(e => e.CrDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_DATE");

                entity.Property(e => e.CrStar).HasColumnName("CR_STAR");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Customerreviews)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMER_CUSTOMERR_ROOM");

                entity.HasOne(d => d.UserPhoneNavigation)
                    .WithMany(p => p.Customerreviews)
                    .HasForeignKey(d => d.UserPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMER_CUSTOMERR_USER");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountId)
                    .IsClustered(false);

                entity.ToTable("DISCOUNT");

                entity.HasIndex(e => e.UserPhone, "RELATIONSHIP_19_FK");

                entity.Property(e => e.DiscountId).HasColumnName("DISCOUNT_ID");

                entity.Property(e => e.DiscountDate)
                    .HasColumnType("date")
                    .HasColumnName("DISCOUNT_DATE");

                entity.Property(e => e.DiscountDateend)
                    .HasColumnType("date")
                    .HasColumnName("DISCOUNT_DATEEND");

                entity.Property(e => e.DiscountDatestart)
                    .HasColumnType("date")
                    .HasColumnName("DISCOUNT_DATESTART");

                entity.Property(e => e.DiscountName)
                    .HasMaxLength(50)
                    .HasColumnName("DISCOUNT_NAME");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.HasOne(d => d.UserPhoneNavigation)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.UserPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DISCOUNT_RELATIONS_USER");
            });

            modelBuilder.Entity<Discountdetail>(entity =>
            {
                entity.HasKey(e => new { e.DiscountId, e.RoomId });

                entity.ToTable("DISCOUNTDETAIL");

                entity.HasIndex(e => e.RoomId, "DISCOUNTDETAIL2_FK");

                entity.HasIndex(e => e.DiscountId, "DISCOUNTDETAIL_FK");

                entity.Property(e => e.DiscountId).HasColumnName("DISCOUNT_ID");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.Percent).HasColumnName("PERCENT");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Discountdetails)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DISCOUNT_DISCOUNTD_DISCOUNT");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Discountdetails)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DISCOUNT_DISCOUNTD_ROOM");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => new { e.ProvinceId, e.DistrictId })
                    .IsClustered(false);

                entity.ToTable("DISTRICT");

                entity.HasIndex(e => e.ProvinceId, "RELATIONSHIP_1_FK");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.DistrictId)
                    .HasMaxLength(10)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .HasColumnName("DISTRICT_NAME");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DISTRICT_RELATIONS_PROVINCE");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.HasKey(e => e.FloorId)
                    .IsClustered(false);

                entity.ToTable("FLOOR");

                entity.Property(e => e.FloorId).HasColumnName("FLOOR_ID");

                entity.Property(e => e.FloorDescription)
                    .HasMaxLength(50)
                    .HasColumnName("FLOOR_DESCRIPTION");

                entity.Property(e => e.FloorName)
                    .HasMaxLength(20)
                    .HasColumnName("FLOOR_NAME");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.HotelId)
                    .IsClustered(false);

                entity.ToTable("HOTEL");

                entity.HasIndex(e => e.UserPhone, "RELATIONSHIP_18_FK");

                entity.HasIndex(e => new { e.ProvinceId, e.DistrictId, e.WardId }, "RELATIONSHIP_3_FK");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.DistrictId)
                    .HasMaxLength(10)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.HotelAddress)
                    .HasMaxLength(200)
                    .HasColumnName("HOTEL_ADDRESS");

                entity.Property(e => e.HotelName)
                    .HasMaxLength(100)
                    .HasColumnName("HOTEL_NAME");

                entity.Property(e => e.HotelStatus).HasColumnName("HOTEL_STATUS");

                entity.Property(e => e.HotelX)
                    .HasMaxLength(50)
                    .HasColumnName("HOTEL_X");

                entity.Property(e => e.HotelY)
                    .HasMaxLength(50)
                    .HasColumnName("HOTEL_Y");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.Property(e => e.WardId)
                    .HasMaxLength(10)
                    .HasColumnName("WARD_ID");

                entity.HasOne(d => d.UserPhoneNavigation)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.UserPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOTEL_RELATIONS_USER");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => new { d.ProvinceId, d.DistrictId, d.WardId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOTEL_RELATIONS_WARD");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .IsClustered(false);

                entity.ToTable("IMAGE");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(100)
                    .HasColumnName("IMAGE_NAME");
            });

            modelBuilder.Entity<ImageHotel>(entity =>
            {
                entity.HasKey(e => new { e.ImageId, e.HotelId });

                entity.ToTable("IMAGE_HOTEL");

                entity.HasIndex(e => e.HotelId, "IMAGE_HOTEL2_FK");

                entity.HasIndex(e => e.ImageId, "IMAGE_HOTEL_FK");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.ImageHotels)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMAGE_HO_IMAGE_HOT_HOTEL");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageHotels)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMAGE_HO_IMAGE_HOT_IMAGE");
            });

            modelBuilder.Entity<ImageRoom>(entity =>
            {
                entity.HasKey(e => new { e.ImageId, e.RoomId });

                entity.ToTable("IMAGE_ROOM");

                entity.HasIndex(e => e.RoomId, "IMAGE_ROOM2_FK");

                entity.HasIndex(e => e.ImageId, "IMAGE_ROOM_FK");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageRooms)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMAGE_RO_IMAGE_ROO_IMAGE");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.ImageRooms)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMAGE_RO_IMAGE_ROO_ROOM");
            });

            modelBuilder.Entity<Orderroom>(entity =>
            {
                entity.HasKey(e => e.OrderroomId)
                    .IsClustered(false);

                entity.ToTable("ORDERROOM");

                entity.HasIndex(e => e.UserPhone, "RELATIONSHIP_15_FK");

                entity.Property(e => e.OrderroomId).HasColumnName("ORDERROOM_ID");

                entity.Property(e => e.OrderroomDate)
                    .HasColumnType("date")
                    .HasColumnName("ORDERROOM_DATE");

                entity.Property(e => e.OrderroomDateend)
                    .HasColumnType("date")
                    .HasColumnName("ORDERROOM_DATEEND");

                entity.Property(e => e.OrderroomDatestart)
                    .HasColumnType("date")
                    .HasColumnName("ORDERROOM_DATESTART");

                entity.Property(e => e.OrderroomStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ORDERROOM_STATUS")
                    .IsFixedLength();

                entity.Property(e => e.OrderroomTotalprice).HasColumnName("ORDERROOM_TOTALPRICE");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.HasOne(d => d.UserPhoneNavigation)
                    .WithMany(p => p.Orderrooms)
                    .HasForeignKey(d => d.UserPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERROO_RELATIONS_USER");
            });

            modelBuilder.Entity<Orderroomdetail>(entity =>
            {
                entity.HasKey(e => new { e.RoomId, e.OrderroomId });

                entity.ToTable("ORDERROOMDETAIL");

                entity.HasIndex(e => e.OrderroomId, "ORDERROOMDETAIL2_FK");

                entity.HasIndex(e => e.RoomId, "ORDERROOMDETAIL_FK");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.OrderroomId).HasColumnName("ORDERROOM_ID");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.HasOne(d => d.Orderroom)
                    .WithMany(p => p.Orderroomdetails)
                    .HasForeignKey(d => d.OrderroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERROO_ORDERROOM_ORDERROO");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Orderroomdetails)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERROO_ORDERROOM_ROOM");
            });

            modelBuilder.Entity<Pricelistbr>(entity =>
            {
                entity.HasKey(e => e.PricelistbrId)
                    .IsClustered(false);

                entity.ToTable("PRICELISTBR");

                entity.Property(e => e.PricelistbrId).HasColumnName("PRICELISTBR_ID");

                entity.Property(e => e.PricelistbrMonth).HasColumnName("PRICELISTBR_MONTH");

                entity.Property(e => e.PricelistbrName)
                    .HasMaxLength(20)
                    .HasColumnName("PRICELISTBR_NAME");

                entity.Property(e => e.PricelistbrPrice).HasColumnName("PRICELISTBR_PRICE");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvinceId)
                    .IsClustered(false);

                entity.ToTable("PROVINCE");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(50)
                    .HasColumnName("PROVINCE_NAME");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .IsClustered(false);

                entity.ToTable("ROOM");

                entity.HasIndex(e => e.FloorId, "RELATIONSHIP_22_FK");

                entity.HasIndex(e => e.HotelId, "RELATIONSHIP_4_FK");

                entity.HasIndex(e => e.TorId, "RELATIONSHIP_5_FK");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.FloorId).HasColumnName("FLOOR_ID");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.RoomDescription)
                    .HasMaxLength(1000)
                    .HasColumnName("ROOM_DESCRIPTION");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(50)
                    .HasColumnName("ROOM_NAME");

                entity.Property(e => e.RoomStatus).HasColumnName("ROOM_STATUS");

                entity.Property(e => e.TorId).HasColumnName("TOR_ID");

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.FloorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_RELATIONS_FLOOR");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_RELATIONS_HOTEL");

                entity.HasOne(d => d.Tor)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.TorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_RELATIONS_TYPEOFRO");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .IsClustered(false);

                entity.ToTable("SERVICE");

                entity.HasIndex(e => e.HotelId, "RELATIONSHIP_16_FK");

                entity.HasIndex(e => e.UserPhone, "RELATIONSHIP_17_FK");

                entity.Property(e => e.ServiceId).HasColumnName("SERVICE_ID");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.ServiceDescription)
                    .HasMaxLength(200)
                    .HasColumnName("SERVICE_DESCRIPTION");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(20)
                    .HasColumnName("SERVICE_NAME");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_SERVICE_RELATIONS_HOTEL");

                entity.HasOne(d => d.UserPhoneNavigation)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.UserPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SERVICE_RELATIONS_USER");
            });

            modelBuilder.Entity<Typeofaccount>(entity =>
            {
                entity.HasKey(e => e.ToaId)
                    .IsClustered(false);

                entity.ToTable("TYPEOFACCOUNT");

                entity.Property(e => e.ToaId).HasColumnName("TOA_ID");

                entity.Property(e => e.ToaPower)
                    .HasMaxLength(10)
                    .HasColumnName("TOA_POWER");
            });

            modelBuilder.Entity<Typeofroom>(entity =>
            {
                entity.HasKey(e => e.TorId)
                    .IsClustered(false);

                entity.ToTable("TYPEOFROOM");

                entity.HasIndex(e => e.UserPhone, "RELATIONSHIP_6_FK");

                entity.Property(e => e.TorId).HasColumnName("TOR_ID");

                entity.Property(e => e.TorCapacity).HasColumnName("TOR_CAPACITY");

                entity.Property(e => e.TorName)
                    .HasMaxLength(50)
                    .HasColumnName("TOR_NAME");

                entity.Property(e => e.TorPrice).HasColumnName("TOR_PRICE");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.HasOne(d => d.UserPhoneNavigation)
                    .WithMany(p => p.Typeofrooms)
                    .HasForeignKey(d => d.UserPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TYPEOFRO_RELATIONS_USER");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserPhone)
                    .IsClustered(false);

                entity.ToTable("USER");

                entity.HasIndex(e => e.AccountUsername, "RELATIONSHIP_10_FK");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .HasColumnName("USER_PHONE");

                entity.Property(e => e.AccountUsername)
                    .HasMaxLength(15)
                    .HasColumnName("ACCOUNT_USERNAME");

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(200)
                    .HasColumnName("USER_ADDRESS");

                entity.Property(e => e.UserDateofbirth)
                    .HasColumnType("date")
                    .HasColumnName("USER_DATEOFBIRTH");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .HasColumnName("USER_EMAIL");

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserSex).HasColumnName("USER_SEX");

                entity.HasOne(d => d.AccountUsernameNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccountUsername)
                    .HasConstraintName("FK_USER_RELATIONS_ACCOUNT");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => new { e.ProvinceId, e.DistrictId, e.WardId })
                    .IsClustered(false);

                entity.ToTable("WARD");

                entity.HasIndex(e => new { e.ProvinceId, e.DistrictId }, "RELATIONSHIP_2_FK");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.DistrictId)
                    .HasMaxLength(10)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.WardId)
                    .HasMaxLength(10)
                    .HasColumnName("WARD_ID");

                entity.Property(e => e.WardName)
                    .HasMaxLength(50)
                    .HasColumnName("WARD_NAME");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => new { d.ProvinceId, d.DistrictId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WARD_RELATIONS_DISTRICT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
