namespace Feature.Modules.CommonUI.Runtime.Element.ScrollAdapter.CellView
{
    using UnityEngine;

    public class PrefabCellViewHolder : BaseCellViewHolder
    {
        private GameObject child;

        public GameObject GetOrCreateChild(GameObject prefab)
        {
            if (this.child != null) return this.child;

            this.child = Instantiate(prefab, this.transform);
            this.child.transform.localPosition = Vector3.zero;
            this.child.transform.localRotation = Quaternion.identity;

            return this.child;
        }
    }
}