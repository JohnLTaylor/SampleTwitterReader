namespace TwitterAPI.Interfaces;

public interface ITwitterApiConnector
{
    Task<Stream> Open();
}