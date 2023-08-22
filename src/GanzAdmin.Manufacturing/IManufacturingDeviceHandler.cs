using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.Manufacturing
{
    public interface IManufacturingDeviceHandler
    {
        object LockObject { get; }
        string Id { get; }
        List<Item> QueuedItem { get; }
        Item CurrentItem { get; }
        State CurrentState { get; }
        
    }
}
