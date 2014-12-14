using System;

namespace BTreeExample
{
    public enum ComparePossibles { EQUAL_TO, LESS_THAN, MORE_THAN }
    
    class IntBTree
    {
        private IntNode root;

        public IntBTree()
        {
            this.root = null;
        }

        public IntBTree(IntData root)
        {
            Add(root);
        }

        public void Add(IntData d)
        {
            IntNode n = new IntNode(null, null, null, d);
            this.Add(n);
        }

        public void Add(IntNode n)
        {
            if (this.root == null)
            {
                // generate root
                this.root = n;
            }
            else
            {
                if (n.p == null)
                    n.p=this.root;

                if (n.compare(n.p) == ComparePossibles.LESS_THAN || // if less than or equal to
                    n.compare(n.p) == ComparePossibles.EQUAL_TO)    // it goes left
                {
                    // traverse or insert node on left tree
                    if (n.p.l == null)
                    { // at the end of the tree, insert here
                        n.p.l = n;
                    }
                    else
                    {
                        n.p = n.p.l;  // recurse, try again
                        this.Add(n);
                    }
                }
                else
                {
                    // traverse or insert node on right tree
                    if (n.p.r == null)
                    {
                        n.p.r = n; // right child of parent 
                    }
                    else
                    {
                        n.p = n.p.r; // parent and equals right child of parent
                        this.Add(n);
                    }
                }

            }

        }

        public void WriteInOrder()
        { WriteInOrder(this.root); }

        public void WriteInOrder(IntNode n)
        {
            if (n.l != null)
                WriteInOrder(n.l);
            Console.Write("{0}\n", n.getData().getValue());
            if (n.r != null)
                WriteInOrder(n.r);
        }

        static void Main(string[] args)
        {
            // load tree with ints from cmd line

            IntBTree ibt = new IntBTree();
            for (int i=0; i<args.Length; i++)
                ibt.Add(new IntData(Int32.Parse(args[i])));
            
            ibt.WriteInOrder();
            Console.Write("Tree displayed.");
        }
    }

    interface Data<T> // base interface of type defined at compile time
    {
        ComparePossibles compare(T d);
        T getValue();
    }

    class IntData : Data<int> // int specific implementation of interface
    {
        private int value;

        public IntData(int value)
        {
            this.value = value;
        }

        public int getValue()
        {
            return this.value;
        }

        public ComparePossibles compare(int d)
        {
            if (d > this.value)
            {
                return ComparePossibles.MORE_THAN;
            }
            else if (d == this.value)
            {
                return ComparePossibles.EQUAL_TO;
            }
            else
            {
                return ComparePossibles.LESS_THAN;
            }
        }

    }

    class IntNode
    {
        private IntData c; // content 
        public IntNode l, r, p; // left, right, parent

        public IntNode(IntNode l, IntNode r, IntNode p, IntData c)
        {
            this.l = l; // null if tree subtree terminates
            this.r = r;
            this.p = p;
            this.c = c;
        }

        public IntData getData()
        {
            return this.c;
        }

        public ComparePossibles compare(IntNode n)
        {
            return n.getData().compare( c.getValue() );
        }
    }
}
