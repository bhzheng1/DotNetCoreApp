//Service Lifestyle/Lifetimes
//We can configure services with different types of lifestyles like following.

//Transient
//This lifestyle services are created each time they are requested.

//Scoped
//Scoped lifestyle services are created once per request.

//Singleton
//A singleton service is created once at first time it is requested and this instance of service is used by every sub-requests.

namespace DependencyInjection.Basics
{
    public class SingletonDateOperation
    {
        public SingletonDateOperation()
        {
            Console.WriteLine("Singleton service is created!");
        }
    }
}
