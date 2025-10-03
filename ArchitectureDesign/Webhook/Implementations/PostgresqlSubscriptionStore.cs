using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Webhook.Core;
using Webhook.Interface;


namespace Webhook.Implementation;

internal sealed class PostgresqlSubscriptionStore(IConfiguration _configuration, ILogger<PostgresqlSubscriptionStore> _logger) : ISubscriptionStore
{
    public async Task Add(WebhookSubscription subscription)
    {
        _logger.LogInformation("Begin add web hook with type {EventType}", subscription.EventType);
        await using var connection = new NpgsqlConnection(_configuration["Webhook:Store:Posgresql:ConnectionStrings"]);


        var affectedRow = await connection.ExecuteAsync($@"
                                INSERT INTO WebhookSubscription VALUES (@)",
                                new
                                {
                                    EventType = subscription.EventType,
                                    CreatedOnUtc = subscription.CreatedOnUtc,
                                    WebhookUrl = subscription.WebhookUrl,
                                    ExpiredAt = subscription.ExpiredAtUtc
                                });

        _logger.LogInformation("End at add web hook with type {EventType}", subscription.EventType);
    }

    public async Task<IReadOnlyCollection<WebhookSubscription>> GetSubscriptions(string eventType)
    {
        _logger.LogInformation("Begin query web hook subscriptions with type {EventType}", eventType);
        await using var connection = new NpgsqlConnection(_configuration["Webhook:Store:Posgresql:ConnectionStrings"]);


        var reader = await connection.QueryMultipleAsync(
                                            $@"SELECT [Id], [EventType], [WebhookUrl], [CreatedOnUtc], [ExpiredAt] 
                                               FROM WebhookSubscription 
                                               WHERE [EventType] = @EventType",
                                            new {EventType = eventType });

        _logger.LogInformation("End at query webhook subscription with type {EventType}", eventType);

        return (await reader.ReadAsync<WebhookSubscription>()).ToList();
    }

    public async Task Remove(Guid subscriptionId)
    {
        _logger.LogInformation("Begin query web hook subscriptions with type {EventType}", subscriptionId);

        await using var connection = new NpgsqlConnection(_configuration["Webhook:Store:Posgresql:ConnectionStrings"]);


        var affectedRow = await connection.ExecuteAsync(
                                            $@"DELETE FROM WebhookSubscription WHERE [Id] = @Id",
                                            new { Id = subscriptionId });

        _logger.LogInformation("End at remove webhook subscription by Id = {Id}", subscriptionId);
    }
}
