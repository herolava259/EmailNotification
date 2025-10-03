using Common.EventSourcing.Kernel.Core;
using Common.EventSourcing.Kernel.Generic;
using System.Reflection;
using System.Text.Json;

namespace Common.EventSourcing.Kernel.Storage;

public record StreamEventRecord
{

    public static readonly IReadOnlyDictionary<string, string> PropertyMapping = new Dictionary<string, string>
    {
        { "EventId", "EventId" },
        { "IdempotenceKey", "IdempotenceKey" },
        { "BranchVersionId", "BranchVersionId" },
        // TODO: Declare all field mapping
    };
    public string EventId { get; private set; } = String.Empty;

    public string IdempotenceKey { get; set; } = String.Empty;

    public DateTimeOffset TimeStamp { get; private set; } = DateTimeOffset.UtcNow;

    public StreamEventState EventState { get; private set; } = StreamEventState.Corrected;

    public string VersionChain { get; private set; } = String.Empty;

    public string EventType { get; private set; } = String.Empty;

    public string EventData { get; private set; } = String.Empty;

    public string AggregateRootId { get; set; }

    public DateTimeOffset OccuredOn { get; set; }

    private StreamEventRecord()
    {
        
    }

    public static StreamEventRecord FromStreamEvent<TDomainEvent>(StreamEvent<TDomainEvent> streamEvent)
        where TDomainEvent : IDomainEvent
    {
        return default;
    }

    //public static StreamEventRecord FromStreamEvent<TStreamEvent>(TStreamEvent streamEvent)
    //    where TStreamEvent: StreamEvent
    //{
    //    var streamEventType = typeof(TStreamEvent);

    //    var newRecord = new StreamEventRecord
    //    {
    //        EventId = streamEvent.Id,
    //        TimeStamp = streamEvent.TimeStamp,
    //        EventState = streamEvent.EventState,
    //        VersionChain = streamEvent.CurrentBranch.ToString(),
    //        EventType = streamEventType.AssemblyQualifiedName ?? streamEventType.FullName ?? "Unknown",
    //        AggregateRootId = streamEvent.AggregateRootId.ToString(),
    //        OccuredOn = streamEvent.OccuredOn

    //    };
    //    var jsonObject = new Dictionary<string, object>();

    //    var childFields = streamEventType.GetFields(BindingFlags.Public |
    //                                                BindingFlags.NonPublic|
    //                                                BindingFlags.Instance |
    //                                                BindingFlags.DeclaredOnly)
    //                                     ;

    //    var childProperties = streamEventType.GetProperties(BindingFlags.Public |
    //                                   BindingFlags.NonPublic |
    //                                   BindingFlags.Instance |
    //                                   BindingFlags.DeclaredOnly)
    //                                       .Where(p => !p.CanWrite);

    //    foreach(var field in childFields)
    //    {
    //        jsonObject[field.Name] = field.GetValue(streamEvent);

    //    }

    //    foreach (var prop in childFields)
    //        jsonObject[prop.Name] = prop.GetValue(streamEvent);
    //    newRecord.EventData = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
    //    return newRecord;
    //}

    //public StreamEvent ToStreamEvent()
    //{

    //    var currentEventType = Assembly.GetExecutingAssembly()
    //                                   .GetTypes()
    //                                   .Single(t => !t.IsAbstract 
    //                                                && t.IsSealed && t.IsSubclassOf(typeof(StreamEvent)) 
    //                                                && t.FullName == this.EventType);

    //    var recordPropeties = typeof(StreamEventRecord).GetProperties(BindingFlags.Public | BindingFlags.Instance
    //                                                                        | BindingFlags.DeclaredOnly
    //                                                                        | BindingFlags.NonPublic)
    //                                                   ;

    //    var parentProperties = typeof(StreamEvent).GetProperties(BindingFlags.Public | BindingFlags.Instance
    //                                                    | BindingFlags.DeclaredOnly
    //                                                    | BindingFlags.NonPublic)
    //                                              .Join(recordPropeties,
    //                                                    pp => pp.Name,
    //                                                    rp => rp.Name,
    //                                                    (pp, rp) => rp)
    //                                              .ToList();

    //    var currentEventProperties = currentEventType.GetProperties(BindingFlags.Public | BindingFlags.Instance
    //                                                                        | BindingFlags.DeclaredOnly
    //                                                                        | BindingFlags.NonPublic);



    //    object?[] values = parentProperties.Select(p => p.GetValue(this)).ToArray();


    //    var ctorInfo = currentEventType.GetConstructor(parentProperties.Select(ci => ci.GetType()).ToArray());

    //    var streamEvent = ctorInfo.Invoke(values);

    //    using var jsonDoc = JsonDocument.Parse(this.EventData);

    //    var jsonObject = jsonDoc.RootElement;

    //    foreach(var prop in jsonObject.EnumerateObject())
    //    {
            
    //    }

    //    return streamEvent as StreamEvent;

    //}
}
