
Console.WriteLine("Hello!");


double num1;
while (true)
{
    Console.Write("Input the first number or c to cancel: ");
    string r = Console.ReadLine();
    if (r == "c")
    {
        return;
    }
    else if (!double.TryParse(r, out num1))
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
    else
    {
        break;
    }
}
double num2;
while (true)
{
    Console.Write("Input the second number or c to cancel: ");
    string r = Console.ReadLine();
    if (r == "c")
    {
        return;
    }
    else if (!double.TryParse(r, out num2))
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
    else
    {
        break;
    }
}

Console.WriteLine("What do you want to do with those numbers?");
Console.WriteLine("[A]dd");
Console.WriteLine("[S]ubtract");
Console.WriteLine("[M]ultiply");
Console.Write("Choose an operation: ");
string operation = Console.ReadLine();

double result;

switch (operation)
{
    case "A":
        result = num1 + num2;
        break;
    case "S":
        result = num1 - num2;
        break;
    case "M":
        result = num1 * num2;
        break;
    default:
        Console.WriteLine("Invalid operation");
        return;
}

Console.WriteLine($"result: {result}");

Console.WriteLine("Press any key to close.");
Console.ReadKey();