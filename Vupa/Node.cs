using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vupa
{
    class Node : IComparable<Node>
    {
        private int f;

        public int F
        {
            get { return f; }
            set { f = value; }
        }

        private int g;

        private int h;

        public int H
        {
            get { return h; }
            set { h = value; }
        }

        Point position;

        Node parent;

        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public int G
        {
            get { return g; }
            set { g = value; }
        }

        public Node(Point position)
        {
            this.position = position;
        }

        //calculates the G, h, f values
        public void CalcValues(Node parentNode, Node goalNode, int cost)
        {
            parent = parentNode;

            g = parentNode.G + cost;

            h = ((Math.Abs(position.X - goalNode.position.X) + Math.Abs(goalNode.position.Y - position.Y)) * 10);

            f = h + g;
        }

        //compares the nodes by checking if the values of the nodes around the currentnode is higher, lower, equal
        public int CompareTo(Node other)
        {
            if (f > other.f)
            {
                return 1;
            }
            else if (f < other.f)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
