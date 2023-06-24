using ContaCorrentLibrary.Models.Enums;

namespace ContaCorrentLibrary.Models
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }
        public TypeEnum Type { get; set; }
    }
}
