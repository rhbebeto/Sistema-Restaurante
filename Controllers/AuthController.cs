using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SistemaRestaurante.Data;
using SistemaRestaurante.Models;

namespace SistemaRestaurante.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context) => _context = context;

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            // Validação: se algum campo estiver vazio, retorna erro
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ModelState.AddModelError("", "Preencha os campos de email e senha.");
                return View();
            }

            var user = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View();
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Role, user.Setor)   // Por exemplo: "Cozinha", "Copa" ou "Admin"
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Pedido");
        }

        // GET: /Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();
    }
}
