using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Common.Behaviours
{
    public class CachingBehavior<TRequest, TResponse>(HybridCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly HybridCache _cache = cache;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(request is not ICachedQuery cachedRequest)
            {
                return await next(cancellationToken);
            }
            _logger.LogInformation("Checking cache for {RequestName}", typeof(TRequest).Name);

            var result = await _cache.GetOrCreateAsync(key: cachedRequest.CacheKey, factory: async ct =>
            {
                var innerResult = await next(ct);
                if (innerResult is IResult r && r.IsSuccess)
                {
                    return innerResult;
                }

                return default!;
            }, options: new HybridCacheEntryOptions
            {
                Expiration = cachedRequest.Expiration,
            },
            tags: cachedRequest.Tags,
            cancellationToken:cancellationToken
            

            );

            return result;
            
                
        }
    }
}
