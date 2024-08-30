namespace Library.Services;

using BCrypt.Net;
using Microsoft.Extensions.Options;
using Library.Models;
using Library.Data;
using Library.Authorization;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


public interface IbookService
{
    
}

public class bookService : IbookService
{
    private LibraryContext _context;
    private IJwtUtils _jwtUtils;
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;

    public bookService(
        LibraryContext context,
        IJwtUtils jwtUtils,
        IOptions<AppSettings> appSettings,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _appSettings = appSettings.Value;
        _mapper = mapper;
    }

    
}