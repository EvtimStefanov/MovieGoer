using Domain.Primitives;

namespace Domain.Entities
{
    public class Movie : Entity
    {
        public Movie(Guid id, string name, DateTime releasedOn)
            : base(id)
        {
            Name = name;
            ReleasedOn = releasedOn;
        }

        private Movie() { }

        public string? Name { get; private set; }
        public DateTime ReleasedOn { get; private set; }
    }
}
