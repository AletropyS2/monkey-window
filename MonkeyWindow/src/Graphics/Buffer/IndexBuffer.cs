
using OpenGL;

namespace MonkeyWindow.Graphics;

public class IndexBuffer
{

    private uint m_RendererID;
    private int m_Count;

    public IndexBuffer(uint[] data, int count)
    {
        m_Count = count;

        m_RendererID = Gl.GenBuffer();

        Gl.BindBuffer(BufferTarget.ElementArrayBuffer, m_RendererID);
        Gl.BufferData<uint>(BufferTarget.ElementArrayBuffer, count * sizeof(uint), data, BufferUsageHint.StaticDraw);
    }

    ~IndexBuffer()
    {
        Gl.DeleteBuffer(m_RendererID);
    }

    public void Bind()
    {
        Gl.BindBuffer(BufferTarget.ElementArrayBuffer, m_RendererID);
    }

    public void Unbind()
    {
        Gl.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    public int GetCount() { return m_Count; }

}