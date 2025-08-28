

namespace Market.Application.SeviceInterfacies
{
    public interface IGeneratePdfService<T>
    {
        public byte[] GenerateReport(IEnumerable<T> data);
    }
}
