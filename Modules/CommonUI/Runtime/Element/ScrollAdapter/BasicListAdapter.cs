namespace Feature.Modules.CommonUI.Runtime.Element.ScrollAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Cysharp.Threading.Tasks;
    using Feature.Modules.CommonUI.Runtime.Element.ScrollAdapter.CellView;
    using GameCore.Extensions.VContainer;
    using GameCore.Services.ScreenFlow.Base.Item;
    using Plugins.EnhancedScroller_v2.Plugins;
    using UnityEngine;
    using UnityEngine.UI;
    using VContainer;

    public abstract class BasicListAdapter<TModel, TView, TPresenter> : BaseAdapter<PrefabCellViewHolder>
        where TView : BaseItemView
        where TPresenter : BaseItemPresenter<TView, TModel>
    {
        public SmallList<TModel> Models { get; } = new();

        private Dictionary<TView, TPresenter> viewToPresenterCache = new();
        private LayoutElement                 prefabLayout;
        private IObjectResolver               resolver;

        protected override PrefabCellViewHolder CreateCellViewHolder()
        {
            var newInstance = new GameObject("CellView");
            newInstance.SetActive(false);
            newInstance.transform.SetParent(this.transform);

            var cellViewHolder = newInstance.AddComponent<PrefabCellViewHolder>();
            cellViewHolder.cellIdentifier = $"{nameof(PrefabCellViewHolder)}-{typeof(TView).Name}";

            return cellViewHolder;
        }

#region Public Method

        public void InitAdapter(IEnumerable<TModel> models, IObjectResolver objectResolver)
        {
            this.resolver = objectResolver;
            foreach (var model in models)
            {
                this.Models.Add(model);
            }

            this.ReloadData();
        }

        public override void ScrollTo
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
        )
        {
            this.JumpToDataIndex(modelIndex, scrollerOffset, cellOffset, useSpacing, tweenType, tweenTime, jumpComplete, loopJumpDirection, forceCalculateRange);
        }

#endregion

        private TPresenter GetPresenterByView(TView view)
        {
            if (this.viewToPresenterCache.TryGetValue(view, out var presenter))
            {
                return presenter;
            }

            var instancePresenter = this.resolver.InstantiateConcrete<TPresenter>();
            instancePresenter.SetView(view);
            instancePresenter.OpenView();
            instancePresenter.InitView();
            this.viewToPresenterCache.Add(view, instancePresenter);

            return instancePresenter;
        }

        private LayoutElement GetPrefabLayout()
        {
            if (this.prefabLayout != null) return this.prefabLayout;
            if (this.prefab.TryGetComponent(out this.prefabLayout))
            {
                return this.prefabLayout;
            }

            throw new Exception($"Couldn't find component: {nameof(LayoutElement)} in prefab: {this.prefab.name}");
        }

#region Implement IEnhancedScrollerDelegate

        public override int GetNumberOfCells(EnhancedScroller scroller)
        {
            return this.Models.Count;
        }

        public override float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return this.scrollDirection switch
                   {
                       ScrollDirectionEnum.Vertical   => this.GetPrefabLayout().preferredHeight,
                       ScrollDirectionEnum.Horizontal => this.GetPrefabLayout().preferredWidth,
                       _                              => throw new ArgumentOutOfRangeException()
                   };
        }

        public override EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = this.GetCellViewHolderByScroller(scroller);

            if (cellView == null) return null;
            cellView.gameObject.SetActive(true);
            cellView.gameObject.name = $"CellView: {cellIndex}, Data: {dataIndex}";
            var child = cellView.GetOrCreateChild(this.prefab);
            child.SetActive(true);

            var model     = this.Models[dataIndex];
            var view      = child.GetComponent<TView>();
            var presenter = this.GetPresenterByView(view);
            presenter.BindData(model);

            return cellView;
        }

#endregion
    }
}