using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class FotoValueResolver : IValueResolver<FormularioAutomovelViewModel, Automovel, byte[]>
{
    public FotoValueResolver() { }

    public byte[] Resolve(
        FormularioAutomovelViewModel source,
        Automovel destination,
        byte[] destMember,
        ResolutionContext context
    )
    {
        using (var memoryStream = new MemoryStream())
        {
            source.Foto.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}