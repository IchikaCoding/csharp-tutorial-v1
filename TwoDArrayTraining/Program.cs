
// int[行数, 列数]
// int[,]で2次元配列？int[,,]で3次元配列？
// これってからの配列ができるの？
int[,] grid = new int[3, 4];  // 3行4列


// 値を入れる：grid[行, 列]
grid[0, 0] = 1;  // 0行0列
grid[1, 2] = 5;  // 1行2列

// 初期値と一緒に作る
// 直で値を代入していくタイプ？行と列は指定しないで値を最初からいれるってこと？
int[,] matrix = {
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

// ================================================================
// 出力:
// 1 2 3
// 4 5 6

int[,] m =
{
    {1,2,3 },
    {4,5,6 },
};

// 行をぐるぐる
// 列をぐるぐる

for(int row = 0; row < m.GetLength(0); row++)
{
    for (int col = 0; col < m.GetLength(1); col++)
    {
        Console.Write(m[row, col] + " ");
    }
    Console.WriteLine();
}

// 全部の値を取得したい時はforeachでできるよん
foreach (int element in m)
{
    Console.WriteLine(element);
}