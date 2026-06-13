
using StructTrainingV2;

Point p;
p.X = 10;
p.Y = 20;

Console.WriteLine($"X: {p.X}");
Console.WriteLine($"Y: {p.Y}");

Size screen;
screen.Width = 1920;
screen.Height = 1080;

double Calc()
{
    double result =  screen.Width* screen.Height;
    Console.WriteLine($"result: {result}");
    return result;
}
Calc();