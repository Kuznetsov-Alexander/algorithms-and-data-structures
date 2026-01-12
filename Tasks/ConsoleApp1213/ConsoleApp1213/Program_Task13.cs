using System;
using System.Collections.Generic;
using System.Globalization;
using MyCollections;   
using MyCollectionsEx;  

namespace Task13
{
    class Program
    {
        enum TokenType { Number, Variable, Operator, Function, Comma, LeftParen, RightParen }

        class Token
        {
            public TokenType Type;
            public string Text;
            public double? Value;

            public Token(TokenType t, string text)
            {
                Type = t;
                Text = text;
            }
        }

        static readonly Dictionary<string, (int, string)> Ops =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "+",  (2, "L") },
                { "-",  (2, "L") },
                { "*",  (3, "L") },
                { "/",  (3, "L") },
                { "%",  (3, "L") },
                { "mod",(3, "L") },
                { "div",(3, "L") },
                { "^",  (4, "R") },
                { "u-", (5, "R") } 
            };

        static readonly HashSet<string> Funcs =
            new(StringComparer.OrdinalIgnoreCase)
            {
                "sqrt","abs","sin","cos","tan","ln","log","exp","trunc","min","max"
            };

        static void Main(string[] args)
        {
            string expr;
            var vars = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

            if (args.Length > 0)
            {
                expr = args[0];

                if (args.Length > 1)
                    ParseVars(args, vars);
            }
            else
            {
                Console.Write("Введите выражение: ");
                expr = Console.ReadLine() ?? "";
            }

            try
            {
                var rpn = ToRPN(expr);
                double result = EvalRPN(rpn, vars);
                Console.WriteLine(result.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }


        static List<Token> ToRPN(string s)
        {
            var output = new List<Token>();
            var ops = new MyStack<Token>();

            Token? prev = null;
            int i = 0;

            while (i < s.Length)
            {
                char c = s[i];

                
                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                if (char.IsDigit(c) || (c == '.' && i + 1 < s.Length && char.IsDigit(s[i + 1])))
                {
                    int start = i;
                    bool dot = false;

                    while (i < s.Length &&
                           (char.IsDigit(s[i]) || (!dot && s[i] == '.')))
                    {
                        if (s[i] == '.') dot = true;
                        i++;
                    }

                    string num = s[start..i];
                    var t = new Token(TokenType.Number, num);
                    t.Value = double.Parse(num, CultureInfo.InvariantCulture);
                    output.Add(t);
                    prev = t;
                    continue;
                }

                if (char.IsLetter(c))
                {
                    int start = i;
                    while (i < s.Length && (char.IsLetterOrDigit(s[i]) || s[i] == '_'))
                        i++;

                    string id = s[start..i];

                    if (Funcs.Contains(id))
                    {
                        ops.Push(new Token(TokenType.Function, id));
                    }
                    else
                    {
                        output.Add(new Token(TokenType.Variable, id));
                        prev = output[^1];
                    }
                    continue;
                }

                if (c == '(')
                {
                    ops.Push(new Token(TokenType.LeftParen, "("));
                    prev = new Token(TokenType.LeftParen, "(");
                    i++;
                    continue;
                }

                if (c == ')')
                {
                    bool ok = false;
                    while (!ops.Empty())
                    {
                        var t = ops.Pop();
                        if (t.Type == TokenType.LeftParen)
                        {
                            ok = true;
                            break;
                        }
                        output.Add(t);
                    }

                    if (!ok) throw new Exception("Несогласованные скобки");

                    if (!ops.Empty() && ops.Peek().Type == TokenType.Function)
                        output.Add(ops.Pop());

                    i++;
                    prev = new Token(TokenType.RightParen, ")");
                    continue;
                }

                if (c == ',')
                {
                    while (!ops.Empty() && ops.Peek().Type != TokenType.LeftParen)
                        output.Add(ops.Pop());
                    if (ops.Empty()) throw new Exception("Запятая вне функции");

                    i++;
                    prev = new Token(TokenType.Comma, ",");
                    continue;
                }

                if ("+-*/^%".Contains(c))
                {
                    string op = c.ToString();

                    if (c == '-' &&
                        (prev == null ||
                         prev.Type == TokenType.Operator ||
                         prev.Type == TokenType.LeftParen ||
                         prev.Type == TokenType.Comma))
                    {
                        op = "u-";
                    }

                    var cur = Ops[op];

                    while (!ops.Empty())
                    {
                        var top = ops.Peek();
                        if (top.Type == TokenType.Operator)
                        {
                            var tinfo = Ops[top.Text];

                            bool pop =
                                (tinfo.Item1 > cur.Item1) ||
                                (tinfo.Item1 == cur.Item1 && cur.Item2 == "L");

                            if (pop)
                            {
                                output.Add(ops.Pop());
                                continue;
                            }
                        }
                        break;
                    }

                    ops.Push(new Token(TokenType.Operator, op));
                    prev = new Token(TokenType.Operator, op);
                    i++;
                    continue;
                }

                throw new Exception($"Недопустимый символ: '{c}'");
            }

            while (!ops.Empty())
            {
                var t = ops.Pop();
                if (t.Type == TokenType.LeftParen)
                    throw new Exception("Несогласованные скобки");
                output.Add(t);
            }

            return output;
        }

        static double EvalRPN(List<Token> rpn, Dictionary<string, double> vars)
        {
            var st = new MyStack<double>();

            foreach (var t in rpn)
            {
                switch (t.Type)
                {
                    case TokenType.Number:
                        st.Push(t.Value!.Value);
                        break;

                    case TokenType.Variable:
                        if (!vars.TryGetValue(t.Text, out double v))
                        {
                            Console.Write($"Введите {t.Text}: ");
                            v = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);
                            vars[t.Text] = v;
                        }
                        st.Push(v);
                        break;

                    case TokenType.Operator:
                        {
                            string op = t.Text;

                            if (op == "u-")
                            {
                                double x = st.Pop();
                                st.Push(-x);
                                break;
                            }

                            double b = st.Pop();
                            double a = st.Pop();

                            st.Push(op switch
                            {
                                "+" => a + b,
                                "-" => a - b,
                                "*" => a * b,
                                "/" => (b == 0 ? throw new DivideByZeroException() : a / b),
                                "%" => a % b,
                                "mod" => a % b,
                                "div" => Math.Truncate(a / b),
                                "^" => Math.Pow(a, b),
                                _ => throw new Exception($"Неизвестный оператор {op}")
                            });
                        }
                        break;

                    case TokenType.Function:
                        {
                            string f = t.Text.ToLower();

                            if (f is "min" or "max")
                            {
                                double b = st.Pop();
                                double a = st.Pop();
                                st.Push(f == "min" ? Math.Min(a, b) : Math.Max(a, b));
                                break;
                            }

                            double x = st.Pop();

                            st.Push(f switch
                            {
                                "sqrt" => Math.Sqrt(x),
                                "abs" => Math.Abs(x),
                                "sin" => Math.Sin(x),
                                "cos" => Math.Cos(x),
                                "tan" => Math.Tan(x),
                                "ln" => Math.Log(x),
                                "log" => Math.Log10(x),
                                "exp" => Math.Exp(x),
                                "trunc" => Math.Truncate(x),
                                _ => throw new Exception($"Неизвестная функция {f}")
                            });
                        }
                        break;

                    default:
                        throw new Exception("Ошибка в ОПЗ");
                }
            }

            if (st.Size() != 1)
                throw new Exception("Некорректное выражение");

            return st.Pop();
        }
        
        static void ParseVars(string[] args, Dictionary<string, double> vars)
        {
            for (int i = 1; i < args.Length; i++)
            {
                string s = args[i];
                if (!s.Contains('=')) continue;
                var p = s.Split('=');
                vars[p[0]] = double.Parse(p[1], CultureInfo.InvariantCulture);
            }
        }
    }
}
