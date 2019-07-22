using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// 文本框块选择帮助器
    /// </summary>
    /// <typeparam name="TEntity">保存的强类型实体类型</typeparam>
    public class TextEditBlockSelectHelper<TEntity>
    {
        private const char DefaultSpliteChar = ';';
        private readonly char _splitChar;
        private readonly TextEdit _textEdit;
        private int _currStartIndex; // 当前开始位置
        private int _currSelectionLength; // 当前选择的长度

        private bool _isAddingBlock; // 正在执行添加块操作

        private readonly List<TEntity> _entityList = new List<TEntity>(); // 块实体列表
        private readonly Dictionary<TEntity, string> _displayDictionary; // 显示字符串字典
        private readonly IEqualityComparer<TEntity> _equalityComparer;

        /// <summary>
        /// 构造一个文本框块选择帮助器，以';'作为分隔符
        /// </summary>
        /// <param name="textEdit">文本框</param>
        public TextEditBlockSelectHelper(TextEdit textEdit)
            : this(textEdit, DefaultSpliteChar)
        {
        }

        /// <summary>
        /// 构造一个文本框块选择帮助器
        /// </summary>
        /// <param name="textEdit">文本框</param>
        /// <param name="splitChar">块分隔字符</param>
        public TextEditBlockSelectHelper(TextEdit textEdit, char splitChar)
            : this(textEdit, DefaultSpliteChar, null)
        {
        }

        /// <summary>
        /// 构造一个文本框块选择帮助器
        /// </summary>
        /// <param name="textEdit">文本框</param>
        /// <param name="splitChar">块分隔字符</param>
        /// <param name="equalityComparer">比较器</param>
        public TextEditBlockSelectHelper(TextEdit textEdit, char splitChar, IEqualityComparer<TEntity> equalityComparer)
        {
            _equalityComparer = equalityComparer;
            _displayDictionary = new Dictionary<TEntity, string>(equalityComparer);
            _textEdit = textEdit;
            _splitChar = splitChar;

            _textEdit.Enter -= _textEdit_Enter;
            _textEdit.Enter += _textEdit_Enter;

            _textEdit.MouseUp -= textEdit_MouseUp;
            _textEdit.MouseUp += textEdit_MouseUp;

            _textEdit.KeyUp -= textEdit_KeyUp; // KeyUp事件用于选择块
            _textEdit.KeyUp += textEdit_KeyUp;

            _textEdit.KeyDown -= textEdit_KeyDown; // KeyDown 事件用于屏蔽输入
            _textEdit.KeyDown += textEdit_KeyDown;

            _textEdit.TextChanged += new EventHandler(_textEdit_TextChanged);

            _textEdit.Tag = GetBlocks();
        }

        private void _textEdit_Enter(object sender, EventArgs e)
        {
            SelectBlock();
            _textEdit.Focus();
        }

        private void _textEdit_TextChanged(object sender, EventArgs e)
        {
            if (!_isAddingBlock)
                ResetText();
        }

        #region Public Methods

        /// <summary>
        /// 添加一个块，以entity.ToString() 作为显示字符串
        /// </summary>
        /// <param name="entity">块包含的实体</param>
        public void AddBlock(TEntity entity)
        {
            AddBlock(entity.ToString(), entity);
        }

        /// <summary>
        /// 添加一个块
        /// </summary>
        /// <param name="displayString">显示字符串</param>
        /// <param name="entity">块包含的实体</param>
        public void AddBlock(string displayString, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(displayString))
                throw new ArgumentException("显示字符串不能为空");

            if (displayString.Contains(_splitChar))
                throw new ArgumentException($"显示字符串不能包含分隔符“{_splitChar}”");

            if (!_entityList.Contains(entity, _equalityComparer))
                _entityList.Add(entity);

            _displayDictionary[entity] = displayString;

            ResetText();
        }

        /// <summary>
        /// 重设文本
        /// </summary>
        private void ResetText()
        {
            _isAddingBlock = true;
            _textEdit.Text = string.Join(_splitChar.ToString(), _entityList.Select(e => _displayDictionary[e]));
            _isAddingBlock = false;
        }

        /// <summary>
        /// 获取所有已经添加的块
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> GetBlocks()
        {
            return _entityList.AsReadOnly();
        }
        #endregion

        #region EventHandler
        private void textEdit_KeyDown(object sender, KeyEventArgs e)
        {
            // 计算开始位置
            _currStartIndex = _textEdit.SelectionStart;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                _currStartIndex--; // 左移一位
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                _currStartIndex += _textEdit.SelectionLength; //移动到选区尾部
            else if (e.KeyCode == Keys.Home)
                _currStartIndex = 0; // 移动到文本头部
            else if (e.KeyCode == Keys.End)
                _currStartIndex = _textEdit.Text.Length - 1; //移动到文本尾部

            var isKeyAvailable = e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete;
            if (!isKeyAvailable)
                e.SuppressKeyPress = true;
            else
            {
                RemoveCurrBlock();
            }
        }

        private void textEdit_KeyUp(object sender, KeyEventArgs e)
        {
            // 上下左右，删除，Home，End
            var isKeyAvailable = e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete ||
                                    e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                                    e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                                    e.KeyCode == Keys.Home || e.KeyCode == Keys.End;
            if (isKeyAvailable)
                SelectBlock();
        }

        private void textEdit_MouseUp(object sender, MouseEventArgs e)
        {
            _currStartIndex = _textEdit.SelectionStart;
            SelectBlock();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 选取块
        /// </summary>
        private void SelectBlock()
        {
            _currSelectionLength = _textEdit.SelectionLength;

            CalcSelectionPosition();

            _textEdit.SelectionStart = _currStartIndex;
            _textEdit.SelectionLength = _currSelectionLength;
        }

        /// <summary>
        /// 计算选区位置
        /// </summary>
        private void CalcSelectionPosition()
        {
            var text = _textEdit.Text;

            var startIndex = GetStartIndex(text, _currStartIndex);
            var endIndex = GetEndIndex(text, _currStartIndex);

            _currStartIndex = startIndex;

            _currStartIndex = startIndex;
            _currSelectionLength = endIndex - startIndex + 1;
        }

        /// <summary>
        /// 在文本中获取当前段的开始位置
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="index">当前位置</param>
        /// <returns>当前段的开始位置</returns>
        private int GetStartIndex(string text, int index)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            if (index <= 0)
                return 0;

            if (index >= text.Length)
                index = text.Length - 1;

            if (text[index] == _splitChar) // 如果选中分隔符，则在分隔符前面找位置
                index--;

            for (var i = index; i >= 0; i--) // 从当前位置找到第0位
                if (text[i] == _splitChar)
                    return i + 1;  // 返回分隔符后面一位

            return 0;
        }

        /// <summary>
        /// 在文本中获取当前段的结束位置
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="index">当前位置</param>
        /// <returns>当前段的结束位置</returns>
        private int GetEndIndex(string text, int index)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;
            var lastIndex = text.Length - 1;

            if (index <= 0)
                index = 0;

            if (index >= lastIndex)
                return lastIndex;

            if (text[index] == _splitChar)
                return index;

            for (var i = index; i < text.Length; i++)
                if (text[i] == _splitChar)
                    return i; // 返回分隔符所在的位置

            return lastIndex;
        }

        /// <summary>
        /// 删除当前块
        /// </summary>
        private void RemoveCurrBlock()
        {
            var entityIndex = GetCurrBlockIndex();
            if (entityIndex >= 0 && entityIndex < _entityList.Count) // 有选中块
            {
                var entity = _entityList[entityIndex];
                _entityList.RemoveAt(entityIndex);
                _displayDictionary.Remove(entity);
            }
        }

        /// <summary>
        /// 获取当前块序号
        /// </summary>
        /// <returns>当前块序号</returns>
        private int GetCurrBlockIndex()
        {
            var entityIndex = -1; // 默认为没有实体

            var text = _textEdit.Text;
            var startIndex = _textEdit.SelectionStart;

            if (string.IsNullOrEmpty(text) || startIndex < 0)
                return entityIndex;

            for (var i = 0; i <= startIndex; i++)
                if (text[i] == _splitChar)
                    entityIndex++;

            if (text[startIndex] != _splitChar) // 最后一段未计算
                entityIndex++;

            return entityIndex;
        }
        #endregion
    }
}
