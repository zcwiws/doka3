using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp5
{
    class Program
    {
        // Определение узла связного списка
        public class ListNode
        {
            public int Value;
            public ListNode Next;

            public ListNode(int value)
            {
                Value = value;
                Next = null;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер задания (1-12): ");
            int taskNumber = Convert.ToInt32(Console.ReadLine());

            // Создание списка 10 -> 20 -> 30 -> 40 -> 50 для примера
            ListNode head = new ListNode(10);
            head.Next = new ListNode(20);
            head.Next.Next = new ListNode(30);
            head.Next.Next.Next = new ListNode(40);
            head.Next.Next.Next.Next = new ListNode(50);

            // Выбор задания по номеру с помощью case
            switch (taskNumber)
            {
                case 1:
                    ListNode reversedHead = ReverseList(head);
                    PrintList(reversedHead);
                    break;
                case 2:
                    ListNode listWithDuplicates = new ListNode(10);
                    listWithDuplicates.Next = new ListNode(20);
                    listWithDuplicates.Next.Next = new ListNode(10);
                    listWithDuplicates.Next.Next.Next = new ListNode(30);
                    ListNode noDuplicatesHead = RemoveDuplicates(listWithDuplicates);
                    PrintList(noDuplicatesHead);
                    break;
                case 3:
                    Console.WriteLine(GetNthFromEnd(head, 2)?.Value);
                    break;
                case 4:
                    ListNode list1 = new ListNode(10);
                    list1.Next = new ListNode(20);
                    ListNode list2 = new ListNode(30);
                    list2.Next = new ListNode(40);
                    ListNode mergedList = MergeLists(list1, list2);
                    PrintList(mergedList);
                    break;
                case 5:
                    ListNode sortedList = SortList(head);
                    PrintList(sortedList);
                    break;
                case 6:
                    Console.WriteLine(IsPalindrome(head));
                    break;
                case 7:
                    Console.WriteLine(AverageValue(head));
                    break;
                case 8:
                    ListNode copiedList = CopyList(head);
                    PrintList(copiedList);
                    break;
                case 9:
                    var (evenList, oddList) = SplitListByParity(head);
                    PrintList(evenList);
                    PrintList(oddList);
                    break;
                case 10:
                    ListNode shiftedList = RotateList(head, 2);
                    PrintList(shiftedList);
                    break;
                case 11:
                    ListNode listA = new ListNode(10);
                    listA.Next = new ListNode(20);
                    listA.Next.Next = new ListNode(30);
                    ListNode listB = new ListNode(15);
                    listB.Next = new ListNode(30);
                    ListNode intersection = GetIntersection(listA, listB);
                    Console.WriteLine(intersection?.Value);
                    break;
                case 12:
                    Console.WriteLine(IsCircular(head));
                    break;
                default:
                    Console.WriteLine("Неверный номер задания");
                    break;
            }
        }

        // 1. Реверсирование списка
        static ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode current = head;
            while (current != null)
            {
                ListNode next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            return prev;
        }

        // 2. Удаление дубликатов из списка
        static ListNode RemoveDuplicates(ListNode head)
        {
            HashSet<int> seen = new HashSet<int>();
            ListNode current = head;
            ListNode prev = null;
            while (current != null)
            {
                if (seen.Contains(current.Value))
                {
                    prev.Next = current.Next;
                }
                else
                {
                    seen.Add(current.Value);
                    prev = current;
                }
                current = current.Next;
            }
            return head;
        }

        // 3. Получение п-го узла с конца списка
        static ListNode GetNthFromEnd(ListNode head, int n)
        {
            ListNode first = head;
            ListNode second = head;
            for (int i = 0; i < n; i++)
            {
                if (second == null) return null;
                second = second.Next;
            }
            while (second != null)
            {
                first = first.Next;
                second = second.Next;
            }
            return first;
        }

        // 4. Объединение двух списков
        static ListNode MergeLists(ListNode list1, ListNode list2)
        {
            if (list1 == null) return list2;
            if (list2 == null) return list1;

            ListNode mergedHead = list1;
            while (list1.Next != null)
            {
                list1 = list1.Next;
            }
            list1.Next = list2;
            return mergedHead;
        }

        // 5. Сортировка списка (сортировка вставками)
        static ListNode SortList(ListNode head)
        {
            if (head == null || head.Next == null) return head;

            ListNode sorted = null;
            while (head != null)
            {
                ListNode current = head;
                head = head.Next;
                if (sorted == null || sorted.Value >= current.Value)
                {
                    current.Next = sorted;
                    sorted = current;
                }
                else
                {
                    ListNode temp = sorted;
                    while (temp.Next != null && temp.Next.Value < current.Value)
                    {
                        temp = temp.Next;
                    }
                    current.Next = temp.Next;
                    temp.Next = current;
                }
            }
            return sorted;
        }

        // 6. Проверка на палиндром
        static bool IsPalindrome(ListNode head)
        {
            if (head == null) return true;

            ListNode slow = head;
            ListNode fast = head;
            Stack<int> stack = new Stack<int>();

            while (fast != null && fast.Next != null)
            {
                stack.Push(slow.Value);
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            if (fast != null) slow = slow.Next;

            while (slow != null)
            {
                if (slow.Value != stack.Pop()) return false;
                slow = slow.Next;
            }
            return true;
        }

        // 7. Среднее значение всех узлов
        static double AverageValue(ListNode head)
        {
            if (head == null) return 0;
            int sum = 0;
            int count = 0;
            ListNode current = head;
            while (current != null)
            {
                sum += current.Value;
                count++;
                current = current.Next;
            }
            return (double)sum / count;
        }
        // 8. Копирование списка
        static ListNode CopyList(ListNode head)
        {
            if (head == null) return null;

            ListNode newHead = new ListNode(head.Value);
            ListNode current = head.Next;
            ListNode newCurrent = newHead;

            while (current != null)
            {
                newCurrent.Next = new ListNode(current.Value);
                newCurrent = newCurrent.Next;
                current = current.Next;
            }
            return newHead;
        }

        // 9. Разделение списка на четные и нечетные значения
        static (ListNode, ListNode) SplitListByParity(ListNode head)
        {
            ListNode evenHead = null, oddHead = null;
            ListNode evenTail = null, oddTail = null;

            while (head != null)
            {
                if (head.Value % 2 == 0)
                {
                    if (evenHead == null)
                    {
                        evenHead = head;
                        evenTail = evenHead;
                    }
                    else
                    {
                        evenTail.Next = head;
                        evenTail = evenTail.Next;
                    }
                }
                else
                {
                    if (oddHead == null)
                    {
                        oddHead = head;
                        oddTail = oddHead;
                    }
                    else
                    {
                        oddTail.Next = head;
                        oddTail = oddTail.Next;
                    }
                }
                head = head.Next;
            }

            if (evenTail != null) evenTail.Next = null;
            if (oddTail != null) oddTail.Next = null;

            return (evenHead, oddHead);
        }

        // 10. Циклический сдвиг списка на k позиций
        static ListNode RotateList(ListNode head, int k)
        {
            if (head == null || k == 0) return head;

            ListNode current = head;
            int length = 1;

            while (current.Next != null)
            {
                length++;
                current = current.Next;
            }

            k = k % length;
            if (k == 0) return head;

            current.Next = head; // Создаем замкнутый цикл

            for (int i = 0; i < length - k; i++)
            {
                current = current.Next;
            }

            ListNode newHead = current.Next;
            current.Next = null;
            return newHead;
        }

        // 11. Нахождение пересечения двух списков
        static ListNode GetIntersection(ListNode head1, ListNode head2)
        {
            HashSet<ListNode> nodes = new HashSet<ListNode>();

            while (head1 != null)
            {
                nodes.Add(head1);
                head1 = head1.Next;
            }

            while (head2 != null)
            {
                if (nodes.Contains(head2)) return head2;
                head2 = head2.Next;
            }

            return null;
        }

        // 12. Проверка на цикличность списка
        static bool IsCircular(ListNode head)
        {
            if (head == null) return false;

            ListNode slow = head;
            ListNode fast = head;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;

                if (slow == fast) return true;
            }

            return false;
        }

        
        static void PrintList(ListNode head)
        {
            while (head != null)
            {
                Console.Write(head.Value + " ");
                head = head.Next;
            }
            Console.WriteLine();
        }
    }
}