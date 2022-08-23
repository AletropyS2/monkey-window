
using MonkeyWindow.Graphics;
using OpenGL;
using Shader = MonkeyWindow.Graphics.Shader;

namespace MonkeyWindow.Graphics;

public class Renderer
{

    public void Clear()
    {
        Gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public void Draw(VertexArray va, IndexBuffer ib, Shader shader)
    {
        shader.Bind();
        va.Bind();
        ib.Bind();

        Gl.DrawElements(BeginMode.Triangles, ib.GetCount(), DrawElementsType.UnsignedInt, IntPtr.Zero);
    }

}