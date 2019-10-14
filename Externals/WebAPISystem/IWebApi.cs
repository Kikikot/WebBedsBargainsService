namespace Externals.WebApiSystem
{
    public interface IWebApi
    {
        T Get<T>(string url, double? timeOutMilliSeconds = null);
    }
}
