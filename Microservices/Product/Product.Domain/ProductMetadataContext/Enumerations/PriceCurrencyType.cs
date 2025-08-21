using Common.ArchitechtureDesign.Generic.Utility.SmartEnum;


namespace Product.Domain.ProductMetadataContext.Enumerations;

public class PriceCurrencyType : Enumeration<PriceCurrencyType>
{
    public static readonly PriceCurrencyType USD = new USDCurrency();
    public static readonly PriceCurrencyType VND = new VNDCurrency();
    public static readonly PriceCurrencyType ETH = new ETHCurrency();

    private PriceCurrencyType(uint value, string name)
        : base(value, name)
    {
        
    }

    private sealed class VNDCurrency : PriceCurrencyType
    {
        public VNDCurrency(): base(100_000_002, "VND")
        {
            
        }
    }

    private sealed class USDCurrency : PriceCurrencyType
    {
        public USDCurrency(): base(100_000_001, "USD")
        {
            
        }
    }

    private sealed class ETHCurrency : PriceCurrencyType
    {
        public ETHCurrency(): base(100_000_003, "ETH")
        {
            
        }
    }
}
