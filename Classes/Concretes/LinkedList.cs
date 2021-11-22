using System.Collections;
using System.Collections.Generic;

namespace LinkedList;

public class LinkedList<T>: IEnumerable<T>
{
    private Node? startNode;
    private Node? endNode;

    public LinkedList()
    {
        startNode = null;
        endNode = null;
    }

    public void Add(T value)
    {
        Node newNode = new Node(value);
        if (endNode == null) {
            startNode = newNode;
            endNode = startNode;
        } else {
            endNode.Next = newNode;
            endNode = newNode;
        }
    }

    public T At(int index)
    {
        Node? node;
        try {
            node = Get(index);
        } catch (IndexOutOfRangeException) {
            throw;
        }
        return node.Value;
    }

    public void Insert(int index, T value)
    {
        // Create a new node
        Node newNode = new Node(value);
        // Get the node currently at the index to be inserted into
        Node toMoveUp;
        try {
            toMoveUp = Get(index);
        } catch (IndexOutOfRangeException) {
            throw;
        }
        // Get the node that's before the node to move up, if it exists
        Node? nodeBefore;
        try {
           nodeBefore = Get(index-1);
        } catch (ArgumentException) {
           nodeBefore = null;
        }
        // Stitch the new node into the list
        newNode.Next = toMoveUp;
        if (nodeBefore != null) {
            nodeBefore.Next = newNode;
        } else {
            startNode = newNode;
        }
    }

    public void Replace(int index, T value)
    {
        Node toReplace = Get(index);
        toReplace.Value = value;
    }

    public T Pop(int index)
    {
        // Get the Node to pop
        Node toPop = Get(index);
        // Restitch the list back together if needed
        Node? oldNext = toPop.Next ?? null;
        Node oldPrev = Get(index-1);
        if (endNode == toPop) {
            endNode = oldPrev;
        }
        if (oldNext != null) {
            oldPrev.Next = oldNext;
        } else {
            oldPrev.Next = null;
        }
        // return the popped value
        return toPop.Value;
    }

    public void Swap(int a, int b)
    {
        Node nodeA = Get(a);
        Node nodeB = Get(b);
        T valueA = nodeA.Value;
        T valueB = nodeB.Value;
        nodeA.Value = valueB;
        nodeB.Value = valueA;
    }

    private Node Get(int index)
    {   
        // If there are no nodes
        if (startNode == null) throw new NullReferenceException();
        // If the index is less than 0
        if (index < 0) throw new ArgumentException();
        // If index == 0 just return the start node
        if (index == 0) return startNode;
        // Starting at the start node
        Node currentNode = startNode;
        int currentIndex = 0;
        while (currentIndex < index) {
            if (currentNode.Next == null) {
                throw new IndexOutOfRangeException();
            }
            currentNode = currentNode.Next;
            currentIndex++;
        }
        return currentNode;
    }

    // Interface methods
    public IEnumerator<T> GetEnumerator() 
    {
        Node? currentNode = startNode;
        if (currentNode == null) yield break;
        while (currentNode != null) {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Other methods
    public override string ToString()
    {
        string output = "";
        Node current = Get(0);
        while (true) {
            // Add the current node's value to the output string
            output += current.ToString();
            output += " -> ";
            // If there is a next node, set the current node to it
            if (current.Next != null) {
                current = current.Next;
            } else break; // else break from the loop and return the output string
        }
        return output;
    }

    // Node class - only used from within this class
    internal class Node
    {
        internal T Value {get; set;}
        internal Node? Next {get; set;} = null;

        internal Node(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}