namespace Main.Api.Models
{
    public class BasePerson
    {
        public virtual Guid Id { get; set; }
        public virtual bool ToDelete { get; protected set; } = false;
        public virtual DateTime CreationDate { get; protected set; }

        public BasePerson()
        {
            CreationDate = DateTime.Now;
        }
    }
}