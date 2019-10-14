using System.Collections.Generic;

namespace WebBedsBargainsService.Model
{
    public class BSHotelAvailability
    {
        public string Name { get; set; }

        private List<BSRate> _rates = new List<BSRate>();
        public IEnumerable<BSRate> Rates { get { return _rates; } }

        public void AddRate(string boardType, decimal finalPrice)
        {
            _rates.Add(new BSRate { BoardType = boardType, FinalPrice = finalPrice });
        }
    }
}
