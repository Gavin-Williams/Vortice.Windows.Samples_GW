// Copyright � Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Framework;

public abstract partial class Application : IDisposable
{
    private readonly AppPlatform _platform;

    public event EventHandler<EventArgs>? Disposed;

    protected Application()
    {
        _platform = AppPlatform.Create(this);
        //_platform.Activated += GamePlatform_Activated;
        //_platform.Deactivated += GamePlatform_Deactivated;

        //PlatformConstruct();

        Current = this;
    }

    public static Application? Current { get; private set; }

    public bool IsDisposed { get; private set; }
    public Window MainWindow => _platform.MainWindow;
    public virtual SizeI DefaultSize => new(800, 600);
    public bool EnableVerticalSync { get; set; } = true;

    ~Application()
    {
        Dispose(dispose: false);
    }

    public void Dispose()
    {
        Dispose(dispose: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool dispose)
    {
        if (dispose && !IsDisposed)
        {
            Disposed?.Invoke(this, EventArgs.Empty);
            IsDisposed = true;
        }
    }

    public void Run()
    {
        _platform.Run();

        if (_platform.IsBlockingRun)
        {
        }
    }

    internal void Tick()
    {
        if (!BeginDraw())
            return;

        Render();

        EndDraw();
    }

    protected virtual bool BeginDraw()
    {
        return true;
    }

    protected virtual void EndDraw()
    {
    }

    protected internal abstract void Render();

    // Platform events
    internal void OnDisplayChange()
    {

    }
}
