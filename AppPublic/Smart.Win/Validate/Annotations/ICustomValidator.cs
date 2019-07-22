using System.ComponentModel.DataAnnotations;

namespace SmartSolution.Utilities.Validate.Annotations
{
    /// <summary>
    /// 自定义验证器
    /// </summary>
    public interface ICustomValidator
    {
        /// <summary>
        /// 键
        /// </summary>
        string Key { get; }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>验证结果</returns>
        ValidationResult Validate(object val, ValidationContext context);



    }
}
