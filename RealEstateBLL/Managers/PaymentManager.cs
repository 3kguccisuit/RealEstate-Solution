using DTO.Models.BaseModels;

namespace RealEstateDLL.Managers
{
    public class PaymentManager : DictionaryManager<string, Payment>
    {
        // Inherits functionality from DictionaryManager<string, Payment>
        public List<Payment> GetPaymentsByType(string type)
        {
            return GetAll()
                .Where(payment => payment.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
