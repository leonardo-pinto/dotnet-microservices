namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string name, object key) : base($"Entity {name} = {key} not found")
        {

        }
    }
}
