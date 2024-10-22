int chickenCount = 3; //Начальное кол-во куриц
int eggSupply = 0; //Запас яиц
int day = 0;

string savePath = "save.game";

ChickenState[] chickenStatus = new ChickenState[chickenCount];
int[] chickenEggCount = new int[chickenCount];

for (int i = 0; i < chickenCount; i++)
    chickenStatus[i] = ChickenState.Alive;


if (!LoadGame()) NewDay();

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

        Console.WriteLine("Выберите одно из действий:\n1. Покормить куриц\n2. Собрать яйца\n3. Завершить ход (ничего не делать)\n4. Выйти\n5. Сохранить игру");
        var res = int.TryParse(Console.ReadLine(), out int number);
        if (res)
        {
            if (number == 1) { FeedChickens(); return 1; }
            else if (number == 2) { CollectEggs(); return 2; }
            else if (number == 3) { NewDay(); return 3; }
            else if (number == 4) return -1;
            else if (number == 5) { SaveGame(); return 5; }
        }
        
    }
}

void CollectEggs()
{
    for (int i = 0; i < chickenCount; i++)
    {
        eggSupply += chickenEggCount[i];
        chickenEggCount[i] = 0;
    }
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

void SaveGame()
{
    using BinaryWriter bw = new BinaryWriter(File.Open(savePath, FileMode.OpenOrCreate));
    try
    {
        bw.Write(day);
        bw.Write(chickenCount);
        bw.Write(eggSupply);
        for (int i = 0; i < chickenCount; i++)
        {
            bw.Write(chickenStatus[i].ToString());
        }
        for (int i = 0; i < chickenCount; i++)
        {
            bw.Write(chickenEggCount[i]);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Не удалось сохранить игру");
    }
}

bool LoadGame()
{
    if (File.Exists(savePath))
    {
        using BinaryReader br = new BinaryReader(File.Open(savePath, FileMode.Open));
        try
        {
            day = br.ReadInt32();
            chickenCount = br.ReadInt32();
            eggSupply = br.ReadInt32();
            for (int i = 0; i < chickenCount; i++)
            {
                chickenStatus[i] = (ChickenState)Enum.Parse(typeof(ChickenState), br.ReadString());
            }
            for (int i = 0; i < chickenCount; i++)
            {
                chickenEggCount[i] = br.ReadInt32();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Не удалось загрузить файл сохранения");
            return false;
        }

    } else
    {
        return false;
    }
}

enum ChickenState
{
    Alive,
    Dead,
    Feedup,
    Hungry
}

