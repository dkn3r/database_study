using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lb1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Перевірка наявності таблиці Categories
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE [Categories] (
                        [CategoryID] int NOT NULL IDENTITY,
                        [CategoryName] nvarchar(50) NOT NULL,
                        [Description] nvarchar(max) NOT NULL,
                        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryID])
                    );
                END
            ");

            // Перевірка наявності таблиці Users
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE [Users] (
                        [UserID] int NOT NULL IDENTITY,
                        [Username] nvarchar(50) NOT NULL,
                        [Email] nvarchar(max) NOT NULL,
                        [Password] nvarchar(max) NOT NULL,
                        [PhoneNumber] nvarchar(max) NOT NULL,
                        CONSTRAINT [PK_Users] PRIMARY KEY ([UserID])
                    );
                END
            ");

            // Перевірка наявності таблиці Ads
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ads]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE [Ads] (
                        [AdID] int NOT NULL IDENTITY,
                        [Title] nvarchar(max) NOT NULL,
                        [Description] nvarchar(max) NOT NULL,
                        [Price] decimal(18,2) NOT NULL,
                        [CreatedDate] datetime2 NOT NULL,
                        [UserID] int NOT NULL,
                        [CategoryID] int NOT NULL,
                        CONSTRAINT [PK_Ads] PRIMARY KEY ([AdID]),
                        CONSTRAINT [FK_Ads_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([CategoryID]) ON DELETE CASCADE,
                        CONSTRAINT [FK_Ads_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]) ON DELETE CASCADE
                    );
                END
            ");

            // Перевірка наявності таблиці Messages
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE [Messages] (
                        [MessageID] int NOT NULL IDENTITY,
                        [MessageContent] nvarchar(max) NOT NULL,
                        [SentDate] datetime2 NOT NULL,
                        [AdID] int NOT NULL,
                        [SenderUserID] int NOT NULL,
                        [ReceiverUserID] int NOT NULL,
                        CONSTRAINT [PK_Messages] PRIMARY KEY ([MessageID]),
                        CONSTRAINT [FK_Messages_Ads_AdID] FOREIGN KEY ([AdID]) REFERENCES [Ads] ([AdID]) ON DELETE CASCADE,
                        CONSTRAINT [FK_Messages_Users_ReceiverUserID] FOREIGN KEY ([ReceiverUserID]) REFERENCES [Users] ([UserID]) ON DELETE CASCADE,
                        CONSTRAINT [FK_Messages_Users_SenderUserID] FOREIGN KEY ([SenderUserID]) REFERENCES [Users] ([UserID]) ON DELETE CASCADE
                    );
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Messages");
            migrationBuilder.DropTable(name: "Ads");
            migrationBuilder.DropTable(name: "Categories");
            migrationBuilder.DropTable(name: "Users");
        }
    }
}
