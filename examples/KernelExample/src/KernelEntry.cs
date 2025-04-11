using System.Runtime;
using System.Runtime.CompilerServices;
using Cosmos.Boot.Limine;
using EarlyBird;

using static EarlyBird.Graphics;

unsafe class Program
{
    static readonly LimineFramebufferRequest Framebuffer = new();
    static readonly LimineHHDMRequest HHDM = new();

    [RuntimeExport("kmain")]
    static void Main()
    {
        MemoryOp.InitializeHeap(HHDM.Offset, 0x1000000);
        LimineFramebuffer* fb = Framebuffer.Response->Framebuffers[0];
        Canvas.Address = (uint*)fb->Address;
        Canvas.Pitch = (uint)fb->Pitch;
        Canvas.Width = (uint)fb->Width;
        Canvas.Height = (uint)fb->Height;

        Canvas.ClearScreen(Color.Black);

        Canvas.DrawString("CosmosOS booted.", 0, 0, Color.White);

        Serial.ComInit();

        Canvas.DrawString("UART started.", 0, 28, Color.White);

        Serial.WriteString("Hello from UART\n");

        while (true);
    }
}

public unsafe static class Serial
{
    [MethodImpl(MethodImplOptions.InternalCall)]
    [RuntimeImport("*", "com_init")]
    public static extern void ComInit();

    [MethodImpl(MethodImplOptions.InternalCall)]
    [RuntimeImport("*", "com_write")]
    public static extern void ComWrite(char value);

    public static void WriteString(string str)
    {
        fixed (char* ptr = str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                ComWrite(ptr[i]);
            }
        }
    }
}