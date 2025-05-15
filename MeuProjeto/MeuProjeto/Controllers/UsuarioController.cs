using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Repoositorio;


namespace MeuProjeto.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly LoginRepositorio _loginRepositorio;

        public UsuarioController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _loginRepositorio.ObterUsuario(email);
             if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Usuario");
            }
        
            ModelState.AddModelError("", "Email ou senha inválidos.");
  
            return View();
        }
    }
}
