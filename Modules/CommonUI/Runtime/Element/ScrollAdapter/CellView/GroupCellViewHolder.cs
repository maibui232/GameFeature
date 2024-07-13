namespace Feature.Modules.CommonUI.Runtime.Element.ScrollAdapter.CellView
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GroupCellViewHolder : BaseCellViewHolder
    {
        private Dictionary<int, GameObject> indexToChild = new();

        public GameObject GetOrCreateChild(int index, GameObject prefab)
        {
            if (this.indexToChild.TryGetValue(index, out var child))
            {
                return child;
            }

            var instanceChild = Instantiate(prefab, this.transform);
            this.indexToChild.Add(index, instanceChild);

            return instanceChild;
        }
    }
}