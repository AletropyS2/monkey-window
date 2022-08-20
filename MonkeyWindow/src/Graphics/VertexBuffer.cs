
using OpenGL;

namespace MonkeyWindow.Graphics;

public class VertexBuffer<T> where T : struct
{

    private uint m_RendererID;

    public VertexBuffer(T[] data, int size)
    {
        m_RendererID = Gl.GenBuffer();

        Gl.BindBuffer(BufferTarget.ArrayBuffer, m_RendererID);
        Gl.BufferData<T>(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);
    }

    ~VertexBuffer()
    {
        Gl.DeleteBuffer(m_RendererID);
    }

    public void Bind()
    {
        Gl.BindBuffer(BufferTarget.ArrayBuffer, m_RendererID);
    }

    public void Unbind()
    {
        Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

}