using System;

namespace Root.GameStarter
{
    public interface IStarter
    {
        event Action OnGameStart;
    }
}