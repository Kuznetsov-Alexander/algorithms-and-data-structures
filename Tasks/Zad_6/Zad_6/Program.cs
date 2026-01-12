using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        var sr = new FastScanner(Console.OpenStandardInput());
        int n = sr.NextInt();
        long[,] a = new long[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                a[i, j] = sr.NextInt();

        int full = 1 << n;
        const long INF = (long)4e18;
        long[,] dp = new long[full, n];
        int[,] parent = new int[full, n];

        for (int mask = 0; mask < full; mask++)
            for (int v = 0; v < n; v++)
            {
                dp[mask, v] = INF;
                parent[mask, v] = -1;
            }
        for (int v = 0; v < n; v++)
        {
            int mask = 1 << v;
            dp[mask, v] = 0;
            parent[mask, v] = -1;
        }
        for (int mask = 0; mask < full; mask++)
        {
            for (int v = 0; v < n; v++)
            {
                if (dp[mask, v] == INF) continue;
                for (int u = 0; u < n; u++)
                {
                    if ((mask & (1 << u)) != 0) continue;
                    int nmask = mask | (1 << u);
                    long cand = dp[mask, v] + a[v, u];
                    if (cand < dp[nmask, u])
                    {
                        dp[nmask, u] = cand;
                        parent[nmask, u] = v;
                    }
                }
            }
        }
        int allMask = full - 1;
        long best = INF;
        int last = -1;
        for (int v = 0; v < n; v++)
        {
            if (dp[allMask, v] < best)
            {
                best = dp[allMask, v];
                last = v;
            }
        }
        var order = new List<int>();
        int curMask = allMask;
        int cur = last;
        while (cur != -1)
        {
            order.Add(cur);
            int prev = parent[curMask, cur];
            curMask ^= (1 << cur);
            cur = prev;
        }
        order.Reverse();

        var sb = new StringBuilder();
        sb.AppendLine(best.ToString());
        for (int i = 0; i < order.Count; i++)
        {
            if (i > 0) sb.Append(' ');
            sb.Append(order[i] + 1); // 1-based
        }
        sb.AppendLine();
        Console.Write(sb.ToString());
    }

    class FastScanner
    {
        private readonly Stream stream;
        private readonly byte[] buffer = new byte[1 << 16];
        private int len = 0, ptr = 0;
        public FastScanner(Stream s) { stream = s; }
        private byte Read()
        {
            if (ptr >= len)
            {
                len = stream.Read(buffer, 0, buffer.Length);
                ptr = 0;
                if (len == 0) return 0;
            }
            return buffer[ptr++];
        }
        public int NextInt()
        {
            byte c = Read();
            while (c <= 32) c = Read();
            int sign = 1;
            if (c == '-')
            {
                sign = -1;
                c = Read();
            }
            int val = 0;
            while (c > 32)
            {
                val = val * 10 + c - '0';
                c = Read();
            }
            return val * sign;
        }
    }
}
