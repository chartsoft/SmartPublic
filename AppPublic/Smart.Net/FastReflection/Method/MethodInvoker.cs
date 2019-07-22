using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Smart.Net45.FastReflection.Method
{
    /// <summary>
    /// 方法Invoker接口
    /// </summary>
    public interface IMethodInvoker
    {
        /// <summary>
        /// 通过反射执行方法
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="parameters">方法参数</param>
        /// <returns>执行结果</returns>
        object Invoke(object instance, params object[] parameters);
    }

    /// <summary>
    /// 方法Invoker
    /// </summary>
    public class MethodInvoker : IMethodInvoker
    {

        private readonly Func<object, object[], object> invoker;

        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo MethodInfo { get; }

        /// <summary>
        /// 方法Invoker
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        public MethodInvoker(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            invoker = CreateInvokeDelegate(methodInfo);
        }

        /// <summary>
        /// 动态委托创建
        /// <remarks>
        /// <![CDATA[
        /// 签名：((TInstance)instance).Method((T0)parameters[0], (T1)parameters[1], ...)
        /// ]]>
        /// </remarks>
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>方法调用 动态委托实例</returns>
        private static Func<object, object[], object> CreateInvokeDelegate(MethodInfo methodInfo)
        {
            var instanceParameter = Expression.Parameter(typeof(object), "instance");
            var parametersParameter = Expression.Parameter(typeof(object[]), "parameters");
            var parameterExpressions = new List<Expression>();
            var paramInfos = methodInfo.GetParameters();
            for (var i = 0; i < paramInfos.Length; i++)
            {
                var valueObj = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                var valueCast = Expression.Convert(valueObj, paramInfos[i].ParameterType);
                parameterExpressions.Add(valueCast);
            }
            var instanceCast = methodInfo.IsStatic ? null :Expression.Convert(instanceParameter, methodInfo.ReflectedType);
            var methodCall = Expression.Call(instanceCast, methodInfo, parameterExpressions);
            if (methodCall.Type == typeof(void))
            {
                var lambda = Expression.Lambda<Action<object, object[]>>(methodCall, instanceParameter, parametersParameter);
                var execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                var castMethodCall = Expression.Convert(methodCall, typeof(object));
                var lambda =Expression.Lambda<Func<object, object[], object>>(castMethodCall, instanceParameter, parametersParameter);
                return lambda.Compile();
            }
        }

        /// <summary>
        /// 反射执行方法调用
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>执行结果</returns>
        public object Invoke(object instance, params object[] parameters)
        {
            return invoker(instance, parameters);
        }
        /// <summary>
        /// 反射执行方法调用
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>执行结果</returns>
        object IMethodInvoker.Invoke(object instance, params object[] parameters)
        {
            return Invoke(instance, parameters);
        }

    }
}
