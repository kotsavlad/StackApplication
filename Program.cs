using System;
using System.Collections.Generic;
using StackApplication;

var stack = new ListStack<int>();
stack.PushAll(1, 2, 3, 4);
Console.Write("stack: ");
stack.Print();
var stack2 = (ListStack<int>) stack.Clone();
var stack3 = new ArrayStack<int>(new[] {1, 2, 3, 4});
Console.WriteLine($"Is stack3 equal to stack2? {stack3.Equals(stack2)}");
Console.WriteLine($"stack3 == stack2? {stack3 == stack2}");
var stack4 = new ArrayStack<int>();
stack4.PushAll(5, 6);
Console.WriteLine($"stack3 + stack4 = {(stack3 + stack4).Stringify()}");
stack.Pop();
stack.Push(5);
foreach (var item in stack)
{
    Console.WriteLine(item);
}

Console.WriteLine($"Capacity = {stack3.Capacity}, Count = {stack3.Count}, Peak = {stack3.Peak}");
stack2.PushAll(new List<int> {-2, -1, 0});
Console.WriteLine($"stack2: {stack2.Stringify()}");
Console.WriteLine($"stack2[3] = {stack2[3]}");
Console.WriteLine($"Is stack equal to stack2? {stack.Equals(stack2)}");
stack2.Reverse();
Console.WriteLine($"stack2 after reverse: {stack2.Stringify()}");
Console.WriteLine($"stack contains 4? {stack.Contains(4)}");
Console.WriteLine($"stack2 contains 4? {stack2.Contains(4)}");
