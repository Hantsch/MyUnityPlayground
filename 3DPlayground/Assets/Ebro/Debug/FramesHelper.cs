using UnityEngine;

public class FramesHelper
{
    private int FrameCount;
    private int FpsDisplayRate = 4;
    private float DeltaTime;
    private int _Frames;

    public int Frames
    {
        get
        {
            return this._Frames;
        }
    }

    public void Count()
    {
        this.FrameCount++;
        this.DeltaTime += Time.deltaTime;
        if (this.DeltaTime > 1.0 / this.FpsDisplayRate)
        {
            this._Frames = (int)Mathf.Round((float)FrameCount / this.DeltaTime);
            this.FrameCount = 0;
            this.DeltaTime -= 1.0f / this.FpsDisplayRate;
        }
    }
}