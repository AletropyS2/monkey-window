
using System.Numerics;
using OpenGL;

namespace MonkeyWindow.Graphics;

struct VertexBufferElement
{
    public int count;
    public VertexAttribPointerType type;
    public bool normalized;

    public static int GetSizeOfType(uint type)
    {
        switch(type)
        {
            case (uint)VertexAttribPointerType.Float: return sizeof(float);
            case (uint)VertexAttribPointerType.Double: return sizeof(double);
            case (uint)VertexAttribPointerType.Int: return sizeof(int);
            case (uint)VertexAttribPointerType.UnsignedInt: return sizeof(uint);
            case (uint)VertexAttribPointerType.Short: return sizeof(short);
            case (uint)VertexAttribPointerType.UnsignedShort: return sizeof(ushort);
            case (uint)VertexAttribPointerType.Byte: return sizeof(byte);
            case (uint)VertexAttribPointerType.UnsignedByte: return sizeof(byte);
            default: return 0;
        }
    }
}

class VertexBufferLayout
{
    private List<VertexBufferElement> m_Elements = new List<VertexBufferElement>();
    private int m_Stride = 0;

    public void PushFloat(int count)
    {
        m_Elements.Add(new VertexBufferElement()
        {
            count = count,
            type = VertexAttribPointerType.Float,
            normalized = false
        });
        m_Stride += count * sizeof(float);
    }

    public void PushUint(int count)
    {
        m_Elements.Add(new VertexBufferElement()
        {
            count = count,
            type = VertexAttribPointerType.UnsignedInt,
            normalized = false
        });
        m_Stride += count * sizeof(uint);
    }

    public void PushChar(int count)
    {
        m_Elements.Add(new VertexBufferElement()
        {
            count = count,
            type = VertexAttribPointerType.UnsignedByte,
            normalized = true
        });
        m_Stride += count * sizeof(byte);
    }

    public List<VertexBufferElement> GetElements()
    {
        return m_Elements;
    }

    public int GetStride()
    {
        return m_Stride;
    }
}