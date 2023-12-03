/// <summary>
/// Created by Alexander Carvalho, interface for all map point related operations, for functionality documentation refer to the MapPointOperations class
/// </summary>
using PARKIT_enterprise_final.Models.Operations;

namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IMapPointProvider
    {
        List<MapPoint> GetMapPoints();
    }
}
