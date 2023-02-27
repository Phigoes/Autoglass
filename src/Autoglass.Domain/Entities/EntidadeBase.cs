namespace Autoglass.Domain.Entities
{
    public abstract class EntidadeBase
    {
        protected EntidadeBase() { }

        public int Id { get; private set; }
    }
}
