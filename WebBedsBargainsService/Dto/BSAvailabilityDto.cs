using System.Collections.Generic;

namespace WebBedsBargainsService.Dto
{
    public enum AvailabilityRateType
    {
        PerNight = 1,
        Stay = 2
    }

    public class BSAvailabilityDto
    {
        public HotelDto hotel { get; set; }
        public List<RateDto> rates { get; set; }
    }
}
