using System.Collections.Generic;

namespace ExpressionEvaluator.Core;

public class Evaluator
{
    public static double Evaluate(string infix)
    {
  
        var postfix = InfixToPostfix(infix);
        return EvaluatePostfix(postfix);
    }

    private static Queue<string> InfixToPostfix(string infix)
    {
        var cola = new Queue<string>(); 
        var pila = new Stack<char>();

        for (int i = 0; i < infix.Length; i++)
        {
            char c = infix[i];

            // 
            if (char.IsDigit(c) || c == '.')
            {
                string num = "";

                while (i < infix.Length && (char.IsDigit(infix[i]) || infix[i] == '.'))
                {
                    num += infix[i];
                    i++;
                }

                cola.Enqueue(num);
                i--; 
            }
            else if (EsOperador(c))
            {
                if (pila.Count == 0 || c == '(')
                {
                    pila.Push(c);
                }
                else if (c == ')')
                {
                    while (pila.Peek() != '(')
                    {
                        cola.Enqueue(pila.Pop().ToString());
                    }
                    pila.Pop(); 
                }
                else
                {
                    
                    while (pila.Count > 0 && PrioridadPila(pila.Peek()) >= PrioridadEntrada(c))
                    {
                        cola.Enqueue(pila.Pop().ToString());
                    }
                    pila.Push(c);
                }
            }
        }

        
        while (pila.Count > 0)
        {
            cola.Enqueue(pila.Pop().ToString());
        }

        return cola;
    }

    private static double EvaluatePostfix(Queue<string> postfix)
    {
        var pila = new Stack<double>();

        while (postfix.Count > 0)
        {
            string item = postfix.Dequeue();

            
            if (item.Length == 1 && EsOperador(item[0]))
            {
                double b = pila.Pop();
                double a = pila.Pop();

                double res = 0;

                switch (item[0])
                {
                    case '+': res = a + b; break;
                    case '-': res = a - b; break;
                    case '*': res = a * b; break;
                    case '/': res = a / b; break;
                    case '^': res = Math.Pow(a, b); break;
                }

                pila.Push(res);
            }
            else
            {
               
                pila.Push(double.Parse(item));
            }
        }

        return pila.Pop();
    }

  
    private static int PrioridadPila(char c)
    {
        if (c == '^') return 3;
        if (c == '*' || c == '/') return 2;
        if (c == '+' || c == '-') return 1;
        if (c == '(') return 0;
        return 0;
    }

    private static int PrioridadEntrada(char c)
    {
        if (c == '^') return 4;
        if (c == '*' || c == '/') return 2;
        if (c == '+' || c == '-') return 1;
        if (c == '(') return 5;
        return 0;
    }

    private static bool EsOperador(char c)
    {
        return "+-*/^()".Contains(c);
    }
}
