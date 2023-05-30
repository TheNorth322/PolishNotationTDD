using PolishNotationTDD;

public static class Program
{
    public static void Main()
    {
        String input = "(5+8-4*2))";
        RPNParser rpnParser = new RPNParser();
        rpnParser.Parse(input);
    }
}