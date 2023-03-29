namespace WebShopCleanCode.Builder.BuildMenu;

public class Menu
{
    public string Name { get; set; }
    public int AmountOfOptions { get; set; }
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string Option4 { get; set; }
    public string Info { get; set; }

    public Menu()
    {
        
    }
    public Menu(string name, int amountOfOptions, string option1, string option2, string option3, string option4, string info)
    {
        Name = name;
        AmountOfOptions = amountOfOptions;
        Option1 = option1;
        Option2 = option2;
        Option3 = option3;
        Option4 = option4;
        Info = info;
    }

    public void DisplayMenu(int currentChoice)
    {
        if (!Name.Equals("Purchase menu"))
        {
            Console.WriteLine(Info);
            Console.WriteLine("1: " + Option1);
            Console.WriteLine("2: " + Option2);
            Console.WriteLine("3: " + Option3);
            if (AmountOfOptions > 3)
            {
                Console.WriteLine("4: " + Option4);
            }
        }
        ChoiceRepresentor(currentChoice);
    }
    
    public void ChoiceRepresentor(int currentChoice)
    {
        for (int i = 0; i < AmountOfOptions; i++)
        {
            Console.Write(i + 1 + "\t");
        }
        Console.WriteLine();
        for (int i = 1; i < currentChoice; i++)
        {
            Console.Write("\t");
        }
        Console.WriteLine("|");
    }
}