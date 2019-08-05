using UnityEngine;
using System.Collections;
using System;

public class MinHeap 
{
	// The nodes of this heap
	Node[] contents;
    // The number of elements in the heap
	int heapSize;
	
    /*
     * Construct a new min heap from an array.
     */
	public MinHeap (Node[] nodes)
    {
		heapSize = nodes.Length;
		contents = nodes;
		BuildMinHeap();
	}
	
    /*
     * Starting from the first non-leaf node, min-heapify contents.
     */
	void BuildMinHeap() 
    {
		int i;
		for(i=heapSize/2-1;i>=0;i--) 
        {
			MinHeapify(i);
		}
	}
	
    /*
     * Sort the array start from index i in min-heap order.
     */
	void MinHeapify(int i) 
    {
		int l = LeftChild(i);
		int r = RightChild(i);
		int smallest;
		if(l < heapSize && contents[l].Key < contents[i].Key) 
        {
			smallest = l;
		} 
        else 
        {
			smallest = i;
		}
		if(r < heapSize && contents[r].Key < contents[smallest].Key) 
        {
			smallest = r;
		}
		if(smallest!=i) 
        {
			Swap(i, smallest);
			MinHeapify(smallest);
		}
	}
	
    /*
     * Insert a new node into the correct position in the heap.
     */
	void Insert(Node node) {
        try
        {
            if (heapSize == contents.Length)
            {
                Node[] tempArray = new Node[2 * heapSize];
                contents.CopyTo(tempArray, 0);
                contents = tempArray;
            }
            contents[heapSize] = node;
            heapSize++;
            BubbleUp(heapSize);
        }
        catch (IndexOutOfRangeException)
        {
            contents = new Node[1] {node};
            heapSize++;
        }
		
	}
	
    /*
     * Recursively swap a node with its parent if its key is smaller
     * than its parent's key.
     */
	void BubbleUp(int i) 
    {
		if(i > 0 && contents[i].Key < contents[Parent(i)].Key) 
        {
			Swap(i, Parent(i));
			BubbleUp(Parent(i));
		}
	}
	
    /*
     * Swap the position of two nodes in the heap.
     */
	void Swap(int i, int j) 
    {
		Node temp1 = (Node)contents[i];
		Node temp2 = (Node)contents[j];
		contents[i] = temp2;
		contents[j] = temp1;
	}
	
    /*
     * Return the node with the smallest key value.
     */
	public Node ExtractMin() 
    {
        try
        {
            Node temp = (Node)contents[0];
            contents[0] = contents[heapSize - 1];
            heapSize--;
            MinHeapify(0);
            return temp;
        }
        catch (IndexOutOfRangeException)
        {
            throw new HeapEmptyException(
                "Error: extracting from empty heap");
        }		
	}
	
    /*
     * Return the index of the left child of the node at index i.
     */
	int LeftChild(int i) 
    {
		return i*2+1;
	}
	
    /*
     * Return the index of the right child of the node at index i.
     */
	int RightChild(int i) 
    {
		return i*2+2;
	}
	
    /*
     * Return the index of the parent of the node at index i.
     */
	int Parent(int i) 
    {
		return (i-2)/2;
	}
	
    /*
     * Print the value of the key of each node in the heap.
     */
	void PrintHeap() 
    {
		foreach(Node node in contents) 
        {
			Console.WriteLine("{0} ", node.Key);
		}
	}
	
    /*
     * A node of a min-heap.
     */
	public class Node {
	    // The distance between the soldier and the goal
		float key;
        // The soldier this node represents
        GameObject soldier;

        /*
         * Property for key field
         */
        public float Key
        {
            get { return key; }
            set { key = value; }
        }

        /*
         * Property for soldier field.
         */
        public GameObject Soldier
        {
            get { return soldier; }
            set { soldier = value; }
        }
		
        /*
         * Create a new node for a min-heap.
         */
		public Node(float data, GameObject soldierObject) 
        {
			key = data;
            soldier = soldierObject;
		}
	}
}