// See https://aka.ms/new-console-template for more information
public class MyService
{
    private readonly IMyServiceDependency _dependency;

    public MyService(IMyServiceDependency dependency)
    {
        _dependency = dependency;
    }

    public void DoIt()
    {
        _dependency.DoIt();
    }
}
