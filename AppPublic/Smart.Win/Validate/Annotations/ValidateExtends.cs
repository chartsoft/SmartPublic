using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using SmartSolution.Utilities.Errors;
using SmartSolution.Utilities.Extends;
using SmartSolution.Utilities.Helpers;

namespace SmartSolution.Utilities.Validate.Annotations
{
    /// <summary>
    /// 验证扩展
    /// </summary>
    public static class ValidateExtends
    {
        /// <summary>
        /// 验证模型是否有效
        /// </summary>
        /// <typeparam name="T">验证模型类型</typeparam>
        /// <param name="model">验证模型</param>
        /// <returns>有效true，无效false</returns>
        public static bool ValidateValid<T>(this T model) where T : IValidateModel
        {
            if (model == null) return false;
            var results = model.GetValidateResults();
            return !results.Any();
        }

        /// <summary>
        /// 获取验证结果列表
        /// </summary>
        /// <typeparam name="T">验证模型类型</typeparam>
        /// <param name="model">验证模型</param>
        /// <returns>验证结果列表</returns>
        public static List<ValidateResult> GetValidateResults<T>(this T model) where T : IValidateModel
        {
            var result = new List<ValidateResult>();
            var props = typeof(T).GetProperties();
            if (!props.Any()) return result;
            props.SafeForEach(prop =>
            {
                var attrs = prop.GetCustomAttributes<ValidationAttribute>();
                var attrArray = attrs as ValidationAttribute[] ?? attrs.ToArray();
                if (!attrArray.Any()) return;
                var context = new ValidationContext(model);
                attrArray.SafeForEach(attr =>
                {
                    var vr = attr.GetValidationResult(prop.GetValue(context.ObjectInstance), context);
                    if (vr == ValidationResult.Success) return;
                    result.Add(new ValidateResult(vr.ErrorMessage, prop.Name, false, 0));
                });
            });
            return result;
        }

        /// <summary>
        /// 抛出验证结果集中的 第一条验证不通过信息
        /// </summary>
        /// <typeparam name="T">验证模型类型</typeparam>
        /// <param name="model">验证模型</param>
        public static void ThrowValidateResult<T>(this T model) where T : IValidateModel
        {
            if (model == null) return;
            var results = model.GetValidateResults();
            if (!results.Any()) return;
            var first = results[0];
            ExceptionHelper.ThrowBusinessException(first.ErrorMessage, UtilityErrors.ErpValidationDataInputError);
        }
        /// <summary>
        /// 抛出验证结果集中的 所有验证不通过信息
        /// </summary>
        /// <typeparam name="T">验证模型类型</typeparam>
        /// <param name="model">验证模型</param>
        public static void ThrowValidateResults<T>(this T model) where T : IValidateModel
        {
            if (model == null) return;
            var results = model.GetValidateResults();
            if (!results.Any()) return;
            var msg = string.Join(";", results.Select(r => r.ErrorMessage));
            ExceptionHelper.ThrowBusinessException(msg, UtilityErrors.ErpValidationDataInputError);
        }

    }
}
