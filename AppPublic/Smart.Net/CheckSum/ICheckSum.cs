namespace Smart.Net45.CheckSum
{
	/// <summary>
	/// У��ӿ�
	/// </summary>
	public interface ICheckSum
	{
		/// <summary>
		/// У��ֵ
		/// </summary>
		long Value{get;}

		/// <summary>
		/// ����
		/// </summary>
		void Reset();

		/// <summary>
		/// ���У��ֵ
		/// </summary>
		/// <param name = "value">
		/// Ҫ��ӵ�У��ֵ����λ����
		/// </param>
		void Update(int value);

		/// <summary>
		/// ����У��ֵ
		/// </summary>
		/// <param name="buffer">
		/// �ֽ�����
		/// </param>
		void Update(byte[] buffer);

		/// <summary>
		/// ���У���ֽ�����
		/// </summary>
		/// <param name = "buffer">
		/// �ֽ�����
		/// </param>
		/// <param name = "offset">
		/// ��ƫ����
		/// </param>
		/// <param name = "count">
		/// ���ڼ�����ֽڳ���
		/// </param>
		void Update(byte[] buffer, int offset, int count);
	}
}
