using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Application.Helpers;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Application.Services
{
    public class UsuarioSenhaService : IUsuarioSenhaService
    {
        private readonly IUsuarioSenhaRepository _repo;

        public UsuarioSenhaService(IUsuarioSenhaRepository repo)
        {
            _repo = repo;
        }

        public bool ValidarSenha(int codUsuario, string senhaDigitada)
        {
            var usuarioSenha = _repo.ObterPorUsuario(codUsuario);
            if (usuarioSenha == null)
                return false;

            return SenhaHelper.VerificarSenha(senhaDigitada, usuarioSenha.SaltSenhaAtual, usuarioSenha.HashSenhaAtual);
        }

        public void CriarSenhaInicial(int codUsuario, string senhaInicial)
        {
            var salt = SenhaHelper.GerarSalt();
            var hash = SenhaHelper.GerarHash(senhaInicial, salt);

            var novaSenha = new UsuarioSenha
            {
                CodUsuario = codUsuario,
                SaltSenhaAtual = salt,
                HashSenhaAtual = hash
            };

            _repo.Adicionar(novaSenha);
        }

        public void AtualizarSenha(int codUsuario, string novaSenha)
        {
            var usuarioSenha = _repo.ObterPorUsuario(codUsuario);
            if (usuarioSenha == null)
                throw new Exception("Usuário não encontrado para atualização de senha.");

            // Rotaciona senhas anteriores
            usuarioSenha.SaltSenha2 = usuarioSenha.SaltSenha1;
            usuarioSenha.HashSenha2 = usuarioSenha.HashSenha1;

            usuarioSenha.SaltSenha1 = usuarioSenha.SaltSenhaAtual;
            usuarioSenha.HashSenha1 = usuarioSenha.HashSenhaAtual;

            // Nova senha
            var novoSalt = SenhaHelper.GerarSalt();
            var novoHash = SenhaHelper.GerarHash(novaSenha, novoSalt);

            usuarioSenha.SaltSenhaAtual = novoSalt;
            usuarioSenha.HashSenhaAtual = novoHash;
            usuarioSenha.DataUltimaAtualizacao = DateTime.Now;

            _repo.Atualizar(usuarioSenha);
        }
    }
}
