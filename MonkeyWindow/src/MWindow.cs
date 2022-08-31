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
            -0.5f, -0.5f, 0f, 0f, //0 Bottom left
            0.5f, -0.5f, 1f, 0f, //1 Bottom right
            0.5f, 0.5f, 1f, 1f,  //2 Top right
            -0.5f, 0.5f, 0f, 1f //3 Top left
        };

        uint[] indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        Gl.Enable(EnableCap.Blend);

        VertexArray va = new VertexArray();

        VertexBuffer<float> vb = new VertexBuffer<float>(vertices, vertices.Length * sizeof(float));
        IndexBuffer ib = new IndexBuffer(indices, indices.Length);

        VertexBufferLayout layout = new VertexBufferLayout();
        layout.PushFloat(2);
        layout.PushFloat(2);

        va.AddBuffer(vb, layout);

        Matrix4 projection = Matrix4.CreateOrthographic(4f, 3f, -1.0f, 1.0f);

        DefaultShaders.SimpleShader.Bind();

        DefaultShaders.SimpleShader.SetUniform4f("u_Color", 0.8f, 0.3f, 0.8f, 1.0f);

        Texture texture = new Texture("res/textures/test.png", PixelFormat.Bgra, TextureParameter.Nearest);
        texture.Bind();
        DefaultShaders.SimpleShader.SetUniform1i("u_Texture", 0);
        DefaultShaders.SimpleShader.SetUniformMat4f("u_MVP", projection);

        va.Unbind();
        ib.Unbind();
        vb.Unbind();
        DefaultShaders.SimpleShader.Unbind();


        Renderer renderer = new Renderer();

        float r = 0.0f;
        float increment = 0.05f;

        while(!Glfw.WindowShouldClose(window))
        {
            Paint(renderer);

            DefaultShaders.SimpleShader.Bind();
            DefaultShaders.SimpleShader.SetUniform4f("u_Color", r, 0.3f, 0.8f, 1.0f);
            
            renderer.Draw(va, ib, DefaultShaders.SimpleShader);

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

    private void Paint(Renderer r)
    {
        // Drawing stuff
        r.Clear();

        OnWindowPaint();
    }

}