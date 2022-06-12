using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using MertBayraktar.Social.Network.Api.Data;
using MertBayraktar.Social.Network.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public UserController(UserManager<User> userManager, IConfiguration configuration, IMemoryCache memoryCache, IDistributedCache distributedCache, Context db)
        {
            _userManager = userManager;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _db = db;

        }
        //Cache işlemi
        [HttpGet("GetUserFriendsCache")]
        [ResponseCache(Duration =2000,VaryByHeader ="Friendship",VaryByQueryKeys = new string[] {"frienshipId"})]
         public IEnumerable<Friendship> GetFriendship(int friendshipId)
        {

            Friendship[] friendships = _db.Friendships.Where(f => f.Id == friendshipId).ToArray();
            
            if (_memoryCache.TryGetValue("friendships", out friendships))
            {
                return friendships;
            }
            var friendshipByts = _distributedCache.Get("friendships");
            var friendshipJson = Encoding.UTF8.GetString(friendshipByts);
            var friendshipArr = JsonSerializer.Deserialize<Friendship[]>(friendshipJson);

            MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions();
            memoryCacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            memoryCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(30);
            memoryCacheEntryOptions.Priority = CacheItemPriority.High;

            _memoryCache.Set("friendships", friendships, memoryCacheEntryOptions);

            var distFriendshipsArr = JsonSerializer.Serialize(friendships);

            _distributedCache.Set("friendships", Encoding.UTF8.GetBytes(distFriendshipsArr));
            return friendships;
        }
       
        //Token işlemi
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User users)
        {
            List<Claim> claims = new List<Claim>();
            var user = await _userManager.FindByEmailAsync(users.Email);
            if (user == null) throw new Exception("Kullanıcı Bulunamadı");

            var result = await _userManager.CheckPasswordAsync(user, users.PasswordHash);
            if (result)
            {

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                claims.Add(new Claim(ClaimTypes.Name, user.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var token = GetToken(claims);

                var handler = new JwtSecurityTokenHandler();
                string jwt = handler.WriteToken(token);

                return Ok(new
                {
                    token = jwt,
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }


        private JwtSecurityToken GetToken(List<Claim> claims)
        {

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(

                 signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                 issuer: _configuration["JWT:Issuer"],
                 audience: _configuration["JWT:Audience"],
                 expires: DateTime.Now.AddDays(1),
                 claims: claims
                );

            return token;

        }

    }
}

