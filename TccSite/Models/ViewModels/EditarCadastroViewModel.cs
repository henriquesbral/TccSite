using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TccSite.Domain.Entities;

namespace TccSite.Domain.ViewModels
{
    public class EditarCadastroViewModel
    {
        public int CodUsuario { get; set; }
        public int CodPessoaCadastro { get; set; }

        // PessoaCadastro
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public int CodCidade { get; set; }
        public DateTime DataNascimento { get; set; }

        // Usuario
        public string Email { get; set; }

        // Upload de imagem
        public IFormFile ImagemPerfil { get; set; }
        public string ImagemUrl { get; set; }

        // Dropdowns
        public List<Estado> Estados { get; set; } = new();
        public List<Cidade> Cidades { get; set; } = new();
        public int CodEstadoSelecionado { get; set; } // Para popular o select
    }
}
