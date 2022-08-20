using GLFW;
using MonkeyWindow.Debug;
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

        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

        // Creating window and setting context
        window = Glfw.CreateWindow(width, height, title,GLFW.Monitor.None, GLFW.Window.None);
        Glfw.MakeContextCurrent(window);

        // Setting up OpenGL
        Glfw.SwapInterval(1);

        float[] vertices = new float[]
        {
            -0.5f, -0.5f,
            0.5f, -0.5f,
            0.5f, 0.5f,
            -0.5f, 0.5f
        };

        uint[] indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        VertexArray va = new VertexArray();

        VertexBuffer<float> vb = new VertexBuffer<float>(vertices, vertices.Length * sizeof(float));
        IndexBuffer ib = new IndexBuffer(indices, indices.Length);

        VertexBufferLayout layout = new VertexBufferLayout();
        layout.PushFloat(2);

        va.AddBuffer(vb, layout);

        DefaultShaders.SimpleShader.Use();

        int location = Gl.GetUniformLocation(DefaultShaders.SimpleShader.Program, "u_Color");

        if(location == -1)
        {
            MDebug.Error("Failed to get uniform location");
        }

        Gl.Uniform4f(location, 0.8f, 0.3f, 0.8f, 1.0f);

        float r = 0.0f;
        float increment = 0.05f;

        while(!Glfw.WindowShouldClose(window))
        {
            Paint();
            
            Gl.Uniform4f(location, r, 0.3f, 0.8f, 1.0f);

            va.Bind();
            ib.Bind();

            Gl.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, IntPtr.Zero);

            if(r > 1.0f)
            {
                increment = -0.05f;
            }
            else if(r < 0.0f)
            {
                increment = 0.05f;
            }

            r += increment;

            Glfw.PollEvents();
            Glfw.SwapBuffers(window);
        }

        Glfw.DestroyWindow(window);
        Glfw.Terminate();
    }

    private void Paint()
    {
        // Drawing stuff
        

        OnWindowPaint();
    }

}