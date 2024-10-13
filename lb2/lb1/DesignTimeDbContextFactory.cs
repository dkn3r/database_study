﻿using lb1;
using Microsoft.EntityFrameworkCore;

public ClassifiedsContext CreateDbContext(string[] args)
{
    var optionsBuilder = new DbContextOptionsBuilder<ClassifiedsContext>();

    // Використовуйте правильний рядок підключення
    optionsBuilder.UseSqlServer("Server=DESKTOP-OR45QOH;Database=ClassifiedsDB;Trusted_Connection=True;TrustServerCertificate=True;");

    return new ClassifiedsContext(optionsBuilder.Options);
}