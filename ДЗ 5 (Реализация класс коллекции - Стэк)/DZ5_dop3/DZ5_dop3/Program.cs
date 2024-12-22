namespace DZ5_dop3
{
    public class Stack
    {
        private StackItem[] _stackItems;
        private StackItem[] _tempStackItems;
        public int Size { get; set; }
        private class StackItem
        {
            public string? Value { get; set; }
            public StackItem? Previous { get; set; }
        }

        public Stack(params string[] args)
        {
            _stackItems = new StackItem[args.Length];
            Size = 0;

            for (int i = 0; i < _stackItems.Length; i++)
            {
                var stackItem = new StackItem { Value = args[i] };
                _stackItems[i] = stackItem;

                if (i == 0)
                    _stackItems[i].Previous = null;
                else 
                {
                    var stackItemPrevious = new StackItem { Value = args[i - 1] };
                    _stackItems[i].Previous = stackItemPrevious;
                }
                Size++;
            }
        }

        public void Add(string arg)
        {
            Size++;
            _tempStackItems = new StackItem[Size];
            Array.Copy(_stackItems, _tempStackItems, Size - 1);

            var stackItem = new StackItem { Value = arg };
            _tempStackItems[Size - 1] = stackItem;
            if (Size - 1 == 0)
                _tempStackItems[Size - 1].Previous = null;
            else
                _tempStackItems[Size - 1].Previous = _tempStackItems[Size - 2];

            _stackItems = new StackItem[Size];
            Array.Copy(_tempStackItems, _stackItems, Size);
        }

        public string? Pop()
        {
            try
            {
                var delElem = _stackItems[Size - 1].Value;
                Size--;

                _tempStackItems = new StackItem[Size];
                Array.Copy(_stackItems, _tempStackItems, Size);

                _stackItems = new StackItem[Size];
                Array.Copy(_tempStackItems, _stackItems, Size);

                return delElem;
            }
            catch (Exception)
            {
                Console.WriteLine("Стек пустой");
            }
            return null;
        }

        public string? Top
        {
            get
            {
                if (Size > 0) return _stackItems[Size - 1].Value;
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
