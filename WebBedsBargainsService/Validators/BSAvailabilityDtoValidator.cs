using System;
using System.Collections.Generic;
using WebBedsBargainsService.Base;
using WebBedsBargainsService.Dto;

namespace WebBedsBargainsService.Validators
{
    public class BSAvailabilityDtoValidator : IBSAvailabilityDtoValidator
    {
        public bool IsAcceptedResponse(BSAvailabilityDto dto)
        {
            return dto != null

                && IsCorrectHotel(dto.hotel)

                && SomeRateOk(dto.rates);
        }

        private bool SomeRateOk(List<RateDto> rates)
        {
            bool someRateFound = false;

            int length = rates != null ? rates.Count : 0;

            for (int i = 0; (!someRateFound && i < length); i++)
                someRateFound = IsCorrectRate(rates[i]);

            return someRateFound;
        }

        public bool IsCorrectRate(RateDto rateDto)
        {
            return rateDto != null

                && !IsBlankBoardType(rateDto.boardType)

                && IsCorrectRateType(rateDto.rateType)

                && IsCorrectPrice(rateDto.value);
        }

        private bool IsCorrectPrice(decimal? value)
        {
            return value.HasValue && value >= 0;
        }

        private bool IsCorrectRateType(string rateType)
        {
            return Enum.TryParse(rateType, out AvailabilityRateType type);
        }

        private bool IsBlankBoardType(string boardType)
        {
            return string.IsNullOrEmpty(boardType);
        }

        private bool IsCorrectHotel(HotelDto hotel)
        {
            return hotel != null

                && IsCorrectPropertyId(hotel.propertyID)

                && !IsBlankHotelName(hotel.name);
        }

        private bool IsCorrectPropertyId(int? propertyID)
        {
            return true;
            // return propertyID.HasValue; It's not especified to be a problem.
        }

        private bool IsBlankHotelName(string hotelName)
        {
            return string.IsNullOrEmpty(hotelName);
        }
    }
}
