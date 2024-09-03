namespace Modules.CommonUI.Runtime.Element.ScrollAdapter
{
    using System;
    using Modules.CommonUI.Runtime.Element.ScrollAdapter.CellView;
    using Plugins.EnhancedScroller_v2.Plugins;
    using UnityEngine;

    public abstract class BaseAdapter<TCellViewHolder> : EnhancedScroller, IEnhancedScrollerDelegate where TCellViewHolder : BaseCellViewHolder
    {
        [SerializeField] protected GameObject prefab;

        protected TCellViewHolder CellViewHolder;

        protected virtual void Start()
        {
            this.Delegate       = this;
            this.CellViewHolder = this.CreateCellViewHolder();
            if (string.IsNullOrEmpty(this.CellViewHolder.cellIdentifier))
            {
                throw new Exception($"{this.GetType().Name} doesn't optimized, set {nameof(this.CellViewHolder.cellIdentifier)} please!");
            }
        }

        protected abstract TCellViewHolder CreateCellViewHolder();

        protected TCellViewHolder GetCellViewHolderByScroller(EnhancedScroller scroller)
        {
            return scroller.GetCellView(this.CellViewHolder) as TCellViewHolder;
        }

        public abstract void ScrollTo
        (
            int                   modelIndex,
            float                 scrollerOffset      = 0,
            float                 cellOffset          = 0,
            bool                  useSpacing          = true,
            TweenType             tweenType           = TweenType.linear,
            float                 tweenTime           = 0.5f,
            Action                jumpComplete        = null,
            LoopJumpDirectionEnum loopJumpDirection   = LoopJumpDirectionEnum.Closest,
            bool                  forceCalculateRange = false
        );

        public abstract int GetNumberOfCells(EnhancedScroller scroller);

        public abstract float GetCellViewSize(EnhancedScroller scroller, int dataIndex);

        public abstract EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex);
    }
}