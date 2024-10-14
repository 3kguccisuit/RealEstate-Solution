namespace RealEstateBLL.Interfaces
{

    public interface IPayment
{
    string ID { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}
}