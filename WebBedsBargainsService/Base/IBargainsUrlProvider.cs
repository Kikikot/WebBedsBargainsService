namespace WebBedsBargainsService.Base
{
    public interface IBargainsUrlProvider
    {
        string GetAvailabilitiesUrl(int destination, int nights);
    }
}
