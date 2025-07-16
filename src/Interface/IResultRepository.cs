namespace src.Interface
{
    public interface IResultRepository
    {
        Task<string> CreateResult();
        Task<string> ViewResult();
        Task<string> ViewAllResult();
    }
}
