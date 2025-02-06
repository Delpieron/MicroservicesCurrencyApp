using Microsoft.AspNetCore.Mvc;
using RestApiMicroService.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using RestApiMicroService.Data;
using RestApiMicroService.Controllers.RequestModels;
using LoginRequest = RestApiMicroService.Controllers.RequestModels.LoginRequest;

namespace RestApiMicroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(AppDbContext context) : ControllerBase
    {

        // Endpoint rejestracji nowego użytkownika
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            // Sprawdzenie, czy użytkownik o podanym username już istnieje
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }

            // Generujemy sól (16 bajtów)
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string saltBase64 = Convert.ToBase64String(saltBytes);

            // Hashujemy hasło przy użyciu PBKDF2 (10 000 iteracji, SHA256, 32 bajty klucza)
            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(request.Password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                hashBytes = pbkdf2.GetBytes(32);
            }
            string hashBase64 = Convert.ToBase64String(hashBytes);

            // Tworzymy nowego użytkownika; email można pobierać z żądania lub ustawić domyślnie
            var user = new User
            {
                Username = request.Username,
                Email = string.IsNullOrWhiteSpace(request.Email)
                          ? $"{request.Username}@example.com"
                          : request.Email,
                PasswordHash = hashBase64,
                PasswordSalt = saltBase64
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }


        // Endpoint logowania użytkownika
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            // Pobieramy użytkownika z bazy
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Konwertujemy zapisaną sól z Base64 do bajtów
            byte[] saltBytes = Convert.FromBase64String(user.PasswordSalt);

            // Generujemy hash dla podanego hasła używając zapisanej soli
            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(request.Password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                hashBytes = pbkdf2.GetBytes(32);
            }
            string computedHashBase64 = Convert.ToBase64String(hashBytes);

            // Porównanie hasha z wartością zapisaną w bazie
            if (!computedHashBase64.Equals(user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            // W przypadku poprawnego logowania można tutaj wygenerować token (np. JWT)
            return Ok("User logged in successfully.");
        }
    
    }
}
