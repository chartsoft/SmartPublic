using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Smart.Standard.Enum;
using Smart.Standard.Extends;

namespace Smart.Standard.Helper
{
    /// <summary>
    /// 加密辅助类
    /// </summary>
    public class CryptoHelper
    {
        /// <summary>
        /// 为明文密码进行SHA256Hash
        /// </summary>
        /// <param name="plainPwd">明文的密码</param>
        /// <param name="salte">随机盐【需要存储】</param>
        /// <returns></returns>
        public static string CreateHashPwdBySalte(string plainPwd, out string salte)
        {
            var salteArr = Guid.NewGuid().ToByteArray();
            salte = Convert.ToBase64String(salteArr);
            var pwdArr = Encoding.UTF8.GetBytes(plainPwd);
            pwdArr = MergeBytesArrays(pwdArr, salteArr);
            SHA256 sha256 = new SHA256Managed();
            pwdArr = sha256.ComputeHash(pwdArr);
            sha256.Clear();
            return Convert.ToBase64String(pwdArr);
        }
        /// <summary>
        /// 合并Byte数组
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static byte[] MergeBytesArrays(params byte[][] arrays)
        {
            var bList = new List<byte>();
            foreach (var array in arrays)
            {
                bList.AddRange(array);
            }
            return bList.ToArray();
        }
        /// <summary>
        /// 对明文密码进行SHA256Hash后与密文密码进行比较
        /// </summary>
        /// <param name="plainPwd">明文密码</param>
        /// <param name="salte">随机盐</param>
        /// <param name="cipherPwd">密文密码</param>
        /// <returns></returns>
        public static bool EqualPwdSha256(string plainPwd, string salte, string cipherPwd)
        {
            var pwdArr = Encoding.UTF8.GetBytes(plainPwd);
            var salteArr = Convert.FromBase64String(salte);
            var tempArr = MergeBytesArrays(pwdArr, salteArr);
            SHA256 sha256 = new SHA256Managed();
            tempArr = sha256.ComputeHash(tempArr);
            sha256.Clear();
            var tempStr = Convert.ToBase64String(tempArr);
            return string.Equals(tempStr, cipherPwd, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="authInfo">用户凭据</param> 
        /// <param name="appSecret">App密钥</param>
        /// <returns></returns>
        public static string CreateToken<T>(T authInfo, string appSecret)
        {
            var key = Encoding.UTF8.GetBytes(appSecret);
            //采用HS256加密算法
            var algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(authInfo, key);
            return token;
        }

        /// <summary>
        /// token解密,TokenExpiredException异常已经过期,SignatureVerificationException签名暂时不可用或错误
        /// </summary>
        /// <param name="token">待解密的Token</param>
        /// <param name="appSecret">解密秘钥</param>
        /// <returns></returns>
        public static string DecToken(string token, string appSecret)
        {
            var key = Encoding.UTF8.GetBytes(appSecret);
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider dateTimeProvider = new UtcDateTimeProvider();
            IJwtValidator jwtValidator = new JwtValidator(serializer, dateTimeProvider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, jwtValidator, urlEncoder);
            return decoder.Decode(token, key, true);
        }

        /// <summary>
        /// Token声明创建
        /// </summary>
        /// <returns></returns>
        public static string TokenClaim(string appSecret, Dictionary<ClaimKinds, object> claims)
        {
            var payload = new Dictionary<string, object>();
            foreach (var item in claims)
            {
               payload.Add(item.Key.ToString().ToLower(),item.Value);
            }      
            return CreateToken(payload, appSecret);
        }

        /// <summary>
        /// Token声明获取对象,TokenExpiredException异常已经过期,SignatureVerificationException签名暂时不可用或错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <param name="appSecret"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static T GetTokenClaim<T>(string token, string appSecret, ClaimKinds claim = ClaimKinds.CusClaim)
        {
            var dic = DecToken(token, appSecret).ParseJson<Dictionary<string, object>>();
            var obj = dic[claim.ToString().ToLower()];
            try
            {
                return obj.CastTo<T>();
            }
            catch (Exception)
            {
                return SerilizeHelper.DeserilizeJson<T>(obj.PackJson());
            }
        }
        #region 生成AppSecret
        /// <summary>
        /// AppSecret
        /// </summary>
        /// <returns></returns>
        public static string AppSecret()
        {
            var appSecret = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return appSecret;
        }
        #endregion

    }
}
