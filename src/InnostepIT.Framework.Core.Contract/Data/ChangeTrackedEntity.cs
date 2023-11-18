namespace InnostepIT.Framework.Core.Contract.Data
{
    public abstract class ChangeTrackedEntity : Entity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime LastChangedAt { get; set; }
        public string? LastChangedBy { get; set; }
    }
}
