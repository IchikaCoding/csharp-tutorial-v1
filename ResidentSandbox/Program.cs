// 名前空間は、using しないとクラスとか共有できないわよ！
using ResidentSandbox;

var Alpaca = new Resident()
{
    Name = "アルパカどん🦙",
    AnimalType = "Animal",
    Job = "窓から人類観察",
    ResidentSince = new DateTime(1600, 9, 26)
};

var SatsumaimoPan = new Resident()
{
    Name = "さつまいもパンまん🍠",
    AnimalType = "Human",
    Job = "パン屋さん",
    ResidentSince = DateTime.Now
};

Console.WriteLine(Alpaca.Name);
Console.WriteLine(SatsumaimoPan.Name);