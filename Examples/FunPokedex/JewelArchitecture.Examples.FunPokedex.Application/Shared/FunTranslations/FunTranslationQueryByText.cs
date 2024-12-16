using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Examples.FunPokedex.Domain.Shared;

namespace JewelArchitecture.Examples.FunPokedex.Application.Shared.FunTranslations;

public record FunTranslationQueryByText(string Text, FunTranslation TranslationType, string CacheKey): IQuery;
