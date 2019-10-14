using System;
using WebBedsBargainsService.Base;
using WebBedsBargainsService.Dto;
using WebBedsBargainsService.Model;

namespace WebBedsBargainsService.Translators
{
    public class BSAvailabilityTranslator : IBSAvailabilityTranslator
    {
        private readonly IBSAvailabilityDtoValidator _validator;

        public BSAvailabilityTranslator(
            IBSAvailabilityDtoValidator validator
        ) {
            _validator = validator;
        }

        public BSHotelAvailability Translate(BSAvailabilityDto dto, int nights)
        {
            if (!_validator.IsAcceptedResponse(dto))
                return null;

            var bsHotel = new BSHotelAvailability {
                Name = dto.hotel.name
            };

            foreach (var rate in dto.rates)
                if (_validator.IsCorrectRate(rate))
                    bsHotel.AddRate(
                        boardType: rate.boardType,
                        finalPrice: GetRateFinalPrice(rate, nights)
                    );

            return bsHotel;
        }

        private decimal GetRateFinalPrice(RateDto rate, int nights)
        {
            if (!Enum.TryParse<AvailabilityRateType>(rate.rateType, out var rateType))
                return 0;

            switch (rateType)
            {
                case AvailabilityRateType.PerNight:
                    return rate.value.Value * nights;

                case AvailabilityRateType.Stay:
                    return rate.value.Value;
            }
            return 0;
        }
    }
}
