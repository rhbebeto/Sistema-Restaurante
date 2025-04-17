using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRestaurante.Data;
using SistemaRestaurante.Models;

namespace SistemaRestaurante.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Pedido/Create
        public IActionResult Create()
        {
            // Carrega lista de produtos para o dropdown
            ViewBag.Produtos = _context.Produtos.ToList();
            return View();
        }

        // POST: /Pedido/Create
        [HttpPost]
        public IActionResult Create(int produtoId, int quantidade, string nomeSolicitante, int mesa)
        {
            // Monta o pedido sem validações extras
            var pedido = new PedidoModel
            {
                NomeSolicitante = nomeSolicitante,
                Mesa = mesa,
                Status = "Em preparo",
                DataHora = DateTime.Now
            };
            pedido.Itens.Add(new PedidoItemModel
            {
                ProdutoId = produtoId,
                Quantidade = quantidade

            });

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            // Redireciona para o Index
            return RedirectToAction(nameof(Index));
        }

        // GET: /Pedido/Index
        public IActionResult Index()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(pi => pi.Produto)
                .ToList();
            return View(pedidos);
        }

        // … construtor e Index/Create já existentes …

        // Apenas quem for do setor “Cozinha” vê pratos
        [Authorize(Roles = "Cozinha")]
        public IActionResult Cozinha()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Itens.Any(i => i.Produto.Tipo == "Prato"))
                .ToList();
            return View(pedidos);
        }

        // Apenas quem for do setor “Copa” vê bebidas
        [Authorize(Roles = "Copa")]
        public IActionResult Copa()
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Itens.Any(i => i.Produto.Tipo == "Bebida"))
                .ToList();
            return View(pedidos);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AtualizarStatus(int pedidoId, string novoStatus)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == pedidoId);
            if (pedido != null)
            {
                pedido.Status = novoStatus;
                _context.SaveChanges();
            }

            // Redireciona para a view conforme o setor do usuário
            if (User.IsInRole("Cozinha"))
                return RedirectToAction("Cozinha");

            if (User.IsInRole("Copa"))
                return RedirectToAction("Copa");

            // Caso seja admin ou outro perfil, volta para o Index
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var pedido = _context.Pedidos
                            .Include(p => p.Itens) // caso tenha relacionamentos, inclua-os para evitar problemas de dependência
                            .FirstOrDefault(p => p.Id == id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult History()
        {
            var historico = _context.Pedidos
                .Include(p => p.Itens)                             // carrega os itens relacionados :contentReference[oaicite:0]{index=0}
                .ThenInclude(pi => pi.Produto)                     // e para cada item carrega o produto :contentReference[oaicite:1]{index=1}
                .Where(p => p.Status == "Entregue")                // filtra somente os entregues :contentReference[oaicite:2]{index=2}
                .ToList();

            return View(historico);
        }

    }


}