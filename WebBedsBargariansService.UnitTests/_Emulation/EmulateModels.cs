using WebBedsBargainsService.Dto;
using WebBedsBargainsService.Model;

namespace WebBedsBargariansService.UnitTests._Emulation
{
    public static class EmulateModels
    {

        private static decimal GetPrice(AvailabilityRateType rateType, int nights)
        {
            return EmulateDtosAndModelsValues.RATE_PRICE * (rateType == AvailabilityRateType.PerNight ? nights : 1);
        }

        public static BSHotelAvailability GetPerfect(AvailabilityRateType rateType, int nights)
        {
            var av =  new BSHotelAvailability { Name = EmulateDtosAndModelsValues.HOTEL_NAME };
            av.AddRate(EmulateDtosAndModelsValues.RATE_NAME_1, GetPrice(rateType, nights));
            return av;
        }

        public static BSHotelAvailability GeValidWithBadBoardPrice(int nights)
        {
            var av = new BSHotelAvailability { Name = EmulateDtosAndModelsValues.HOTEL_NAME };
            av.AddRate(EmulateDtosAndModelsValues.RATE_NAME_2, GetPrice(AvailabilityRateType.Stay, nights));
            return av;
        }
    }
}
