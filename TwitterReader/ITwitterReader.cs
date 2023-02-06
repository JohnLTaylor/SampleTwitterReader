namespace TwitterReader;

public interface ITwitterReader
{
    Task Run(CancellationToken ctx);
}