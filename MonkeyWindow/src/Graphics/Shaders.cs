using OpenGL;

namespace MonkeyWindow.Graphics;

public class Shaders
{

    public uint Program { get; private set; }

    public Shaders(string vertexShader, string fragmentShader)
    {
        var vs = CompileShader(ShaderType.VertexShader, vertexShader);
        var fs = CompileShader(ShaderType.FragmentShader, fragmentShader);

        Program = Gl.CreateProgram();

        Gl.AttachShader(Program, vs);
        Gl.AttachShader(Program, fs);
        Gl.LinkProgram(Program);

        Gl.DeleteShader(vs);
        Gl.DeleteShader(fs);
    }

    public void Use()
    {
        Gl.UseProgram(Program);
    }

    private uint CompileShader(ShaderType type, string source)
    {
        uint shader = Gl.CreateShader(type);
        Gl.ShaderSource(shader, source);
        Gl.CompileShader(shader);

        bool compiled = Gl.GetShaderCompileStatus(shader);

        if(!compiled)
        {
            string infoLog = Gl.GetShaderInfoLog(shader);
            Console.WriteLine(infoLog);
            Gl.DeleteShader(shader);
            return 0;
        }

        return shader;
    }

}