﻿namespace PROINSA_GP_WEB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}

