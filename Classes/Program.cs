namespace LinkedList;

class Program
{
    public static void Main(string[] args)
    {
        LinkedList<Person> people = new LinkedList<Person>();
        people.Add(new Person("Ben", 31));
        people.Add(new Person("Gemma", 33));
        people.Add(new Person("Daisy", 1));

        foreach (Person p in people) {
            Console.WriteLine(p);
        }

    }
}