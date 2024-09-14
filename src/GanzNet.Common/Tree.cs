using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzNet.Common
{
    public class Tree<T>
    {
        public T Item { get; set; }
        public List<Tree<T>> Children { get; set; } = new List<Tree<T>>();

        public bool HasChildren
        { 
            get
            {
                return this.Children?.Count > 0;
            }
        }
    }
}
