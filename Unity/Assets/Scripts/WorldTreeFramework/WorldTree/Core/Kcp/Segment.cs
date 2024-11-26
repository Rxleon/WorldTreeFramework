using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ET
{
	// KCP Segment Definition
	/// <summary>
	/// KCPƬ�ζ���
	/// </summary>
	internal struct SegmentStruct:IDisposable
    {
		/// <summary>
		/// KCPƬ��ͷ��
		/// </summary>
		public Kcp.SegmentHead SegHead;
		/// <summary>
		/// �ش�ʱ���
		/// </summary>
		public uint Resendts;
		/// <summary>
		/// ��ʱ�ش�ʱ��
		/// </summary>
		public int  Rto;
		/// <summary>
		/// �����ش�
		/// </summary>
		public uint Fastack;
		/// <summary>
		/// �ش�����
		/// </summary>
		public uint Xmit;

		/// <summary>
		/// ������
		/// </summary>
		private byte[] buffers;

		/// <summary>
		/// ArrayPool
		/// </summary>
		private ArrayPool<byte> arrayPool;

		/// <summary>
		/// �Ƿ�Ϊ��
		/// </summary>
		public bool IsNull => this.buffers == null;

		/// <summary>
		/// д������
		/// </summary>
		public int WrittenCount
        {
            get => (int) this.SegHead.Len;
            private set => this.SegHead.Len = (uint) value;
        }

		/// <summary>
		/// ��д�뻺����
		/// </summary>
		public Span<byte> WrittenBuffer => this.buffers.AsSpan(0, (int) this.SegHead.Len);

		/// <summary>
		/// ���л�����
		/// </summary>
		public Span<byte> FreeBuffer => this.buffers.AsSpan(WrittenCount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SegmentStruct(int size, ArrayPool<byte> arrayPool)
        {
            this.arrayPool = arrayPool;
            buffers = arrayPool.Rent(size);
            this.SegHead = default;
            this.Resendts = default;
            this.Rto = default;
            this.Fastack = default;
            this.Xmit = default;
        }

		/// <summary>
		/// �ӻ�������ȡ����
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(Span<byte> data, ref int size)
        {
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(data),this.SegHead);
            size += Unsafe.SizeOf<Kcp.SegmentHead>();
        }

		/// <summary>
		/// �ӻ�������ȡ����
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Advance(int count)
        {
            this.WrittenCount += count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            arrayPool.Return(this.buffers);
        }
    }
}
