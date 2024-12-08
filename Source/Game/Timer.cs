using FlaxEngine;

namespace Game;

class Timer
{
    float currentTime = 0;
    bool paused = false;
    bool finished = false;
    public void Start(float time)
    {
        currentTime = time;
    }

    public void Update()
    {
        if (!paused && !finished)
        {
            currentTime -= Time.DeltaTime;
            if (currentTime <= 0)
            {
                finished = true;
            }
        }
    }

    public bool Finished()
    {
        return finished;
    }

    public void Pause() => paused = true;
    public void Resume() => paused = false;
}