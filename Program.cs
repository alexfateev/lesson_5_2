int chickenCount = 3; //Начальное кол-во куриц
int eggSupply = 0; //Запас яиц
int day = 0;

ChickenState[] chickenStatus = new ChickenState[chickenCount];
int[] chickenEggCount = new int[chickenCount];

for (int i = 0; i < chickenCount; i++)
    chickenStatus[i] = ChickenState.Alive;

NewDay();

while (true)
{
    Console.Clear();
    var res = ShowMenu();
    if (res == -1) break;
}

int ShowMenu()
{
    while (true)
    {

        ShowSatus();

        Console.WriteLine("Выберите одно из действий:\n1. Покормить куриц\n2. Собрать яйца\n3. Завершить ход (ничего не делать)\n4. Выйти\n");
        var res = int.TryParse(Console.ReadLine(), out int number);
        if (res)
        {
            if (number == 1) FeedChickens();
            else if (number == 2) CollectEggs();
            else if (number == 3)
            {
                NewDay();
                return 3;
            }
            else if (number == 4) return -1;
        }
        Console.Clear();
    }
}

void CollectEggs()
{
    int collect = 0;
    for (int i = 0; i < chickenCount; i++)
    {

        collect += chickenEggCount[i];
        eggSupply += chickenEggCount[i];
        chickenEggCount[i] = 0;

    }
    Console.WriteLine($"Собрано яиц: {collect}. Всего яиц: {eggSupply}");
}

void FeedChickens()
{
    for (int i = 0; i < chickenCount; i++)
    {
        if (chickenStatus[i] != ChickenState.Dead)
        {
            chickenStatus[i] = ChickenState.Feedup;
        }
    }
}

void NewDay()
{
    day++;
    for (int i = 0; i < chickenCount; i++)
    {
        if (chickenStatus[i] == ChickenState.Hungry) chickenStatus[i] = ChickenState.Dead;
        else if (chickenStatus[i] == ChickenState.Feedup)
        {
            chickenEggCount[i]++;
            chickenStatus[i] = ChickenState.Hungry;
        }
    }
}
void ShowSatus()
{
    GetChickenStatus(out int alive, out int dead, out int eggCollectt);
    Console.WriteLine($"День: {day}\t Собрано яиц: {eggSupply}\t Живых куриц: {alive}\t Мертвых куриц:{dead}\tЯиц для сбора: {eggCollectt}\n");
}

void GetChickenStatus(out int alive, out int dead, out int eggCollect)
{
    alive = 0; dead = 0; eggCollect = 0;
    for (int i = 0; i < chickenCount; i++)
    {
        if (chickenStatus[i] == ChickenState.Dead) dead++;
        else alive++;
        eggCollect += chickenEggCount[i];
    }
}

enum ChickenState
{
    Alive,
    Dead,
    Feedup,
    Hungry
}

