﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using SuperCoolBooks.Models;
using System;
using System.Collections.Generic;

namespace SuperCoolBooks.Models;

public partial class AuthorBook
{
    public int AuthorId { get; set; }

    public int BooksBookId { get; set; }

    public virtual Author Author { get; set; }

    public virtual Book BooksBook { get; set; }
}