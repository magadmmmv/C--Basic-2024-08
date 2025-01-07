namespace DZ5_dop3
{
    public class Stack
    {
        StackItem? Current;
        public int Size { get; set; }
        class StackItem
        {
            public string Value { get; set; }
            public StackItem? Previous { get; set; }

            public StackItem(string value, StackItem? previous)
            {
                Value = value;
                Previous = previous;
            }
        }

        public Stack(params string[] args)
        {
            Size = 0;

            for (int i = 0; i < args.Length; i++)
            {
                if (Size == 0)
                {
                    Current = new StackItem(args[i], null);
                    Current.Previous = null;
                }
                else
                {
                    var stackItem = new StackItem(args[i], Current);
                    Current = stackItem;
                }

                Size++;
            }
        }

        public void Add(string arg)
        {
            var stackItem = new StackItem(arg, Current);
            Current = stackItem;
            Size++;
        }

        public string Pop()
        {
            try
            {
                if (Current == null)
                    throw new NullReferenceException("Стек пустой");
                var delElem = Current.Value;
                Current = Current.Previous;
                Size--;
                return delElem;
            }
            catch (NullReferenceException)
            {
                return "null";
            }
        }

        public string? Top
        {
            get
            {
                if (Size > 0) 
                    return Current!.Value;
                return null;
            }
        }

        public static Stack Concat(params Stack[] stacks)
        {
            Stack concatStack = new Stack();
            foreach (var stack in stacks)
            {
                concatStack.Merge(stack);
            }
            return concatStack;
        }
    }

    public static class StackExtensions
    {
        public static void Merge(this Stack s1, Stack s2)
        {
            while (s2.Size > 0)
            {
                s1.Add(s2.Pop());
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var s = new Stack("a", "b", "c");

            // size = 3, Top = 'c'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");

            var deleted = s.Pop();

            // Извлек верхний элемент 'c' Size = 2
            Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
            s.Add("d");

            // size = 3, Top = 'd'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");

            s.Pop();
            s.Pop();
            s.Pop();
            // size = 0, Top = null
            Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
            s.Pop();

            // Доп задание 1
            var ss = new Stack("a", "b", "c");

            ss.Merge(new Stack("1", "2", "3"));
            // в стеке ss теперь элементы - "a", "b", "c", "3", "2", "1" <- верхний
            // и хранятся ссылки на предыдущий элемент

            // Доп задание 2
            var sss = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
            // в стеке sss теперь элементы -  "c", "b", "a" "3", "2", "1", "В", "Б", "А" <- верхний
            // и хранятся ссылки на предыдущий элемент
        }
    }
}
