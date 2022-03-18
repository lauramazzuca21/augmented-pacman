using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class Constants {

        public static Dictionary<KeyValuePair<Direction, Direction>, float> RotationVector = new Dictionary<KeyValuePair<Direction, Direction>, float>
        {
            { new KeyValuePair<Direction, Direction>(Direction.RIGHT, Direction.UP), 90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.RIGHT, Direction.DOWN), -90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.RIGHT, Direction.LEFT), -180.0f },
            { new KeyValuePair<Direction, Direction>(Direction.LEFT, Direction.UP), -90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.LEFT, Direction.DOWN), 90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.LEFT, Direction.RIGHT), 180.0f },
            { new KeyValuePair<Direction, Direction>(Direction.UP, Direction.LEFT), 90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.UP, Direction.DOWN), 180.0f },
            { new KeyValuePair<Direction, Direction>(Direction.UP, Direction.RIGHT), -90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.DOWN, Direction.UP), -180.0f },
            { new KeyValuePair<Direction, Direction>(Direction.DOWN, Direction.LEFT), -90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.DOWN, Direction.RIGHT), 90.0f },
            { new KeyValuePair<Direction, Direction>(Direction.LEFT, Direction.LEFT), 0.0f },
            { new KeyValuePair<Direction, Direction>(Direction.DOWN, Direction.DOWN), 0.0f },
            { new KeyValuePair<Direction, Direction>(Direction.RIGHT, Direction.RIGHT), 0.0f },
            { new KeyValuePair<Direction, Direction>(Direction.UP, Direction.UP), 0.0f }
        };
    }
    
}
