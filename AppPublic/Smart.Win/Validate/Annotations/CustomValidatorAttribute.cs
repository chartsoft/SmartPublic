using System;
using SmartSolution.Utilities.Validate.Annotations;

namespace Smart.Win.Validate.Annotations
{
    /// <summary>
    /// 自定义验证器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class CustomValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// 验证键
        /// </summary>
        public string ValidatorKey { get; private set; }

        /// <summary>
        /// 自定义验证器
        /// </summary>
        public CustomValidatorAttribute(string validatorKey)
        {
            ValidatorKey = validatorKey;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validator = CustomValidatorFactory.GetCustomValidator(ValidatorKey);
            var result = validator.Validate(value, validationContext);
            if (result != null) ErrorMessage = result.ErrorMessage;
            return result;
        }

    }
}