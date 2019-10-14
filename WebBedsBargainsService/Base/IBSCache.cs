using Externals.CacheSystem;
using System.Collections.Generic;
using WebBedsBargainsService.Model;

namespace WebBedsBargainsService.Base
{
    public interface IBSCache
    {
        List<BSHotelAvailability> GetBSHotelAvailabilities(int destinationId, int nights);

        void SetBSHotelAvailabilities(int destinationId, int nights, List<BSHotelAvailability> result);
    }
}
