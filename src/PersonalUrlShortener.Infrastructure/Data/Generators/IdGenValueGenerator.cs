using IdGen;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace PersonalUrlShortener.Infrastructure.Data.Generators;

public class IdGenValueGenerator : ValueGenerator<long>
{
    private readonly IIdGenerator<long> _idGenerator;

    public IdGenValueGenerator(IIdGenerator<long> idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public override long Next(EntityEntry entry) => _idGenerator.CreateId();

    public override bool GeneratesTemporaryValues => false;
}