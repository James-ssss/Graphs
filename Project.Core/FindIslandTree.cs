using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    class FindIslandTree
    {
        public static void Finding()
        {
            int cost = 0;
            List<Tuple<int, Tuple<int, int>>> g = new List<Tuple<int, Tuple<int, int>>>(); //лист всех рёбер графа
            List<Tuple<int, int>> res = new List<Tuple<int, int>>();

            int n = g.Count;
            g.Sort(); //сортируем рёбра

            List<int> treeId = new();
            int m = treeId.Count;
            for (int i = 0; i < m; ++i)
            {
                treeId[i] = i;
            }
            for (int i = 0; i < n; ++i)
            {
                int a = g[i].Item2.Item1, b = g[i].Item2.Item2, l = g[i].Item1;
                if (treeId[a] != treeId[b])
                {
                    cost += l;
                    Tuple<int, int> pair = new Tuple<int, int>(a,b);
                    res.Add(pair);
                    int oldId = treeId[b], newId = treeId[a];
                    for (int j = 0; j < m; ++j)
                    {
                        if (treeId[j] == oldId)
                        {
                            treeId[j] = newId;
                        }
                    }
                }
            }
        }
    }
}
