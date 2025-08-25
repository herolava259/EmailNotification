
namespace Common.ArchitechtureDesign.Generic.EventSourcing.RetroactiveEvent;

public enum RetroactiveType: ushort 
{
    None = 0,
    OutOfOrder = 1,
    Rejected = 2,
    Incorrected = 3
}
