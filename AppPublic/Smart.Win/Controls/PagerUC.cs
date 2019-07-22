using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public partial class PagerUC : XtraUserControl
    {

        #region [Construction]

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PagerUC()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                _comboPageSize.Properties.Items.Clear();
                _comboPageSize.Properties.Items.AddRange(PageSizeAllow);
            }
            _comboPageSize.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            _comboCurrentPage.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            BindControlEvents();
        }

        private void DataPagerUC_Load(object sender, EventArgs e)
        {

        }

        private void BindControlEvents()
        {
            _btnFirstPage.Click += btnFirstPage_Click;
            _btnLastPage.Click += btnLastPage_Click;
            _btnNextPage.Click += btnNextPage_Click;
            _btnPrevPage.Click += btnPrevPage_Click;
            ////_comboCurrentPage.EditValueChanged += _comboCurrentPage_EditValueChanged;
            ////_comboPageSize.EditValueChanged += _comboPageSize_EditValueChanged;
        }

        #endregion

        #region [Property]

        private List<int> _pageSizeAllow = new List<int> { 20, 50, 100, 150, 200, 300 };
        /// <summary>
        /// 允许每页显示条数
        /// </summary>
        private List<int> PageSizeAllow => _pageSizeAllow;

        /// <summary>
        /// 设置分页条数下拉框
        /// </summary>
        /// <param name="sizeList">分页条数列表</param>
        public void SetPageSizeAllow(List<int> sizeList)
        {
            if (sizeList == null || sizeList.Count == 0) { return; }
            _pageSizeAllow = sizeList;
            if (!_pageSizeAllow.Contains(PageSize) && _pageSizeAllow.Contains(50))
            {
                _pageSize = 50;
            }
            else
            {
                _pageSize = _pageSizeAllow[_pageSizeAllow.Count - 1];
            }
            if (!DesignMode)
            {
                _comboPageSize.Properties.Items.Clear();
                _comboPageSize.Properties.Items.AddRange(_pageSizeAllow);
                _comboPageSize.EditValue = _pageSize;
            }
        }

        private int _pageSize = 50;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("PageSize必须大于0。");
                }

                if (_pageSize != value)
                {
                    if (!PageSizeAllow.Contains(value))
                    {
                        value = PageSizeAllow[PageSizeAllow.Count - 1];
                    }
                    _pageSize = value;
                    _comboPageSize.EditValueChanged -= _comboPageSize_EditValueChanged;
                    _comboPageSize.EditValue = _pageSize;
                    var pCount = (TotalRecord / value) + (TotalRecord % value > 0 ? 1 : 0);
                    PageCount = pCount;
                    SetButtonState();
                    if (!firePageChangedEvent)
                    {
                        firePageChangedEvent = true;
                        OnPageChanged();
                    }
                    _comboPageSize.EditValueChanged += _comboPageSize_EditValueChanged;
                }
            }
        }
        /// <summary>
        /// 设置分页参数
        /// </summary>
        /// <param name="totalRecord">数据总条数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页码</param>
        public void SetPagerParameter(int totalRecord, int pageSize, int pageIndex)
        {
            firePageChangedEvent = true;
            TotalRecord = totalRecord;
            PageSize = pageSize;
            PageIndex = pageIndex;
            firePageChangedEvent = false;
        }

        private bool firePageChangedEvent;

        private int _recordCount;
        /// <summary>
        /// 总记录条数
        /// </summary>
        public int TotalRecord
        {
            get
            {
                if (_recordCount <= 0) { return 0; }
                return _recordCount;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("RecordCount不能小于0。");
                }
                if (_recordCount != value)
                {
                    _recordCount = value;
                    var pCount = (_recordCount / PageSize) + (_recordCount % PageSize > 0 ? 1 : 0);
                    PageCount = pCount;
                    _lblTotalNum.Text = $@"共{TotalRecord}条";
                    SetButtonState();
                    if (!firePageChangedEvent)
                    {
                        firePageChangedEvent = true;
                        OnPageChanged();
                    }
                }
            }
        }

        private int _pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (_pageCount <= 0) { return 0; }
                return _pageCount;
            }
            private set
            {
                if (value != _pageCount)
                {
                    _comboCurrentPage.EditValueChanged -= _comboCurrentPage_EditValueChanged;
                    _comboCurrentPage.Properties.Items.Clear();
                    for (var i = 1; i <= value; i++)
                    {
                        _comboCurrentPage.Properties.Items.Add(i);
                    }
                    _comboCurrentPage.EditValueChanged += _comboCurrentPage_EditValueChanged;
                    _pageCount = value;
                    _lblTotalPage.Text = _pageCount + @"页";
                }
            }
        }

        private int _pageIndex = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (_pageIndex <= 0) { return 1; }
                return _pageIndex;
            }
            private set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                if (value > PageCount)
                {
                    value = PageCount;
                }
                if (value != _pageIndex)
                {
                    _pageIndex = value;
                    _comboCurrentPage.EditValueChanged -= _comboCurrentPage_EditValueChanged;
                    _comboCurrentPage.EditValue = _pageIndex;
                    _comboCurrentPage.EditValueChanged += _comboCurrentPage_EditValueChanged;
                    SetButtonState();
                    if (!firePageChangedEvent)
                    {
                        firePageChangedEvent = true;
                        OnPageChanged();
                    }
                }
            }
        }

        #endregion

        #region [Button]

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            firePageChangedEvent = false;
            PageIndex = 1;
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            firePageChangedEvent = false;
            PageIndex = PageIndex - 1;
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            firePageChangedEvent = false;
            PageIndex = PageIndex + 1;
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            firePageChangedEvent = false;
            PageIndex = PageCount;
        }

        #endregion

        #region [Combo]

        private void _comboCurrentPage_EditValueChanged(object sender, EventArgs e)
        {
            var curPage = (int)_comboCurrentPage.EditValue;
            firePageChangedEvent = false;
            PageIndex = curPage;
        }

        private void _comboPageSize_EditValueChanged(object sender, EventArgs e)
        {
            var comboPageSize = (int)_comboPageSize.EditValue;
            firePageChangedEvent = false;
            var pCount = (TotalRecord / comboPageSize) + (TotalRecord % comboPageSize > 0 ? 1 : 0);
            if (PageIndex > pCount)
            {
                _pageSize = comboPageSize;
                PageIndex = pCount;
                PageCount = pCount;
                SetButtonState();
                firePageChangedEvent = true;
            }
            PageSize = comboPageSize;
        }

        #endregion

        #region [SetControlState]

        /// <summary>
        /// 设置控件状态
        /// </summary>
        private void SetButtonState()
        {
            ResetControl();

            if (_pageIndex > 1)
            {
                _btnFirstPage.Enabled = true;
                _btnPrevPage.Enabled = true;
            }
            if (_pageIndex < PageCount)
            {
                _btnNextPage.Enabled = true;
                _btnLastPage.Enabled = true;
            }
        }

        /// <summary>
        /// 初始化控件状态
        /// </summary>
        private void ResetControl()
        {
            _btnFirstPage.Enabled = false;
            _btnPrevPage.Enabled = false;
            _btnNextPage.Enabled = false;
            _btnLastPage.Enabled = false;
        }

        #endregion

        #region [Event]

        /// <summary>
        /// 页改变事件
        /// </summary>
        public event EventHandler PageChanged;

        /// <summary>
        /// 激发页面改变事件
        /// </summary>
        public void OnPageChanged()
        {
            if (PageChanged != null)
            {
                PageChanged(this, null);
            }
        }

        #endregion



    }
}
