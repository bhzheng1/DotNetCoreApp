namespace DependencyInjection.MultipleRegistration
{

    public class MyClassWithValue2 : IHasValue
    {
        public object Value { get; set; }

        public MyClassWithValue2()
        {
            Value = 43;
        }
    }
}
