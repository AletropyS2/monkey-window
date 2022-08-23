using OpenGL;

namespace MonkeyWindow.Graphics;

public class VertexArray
{

    private uint m_RendererID;

    public VertexArray()
    {
        m_RendererID = Gl.GenVertexArray();
    }

    ~VertexArray()
    {
        Gl.DeleteVertexArray(m_RendererID);
    }

    public void Bind()
    {
        Gl.BindVertexArray(m_RendererID);
    }

    public void Unbind()
    {
        Gl.BindVertexArray(0);
    }

    public void AddBuffer<T>(VertexBuffer<T> vb, VertexBufferLayout layout) where T : struct
    {  
        Bind();
        vb.Bind();
        List<VertexBufferElement> elements = layout.GetElements();

        int offset = 0;

        for(int i = 0; i < elements.Count; i++)
        {
            VertexBufferElement element = elements[i];
            Gl.EnableVertexAttribArray(i);
            Gl.VertexAttribPointer((uint)i, element.count, element.type, element.normalized, layout.GetStride(), new IntPtr(offset));
            offset += element.count * VertexBufferElement.GetSizeOfType((uint)element.type);
        }
         
    }

}