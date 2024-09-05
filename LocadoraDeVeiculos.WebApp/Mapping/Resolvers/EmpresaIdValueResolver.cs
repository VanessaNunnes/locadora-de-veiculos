using AutoMapper;
using System.Security.Authentication;
using LocadoraDeVeiculos.Aplicacao.Servicos;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class EmpresaIdValueResolver : IValueResolver<object, object, int>
{
    private readonly AutenticacaoService servicoAutenticacao;
    private readonly IHttpContextAccessor httpContextAccessor;

    public EmpresaIdValueResolver(AutenticacaoService servicoAutenticacao, IHttpContextAccessor httpContextAccessor)
    {
        this.servicoAutenticacao = servicoAutenticacao;
        this.httpContextAccessor = httpContextAccessor;
    }

    public int Resolve(object source, object destination, int destMember, ResolutionContext context)
    {
        var usuarioClaim = httpContextAccessor.HttpContext?.User;

        var empresaId = servicoAutenticacao.ObterIdEmpresaAsync(usuarioClaim!).Result;

        if (empresaId is null)
            throw new AuthenticationException("Não foi possível obter o ID da empresa requisitada!");

        return empresaId.Value;
    }
}