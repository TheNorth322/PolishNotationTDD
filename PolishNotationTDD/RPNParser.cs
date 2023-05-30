using System.Text;
using System.Text.RegularExpressions;

namespace PolishNotationTDD;

public class RPNParser
{
    private Dictionary<char, int> OperationPriority = new()
    {
        { '(', 0 },
        { '+', 1 },
        { '-', 1 },
        { '*', 2 },
        { '/', 2 },
        { '^', 3 },
        { '~', 4 }
    };

    public string Parse(string expression)
    {
        CheckExpression(expression);
        string infixExpression = GetInfixExpression(expression);
        string postfixExpression = "";
        Stack<char> stack = new Stack<char>();

        for (int i = 0; i < infixExpression.Length; i++)
        {
            char c = infixExpression[i];
            
            if (Char.IsDigit(c))
            {
                postfixExpression += GetNumberString(infixExpression, ref i) + " ";
            }
            else if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                    postfixExpression += stack.Pop();
                stack.Pop();
            }
            else if (OperationPriority.ContainsKey(c))
            {
                char op = c;
                
                if (op == '-' && (i == 0 || (i > 1 && OperationPriority.ContainsKey(infixExpression[i - 1]))))
                    op = '~';

                while (stack.Count > 0 && OperationPriority[stack.Peek()] >= OperationPriority[op])
                    postfixExpression += stack.Pop() + " ";

                stack.Push(op);
            }
        }

        foreach (char op in stack)
            postfixExpression += op;

        return postfixExpression;
    }

    private string GetInfixExpression(string expression)
        => expression.Replace(" ", "").Replace(".", ",");

    private void CheckExpression(string expression)
    {
        if (!Regex.IsMatch(expression, @"^[\d\-+\*\/\s\(\)\^\.\,]+$"))
            throw new ArgumentException("Input string contains invalid characters");
        if (!CheckParenthesesBalance(expression))
            throw new ArgumentException("Input string has unpaired brackets");
    }

    private string GetNumberString(string expression, ref int position)
    {
        StringBuilder strNumber = new StringBuilder();

        for (; position < expression.Length; position++)
        {
            char number = expression[position];
            if (Char.IsDigit(number))
            {
                strNumber.Append(number);
            }
            else if (number == ',')
            {
                strNumber.Append(number);
            }
            else
            {
                position--;
                break;
            }
        }

        return strNumber.ToString();
    }
    
    private bool CheckParenthesesBalance(string expression)
    {
        int balance = 0;

        foreach (char c in expression)
        {
            if (c == '(')
                balance++;
            else if (c == ')')
                balance--;
            
            if (balance < 0)
                return false;
        }

        return balance == 0;
    }
}