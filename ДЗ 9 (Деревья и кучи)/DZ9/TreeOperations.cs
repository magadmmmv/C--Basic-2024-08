namespace DZ9
{
    public class TreeOperations
    {
        public static TreeNode? InputTree()
        {
            TreeNode? root = null;
            while (true)
            {
                Console.Write("Введите имя нового сотрудника: ");
                string? employee = Console.ReadLine();

                if (employee == "")
                {
                    break;
                }

                while (true)
                {
                    int salary = 0;
                    try
                    {
                        while (salary <= 0)
                        {
                            Console.Write($"Введите зарплату {employee}: ");
                            salary = int.Parse(Console.ReadLine()!);
                            if (salary <= 0)
                            {
                                Console.WriteLine($"Зарплата {employee} должна быть больше 0");
                            }
                        }                            

                        if (root == null)
                        {
                            root = new TreeNode()
                            {
                                Employee = employee!,
                                Salary = salary
                            };
                        }
                        else
                        {
                            AddNode(root, new TreeNode()
                            {
                                Employee = employee!,
                                Salary = salary
                            });
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Введите зарплату {employee} в числовом виде");
                    }

                    if (salary > 0)
                        break;
                }

            }

            return root;
        }

        public static TreeNode? InputManualTree()
        {
            TreeNode? root = null;

            // Корневая нода
            root = new TreeNode()
            {
                Employee = "Vasily",
                Salary = 500000
            };

            // Потомки
            AddNode(root, new TreeNode() { Employee = "Anatoly", Salary = 300000});
            AddNode(root, new TreeNode() { Employee = "Fedor", Salary = 250000});
            AddNode(root, new TreeNode() { Employee = "Maxim", Salary = 600000});
            AddNode(root, new TreeNode() { Employee = "Vladimir", Salary = 100000});
            AddNode(root, new TreeNode() { Employee = "Nikolay", Salary = 900000});
            AddNode(root, new TreeNode() { Employee = "Misha", Salary = 800000});
            AddNode(root, new TreeNode() { Employee = "Anna", Salary = 600000});
            AddNode(root, new TreeNode() { Employee = "Alex", Salary = 200000});

            return root;
        }

        public static void AddNode(TreeNode rootNode, TreeNode nodeToAdd)
        {
            if (nodeToAdd.Salary < rootNode.Salary)
            {
                //Зарплата добавляемого меньше зарплаты корневого?
                //идем в левое поддерево
                if (rootNode.Left != null)
                {
                    AddNode(rootNode.Left, nodeToAdd);
                }
                else
                {
                    rootNode.Left = nodeToAdd;
                }
            }
            else
            {
                //Зарплата добавляемого больше или равна зарплате корневого?
                //идем в правое поддерево
                if (rootNode.Right != null)
                {
                    AddNode(rootNode.Right, nodeToAdd);
                }
                else
                {
                    rootNode.Right = nodeToAdd;
                }
            }
        }

        public static void TraverseEmployeers(TreeNode employeeNode)
        {
            if (employeeNode.Left != null)
            {
                TraverseEmployeers(employeeNode.Left);
            }

            Console.WriteLine($"{employeeNode.Employee} - {employeeNode.Salary}");

            if (employeeNode.Right != null)
            {
                TraverseEmployeers(employeeNode.Right);
            }
        }

        public static void FindSalary(TreeNode root)
        {
            Console.WriteLine("Введите размер зарплаты:");
            try
            {
                int salary = int.Parse(Console.ReadLine()!);
                var (node, level) = FindEmployeeNode(root, salary, operationsCount: 0);
                if (node != null)
                {
                    Console.WriteLine($"Найден сотрудник: {node.Employee} - {node.Salary}; количество операций: {level}");
                }
                else
                {
                    Console.WriteLine($"Такой сотрудник не найден");
                }
                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Зарплату необходимо ввести в числовом виде");
                Console.WriteLine();
            }
        }

        private static (TreeNode?, int) FindEmployeeNode(TreeNode root, int salary, int operationsCount)
        {
            if (salary < root.Salary)
            {
                //Ищем в левом поддереве
                if (root.Left != null)
                {
                    return FindEmployeeNode(root.Left, salary, operationsCount + 1);
                }

                return (null, -1);
            }
            if (salary > root.Salary)
            {
                //Ищем в правом поддереве
                if (root.Right != null)
                {
                    return FindEmployeeNode(root.Right, salary, operationsCount + 1);
                }

                return (null, -1);
            }

            return (root, operationsCount + 1);
        }
    }
}
