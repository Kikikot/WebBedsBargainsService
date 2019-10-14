using System.Collections.Generic;
using WebBedsBargainsService.Dto;

namespace WebBedsBargariansService.UnitTests._Emulation
{
    public enum DtoTypes { Perfect = 1, BadName = 2, Huggly = 3, BadPrice = 4 }

    public static class EmulateDtos
    {
        private static HotelDto GetHotel(bool isValid = true)
        {
            return new HotelDto
            {
                propertyID = EmulateDtosAndModelsValues.HOTEL_ID,
                name = isValid ? EmulateDtosAndModelsValues.HOTEL_NAME : null,
                geoId = EmulateDtosAndModelsValues.HOTEL_GEOID,
                rating = EmulateDtosAndModelsValues.HOTEL_RATING
            };
        }

        public static RateDto GetRate(string boarTypeName, AvailabilityRateType rateType, bool isValid = true)
        {
            return new RateDto
            {
                rateType = rateType.ToString(),
                boardType = boarTypeName,
                value = isValid ? EmulateDtosAndModelsValues.RATE_PRICE : -1
            };
        }

        public static BSAvailabilityDto GetAvailabilityDtoByType(DtoTypes dtoType, AvailabilityRateType rateType = AvailabilityRateType.Stay)
        {
            switch (dtoType)
            {
                case DtoTypes.Perfect:
                    return GetPerfect(rateType);
                case DtoTypes.Huggly:
                    return GetWithBadBoardPrice();
                case DtoTypes.BadName:
                    return GetBadHotelName();
                case DtoTypes.BadPrice:
                    return GetWithBadBoardPrice(false);
            }

            return null;
        }

        private static BSAvailabilityDto GetPerfect(AvailabilityRateType rateType)
        {
            return new BSAvailabilityDto
            {
                hotel = GetHotel(),
                rates = new List<RateDto>() { GetRate(EmulateDtosAndModelsValues.RATE_NAME_1, rateType) }
            };
        }

        private static BSAvailabilityDto GetBadHotelName()
        {
            return new BSAvailabilityDto
            {
                hotel = GetHotel(false),
                rates = new List<RateDto>() { GetRate(EmulateDtosAndModelsValues.RATE_NAME_1, AvailabilityRateType.Stay) }
            };
        }

        private static BSAvailabilityDto GetWithBadBoardPrice(bool isValid = true)
        {
            var dto = new BSAvailabilityDto
            {
                hotel = GetHotel(),
                rates = new List<RateDto>() { GetRate(EmulateDtosAndModelsValues.RATE_NAME_1, AvailabilityRateType.Stay, false) }
            };

            if (isValid)
                dto.rates.Add(GetRate(EmulateDtosAndModelsValues.RATE_NAME_2, AvailabilityRateType.Stay));

            return dto;
        }
    }
}
