using Livraria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Controllers
{
    [ApiController]
    [Route("livros")]
    public class LivrosController : ControllerBase
    {
        static List<Livro> livros = new List<Livro>();
        static int contador = 1;

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            foreach (Livro livro in livros)
            {
                if (livro.Id == id)
                {
                    return Ok(livro);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Criar(Livro livro)
        {
            if (livro.Titulo == "" || livro.Autor == "" || livro.Isbn == "")
            {
                return BadRequest("Preencha titulo, autor e isbn");
            }

            if (livro.Preco <= 0 || livro.Estoque < 0)
            {
                return BadRequest("Preco ou estoque invalido");
            }

            livro.Id = contador;
            contador++;
            livros.Add(livro);

            return Created("/livros/" + livro.Id, livro);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Livro livroNovo)
        {
            if (livroNovo.Titulo == "" || livroNovo.Autor == "" || livroNovo.Isbn == "")
            {
                return BadRequest("Preencha titulo, autor e isbn");
            }

            if (livroNovo.Preco <= 0 || livroNovo.Estoque < 0)
            {
                return BadRequest("Preco ou estoque invalido");
            }

            foreach (Livro livro in livros)
            {
                if (livro.Id == id)
                {
                    livro.Titulo = livroNovo.Titulo;
                    livro.Autor = livroNovo.Autor;
                    livro.Isbn = livroNovo.Isbn;
                    livro.Preco = livroNovo.Preco;
                    livro.Estoque = livroNovo.Estoque;

                    return NoContent();
                }
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            foreach (Livro livro in livros)
            {
                if (livro.Id == id)
                {
                    livros.Remove(livro);
                    return NoContent();
                }
            }

            return NotFound();
        }
    }
}
