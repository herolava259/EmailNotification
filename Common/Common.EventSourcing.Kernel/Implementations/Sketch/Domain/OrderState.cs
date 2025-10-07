namespace Common.EventSourcing.Kernel.Implementations.Sketch.Domain;

public enum OrderState: ushort
{
    Pending = 0, 
    Confirmation = 2,
    Cancel = 3,
    Finished = 4,
}
