using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace SmartSolution.Utilities.Win.Controls
{
    public partial class ColumnShowUc : XtraUserControl
    {

        #region [Construction]

        //private GridView _view;
        //private string _employeeId = string.Empty;
        //private List<string> _lockColumn;
        private string _gridId = string.Empty;
        private readonly List<GridColumn> _allGridColumn = new List<GridColumn>();
        /// <summary>
        /// 
        /// </summary>
        public ColumnShowUc()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="lockColumn"></param>
        /// <param name="gridID"></param>
        /// <param name="employeeID"></param>
        public void InitUC(GridView view,List<string> lockColumn,string gridID,string employeeID)
        {
            //this._view = view;
            //this._employeeId = employeeID;
            //this._lockColumn = lockColumn;
            _gridId = gridID;

            foreach (GridColumn column in view.Columns)
            {
                if (column.VisibleIndex >= 0) { _allGridColumn.Add(column); }
            }
            SetViewShow();
            BindControl();
            BindEvents();
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindEvents()
        {
            ctrListShow.MouseDoubleClick += ctrListShow_MouseDoubleClick;
            ctrListHide.MouseDoubleClick += ctrListHide_MouseDoubleClick;
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindControl()
        {
            var shows = new List<ShowColumnItem>();
            var hides = new List<ShowColumnItem>();
            var fieldDic = new Dictionary<string, int>();
            if (AllShowFieldSetting.ContainsKey(_gridId))
            {
                var fields = AllShowFieldSetting[_gridId].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                
                var order = 0;
                foreach (var field in fields)
                {
                    fieldDic[field] = order++;
                }
                foreach (var column in _allGridColumn)
                {
                    if (fieldDic.ContainsKey(column.FieldName))
                    {
                        column.VisibleIndex = fieldDic[column.FieldName];
                    }
                    else
                    {
                        column.VisibleIndex = -1;
                    }
                }
            }
            foreach (var column in _allGridColumn)
            {
                if (column.Visible)
                {
                    shows.Add(new ShowColumnItem
                    {
                        ColumnCaption = column.Caption,
                        ColumnField = column.FieldName,
                        ImageIndex = 0
                    });
                }
                else
                {
                    hides.Add(new ShowColumnItem
                    {
                        ColumnCaption = column.Caption,
                        ColumnField = column.FieldName,
                        ImageIndex = 1
                    });
                }
            }
            if (fieldDic.Count > 0)
            {
                shows.Sort(delegate(ShowColumnItem one, ShowColumnItem two)
                {
                    if (fieldDic.ContainsKey(one.ColumnField) && fieldDic.ContainsKey(two.ColumnField))
                    {
                        if (fieldDic[one.ColumnField] > fieldDic[two.ColumnField]) return 1;
                        if (fieldDic[one.ColumnField] == fieldDic[two.ColumnField]) return 0;
                        return -1;
                    }
                    return -1;
                });
            }
            ctrListShow.DataSource = shows;
            ctrListShow.DisplayMember = "ColumnCaption";
            ctrListShow.ValueMember = "ColumnField";
            ctrListShow.ImageIndexMember = "ImageIndex";
            ctrListHide.DataSource = hides;
            ctrListHide.DisplayMember = "ColumnCaption";
            ctrListHide.ValueMember = "ColumnField";
            ctrListHide.ImageIndexMember = "ImageIndex";
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetViewShow()
        {
            if (AllShowFieldSetting.ContainsKey(_gridId))
            {
                var fields = AllShowFieldSetting[_gridId].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var fieldDic = new Dictionary<string, int>();
                var order = 0;
                foreach (var field in fields)
                {
                    fieldDic[field] = order++;
                }
                foreach (var column in _allGridColumn)
                {
                    if (fieldDic.ContainsKey(column.FieldName))
                    {
                        var vIndex= fieldDic[column.FieldName];
                        column.VisibleIndex =vIndex;
                    }
                    else
                    {
                        column.VisibleIndex = -1;
                    }
                }
            }
        }

        #endregion

        #region [Load]

        private void ColumnShowUC_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region [Command]{Event}

        private void ctrListHide_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommandShow();
        }

        private void ctrListShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommandHide();
        }

        private void cmdShowAll_Click(object sender, EventArgs e)
        {
            CommandShowAll();
        }

        private void cmdHideAll_Click(object sender, EventArgs e)
        {
            CommandHideAll();
        }

        private void ctrPopupItemDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            CommandDown();
        }

        private void ctrPopupItemUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            CommandUp();
        }

        private void ctrPopupHide_ItemClick(object sender, ItemClickEventArgs e)
        {
            CommandHide();
        }

        private void ctrPopupItemShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            CommandShow();
        }

        #endregion

        #region [Command]

        private void CommandHide()
        {
            var item = ctrListShow.SelectedItem as ShowColumnItem;
            if (item != null)
            {
                var shows = ctrListShow.DataSource as List<ShowColumnItem>;
                var hides = ctrListHide.DataSource as List<ShowColumnItem>;
                if (shows.Count == 1)
                {
                    Smart.Win.UtilityHelper.ShowInfoMessage("至少要有一个显示列");
                    return;
                }
                item.ImageIndex = 1;
                shows.Remove(item);
                hides.Add(item);
                ctrListShow.Refresh();
                ctrListHide.Refresh();
            }
        }

        private void CommandShow()
        {
            var item = ctrListHide.SelectedItem as ShowColumnItem;
            if (item != null)
            {
                item.ImageIndex = 0;
                var shows = ctrListShow.DataSource as List<ShowColumnItem>;
                var hides = ctrListHide.DataSource as List<ShowColumnItem>;
                hides.Remove(item);
                shows.Add(item);
                ctrListShow.Refresh();
                ctrListHide.Refresh();
            }
        }

        private void CommandShowAll()
        {
            var shows = ctrListShow.DataSource as List<ShowColumnItem>;
            var hides = ctrListHide.DataSource as List<ShowColumnItem>;
            if (shows == null) { shows = new List<ShowColumnItem>(); }
            if (hides == null) { hides = new List<ShowColumnItem>(); }
            foreach (var hide in hides)
            {
                hide.ImageIndex = 0;
            }
            shows.AddRange(hides);
            hides.Clear();
            ctrListShow.DataSource = shows;
            ctrListHide.DataSource = hides;
            ctrListShow.Refresh();
            ctrListHide.Refresh();
        }

        private void CommandHideAll()
        {
            var shows = ctrListShow.DataSource as List<ShowColumnItem>;
            var hides = ctrListHide.DataSource as List<ShowColumnItem>;
            if (shows == null) { shows = new List<ShowColumnItem>(); }
            if (hides == null) { hides = new List<ShowColumnItem>(); }
            foreach (var show in shows)
            {
                show.ImageIndex = 1;
            }
            hides.AddRange(shows);
            shows.Clear();
            ctrListShow.DataSource = shows;
            ctrListHide.DataSource = hides;
            ctrListShow.Refresh();
            ctrListHide.Refresh();
        }

        private void CommandUp()
        {
            var shows = ctrListShow.DataSource as List<ShowColumnItem>;
            if (shows == null) { shows = new List<ShowColumnItem>(); }
            var item = ctrListShow.SelectedItem as ShowColumnItem;
            if (item != null)
            {
                if (shows[0].Equals(item))
                {
                    Smart.Win.UtilityHelper.ShowInfoMessage("选中显示列已经是第一列了");
                }
                else
                {
                    //上移操作
                    ShowColumnItem brother = null;
                    ShowColumnItem self = null;
                    var newShows = new List<ShowColumnItem>();
                    var found = false;
                    foreach (var show in shows)
                    {
                        if (show.Equals(item))
                        {
                            found = true;
                            self = item;
                            newShows.Remove(brother);
                            newShows.Add(self);
                            newShows.Add(brother);
                        }
                        else
                        {
                            newShows.Add(show);
                            if (!found)
                            {
                                brother = show;
                            }
                        }
                    }
                    ctrListShow.DataSource = newShows;
                    ctrListShow.Refresh();
                    ctrListShow.SelectedItem = self;
                }
            }
            else
            {
                Smart.Win.UtilityHelper.ShowInfoMessage("请选择要上移的显示列");
            }
        }

        private void CommandDown()
        {
            var shows = ctrListShow.DataSource as List<ShowColumnItem>;
            if (shows == null) { shows = new List<ShowColumnItem>(); }
            var item = ctrListShow.SelectedItem as ShowColumnItem;
            if (item != null)
            {
                if (shows[shows.Count-1].Equals(item))
                {
                    Smart.Win.UtilityHelper.ShowInfoMessage("选中显示列已经是最后一列了");
                }
                else
                {
                    //下移操作
                    ShowColumnItem brother = null;
                    ShowColumnItem self = null;
                    var newShows = new List<ShowColumnItem>();
                    var found = false;
                    foreach (var show in shows)
                    {
                        if (show.Equals(item))
                        {
                            found = true;
                            self = item;
                        }
                        else
                        {
                            if (found && brother == null)
                            {
                                brother = show;
                                newShows.Add(brother);
                                newShows.Add(self);
                            }
                            else
                            {
                                newShows.Add(show);
                            }
                        }
                    }
                    ctrListShow.DataSource = newShows;
                    ctrListShow.Refresh();
                    ctrListShow.SelectedItem = self;
                }
            }
            else
            {
                Smart.Win.UtilityHelper.ShowInfoMessage("请选择要下移的显示列");
            }
        }

        #endregion

        #region [Click]
        private void cmdReset_Click(object sender, EventArgs e)
        {
            CommandReset();
        }

        private void CommandReset()
        {
            var showFields = GetOriginFields();
            SaveXML(showFields);
            BindControl();
            FindForm().DialogResult = DialogResult.OK;
        }

        private void ctrButtonCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
        }

        private void ctrButtonOK_Click(object sender, EventArgs e)
        {
            var showFields = GetShowFields();
            //保存XML文件
            SaveXML(showFields);
            FindForm().DialogResult = DialogResult.OK;
        }

        private string GetShowFields()
        {
            var sb = new StringBuilder("|");
            var shows = ctrListShow.DataSource as List<ShowColumnItem>;
            foreach (var show in shows)
            {
                sb.Append(show.ColumnField + "|");
            }
            return sb.ToString();
        }

        private string GetOriginFields()
        {
            var sb = new StringBuilder("|");
            foreach (var column in _allGridColumn)
            {
                sb.Append(column.FieldName + "|");
            }
            var showFields = sb.ToString();
            return showFields;
        }

        private void SaveXML(string showFields)
        {
            try
            {
                //保存XML文件
                if (AllShowFieldSetting.ContainsKey(_gridId))
                {
                    //if (SettingParentNode != null && SettingParentNode.Childs != null)
                    //{
                        //foreach (IConfigNode setting in SettingParentNode.Childs)
                        //{
                        //    if (setting.Properties["id"].Equals(gridID))
                        //    {
                        //        Dictionary<string, string> dic = setting.Properties;
                        //        dic["showField"] = showFields;
                        //        setting.SetProperties(dic);
                        //        break;
                        //    }
                        //}
                    //}
                }
                else
                {
                    //var dic = new Dictionary<string, string>
                    //{
                    //    ["id"] = _gridId,
                    //    ["showField"] = showFields
                    //};
                    //SettingParentNode.CreateChildNode("setting", "", dic);
                }
                AllShowFieldSetting[_gridId] = showFields;
                //GridProfileXml.Save();
                SetViewShow();
            }
            catch
            {
                Smart.Win.UtilityHelper.ShowErrorMessage("保存列显示设置信息失败");
            }
        }
        #endregion

        #region [Config]

        ////object _muxObj = new object();
        ////AppConfigger _gridConfig = null;
        /////// <summary>
        /////// Grid显示列自定义
        /////// </summary>
        ////private AppConfigger GridProfileXml
        ////{
        ////    get
        ////    {
        ////        if (_gridConfig == null)
        ////        {
        ////            try
        ////            {
        ////                string path = Path.Combine(Application.StartupPath, string.Format(@"AppData\{0}", employeeID));
        ////                if (!Directory.Exists(path))
        ////                {
        ////                    Directory.CreateDirectory(path);
        ////                }
        ////                string file = path + @"\GridProfile.xml";
        ////                if (!File.Exists(file))
        ////                {
        ////                    string defaultFile = Path.Combine(Application.StartupPath, string.Format(@"AppData\{0}", "GridProfile.xml"));
        ////                    if (File.Exists(defaultFile))
        ////                    {
        ////                        File.Copy(defaultFile, file);
        ////                    }
        ////                }
        ////                _gridConfig = new AppConfigger(file, "railcar");
        ////            }
        ////            catch
        ////            {
        ////                Common.ShowErrorMessage("保存列显示设置信息失败");
        ////            }
        ////        }
        ////        return _gridConfig;
        ////    }
        ////}
        ////private IConfigNode SettingParentNode
        ////{
        ////    get
        ////    {
        ////        try
        ////        {
        ////            IConfigNode node = GridProfileXml.CreateNode("gridSetting");
        ////            return node;
        ////        }
        ////        catch
        ////        {
        ////            UtilityHelper.ShowErrorMessage("保存列显示设置信息失败");
        ////            return null;
        ////        }
                
        ////    }
        ////}

        private Dictionary<string, string> _allShowFieldSetting;
        /// <summary>
        /// Get所有显示字段设置
        /// </summary>
        private Dictionary<string, string> AllShowFieldSetting
        {
            get
            {
                if (_allShowFieldSetting == null)
                {
                    //lock (_muxObj)
                    //{
                        _allShowFieldSetting = new Dictionary<string, string>();
                        //if (SettingParentNode != null && SettingParentNode.Childs != null)
                        //{
                            //List<IConfigNode> settings = SettingParentNode.Childs;
                            //foreach (IConfigNode setting in settings)
                            //{
                            //    string key = setting.Properties["id"];
                            //    string val = setting.Properties["showField"];
                            //    _allShowFieldSetting.Add(key, val);
                            //}
                    //    }
                    //}
                }
                return _allShowFieldSetting;
            }
        }
        #endregion

        #region [Entity]
        /// <summary>
        /// 显示列项
        /// </summary>
        public class ShowColumnItem
        {
           /// <summary>
           /// 列标题
           /// </summary>
           public string ColumnCaption { get; set; }
           /// <summary>
           /// 列字段
           /// </summary>
           public string ColumnField { get; set; }
           /// <summary>
           /// 图片索引
           /// </summary>
           public int ImageIndex { get; set; }
        }

        #endregion

        #region [Popup]

        private void ctrListShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var itemIndex = ctrListShow.IndexFromPoint(e.Location);
                if (itemIndex >= 0)
                {
                    ctrListShow.SelectedIndex = itemIndex;
                    ctrPopup.ShowPopup(MousePosition);
                }
            }
        }

        private void ctrListHide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var itemIndex = ctrListHide.IndexFromPoint(e.Location);
            if (itemIndex < 0) return;
            ctrListHide.SelectedIndex = itemIndex;
            ctrPopup2.ShowPopup(MousePosition);
        }

        #endregion

    }
}