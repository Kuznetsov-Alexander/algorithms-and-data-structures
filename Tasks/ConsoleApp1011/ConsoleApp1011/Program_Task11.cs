using System;
using System.IO;
using MyCollections;

namespace Task11
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "input.txt";
            string outputPath = "output.txt";
            

            string[] lines = File.ReadAllLines(inputPath);

            MyVector<string> vLines = new MyVector<string>(lines);
            MyVector<string> foundIPs = new MyVector<string>();

            for (int li = 0; li < vLines.Size(); li++)
            {
                string s = vLines.Get(li);
                ExtractIpsFromLine(s, foundIPs);
            }

            using (var sw = new StreamWriter(outputPath))
            {
                for (int i = 0; i < foundIPs.Size(); i++)
                {
                    sw.WriteLine(foundIPs.Get(i));
                }
            }

            Console.WriteLine($"Готово. Найдено {foundIPs.Size()} IP-адресов. Сохранено в {outputPath}");
        }
        
        static void ExtractIpsFromLine(string s, MyVector<string> result)
        {
            if (string.IsNullOrEmpty(s)) return;
            int n = s.Length;
            int i = 0;
            while (i < n)
            {
                if (!char.IsDigit(s[i])) { i++; continue; }

                if (i > 0 && char.IsDigit(s[i - 1]))
                {
                    i++;
                    continue;
                }

                int pos = i;
                int[] parts = new int[4];
                bool ok = true;
                int endPos = -1;

                for (int part = 0; part < 4 && ok; part++)
                {
                    if (pos >= n || !char.IsDigit(s[pos])) { ok = false; break; }
                    int startNum = pos;
                    int digits = 0;
                    int val = 0;
                    while (pos < n && char.IsDigit(s[pos]) && digits < 3)
                    {
                        val = val * 10 + (s[pos] - '0');
                        pos++; digits++;
                    }
                    if (pos < n && char.IsDigit(s[pos]) && digits == 3)
                    {
                        ok = false; break;
                    }
                    if (val < 0 || val > 255) { ok = false; break; }
                    parts[part] = val;

                    if (part < 3)
                    {
                        if (pos >= n || s[pos] != '.') { ok = false; break; }
                        pos++; 
                        if (pos >= n || !char.IsDigit(s[pos])) { ok = false; break; }
                    }
                    else
                    {
                        endPos = pos;
                    }
                }

                if (ok && endPos > i)
                {
                    if (endPos < n && char.IsDigit(s[endPos]))
                    {
                        ok = false;
                    }
                }

                if (ok)
                {
                    string ip = s.Substring(i, endPos - i);
                    result.Add(ip);
                    i = endPos;
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
