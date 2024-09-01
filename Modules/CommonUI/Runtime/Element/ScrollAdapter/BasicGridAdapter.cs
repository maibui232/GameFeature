namespace Feature.Modules.CommonUI.Runtime.Element.ScrollAdapter
{
    using System;
    using System.Collections.Generic;
    using Feature.Modules.CommonUI.Runtime.Element.ScrollAdapter.CellView;
    using GameCore.Extensions;
    using GameCore.Extensions.VContainer;
    using GameCore.Services.ScreenFlow.Base.Item;
    using Plugins.EnhancedScroller_v2.Plugins;
    using UnityEngine;
    using UnityEngine.UI;
    using VContainer;

    public enum GridChildAlignment
    {
        Start, Middle, End
    }

    public abstract class BasicGridAdapter<TModel, TView, TPresenter> : BaseAdapter<GroupCellViewHolder>
        where TView : BaseItemView
        where TPresenter : BaseItemPresenter<TView, TModel>
    {
        [SerializeField] private GridChildAlignment childAlignment;
        [SerializeField] private bool               expandWidth;
        [SerializeField] private bool               expandHeight;

        public SmallList<TModel> Models { get; } = new();

        private Dictionary<TView, TPresenter> viewToPresenter = new();
        private LayoutElement                 prefabLayoutElement;
        private RectTransform                 rectTransform;
        private int                           numberCellViewInHolder;
        private IObjectResolver               resolver;

        protected override GroupCellViewHolder CreateCellViewHolder()
        {
            var newInstance = new GameObject("CellView");
            newInstance.SetActive(false);
            newInstance.transform.SetParent(this.transform);

            var cellViewHolder = newInstance.AddComponent<GroupCellViewHolder>();
            cellViewHolder.cellIdentifier = $"{nameof(GroupCellViewHolder)}-{typeof(TView).Name}";
            HorizontalOrVerticalLayoutGroup group = this.scrollDirection switch
                                                    {
                                                        ScrollDirectionEnum.Vertical   => newInstance.AddComponent<HorizontalLayoutGroup>(),
                                                        ScrollDirectionEnum.Horizontal => newInstance.AddComponent<VerticalLayoutGroup>(),
                                                        _                              => throw new ArgumentOutOfRangeException()
                                                    };
            group.childControlHeight = this.expandHeight;
            group.childControlWidth  = this.expandWidth;
            group.spacing            = this.spacing;
            group.childAlignment = this.childAlignment switch
                                   {
                                       GridChildAlignment.Start => this.scrollDirection switch
                                                                   {
                                                                       ScrollDirectionEnum.Vertical   => TextAnchor.MiddleLeft,
                                                                       ScrollDirectionEnum.Horizontal => TextAnchor.UpperCenter,
                                                                       _                              => throw new ArgumentOutOfRangeException()
                                                                   },
                                       GridChildAlignment.Middle => this.scrollDirection switch
                                                                    {
                                                                        ScrollDirectionEnum.Vertical   => TextAnchor.MiddleCenter,
                                                                        ScrollDirectionEnum.Horizontal => TextAnchor.MiddleCenter,
                                                                        _                              => throw new ArgumentOutOfRangeException()
                                                                    },
                                       GridChildAlignment.End => this.scrollDirection switch
                                                                 {
                                                                     ScrollDirectionEnum.Vertical   => TextAnchor.MiddleRight,
                                                                     ScrollDirectionEnum.Horizontal => TextAnchor.LowerCenter,
                                                                     _                              => throw new ArgumentOutOfRangeException()
                                                                 },
                                       _ => throw new ArgumentOutOfRangeException()
                                   };

            return cellViewHolder;
        }

        private float PrefabCellViewSize()
        {
            this.prefabLayoutElement ??= this.prefab.GetComponent<LayoutElement>();

            return this.scrollDirection switch
                   {
                       ScrollDirectionEnum.Vertical   => this.prefabLayoutElement.preferredWidth,
                       ScrollDirectionEnum.Horizontal => this.prefabLayoutElement.preferredHeight,
                       _                              => throw new ArgumentOutOfRangeException()
                   };
        }

        private float GetCellViewHolderSize()
        {
            this.rectTransform ??= this.GetComponent<RectTransform>();

            return this.scrollDirection switch
                   {
                       ScrollDirectionEnum.Vertical   => this.rectTransform.rect.size.x,
                       ScrollDirectionEnum.Horizontal => this.rectTransform.rect.size.y,
                       _                              => throw new ArgumentOutOfRangeException()
                   };
        }

        private int NumberCellViewInHolder()
        {
            if (this.numberCellViewInHolder != 0) return this.numberCellViewInHolder;
            this.numberCellViewInHolder = Mathf.FloorToInt(this.GetCellViewHolderSize() / this.PrefabCellViewSize());

            return this.numberCellViewInHolder;
        }

        private TPresenter GetPresenterByView(TView view)
        {
            if (this.viewToPresenter.TryGetValue(view, out var presenter))
            {
                return presenter;
            }

            var instancePresenter = this.resolver.InstantiateConcrete<TPresenter>();
            instancePresenter.SetView(view);
            instancePresenter.OpenView();
            instancePresenter.InitView();
            this.viewToPresenter.Add(view, instancePresenter);

            return instancePresenter;
        }

#region Public Method

        public void InitAdapter(IEnumerable<TModel> models, IObjectResolver resolver)
        {
            this.resolver = resolver;
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
            var index = modelIndex / this.NumberCellViewInHolder();
            this.JumpToDataIndex(index, scrollerOffset, cellOffset, useSpacing, tweenType, tweenTime, jumpComplete, loopJumpDirection, forceCalculateRange);
        }

#endregion

        private int CalculateNumberOfCells()
        {
            var sur = this.Models.Count % this.NumberCellViewInHolder();
            if (sur == 0)
            {
                return this.Models.Count / this.NumberCellViewInHolder();
            }

            return this.Models.Count / this.NumberCellViewInHolder() + 1;
        }

#region Implement IEnhancedScrollerDelegate

        public override int GetNumberOfCells(EnhancedScroller scroller)
        {
            return this.CalculateNumberOfCells();
        }

        public override float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return this.PrefabCellViewSize();
        }

        public override EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = this.GetCellViewHolderByScroller(scroller);

            if (cellView == null) return null;
            cellView.gameObject.SetActive(true);
            cellView.gameObject.name = $"CellView: {cellIndex}, Data: {dataIndex}";

            var startIndex  = dataIndex * this.NumberCellViewInHolder();
            var targetIndex = startIndex + this.NumberCellViewInHolder();

            for (var i = startIndex; i < targetIndex; i++)
            {
                var childIndex = i % this.NumberCellViewInHolder();
                var child      = cellView.GetOrCreateChild(childIndex, this.prefab);

                if (this.Models.Count <= i)
                {
                    child.SetActive(false);

                    break;
                }

                child.SetActive(true);

                var model     = this.Models[i];
                var view      = child.GetComponent<TView>();
                var presenter = this.GetPresenterByView(view);
                presenter.BindData(model);
            }

            return cellView;
        }

#endregion
    }
}