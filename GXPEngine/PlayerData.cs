using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class PlayerData
{
    const int startLives = 3;

    int _lives = 0;

    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            int oldLives = _lives;
            _lives = value;
            if (_lives < 0)
            {
                _lives = 0;
                Console.WriteLine("Warning lives cannot be negative {0}, new value: {1}.", oldLives, value);
            }
            Console.WriteLine("Player lives " + _lives);

        }
    }

    public PlayerData()
    {
        Reset();
    }

    public void Reset()
    {
        _lives = startLives;
        Console.WriteLine("Reseting player data. Lives = {0}", _lives);
    }
}

