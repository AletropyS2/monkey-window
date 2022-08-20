using GLFW;
using MonkeyWindow.Graphics;
using OpenGL;

namespace MonkeyWindow;
public class MWindow
{
    private Window window;

    public Action OnWindowPaint = delegate { };

    public MWindow(int width, int height, string title)
    {
        if(!Glfw.Init())
        {
            Console.WriteLine("Failed to initialize GLFW");
            return;
        }

        // Creating window and setting context
        window = Glfw.CreateWindow(width, height, title,GLFW.Monitor.None, GLFW.Window.None);
        Glfw.MakeContextCurrent(window);

        // Setting up OpenGL
        Glfw.SwapInterval(1);

        float[] vertices = new float[]
        {
            -0.5f, -0.5f, 1.0f, 0.0f, 0.0f,
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
            0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
            -0.5f, 0.5f, 1.0f, 1.0f, 0.0f
        };

        uint[] indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };


        uint vbo = Gl.GenBuffer();

        Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        Gl.BufferData<float>(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        Gl.EnableVertexAttribArray(0);
        Gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), IntPtr.Zero);

        Gl.EnableVertexAttribArray(1);
        Gl.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), new IntPtr(2 * sizeof(float)));


        uint vao = Gl.GenBuffer();

        Gl.BindBuffer(BufferTarget.ElementArrayBuffer, vao);
        Gl.BufferData<uint>(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

        DefaultShaders.SimpleShader.Use();

        while(!Glfw.WindowShouldClose(window))
        {
            Paint();
            
            Gl.DrawElements(BeginMode.LineLoop, 6, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Glfw.PollEvents();
            Glfw.SwapBuffers(window);
        }
    }

    private void Paint()
    {
        // Drawing stuff
        

        OnWindowPaint();
    }

}