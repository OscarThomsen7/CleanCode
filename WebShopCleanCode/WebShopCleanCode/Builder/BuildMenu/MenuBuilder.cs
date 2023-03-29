namespace WebShopCleanCode.Builder.BuildMenu;

public class MenuBuilder
{
    string _name ="";
    int _amountOfOptions;
    string _option1 = "";
    string _option2 = "";
    string _option3 = "";
    string _option4 = "";
    string _info = "";

    
    public MenuBuilder SetName(string input)
    {
        _name = input;
        return this;
    }
    public MenuBuilder SetAmountofOptions(int input)
    {
        _amountOfOptions = input;
        return this;
    }

    public MenuBuilder SetOption1(string input)
    {
        _option1 = input;
        return this;
    }
    public MenuBuilder SetOption2(string input)
    {
        _option2 = input;
        return this;
    }
    public MenuBuilder SetOption3(string input)
    {
        _option3 = input;
        return this;
    }
    public MenuBuilder SetOption4(string input)
    {
        _option4 = input;
        return this;
    }
    public MenuBuilder SetInfo(string input)
    {
        _info = input;
        return this;
    }

    public Menu Build()
    {
        return new Menu(_name, _amountOfOptions, _option1, _option2, _option3, _option4, _info);
    }
}