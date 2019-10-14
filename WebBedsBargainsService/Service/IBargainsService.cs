using System;
using System.Collections.Generic;
using WebBedsBargainsService.Model;

namespace WebBedsBargainsService.Service
{
    public interface IBargainsService
    {
        List<BSHotelAvailability> GetAvailabilities(int destinationId, int nights);
    }
}
