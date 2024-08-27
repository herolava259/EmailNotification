

namespace NotificationSystem.Models;

public record Event<T>(T? Data, EventMetadata? Metadata = default);
