using ContaCorrentLibrary.Models.Enums;

namespace ContaCorrentLibrary.Models
{
    public class MovementModel
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TypeEnum Type  { get; set; }
        public DateTime DateTime { get; set; }
    }
}
