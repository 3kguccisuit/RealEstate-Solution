using RealEstateBLL.Models.BaseModels;

namespace RealEstateDAL.Files
{
    public class RootObject
    {
        public List<Estate> EstateList { get; set; }
        public List<Person> PersonList { get; set; }
        public List<Payment> PaymentList { get; set; }
    }
}
