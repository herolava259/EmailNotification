using SharedKernel.GeneralUser.Domain.Constants;
using SharedKernel.GeneralUser.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.GeneralUser.Domain;


public abstract class AddressEntity
{
    public CountryCode CountryCode { get; protected init; } = AddressConstant.CountryCodeDefault;

    public string State { get; protected set; } = AddressConstant.StateDefault;

    public string City { get; protected set; } = AddressConstant.CityDefault;

    public string PinCode { get; protected init; } = AddressConstant.PinCodeDefault;


}
