namespace Smart.Net45.CheckSum
{
	/// <summary>
	/// 校验接口
	/// </summary>
	public interface ICheckSum
	{
		/// <summary>
		/// 校验值
		/// </summary>
		long Value{get;}

		/// <summary>
		/// 重置
		/// </summary>
		void Reset();

		/// <summary>
		/// 添加校验值
		/// </summary>
		/// <param name = "value">
		/// 要添加的校验值，高位忽略
		/// </param>
		void Update(int value);

		/// <summary>
		/// 更新校验值
		/// </summary>
		/// <param name="buffer">
		/// 字节数组
		/// </param>
		void Update(byte[] buffer);

		/// <summary>
		/// 添加校验字节数组
		/// </summary>
		/// <param name = "buffer">
		/// 字节数组
		/// </param>
		/// <param name = "offset">
		/// 左偏移量
		/// </param>
		/// <param name = "count">
		/// 用于计算的字节长度
		/// </param>
		void Update(byte[] buffer, int offset, int count);
	}
}
