using WebBedsBargainsService.Dto;

namespace WebBedsBargainsService.Base
{
    public interface IBSAvailabilityDtoValidator
    {
        bool IsAcceptedResponse(BSAvailabilityDto dto);

        bool IsCorrectRate(RateDto rateDto);
    }
}
