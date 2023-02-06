namespace TwitterReader;

public interface IReporter
{
    Task Run(CancellationToken ctx);
}