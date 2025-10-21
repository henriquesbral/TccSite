using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TccSite.Application.Helpers
{
    public static class SenhaHelper
    {
        /// <summary>
        /// Gera um salt aleatório em Base64.
        /// </summary>
        public static string GerarSalt(int tamanho = 16)
        {
            var bytesSalt = new byte[tamanho];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(bytesSalt);

            return Convert.ToBase64String(bytesSalt);
        }

        /// <summary>
        /// Gera um hash SHA256 baseado na senha + salt.
        /// </summary>
        public static string GerarHash(string senha, string salt)
        {
            using var sha256 = SHA256.Create();
            var combinado = senha + salt;
            var bytes = Encoding.UTF8.GetBytes(combinado);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifica se a senha informada confere com o hash armazenado.
        /// </summary>
        public static bool VerificarSenha(string senhaDigitada, string salt, string hash)
        {
            var novoHash = GerarHash(senhaDigitada, salt);
            return novoHash == hash;
        }

        /// <summary>
        /// Versão assíncrona — para uso em métodos async/await (ex: ao consultar banco).
        /// </summary>
        public static Task<(string Salt, string Hash)> GerarSenhaAsync(string senha)
        {
            return Task.Run(() =>
            {
                var salt = GerarSalt();
                var hash = GerarHash(senha, salt);
                return (salt, hash);
            });
        }

        /// <summary>
        /// Versão assíncrona da verificação de senha.
        /// </summary>
        public static Task<bool> VerificarSenhaAsync(string senhaDigitada, string salt, string hash)
        {
            return Task.Run(() => VerificarSenha(senhaDigitada, salt, hash));
        }
    }
}
