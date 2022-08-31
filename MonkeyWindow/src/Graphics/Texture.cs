
using System.Drawing;
using System.Drawing.Imaging;
using OpenGL;
using static StbImageSharp.StbImage;

public class Texture
{

    private uint m_RendererID;
    private string m_FilePath;
    int m_Width, m_Height;

    public Texture(string filePath, OpenGL.PixelFormat pixelFormat = OpenGL.PixelFormat.Rgba, TextureParameter filter = TextureParameter.Linear)
    {
        m_FilePath = filePath;
    
        var data = GetImageData(filePath);

        m_Width = data.Width;
        m_Height = data.Height;

        m_RendererID = Gl.GenTexture();
        Gl.BindTexture(TextureTarget.Texture2D, m_RendererID);
        Gl.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, filter);
        Gl.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, filter);
        Gl.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureParameter.ClampToEdge);
        Gl.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureParameter.ClampToEdge);

        Gl.TexImage2D(
            TextureTarget.Texture2D, 0,
            PixelInternalFormat.Rgba8,
            m_Width, m_Height, 0,
            pixelFormat, PixelType.UnsignedByte,
            data.Scan0);

        Gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    public static BitmapData GetImageData(string filePath)
    {
        Bitmap bitmap = new Bitmap(filePath);

        bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        return data;
    }

    ~Texture()
    {
        Gl.DeleteTexture(m_RendererID);
    }

    public void Bind(int slot = 0)
    {
        Gl.ActiveTexture(0x84C0+ slot);
        Gl.BindTexture(TextureTarget.Texture2D, m_RendererID);
    }

    public void Unbind()
    {
        Gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    public int GetWidth() { return m_Width; }
    public int GetHeight() { return m_Height; }


}