
using MonkeyWindow.Debug;
using OpenGL;

namespace MonkeyWindow.Graphics;

/* struct ShaderProgramSource
{
    public string VertexShader { get; private set; }
    public string FragmentShader { get; private set; }

    public ShaderProgramSource(string vertexShader, string fragmentShader)
    {
        VertexShader = vertexShader;
        FragmentShader = fragmentShader;
    }
} */

public class Shader
{

    private uint m_RendererID;
    private Dictionary<string, int> m_LocationCache = new Dictionary<string, int>();

    public Shader(string vertexShader, string fragmentShader)
    {
        m_RendererID = CreateShader(vertexShader, fragmentShader);
    }

    ~Shader()
    {
        Gl.DeleteProgram(m_RendererID);
    }

    public void Bind()
    {
        Gl.UseProgram(m_RendererID);
    }

    public void Unbind()
    {
        Gl.UseProgram(0);
    }

    private uint CompileShader(ShaderType type, string source)
    {
        uint id = Gl.CreateShader(type);
        Gl.ShaderSource(id, source);
        Gl.CompileShader(id);

        bool compiled = Gl.GetShaderCompileStatus(id);
        if(!compiled)
        {
            string log = Gl.GetShaderInfoLog(id);
            MDebug.Error($"Error compiling {type} shader: {log}");
            return 0;
        }

        return id;
    }

    private uint CreateShader(string vertexShaderSource, string fragmentShaderSource)
    {
        uint Program = Gl.CreateProgram();
        var vs = CompileShader(ShaderType.VertexShader, vertexShaderSource);
        var fs = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);

        Program = Gl.CreateProgram();

        Gl.AttachShader(Program, vs);
        Gl.AttachShader(Program, fs);
        Gl.LinkProgram(Program);
        Gl.ValidateProgram(Program);

        Gl.DeleteShader(vs);
        Gl.DeleteShader(fs);

        return Program;
    }

    private int GetUniformLocation(string name)
    {
        if(m_LocationCache.ContainsKey(name))
        {
            return m_LocationCache[name];
        }

        int location = Gl.GetUniformLocation(m_RendererID, name);
        if(location == -1)
            MDebug.Warn($"Uniform {name} not found!");

        m_LocationCache.Add(name, location);
        return location;
    }

    public void SetUniform4f(string name, float v0, float v1, float v2, float v3)
    {
        Gl.UseProgram(m_RendererID);
        int location = Gl.GetUniformLocation(m_RendererID, name);
        Gl.Uniform4f(location, v0, v1, v2, v3);
    }

    public void SetUniform1f(string name, float value)
    {
        Gl.UseProgram(m_RendererID);
        int location = Gl.GetUniformLocation(m_RendererID, name);
        Gl.Uniform1f(location, value);
    }

}