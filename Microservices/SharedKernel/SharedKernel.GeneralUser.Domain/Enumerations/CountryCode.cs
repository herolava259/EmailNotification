using Common.ArchitechtureDesign.Generic.Utility.SmartEnum;

namespace SharedKernel.GeneralUser.Domain.Enumerations;

public partial class CountryCode : Enumeration<CountryCode>
{
    private CountryCode(uint value, string name) : base(value, name)
    {
    }

    private sealed class Vietnam : CountryCode
    {
        public Vietnam() : base(84, nameof(Vietnam))
        {
        }
    }

    private sealed class UnitedKingdom : CountryCode
    {
        public UnitedKingdom() : base(44, nameof(UnitedKingdom))
        {
        }
    }

    private sealed class Italy : CountryCode
    {
        public Italy() : base(39, nameof(Italy))
        {
        }
    }

    private sealed class Japan : CountryCode
    {
        public Japan() : base(81, nameof(Japan))
        {
        }
    }


    private sealed class Singapore : CountryCode
    {
        public Singapore() : base(65, nameof(Singapore))
        {
        }
    }

    private sealed class Neverland : CountryCode
    {
        public Neverland() : base(99999, nameof(Neverland))
        {
        }
    }
}


public partial class CountryCode
{
    public static readonly CountryCode VietnamCode = new Vietnam();

    public static readonly CountryCode UnitedKingdomCode = new UnitedKingdom();

    public static readonly CountryCode ItalyCode = new Italy();

    public static readonly CountryCode JapanCode = new Japan();

    public static readonly CountryCode SingaporeCode = new Singapore();

    public static readonly CountryCode NeverlandCode = new Neverland();
}
