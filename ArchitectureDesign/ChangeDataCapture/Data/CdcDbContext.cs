using ChangeDataCapture.Mechanisms.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Data;

public abstract class CdcDbContext: DbContext
{

}

internal sealed class CDCSaveChangeInterceptor: SaveChangesInterceptor
{
    private readonly IDataChangePublisher _publisher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CDCSaveChangeInterceptor(IDataChangePublisher publisher, IHttpContextAccessor httpContextAccessor)
    {
        this._publisher = publisher;
        this._httpContextAccessor = httpContextAccessor;
    }
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {

        if(eventData.Context is not null)
        {
            await PublishDataChangeAsync(eventData.Context);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDataChangeAsync(DbContext context)
    {
        var correlationId = string.Empty;

        if(_httpContextAccessor.HttpContext is not null)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            correlationId = httpContext.Request.Headers?["Correlation-Id"].ToString();
        }


    }
}
