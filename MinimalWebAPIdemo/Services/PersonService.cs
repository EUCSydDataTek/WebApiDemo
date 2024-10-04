using MinimalWebAPIdemo.Models;

namespace MinimalWebAPIdemo.Services;

public interface IPersonService
{
    Person GetPerson();
}

public class PersonService : IPersonService
{
    public Person GetPerson()
    {
        return new Person("John Doe", 30);
    }
}
