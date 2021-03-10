using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Truck.Entity
{
    public partial class TruckContext : DbContext
    {
        public TruckContext()
        {
        }

        public TruckContext(DbContextOptions<TruckContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppUserCredential> AppUserCredentials { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ConfirmationRequest> ConfirmationRequests { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<EcomOrderStatus_Master> EcomOrderStatus_Masters { get; set; }
        public virtual DbSet<Ecom_FavoriteListUserwise> Ecom_FavoriteListUserwises { get; set; }
        public virtual DbSet<Ecom_Invoice> Ecom_Invoices { get; set; }
        public virtual DbSet<Ecom_Order> Ecom_Orders { get; set; }
        public virtual DbSet<Ecom_OrderItem> Ecom_OrderItems { get; set; }
        public virtual DbSet<Ecom_Order_ShipmentDetail> Ecom_Order_ShipmentDetails { get; set; }
        public virtual DbSet<Ecom_Payment> Ecom_Payments { get; set; }
        public virtual DbSet<Ecom_Shipping> Ecom_Shippings { get; set; }
        public virtual DbSet<Ecom_ShoppingCart> Ecom_ShoppingCarts { get; set; }
        public virtual DbSet<Ecom_Topic> Ecom_Topics { get; set; }
        public virtual DbSet<Ecom_TopicDetails_Category> Ecom_TopicDetails_Categories { get; set; }
        public virtual DbSet<Ecom_TopicDetails_Product> Ecom_TopicDetails_Products { get; set; }
        public virtual DbSet<GST> GSTs { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Insurance_Renewed> Insurance_Reneweds { get; set; }
        public virtual DbSet<KYC> KYCs { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LanguageMapping_Master> LanguageMapping_Masters { get; set; }
        public virtual DbSet<Language_Master> Language_Masters { get; set; }
        public virtual DbSet<Menu_Master> Menu_Masters { get; set; }
        public virtual DbSet<Navigation_Master> Navigation_Masters { get; set; }
        public virtual DbSet<Page_Master> Page_Masters { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Category_Master> Product_Category_Masters { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Teams_Role> Teams_Roles { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<Truck_RenewalType> Truck_RenewalTypes { get; set; }
        public virtual DbSet<Vehicle_Company_Master> Vehicle_Company_Masters { get; set; }
        public virtual DbSet<Vehicle_Document> Vehicle_Documents { get; set; }
        public virtual DbSet<Vehicle_Master> Vehicle_Masters { get; set; }
        public virtual DbSet<Vehicle_Model_Master> Vehicle_Model_Masters { get; set; }
        public virtual DbSet<Vehicle_Period> Vehicle_Periods { get; set; }
        public virtual DbSet<Vehicle_Renewal_Info> Vehicle_Renewal_Infos { get; set; }
        public virtual DbSet<Vehicle_Renewal_Master> Vehicle_Renewal_Masters { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.userID)
                    .HasName("PK_AppUser_userID");

                entity.ToTable("AppUser");

                entity.HasIndex(e => e.mobile, "U_Mobile")
                    .IsUnique();

                entity.Property(e => e.AadharCard)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OTP)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.OTPExpireTime).HasPrecision(0);

                entity.Property(e => e.PanCard)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pin)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.company)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.createdDate).HasPrecision(0);

                entity.Property(e => e.dateOfBirth).HasPrecision(0);

                entity.Property(e => e.dpPath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.fullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.mobile)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.pincode)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.userName)
                    .HasMaxLength(145)
                    .IsUnicode(false);

                entity.HasOne(d => d.city)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.cityID)
                    .HasConstraintName("AppUser$user_city");

                entity.HasOne(d => d.country)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.countryID)
                    .HasConstraintName("FK_AppUser_Country");

                entity.HasOne(d => d.state)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.stateID)
                    .HasConstraintName("AppUser$user_state");
            });

            modelBuilder.Entity<AppUserCredential>(entity =>
            {
                entity.HasKey(e => e.credentialID)
                    .HasName("PK_AppUserCredentials_credentialID");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.user)
                    .WithMany(p => p.AppUserCredentials)
                    .HasForeignKey(d => d.userID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AppUserCredentials$user_credentials");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.Area_ID)
                    .HasName("PK_area_areaid");

                entity.ToTable("Area");

                entity.Property(e => e.Area_Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.City_ID)
                    .HasConstraintName("Area$FK_Fk_City_ID");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit");

                entity.Property(e => e.createdDate).HasPrecision(0);

                entity.Property(e => e.deviceID)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.fcmID)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.city_ID)
                    .HasName("PK_City_cityID");

                entity.ToTable("City");

                entity.Property(e => e.City_Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ConfirmationRequest>(entity =>
            {
                entity.HasKey(e => e.confirmationID)
                    .HasName("PK_ConfirmationRequests_confirmationID");

                entity.Property(e => e.code)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.resetTime).HasPrecision(0);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Contact_ID);

                entity.ToTable("Contact");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone_Number)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Request_Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Country_Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EcomOrderStatus_Master>(entity =>
            {
                entity.HasKey(e => e.OrderStatus_ID)
                    .HasName("PK_OrderStatus_ID");

                entity.ToTable("EcomOrderStatus_Master");

                entity.Property(e => e.Last_Updated_Date).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ecom_FavoriteListUserwise>(entity =>
            {
                entity.HasKey(e => e.Favorate_ID)
                    .HasName("Ecom_PK_Favorate_ID");

                entity.ToTable("Ecom_FavoriteListUserwise");

                entity.HasOne(d => d.FK_AppUser)
                    .WithMany(p => p.Ecom_FavoriteListUserwises)
                    .HasForeignKey(d => d.FK_AppUser_Id)
                    .HasConstraintName("Ecom_FavorateLists$FK_AppUser_Id");

                entity.HasOne(d => d.FK_Product)
                    .WithMany(p => p.Ecom_FavoriteListUserwises)
                    .HasForeignKey(d => d.FK_Product_Id)
                    .HasConstraintName("Ecom_FavorateList$FK_Product_Id");
            });

            modelBuilder.Entity<Ecom_Invoice>(entity =>
            {
                entity.HasKey(e => e.Invoice_Id)
                    .HasName("Ecom_PK_Invoice_ID");

                entity.ToTable("Ecom_Invoice");

                entity.Property(e => e.Invoice_Date).HasPrecision(0);

                entity.Property(e => e.Invoice_Path)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.FK_AppUser)
                    .WithMany(p => p.Ecom_Invoices)
                    .HasForeignKey(d => d.FK_AppUser_Id)
                    .HasConstraintName("Ecom_Invoice$FK_AppUser_Id");

                entity.HasOne(d => d.FK_Order)
                    .WithMany(p => p.Ecom_Invoices)
                    .HasForeignKey(d => d.FK_Order_Id)
                    .HasConstraintName("Ecom_Invoice$FK_Order_Id");
            });

            modelBuilder.Entity<Ecom_Order>(entity =>
            {
                entity.HasKey(e => e.Order_ID)
                    .HasName("Ecom_PK_EcomOrders_productID");

                entity.Property(e => e.CancelReason)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FK_Razor_Order_Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Order_Date).HasColumnType("datetime");

                entity.Property(e => e.Order_Discount).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Order_GrandTotal).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Order_Promo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Order_Shipping).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Order_SubTotal).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Order_Tax).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Order_Total).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Payment_Details)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Payment_Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Razor_Order_Ids)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FK_AppUser)
                    .WithMany(p => p.Ecom_Orders)
                    .HasForeignKey(d => d.FK_AppUser_Id)
                    .HasConstraintName("Ecom_Orders$FK_AppUser_Id");

                entity.HasOne(d => d.Fk_Shipping)
                    .WithMany(p => p.Ecom_Orders)
                    .HasForeignKey(d => d.Fk_Shipping_id)
                    .HasConstraintName("Ecom_Orders$FK_Shipping_Id");

                entity.HasOne(d => d.OrderStatusNavigation)
                    .WithMany(p => p.Ecom_Orders)
                    .HasForeignKey(d => d.OrderStatus)
                    .HasConstraintName("Ecom_Orders$FK_orderstatus_id");
            });

            modelBuilder.Entity<Ecom_OrderItem>(entity =>
            {
                entity.HasKey(e => e.OrderItems_ID)
                    .HasName("Ecom_PK_OrderItems_productID");

                entity.Property(e => e.Order_Date).HasPrecision(0);

                entity.Property(e => e.Order_Price).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Order_Tax).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Product_Discount).HasColumnType("decimal(11, 3)");

                entity.HasOne(d => d.FK_Order)
                    .WithMany(p => p.Ecom_OrderItems)
                    .HasForeignKey(d => d.FK_Order_Id)
                    .HasConstraintName("Ecom_OrdersItems$FK_Product_Id");

                entity.HasOne(d => d.FK_Product)
                    .WithMany(p => p.Ecom_OrderItems)
                    .HasForeignKey(d => d.FK_Product_Id)
                    .HasConstraintName("Ecom_OrdersItems$FK_AppUser_Id");
            });

            modelBuilder.Entity<Ecom_Order_ShipmentDetail>(entity =>
            {
                entity.HasKey(e => e.oderShiptmentId);

                entity.Property(e => e.shipmentCompany)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.shipmentDate).HasColumnType("datetime");

                entity.Property(e => e.shipmentID)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.order)
                    .WithMany(p => p.Ecom_Order_ShipmentDetails)
                    .HasForeignKey(d => d.orderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ecom_Order_ShipmentDetails_Ecom_Orders");
            });

            modelBuilder.Entity<Ecom_Payment>(entity =>
            {
                entity.HasKey(e => e.Payment_ID)
                    .HasName("Ecom_PK_Payment_ID");

                entity.ToTable("Ecom_Payment");

                entity.Property(e => e.Payment_Date).HasPrecision(0);

                entity.Property(e => e.Payment_Method)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FK_AppUser)
                    .WithMany(p => p.Ecom_Payments)
                    .HasForeignKey(d => d.FK_AppUser_Id)
                    .HasConstraintName("Ecom_Payment$FK_AppUser_Id");

                entity.HasOne(d => d.FK_Invoice)
                    .WithMany(p => p.Ecom_Payments)
                    .HasForeignKey(d => d.FK_Invoice_Id)
                    .HasConstraintName("Ecom_Payment$FK_Product_Id");
            });

            modelBuilder.Entity<Ecom_Shipping>(entity =>
            {
                entity.HasKey(e => e.Shipment_ID)
                    .HasName("Ecom_PK_Shipment_ID");

                entity.ToTable("Ecom_Shipping");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Created_Date).HasPrecision(0);

                entity.Property(e => e.Email_Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Shipment_Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Shipping_Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.FK_AppUser)
                    .WithMany(p => p.Ecom_Shippings)
                    .HasForeignKey(d => d.FK_AppUser_Id)
                    .HasConstraintName("Ecom_Shipping$FK_Order_Id");

                entity.HasOne(d => d.FK_OrderItem)
                    .WithMany(p => p.Ecom_Shippings)
                    .HasForeignKey(d => d.FK_OrderItem_Id)
                    .HasConstraintName("Ecom_Shipping$FK_OrderItem_Id");

                entity.HasOne(d => d.FK_Order)
                    .WithMany(p => p.Ecom_Shippings)
                    .HasForeignKey(d => d.FK_Order_Id)
                    .HasConstraintName("Ecom_Shipping$FK_OrderItemId");

                entity.HasOne(d => d.FK_Product)
                    .WithMany(p => p.Ecom_Shippings)
                    .HasForeignKey(d => d.FK_Product_Id)
                    .HasConstraintName("Ecom_Shipping$FK_Product_Id");
            });

            modelBuilder.Entity<Ecom_ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.ShoppingCart_ID)
                    .HasName("Ecom_PK_ShoppingCart_ID");

                entity.ToTable("Ecom_ShoppingCart");

                entity.Property(e => e.MRP).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Order_Date).HasPrecision(0);

                entity.Property(e => e.Price).HasColumnType("decimal(11, 3)");

                entity.HasOne(d => d.FK_AppUser)
                    .WithMany(p => p.Ecom_ShoppingCarts)
                    .HasForeignKey(d => d.FK_AppUser_Id)
                    .HasConstraintName("Ecom_ShoppingCart$FK_AppUser_Id");

                entity.HasOne(d => d.FK_Product)
                    .WithMany(p => p.Ecom_ShoppingCarts)
                    .HasForeignKey(d => d.FK_Product_Id)
                    .HasConstraintName("Ecom_ShoppingCart$FK_Product_Id");
            });

            modelBuilder.Entity<Ecom_Topic>(entity =>
            {
                entity.HasKey(e => e.Topic_ID)
                    .HasName("Pk_Topic_ID");

                entity.ToTable("Ecom_Topic");

                entity.Property(e => e.Brand_Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Topic_Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Topic_Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ecom_TopicDetails_Category>(entity =>
            {
                entity.HasKey(e => e.TopicDetails_Category_ID)
                    .HasName("Pk_TopicDetails_Category_ID");

                entity.ToTable("Ecom_TopicDetails_Category");

                entity.HasOne(d => d.FK_Topic)
                    .WithMany(p => p.Ecom_TopicDetails_Categories)
                    .HasForeignKey(d => d.FK_Topic_ID)
                    .HasConstraintName("TopicDetailsCategory_fkFK_Topicid");
            });

            modelBuilder.Entity<Ecom_TopicDetails_Product>(entity =>
            {
                entity.HasKey(e => e.TopicDetails_Product_ID)
                    .HasName("Pk_TopicDetails_Product_ID");

                entity.ToTable("Ecom_TopicDetails_Product");

                entity.HasOne(d => d.FK_Product)
                    .WithMany(p => p.Ecom_TopicDetails_Products)
                    .HasForeignKey(d => d.FK_Productid)
                    .HasConstraintName("TopicDetailsProduct_fkFK_Subcategoryid");

                entity.HasOne(d => d.FK_Topic)
                    .WithMany(p => p.Ecom_TopicDetails_Products)
                    .HasForeignKey(d => d.FK_Topic_ID)
                    .HasConstraintName("TopicDetailsProduct_fkFK_Topicid");
            });

            modelBuilder.Entity<GST>(entity =>
            {
                entity.HasKey(e => e.GST_ID)
                    .HasName("PK_GST_ID");

                entity.ToTable("GST");

                entity.Property(e => e.GST_Value)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.HasKey(e => e.Insurance_ID);

                entity.ToTable("Insurance");

                entity.Property(e => e.Insurance_Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Insurance_Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Insurance_Renewed>(entity =>
            {
                entity.HasKey(e => e.InsuranceRenewed_ID)
                    .HasName("PK_InsuranceRenewed_ID");

                entity.ToTable("Insurance_Renewed");

                entity.Property(e => e.Expiry_Date).HasPrecision(0);

                entity.Property(e => e.Insurance_Company)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Registered_Date).HasPrecision(0);

                entity.Property(e => e.Vehicle_BackImage)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Vehicle_FrontImage)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<KYC>(entity =>
            {
                entity.HasKey(e => e.Kyc_ID)
                    .HasName("PK_KYC_ID");

                entity.ToTable("KYC");

                entity.Property(e => e.AadharCard)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PanCard)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.createdDate).HasPrecision(0);

                entity.Property(e => e.dpPath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.pincode)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.city)
                    .WithMany(p => p.KYCs)
                    .HasForeignKey(d => d.cityID)
                    .HasConstraintName("KYC$user_city");

                entity.HasOne(d => d.state)
                    .WithMany(p => p.KYCs)
                    .HasForeignKey(d => d.stateID)
                    .HasConstraintName("KYC$user_state");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.Lang_ID);

                entity.Property(e => e.Lang_Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LanguageMapping_Master>(entity =>
            {
                entity.HasKey(e => e.LanguageMapping_ID)
                    .HasName("PK_LangMap_ID");

                entity.ToTable("LanguageMapping_Master");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Keys)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Types)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fk_Language)
                    .WithMany(p => p.LanguageMapping_Masters)
                    .HasForeignKey(d => d.Fk_Language_ID)
                    .HasConstraintName("langMaster$langid");

                entity.HasOne(d => d.Fk_Menu)
                    .WithMany(p => p.LanguageMapping_Masters)
                    .HasForeignKey(d => d.Fk_Menu_ID)
                    .HasConstraintName("langMaster$menuid");

                entity.HasOne(d => d.Fk_Page)
                    .WithMany(p => p.LanguageMapping_Masters)
                    .HasForeignKey(d => d.Fk_Page_ID)
                    .HasConstraintName("langMaster$pageid");
            });

            modelBuilder.Entity<Language_Master>(entity =>
            {
                entity.HasKey(e => e.Language_ID)
                    .HasName("PK_Language_ID");

                entity.ToTable("Language_Master");

                entity.Property(e => e.Language)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu_Master>(entity =>
            {
                entity.HasKey(e => e.Menu_ID)
                    .HasName("PK_Menu_ID");

                entity.ToTable("Menu_Master");

                entity.Property(e => e.Menu_Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Navigation_Master>(entity =>
            {
                entity.HasKey(e => e.Navigation_ID)
                    .HasName("PK_Navigation_ID");

                entity.ToTable("Navigation_Master");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Keys)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fk_Language)
                    .WithMany(p => p.Navigation_Masters)
                    .HasForeignKey(d => d.Fk_Language_ID)
                    .HasConstraintName("NavMaster$langid");

                entity.HasOne(d => d.Fk_User)
                    .WithMany(p => p.Navigation_Masters)
                    .HasForeignKey(d => d.Fk_User_ID)
                    .HasConstraintName("NavMaster$userid");
            });

            modelBuilder.Entity<Page_Master>(entity =>
            {
                entity.HasKey(e => e.Page_ID)
                    .HasName("PK_Page_ID");

                entity.ToTable("Page_Master");

                entity.Property(e => e.Page_Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.MRP).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.Photo_Path)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product_CompanyAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Product_CompanyContactNo)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Product_CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product_CountryOrigin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product_Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Product_Load)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product_Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product_Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SP).HasColumnType("decimal(11, 3)");

                entity.Property(e => e.createdDate).HasPrecision(0);

                entity.HasOne(d => d.FK_GSTNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FK_GST)
                    .HasConstraintName("products$FK_Fk_GST");

                entity.HasOne(d => d.FK_ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FK_ProductCategory_Id)
                    .HasConstraintName("Products$products");

                entity.HasOne(d => d.Fk_User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Fk_User_ID)
                    .HasConstraintName("Teams$FK_Fk_User_ID");
            });

            modelBuilder.Entity<Product_Category_Master>(entity =>
            {
                entity.HasKey(e => e.ProductCategory_ID)
                    .HasName("PK_ProdCatmastr_ProductCategory_ID");

                entity.ToTable("Product_Category_Master");

                entity.Property(e => e.Last_Updated_Date).HasColumnType("datetime");

                entity.Property(e => e.ProdCategory_Path).HasMaxLength(100);

                entity.Property(e => e.Product_Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Updated_By).HasMaxLength(50);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Settings_ID);

                entity.HasOne(d => d.FK_Lang)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.FK_LangID)
                    .HasConstraintName("Settings$langid");

                entity.HasOne(d => d.FK_Theme)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.FK_ThemeID)
                    .HasConstraintName("Settings$FK_themeid");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.State_ID)
                    .HasName("PK_states_stateID");

                entity.ToTable("State");

                entity.Property(e => e.State_Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FK_Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.FK_Country_ID)
                    .HasConstraintName("FK_FK_Country_ID");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Team_ID)
                    .HasName("PK_Teams_teamID");

                entity.Property(e => e.Branch)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DP_Image)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.refererCode)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.whatsAppNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.FK_TeamRole)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.FK_TeamRoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teams$FK_TeamRoleID");

                entity.HasOne(d => d.FK_user)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.FK_userID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teams$teams_users");
            });

            modelBuilder.Entity<Teams_Role>(entity =>
            {
                entity.HasKey(e => e.Team_RoleID);

                entity.ToTable("Teams_Role");

                entity.Property(e => e.Team_RoleType)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.HasKey(e => e.Themes_ID);

                entity.Property(e => e.Themes_Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Truck_RenewalType>(entity =>
            {
                entity.HasKey(e => e.Truck_RenewalId)
                    .HasName("PK_Truck_RenewalId");

                entity.ToTable("Truck_RenewalType");

                entity.Property(e => e.Truck_Renewal_Type)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle_Company_Master>(entity =>
            {
                entity.HasKey(e => e.VehicleCompany_ID);

                entity.ToTable("Vehicle_Company_Master");

                entity.Property(e => e.VehicleCompany_Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle_Document>(entity =>
            {
                entity.HasKey(e => e.VehicleDocuments_ID)
                    .HasName("PK_VehicleDocuments_ID");

                entity.Property(e => e.Expiry_Date).HasPrecision(0);

                entity.Property(e => e.Insurance_Company)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Registered_Date).HasPrecision(0);

                entity.Property(e => e.Vehicle_BackImage)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Vehicle_FrontImage)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.FK_APPUSER)
                    .WithMany(p => p.Vehicle_Documents)
                    .HasForeignKey(d => d.FK_APPUSERID)
                    .HasConstraintName("Vehicle_Documents$FK_AppUser_Id");

                entity.HasOne(d => d.FK_Period)
                    .WithMany(p => p.Vehicle_Documents)
                    .HasForeignKey(d => d.FK_Period_ID)
                    .HasConstraintName("FK_FK_Period_ID_Vehicle_Documents");

                entity.HasOne(d => d.FK_VehicleRenewal)
                    .WithMany(p => p.Vehicle_Documents)
                    .HasForeignKey(d => d.FK_VehicleRenewal_ID)
                    .HasConstraintName("FK_Vehicle_Renewal_Info_Vehicle_Documents");

                entity.HasOne(d => d.FK_VehicleRenewalinfo)
                    .WithMany(p => p.Vehicle_Documents)
                    .HasForeignKey(d => d.FK_VehicleRenewalinfo_ID)
                    .HasConstraintName("FK_Vehicle_RenewalInfoID__Vehicle_Documents");
            });

            modelBuilder.Entity<Vehicle_Master>(entity =>
            {
                entity.HasKey(e => e.VehicleType_ID)
                    .HasName("PK_Vehicle_Type_Master");

                entity.ToTable("Vehicle_Master");

                entity.Property(e => e.VehicleType_Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle_Model_Master>(entity =>
            {
                entity.HasKey(e => e.VehicleModel_ID);

                entity.ToTable("Vehicle_Model_Master");

                entity.Property(e => e.VehicleModel_Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle_Period>(entity =>
            {
                entity.HasKey(e => e.VehiclePeriod_ID)
                    .HasName("PK_PmService_ID");

                entity.ToTable("Vehicle_Period");

                entity.Property(e => e.VehiclePeriod_Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle_Renewal_Info>(entity =>
            {
                entity.HasKey(e => e.VehicleRenewalInfo_ID);

                entity.ToTable("Vehicle_Renewal_Info");

                entity.Property(e => e.Expiry_Date).HasPrecision(0);

                entity.Property(e => e.Insurance_Company)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Registered_Date).HasPrecision(0);

                entity.Property(e => e.Vehicle_BackImage)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Vehicle_FrontImage)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Vehicle_ModelNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vehicle_Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Vehicle_Number)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FK_APPUSER)
                    .WithMany(p => p.Vehicle_Renewal_Infos)
                    .HasForeignKey(d => d.FK_APPUSERID)
                    .HasConstraintName("FK_APPUSERID$FK_AppUser_Id");

                entity.HasOne(d => d.FK_Period)
                    .WithMany(p => p.Vehicle_Renewal_Infos)
                    .HasForeignKey(d => d.FK_Period_ID)
                    .HasConstraintName("FK_FK_Period_ID");

                entity.HasOne(d => d.FK_VehicleMaster)
                    .WithMany(p => p.Vehicle_Renewal_Infos)
                    .HasForeignKey(d => d.FK_VehicleMaster_ID)
                    .HasConstraintName("FK_Vehiclemasterid_FK");

                entity.HasOne(d => d.FK_VehicleRenewal)
                    .WithMany(p => p.Vehicle_Renewal_Infos)
                    .HasForeignKey(d => d.FK_VehicleRenewal_ID)
                    .HasConstraintName("FK_Vehicle_Renewal_Info");

                entity.HasOne(d => d.Vehicle_Company)
                    .WithMany(p => p.Vehicle_Renewal_Infos)
                    .HasForeignKey(d => d.Vehicle_Company_ID)
                    .HasConstraintName("FK_Vehicle_Company_ID");

                entity.HasOne(d => d.Vehicle_Model)
                    .WithMany(p => p.Vehicle_Renewal_Infos)
                    .HasForeignKey(d => d.Vehicle_Model_ID)
                    .HasConstraintName("FK_Vehicle_Model_ID");
            });

            modelBuilder.Entity<Vehicle_Renewal_Master>(entity =>
            {
                entity.HasKey(e => e.VehicleRenewal_ID);

                entity.ToTable("Vehicle_Renewal_Master");

                entity.Property(e => e.VehicleRenewal_Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.FK_VehicleMaster)
                    .WithMany(p => p.Vehicle_Renewal_Masters)
                    .HasForeignKey(d => d.FK_VehicleMaster_ID)
                    .HasConstraintName("FK_Vehiclemasterid");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Wallet_ID)
                    .HasName("Ecom_PK_Wallet_ID");

                entity.ToTable("Wallet");

                entity.Property(e => e.Payment_Date).HasPrecision(0);

                entity.Property(e => e.Payment_Method)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
