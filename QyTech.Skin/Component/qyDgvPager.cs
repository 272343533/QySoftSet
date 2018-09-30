using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QyTech.SkinForm.Component
{

    /// <summary>
    /// 声明委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void EventPagingHandler(EventArgs e);
    public partial class qyDgvPager : UserControl
    {
        public qyDgvPager()
        {
            InitializeComponent();
        }
        public event EventPagingHandler EventPaging;
        #region 公开属性
        private int _pageSize = 50;
        /// <summary>
        /// 每页显示记录数(默认50)
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > 0)
                {
                    _pageSize = value;
                }
                else
                {
                    _pageSize = 50;
                }
                this.comboPageSize.Text = _pageSize.ToString();
            }
        }
        private int _currentPage = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (value > 0)
                {
                    _currentPage = value;
                }
                else
                {
                    _currentPage = 1;
                }

            }
        }
        private int _totalCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                if (value >= 0)
                {
                    _totalCount = value;
                }
                else
                {
                    _totalCount = 0;
                }
                this.lblTotalCount.Text = this._totalCount.ToString();
                CalculatePageCount();
                this.lblRecordRegion.Text = GetRecordRegion();
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                if (value >= 0)
                {
                    _pageCount = value;
                }
                else
                {
                    _pageCount = 0;
                }
                this.lblPageCount.Text = _pageCount + "";
            }
        }
        #endregion

        /// <summary>
        /// 计算页数
        /// </summary>
        private void CalculatePageCount()
        {
            if (this.TotalCount > 0)
            {
                this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.TotalCount) / Convert.ToDouble(this.PageSize)));
            }
            else
            {
                this.PageCount = 0;
            }
        }

        /// <summary>
        /// 获取显示记录区间（格式如：1-50）
        /// </summary>
        /// <returns></returns>
        private string GetRecordRegion()
        {
            if (this.PageCount == 1) //只有一页
            {
                return "1-" + this.TotalCount.ToString();
            }
            else  //有多页
            {
                if (this.CurrentPage == 1) //当前显示为第一页
                {
                    return "1-" + this.PageSize;
                }
                else if (this.CurrentPage == this.PageCount) //当前显示为最后一页
                {
                    return ((this.CurrentPage - 1) * this.PageSize + 1) + "-" + this.TotalCount;
                }
                else //中间页
                {
                    return ((this.CurrentPage - 1) * this.PageSize + 1) + "-" + this.CurrentPage * this.PageSize;
                }
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        public void Bind()
        {
            if (this.EventPaging != null)
            {
                this.EventPaging(new EventArgs());
            }
            if (this.CurrentPage > this.PageCount)
            {
                this.CurrentPage = this.PageCount;
            }
            this.txtBoxCurPage.Text = this.CurrentPage + "";
            this.lblTotalCount.Text = this.TotalCount + "";
            this.lblPageCount.Text = this.PageCount + "";
            this.lblRecordRegion.Text = GetRecordRegion();
            if (this.CurrentPage == 1)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
                //this.btnFirst.Image = global::CHVM.Properties.Resources.page_first_disabled;
                //this.btnPrev.Image = global::CHVM.Properties.Resources.page_prev_disabled;
            }
            else
            {
                this.btnFirst.Enabled = true;
                this.btnPrev.Enabled = true;
                //this.btnFirst.Image = global::CHVM.Properties.Resources.page_first;
                //this.btnPrev.Image = global::CHVM.Properties.Resources.page_prev;
            }
            if (this.CurrentPage == this.PageCount)
            {
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                //this.btnNext.Image = global::CHVM.Properties.Resources.page_next_disabled;
                //this.btnLast.Image = global::CHVM.Properties.Resources.page_last_disabled;
            }
            else
            {
                this.btnNext.Enabled = true;
                this.btnLast.Enabled = true;
                //this.btnNext.Image = global::CHVM.Properties.Resources.page_next;
                //this.btnLast.Image = global::CHVM.Properties.Resources.page_last;
            }
            if (this.TotalCount == 0)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                //this.btnFirst.Image = global::CHVM.Properties.Resources.page_first_disabled;
                //this.btnPrev.Image = global::CHVM.Properties.Resources.page_prev_disabled;
                //this.btnNext.Image = global::CHVM.Properties.Resources.page_next_disabled;
                //this.btnLast.Image = global::CHVM.Properties.Resources.page_last_disabled;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.CurrentPage = 1;
            this.Bind();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.CurrentPage -= 1;
            this.Bind();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.CurrentPage += 1;
            this.Bind();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.CurrentPage = this.PageCount;
            this.Bind();
        }

        /// <summary>
        ///  改变每页条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PageSize = Convert.ToInt32(comboPageSize.Text);
            this.Bind();
        }
    }


    //这里重点提两点：一是图片切换：
    //this.btnFirst.Image = global::CHVM.Properties.Resources.page_first_disabled;
    //Image对象是在Properties.Resource.resx中自动生成的，代码如下：
    //internal static System.Drawing.Bitmap page_first
    //{
    //    get
    //    {
    //        object obj = ResourceManager.GetObject("page-first", resourceCulture);
    //        return ((System.Drawing.Bitmap)(obj));
    //    }
    //}

    //internal static System.Drawing.Bitmap page_first_disabled
    //{
    //    get
    //    {
    //        object obj = ResourceManager.GetObject("page_first_disabled", resourceCulture);
    //        return ((System.Drawing.Bitmap)(obj));
    //    }
    //}
    //二是应用了委托事件：我们在这定义了一个分页事件
    //public event EventPagingHandler EventPaging;
    //在数据绑定方法中实现它：
    ///// <summary>
    ///// 数据绑定
    ///// </summary>
    //public void Bind()
    //{
    //    if (this.EventPaging != null)
    //    {
    //        this.EventPaging(new EventArgs());
    //    }
    //    //… 以下省略
    //}
    //这里需要大家对C#的委托和事件有一定的了解，不清楚的可以直接使用，或者先去查阅相关参考资料，这里我们就不谈委托机制了。

    //第三步：应用
    //值得一提的是，WinForm并不能直接把用户自定控件往Windows窗体中拖拽，而自动生成实例（ASP.NET是可以直接拖拽的）。那么如果我们需要在应用中使用，只能自己修改Desginer.cs代码了。
    //先声明：
    //private CHVM.PagingControl.Paging paging1;
    //然后在InitializeComponent() 方法中实例化：
    //this.paging1 = new CHVM.PagingControl.Paging();
    ////
    //// paging1
    ////
    //this.paging1.CurrentPage = 1;
    //this.paging1.Location = new System.Drawing.Point(3, 347);
    //this.paging1.Name = "paging1";
    //this.paging1.PageCount = 0;
    //this.paging1.PageSize = 50;
    //this.paging1.Size = new System.Drawing.Size(512, 30);
    //this.paging1.TabIndex = 8;
    //this.paging1.TotalCount = 0;
    ////在这里注册事件
    //this.paging1.EventPaging += new CHVM.PagingControl.EventPagingHandler(this.paging1_EventPaging);
}