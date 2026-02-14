using ChangeDataCapture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.Mechanisms.Contracts;

internal interface IDataChangePublisher
{
    Task PublishAsync(DeltaChangeRecord changeRecord);

    Task PublishBatchAsync(IEnumerable<DeltaChangeRecord> changeRecords);


}
