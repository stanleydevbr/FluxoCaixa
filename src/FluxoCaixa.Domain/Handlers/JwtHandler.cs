using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Interfaces.Handlers;
using FluxoCaixa.Domain.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FluxoCaixa.Domain.Handlers
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IConfiguration _configuration;

        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UsuarioLogadoViewModel GerarToken(Usuario usuario)
        {
            var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var _issuer = _configuration["Jwt:Issuer"];
            var _audience = _configuration["Jwt:Audience"];
            var _minutos = int.Parse(_configuration["Jwt:ExpirarEmMinutos"]);

            var _claims = new List<Claim>();
            _claims.Add(new Claim(nameof(usuario.Nome), usuario.Nome));
            _claims.Add(new Claim(nameof(usuario.Email), usuario.Email));
            var expirarEm = DateTime.Now.AddMinutes(_minutos);

            var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: _claims,
                expires: expirarEm,
                signingCredentials: signinCredentials
                );

            var usuarioLogado = new UsuarioLogadoViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                ExpiraEm = expirarEm
            };

            return usuarioLogado;
        }
    }
}
