namespace PolishNotationTests;

[TestClass]
public class PolishNotationTests
{
    [TestMethod]
    public void BinaryOperatorTest()
    {
        String input = "5+8";
        String output = "5 8 +";
        RPNParser rpnParser = new RPNParser();

        Assert.Equals(rpnParser.Parse(input), output);
    }

    [TestMethod]
    public void UnaryMinusOperatorTest()
    {
        String input = "-5+8";
        String output = "5 - 8 +";
        RPNParser rpnParser = new RPNParser();

        Assert.Equals(rpnParser.Parse(input), output);
    }

    [TestMethod]
    public void BracketsTest()
    {
        String input = "(5+8)/-4*5";
        String output = "5 8 + / 4 5 * -";
        RPNParser rpnParser = new RPNParser();

        Assert.Equals(rpnParser.Parse(input), output);
    }

    [TestMethod]
    public void InvalidExpressionTest()
    {
        String input = "6++9";
        RPNParser rpnParser = new RPNParser();

        Assert.ThrowsException<ArgumentException>(rpnParser.Parse(input));
    }

    [TestMethod]
    public void NestedBracketsTest()
    {
        String input = "((5+8)-4)^2/5";
        String output = "5 8 + 4 - 2 ^ 5 /";
        RPNParser rpnParser = new RPNParser();

        Assert.Equals(rpnParser.Parse(input), output);
    }

    [TestMethod]
    public void UnaryPlusTest()
    {
        String input = "+5+8";
        String output = "5 + 8 +";
        RPNParser rpnParser = new RPNParser();

        Assert.Equals(rpnParser.Parse(input), output);
    }

    [TestMethod]
    public void EmptyExpressionTest()
    {
        String input = "";
        RPNParser rpnParser = new RPNParser();

        Assert.ThrowsException<ArgumentException>(rpnParser.Parse(input));
    }

    [TestMethod]
    public void UnpairedBracketsTest()
    {
        String input = "(5+8-4*2))";
        RPNParser rpnParser = new RPNParser();

        Assert.ThrowsException<ArgumentException>(rpnParser.Parse(input));
    }
}