using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using Smart.Net45.Extends;
using Smart.Win.Validate;

namespace Smart.Win.Helpers
{

    /// <summary>
    /// 验证辅助类
    /// </summary>
    public class ValidateHelper
    {

        #region [Construction]

        /// <summary>
        /// 构造函数
        /// </summary>
        public ValidateHelper() { }

        /// <summary>
        /// 验证辅助类
        /// </summary>
        /// <param name="errorProvider">错误提供控件</param>
        public ValidateHelper(DXErrorProvider errorProvider)
        {
            if (errorProvider != null)
            {
                _ep = errorProvider;
            }
        }

        #endregion

        private readonly Dictionary<string, BaseEdit> _controlDic = new Dictionary<string, BaseEdit>();
        private readonly Dictionary<string, Func<List<ValidateResult>>> _methodDic = new Dictionary<string, Func<List<ValidateResult>>>();
        private readonly Dictionary<string, bool> _immediateDic = new Dictionary<string, bool>();

        private DXErrorProvider _ep;
        /// <summary>
        /// 注册验证控件
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="ctrValue">控件默认值</param>
        /// <param name="valMethod">验证方法</param>
        /// <param name="immediate"></param>
        public void Register(BaseEdit ctr, object ctrValue, Func<List<ValidateResult>> valMethod, bool immediate)
        {
            InitEditor(ctr, ctrValue, valMethod, immediate);
            ctr.EditValueChanged += ctr_EditValueChanged;
        }

        private void ctr_EditValueChanged(object sender, EventArgs e)
        {
            var editor = sender as BaseEdit;
            if (ValidateEnable && editor != null && _immediateDic.ContainsKey(editor.Name) && _methodDic.ContainsKey(editor.Name))
            {
                var immediate = _immediateDic[editor.Name];
                var valMethod = _methodDic[editor.Name];
                if (immediate && valMethod != null)
                {
                    var combo = sender as ComboBoxEdit;
                    if (combo != null && combo.Properties.TextEditStyle == TextEditStyles.DisableTextEditor)
                    {
                        ValidateEditor(combo, valMethod);
                    }
                    else if (sender is ButtonEdit && ((ButtonEdit)sender).Properties.TextEditStyle == TextEditStyles.DisableTextEditor)
                    {
                        ValidateEditor((BaseEdit)sender, valMethod);
                    }
                    else
                    {
                        var edit = sender as BaseEdit;
                        if (!edit.Focused)
                        {
                            ValidateEditor(edit, valMethod);
                        }
                    }
                }
            }
        }

        private bool _enable = true;

        /// <summary>
        /// 验证是否可用
        /// </summary>
        public bool ValidateEnable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        /// <summary>
        /// 验证所有注册过的控件
        /// </summary>
        /// <returns>通过返回true，否则false</returns>
        public bool Validate(bool isGlobal = false)
        {
            var success = true;
            if (_controlDic.Count > 0)
            {
                foreach (var edit in _controlDic.Values)
                {
                    success = ValidateEditor(edit, _methodDic[edit.Name]) && success;
                }
            }
            return isGlobal ? !_ep.HasErrors : success;
        }

        /// <summary>
        /// 验证注册过的特定控件
        /// </summary>
        /// <param name="ctr">要验证的控件</param>
        public void Validate(BaseEdit ctr)
        {
            if (_controlDic.ContainsKey(ctr.Name) && _methodDic.ContainsKey(ctr.Name))
            {
                ValidateEditor(ctr, _methodDic[ctr.Name]);
            }
            else
            {
                throw new InvalidOperationException("未注册控件，不能进行验证。");
            }
        }

        /// <summary>
        /// 移除控件错误提示
        /// </summary>
        /// <param name="ctr"></param>
        public void RemoveEditorError(BaseEdit ctr)
        {
            _ep.SetError(ctr, null);
        }

        private void InitEditor(BaseEdit ctr, object ctrValue, Func<List<ValidateResult>> valMethod, bool immediate)
        {
            if (_ep == null)
            {
                var frm = ctr.FindForm();
                if (frm == null) return;
                // ReSharper disable once SuspiciousTypeConversion.Global
                var iep = frm as IErrorProvider;
                if (iep == null)
                {
                    throw new ArgumentException("窗体必须实现IErrorProvider,才能注册验证控件");
                }
                _ep = iep.ErrorProvider;
            }
            if (_controlDic.ContainsKey(ctr.Name)) return;
            ctr.EditValue = ctrValue;
            _controlDic[ctr.Name] = ctr;
            _ep.SetErrorType(ctr, ErrorType.Default);
            _ep.SetIconAlignment(ctr, ErrorIconAlignment.MiddleRight);
            _methodDic[ctr.Name] = valMethod;
            _immediateDic[ctr.Name] = immediate;
        }

        private bool ValidateEditor(BaseEdit edit, Func<List<ValidateResult>> valMethod)
        {
            var results = valMethod();
            if (!results.IsNullOrEmpty())
            {
                var sb = new StringBuilder();
                foreach (var result in results.Where(result => !result.Pass))
                {
                    sb.Append($"{result.ErrorMessage}\r\n");
                }
                _ep.SetError(edit, sb.ToString());
            }
            else
            {
                _ep.SetError(edit, null);
            }
            return results.IsNullOrEmpty();
        }
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="errorProvider">errorProvider</param>
        /// <param name="control">控件</param>
        /// <param name="message">提示信息</param>
        public static void ShowError(DXErrorProvider errorProvider, Control control, string message)
        {
            errorProvider.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
            errorProvider.SetError(control, message, ErrorType.Default);
        }
    }

    /// <summary>
    /// 错误提供者接口
    /// </summary>
    public interface IErrorProvider
    {
        /// <summary>
        /// 错误提供者
        /// </summary>
        DXErrorProvider ErrorProvider { get; }
    }

}