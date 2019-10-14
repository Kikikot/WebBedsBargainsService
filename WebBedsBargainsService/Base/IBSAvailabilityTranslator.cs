using System.Collections.Generic;
using WebBedsBargainsService.Dto;
using WebBedsBargainsService.Model;

namespace WebBedsBargainsService.Base
{
    public interface IBSAvailabilityTranslator
    {
        BSHotelAvailability Translate(BSAvailabilityDto dto, int nights);
    }
}
