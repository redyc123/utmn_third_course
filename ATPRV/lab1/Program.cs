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
// 1. MIN:0,209 MAX:0,315 MID: 0,2177699999999999
// 2. MIN:0,448 MAX:0,775 MID: 0,5604699999999998
// 3. MIN:0,188 MAX:0,197 MID: 0,1899800000000001
// 4. MIN:0,453 MAX:0,768 MID: 0,5670300000000003

//1.2
// 1. MIN:0.024 MAX:0.071 MID: 0.027409999999999997
// 2. MIN:0.031 MAX:0.096 MID: 0.03804999999999999
// 3. MIN:0.022 MAX:0.118 MID: 0.029349999999999977
// 4. MIN:0.032 MAX:0.096 MID: 0.037769999999999984


// Задание 2
Console.WriteLine("Задание 2");

static double[] ComputeSequential(int[] A)
{
    int n = A.Length;
    double[] B = new double[n];
    for (int i = 0; i < n; i++)
    {
        double sum = 0;
        for (int j = 0; j < i; j++)
        {
            sum += Math.Pow(A[j], 1.789);
        }
        B[i] = sum;
    }
    return B;
}

static double[] ComputeParallel(int[] A, int numThreads)
{
    int n = A.Length;
    double[] B = new double[n];
    int chunk = n / numThreads;

    Parallel.For(0, numThreads, k =>
    {
        int start = k * chunk;
        int end = (k == numThreads - 1) ? n : start + chunk;

        for (int i = start; i < end; i++)
        {
            double sum = 0;
            for (int j = 0; j < i; j++)
            {
                sum += Math.Pow(A[j], 1.789);
            }
            B[i] = sum;
        }
    });

    return B;
}

// Параллельное вычисление с циклическим разбиением
static double[] ComputeParallelRoundRobin(int[] A, int numThreads)
{
    int n = A.Length;
    double[] B = new double[n];

    Parallel.For(0, numThreads, k =>
    {
        for (int i = k; i < n; i += numThreads)
        {
            double sum = 0;
            for (int j = 0; j < i; j++)
            {
                sum += Math.Pow(A[j], 1.789);
            }
            B[i] = sum;
        }
    });

    return B;
}



int[] sizes = { 1000, 5000, 10000, 20000 };
int[] threads = { 1, 2, 4, 8, 12, 16, 20 };

foreach (int n_size in sizes)
{
    Console.WriteLine($"\nРазмерность массива: {n_size}");
    int[] A = new int[n_size];
    for (int i = 0; i < n_size; i++) A[i] = i + 1;

    foreach (int t in threads)
    {
        // Последовательное заполнение
        DateTime t1 = DateTime.Now;
        double[] Bseq = ComputeSequential(A);
        DateTime t2 = DateTime.Now;
        TimeSpan dt = t2 - t1;
        // Console.WriteLine($"Потоки: {t}, Последовательно: {dt.Milliseconds / 1000.0} мс");

        // Параллельное заполнение
        t1 = DateTime.Now;
        double[] Bpar = ComputeParallel(A, t);
        t2 = DateTime.Now;
        dt = t2 - t1;
        // Console.WriteLine($"Потоки: {t}, Параллельно: {dt.Milliseconds / 1000.0} мс");

         // Параллельное заполнение 2
        t1 = DateTime.Now;
        double[] Bpar2 = ComputeParallelRoundRobin(A, t);
        t2 = DateTime.Now;
        dt = t2 - t1;
        Console.WriteLine($"Потоки: {t}, Параллельно 2: {dt.Milliseconds / 1000.0} мс");
    }
}


