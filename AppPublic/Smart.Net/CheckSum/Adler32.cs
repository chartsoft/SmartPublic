using System;

namespace Smart.Net45.CheckSum
{
	/// <summary>
	/// Adler-32校验算法，速度优于CRC32校验
	/// </summary>
	public sealed class Adler32 : ICheckSum
	{
		/// <summary>
		/// largest prime smaller than 65536
		/// </summary>
		private const uint BaseValue = 65521;

        private uint _checksum;

		/// <summary>
		/// 校验值
		/// </summary>
		public long Value => _checksum;

	    /// <summary>
		///创建Adler32校验实例
		/// </summary>
		public Adler32()
		{
			Reset();
		}

		/// <summary>
		/// 重置校验值
		/// </summary>
		public void Reset()
		{
			_checksum = 1;
		}

		/// <summary>
		/// 添加校验值
		/// </summary>
		/// <param name = "value">
		/// 要添加的校验值，高位忽略
		/// </param>
		public void Update(int value)
		{
			// We could make a length 1 byte array and call update again, but I
			// would rather not have that overhead
			var s1 = _checksum & 0xFFFF;
			var s2 = _checksum >> 16;

			s1 = (s1 + ((uint)value & 0xFF)) % BaseValue;
			s2 = (s1 + s2) % BaseValue;

			_checksum = (s2 << 16) + s1;
		}

		/// <summary>
		/// 添加校验值
		/// </summary>
        /// <param name="buffer"></param>
		/// 要添加的校验值，高位忽略
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer));
			}

			Update(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// 更新CRC校验值
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
		public void Update(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer));
			}

			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), "cannot be negative");
			}

			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), "cannot be negative");
			}

			if (offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), "not a valid index into buffer");
			}

			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(count), "exceeds buffer size");
			}

			//(By Per Bothner)
			var s1 = _checksum & 0xFFFF;
			var s2 = _checksum >> 16;

			while (count > 0)
			{
				// We can defer the modulo operation:
				// s1 maximally grows from 65521 to 65521 + 255 * 3800
				// s2 maximally grows by 3800 * median(s1) = 2090079800 < 2^31
				var n = 3800;
				if (n > count)
				{
					n = count;
				}
				count -= n;
				while (--n >= 0)
				{
					s1 = s1 + (uint)(buffer[offset++] & 0xff);
					s2 = s2 + s1;
				}
				s1 %= BaseValue;
				s2 %= BaseValue;
			}

			_checksum = (s2 << 16) | s1;
		}

	}
}
