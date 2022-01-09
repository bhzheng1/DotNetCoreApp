namespace DependencyInjection.MultipleRegistration
{
    public class MyClassWithValue : IHasValue
    {
        public object Value { get; set; }

        public MyClassWithValue()
        {
            Value = 42;
        }
    }
}
