namespace PersonRESTful.Services
{
    public interface ICSVService
    {
        public IEnumerable<T> ReadCSV<T>();
    }
}
