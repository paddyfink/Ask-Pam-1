namespace AskPam.Interfaces
{
    public interface ILimitedResultRequest
    {
        int MaxResultCount { get; set; }
    }
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        int SkipCount { get; set; }
    }
}
