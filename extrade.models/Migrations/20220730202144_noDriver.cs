using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace extrade.models.Migrations
{
    public partial class noDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_UserID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Product_ProductID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Collection_Marketer_MarketerID",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionDetails_Collection_CollectionID",
                table: "CollectionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionDetails_Product_ProductID",
                table: "CollectionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_AspNetUsers_UserID",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Product_ProductID",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Marketer_AspNetUsers_UserID",
                table: "Marketer");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_AspNetUsers_UserID",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneDriver_Driver_DriverID",
                table: "PhoneDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Vendor_VendorID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductID",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_UserID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Product_ProductID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_AspNetUsers_UserID",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorImage_Vendor_VendorID",
                table: "VendorImage");

            migrationBuilder.AlterColumn<int>(
                name: "DriverID",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Balance",
                table: "AllPayment",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_UserID",
                table: "Cart",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Product_ProductID",
                table: "Cart",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Marketer_MarketerID",
                table: "Collection",
                column: "MarketerID",
                principalTable: "Marketer",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionDetails_Collection_CollectionID",
                table: "CollectionDetails",
                column: "CollectionID",
                principalTable: "Collection",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionDetails_Product_ProductID",
                table: "CollectionDetails",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_AspNetUsers_UserID",
                table: "Favourite",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Product_ProductID",
                table: "Favourite",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Marketer_AspNetUsers_UserID",
                table: "Marketer",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserID",
                table: "Order",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_AspNetUsers_UserID",
                table: "Phone",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneDriver_Driver_DriverID",
                table: "PhoneDriver",
                column: "DriverID",
                principalTable: "Driver",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Vendor_VendorID",
                table: "Product",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductID",
                table: "ProductImage",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_UserID",
                table: "Rating",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Product_ProductID",
                table: "Rating",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_AspNetUsers_UserID",
                table: "Vendor",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorImage_Vendor_VendorID",
                table: "VendorImage",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_UserID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Product_ProductID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Collection_Marketer_MarketerID",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionDetails_Collection_CollectionID",
                table: "CollectionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionDetails_Product_ProductID",
                table: "CollectionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_AspNetUsers_UserID",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Product_ProductID",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Marketer_AspNetUsers_UserID",
                table: "Marketer");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_AspNetUsers_UserID",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneDriver_Driver_DriverID",
                table: "PhoneDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Vendor_VendorID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductID",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_UserID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Product_ProductID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_AspNetUsers_UserID",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorImage_Vendor_VendorID",
                table: "VendorImage");

            migrationBuilder.AlterColumn<int>(
                name: "DriverID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Balance",
                table: "AllPayment",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldDefaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_UserID",
                table: "Cart",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Product_ProductID",
                table: "Cart",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Marketer_MarketerID",
                table: "Collection",
                column: "MarketerID",
                principalTable: "Marketer",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionDetails_Collection_CollectionID",
                table: "CollectionDetails",
                column: "CollectionID",
                principalTable: "Collection",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionDetails_Product_ProductID",
                table: "CollectionDetails",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_AspNetUsers_UserID",
                table: "Favourite",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Product_ProductID",
                table: "Favourite",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Marketer_AspNetUsers_UserID",
                table: "Marketer",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserID",
                table: "Order",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_AspNetUsers_UserID",
                table: "Phone",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneDriver_Driver_DriverID",
                table: "PhoneDriver",
                column: "DriverID",
                principalTable: "Driver",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryID",
                table: "Product",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Vendor_VendorID",
                table: "Product",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductID",
                table: "ProductImage",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_UserID",
                table: "Rating",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Product_ProductID",
                table: "Rating",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_AspNetUsers_UserID",
                table: "Vendor",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorImage_Vendor_VendorID",
                table: "VendorImage",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "UserID");
        }
    }
}
