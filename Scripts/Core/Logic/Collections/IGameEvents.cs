using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IGameEvents
{
    void LoadNextLvl();
    void RestartLvl();
    void StopLvlGame();
    void StartLvlGame();
}
