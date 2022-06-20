﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using MertBayraktar.Social.Network.Api.Business.Abstracts;
using MertBayraktar.Social.Network.Api.Data;
using MertBayraktar.Social.Network.Api.Entities;
using MertBayraktar.Social.Network.Api.Entities.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace MertBayraktar.Social.Network.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly Context _db;
        private readonly IUserRepo _userRepository;
        public UserController(UserManager<User> userManager, IConfiguration configuration, IMemoryCache memoryCache, IDistributedCache distributedCache, Context db, IUserRepo userRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _db = db;
            _userRepository = userRepository;
        }
        //Cache işlemi
        [HttpGet("GetUserFriendsCache")]
        public async Task<IActionResult> GetAllUsersByMemoryCache()
        {
            if (!_memoryCache.TryGetValue("userList", out List<User> users))
            {
                users = await _db.Users.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    SlidingExpiration = TimeSpan.FromSeconds(45),
                    Priority = CacheItemPriority.Normal
                };
                _memoryCache.Set("userList", users, cacheEntryOptions);
            };
            return Ok(users);
        }
        [HttpGet]
        [Route("GetUsersbyQuery")]
        public IActionResult GetAllUsersQuery([FromQuery] UserParameters parameters)
        {
            var users = _userRepository.GetAllUsers(parameters);

            var metadata = new
            {
                users.Result.TotalCount,
                users.Result.PageSize,
                users.Result.CurrentPage,
                users.Result.TotalPages,
                users.Result.HasNext,
                users.Result.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            return Ok(users);
        }
    }
}

            

        