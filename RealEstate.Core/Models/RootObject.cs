using RealEstate.Core.Models.BaseModels;


namespace RealEstate.Core.Models
{
    public class RootObject
    {
        public List<Estate> EstateList { get; set; }
        public List<Person> PersonList { get; set; }
        public List<Payment> PaymentList { get; set; }
    }
}
