// Задание 1

int N = 1;
int n = 1;

double[] one(int n)
{
    double[] results = new double[n];
    for (int c = 0; c < n; c++)
    {
        double[,] a = new double[N, N];
        DateTime t1 = DateTime.Now;
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                a[i, j] = i / (j + 1);
        DateTime t2 = DateTime.Now;
        TimeSpan dt = t2 - t1;
        // Console.WriteLine("Время создания {0} ms {1}", dt.Milliseconds / 1000.0, dt.Ticks);
        results[c] = dt.Milliseconds / 1000.0;
    }
    return results;
}

double[] two(int n)
{
    double[] results = new double[n];
    for (int c = 0; c < n; c++)
    {
        double[,] a = new double[N, N];
        DateTime t1 = DateTime.Now;
        for (int j = 0; j < N; j++)
            for (int i = 0; i < N; i++)
                a[i, j] = i / (j + 1);
        DateTime t2 = DateTime.Now;
        TimeSpan dt = t2 - t1;
        // Console.WriteLine("Время создания {0} ms {1}", dt.Milliseconds / 1000.0, dt.Ticks);
        results[c] = dt.Milliseconds / 1000.0;
    }
    return results;
}

double[] three(int n)
{
    double[] results = new double[n];
    for (int c=0; c < n; c++)
    {        
        double[,] a = new double[N, N];
        DateTime t1 = DateTime.Now;
        for (int i = N - 1; i >= 0; i--)
            for (int j = N - 1; j >= 0; j--)
                a[i, j] = i / (j + 1);
        DateTime t2 = DateTime.Now;
        TimeSpan dt = t2 - t1;
        // Console.WriteLine("Время создания {0} ms {1}", dt.Milliseconds / 1000.0, dt.Ticks);
        results[c] = dt.Milliseconds / 1000.0;
    }
    return results;
}

double[] four(int n)
{
    double[] results = new double[n];
    for (int c = 0; c < n; c++)
    {
        double[,] a = new double[N, N];
        DateTime t1 = DateTime.Now;
        for (int j = N - 1; j >= 0; j--)
            for (int i = N - 1; i >= 0; i--)
                a[i, j] = i / (j + 1);
        DateTime t2 = DateTime.Now;
        TimeSpan dt = t2 - t1;
        // Console.WriteLine("Время создания {0} ms {1}", dt.Milliseconds / 1000.0, dt.Ticks);
        results[c] = dt.Milliseconds / 1000.0;
    }
    return results;
}

Console.WriteLine("Задание 1");

double[] result1 = one(n);
double[] result2 = two(n);
double[] result3 = three(n);
double[] result4 = four(n);

double[][] all_results = [result1, result2, result3, result4];

for (int i = 0; i < all_results.Length; i++)
{
    double[] res = all_results[i];
    double min = res[0];
    double max = res[0];
    double sum = 0.0;
    for (int j = 0; j < res.Length; j++)
    {
        if (min > res[j])
        {
            min = res[j];
        }
        if (max < res[j])
        {
            max = res[j];
        }
        sum = sum + res[j];
    }
    Console.WriteLine("{0}. MIN:{1} MAX:{2} MID: {3}", i + 1, min, max, sum / res.Length);
}
//1.1
// 1. MIN:0.208 MAX:0.426 MID: 0.2334099999999999
// 2. MIN:0.294 MAX:0.46 MID: 0.3386500000000001
// 3. MIN:0.208 MAX:0.321 MID: 0.22632999999999984
// 4. MIN:0.297 MAX:0.531 MID: 0.32477999999999996

//1.2
// 1. MIN:0.024 MAX:0.071 MID: 0.027409999999999997
// 2. MIN:0.031 MAX:0.096 MID: 0.03804999999999999
// 3. MIN:0.022 MAX:0.118 MID: 0.029349999999999977
// 4. MIN:0.032 MAX:0.096 MID: 0.037769999999999984


// Задание 2

double newelem(double aj)
{
    return Math.Pow(aj, 1.789);
}

System.Diagnostics.Process.GetCurrentProcess().Threads;