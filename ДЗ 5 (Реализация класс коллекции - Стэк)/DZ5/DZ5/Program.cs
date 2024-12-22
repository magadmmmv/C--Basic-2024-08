namespace DZ5
{
    public class Stack
    {
        private List<string> _elements = [];

        public Stack(params string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                _elements.Add(args[i]);
            }
        }

        public void Add(string arg)
        {
            _elements.Add(arg);
        }

        public string? Pop()
        {
            try
            {
                var delElem = _elements[Size - 1];
                _elements.RemoveAt(Size - 1);
                return delElem;
            }
            catch (Exception)
            {
                Console.WriteLine("Стек пустой");
            }
            return null;
            //if (Size > 0)
            //{
            //    var delElem = _elements[Size - 1];
            //    _elements.RemoveAt(Size - 1);
            //    return delElem;
            //}
            //else throw new Exception("Стек пустой");
        }

        public int Size => _elements.Count;

        public string? Top {
            get {
                if (Size > 0) return _elements[Size - 1];
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

            // Доп задание 2
            var sss = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
            // в стеке sss теперь элементы -  "c", "b", "a" "3", "2", "1", "В", "Б", "А" <- верхний
        }
    }
}
