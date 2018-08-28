using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CheckCalcTower
{
    static class Other
    {
        public const double LightSpeed = 299792458d;

        public static Vector2 coor31_6 = new Vector2(-53296.00f, -13536.71f);  //src31_6
        public static Vector2 coor31_32 = new Vector2(10115.37f, 5723.23f);    //src31_32
        public static Vector2 coor31_40 = new Vector2(-4341.66f, -64991.58f);  //src31_40
        public static Vector2 coor31_130 = new Vector2(-5077.12f, -6621.27f);  //src31_130
        public static Vector2 coor31_132 = new Vector2(13408.64f, -72532.50f); //src31_132
        public static Vector2 coor31_134 = new Vector2(30351.28f, -6309.96f);  //src31_134
                                                                               //! @fixme исправить координаты
        public static Tower src31_6_0 = new Tower(new Vector3(coor31_6, -120.01f), nameof(src31_6_0));

        public static Tower src31_32_0 = new Tower(new Vector3(coor31_32, 43.66f), nameof(src31_32_0));
        public static Tower src31_32_1 = new Tower(new Vector3(coor31_32, 43.66f), nameof(src31_32_1));
        public static Tower src31_32_2 = new Tower(new Vector3(coor31_32, 43.66f), nameof(src31_32_2));
        public static Tower src31_32_3 = new Tower(new Vector3(coor31_32, 43.66f), nameof(src31_32_3));

        public static Tower src31_40_0 = new Tower(new Vector3(coor31_40, -198.50f), nameof(src31_40_0));

        public static Tower src31_130_0 = new Tower(new Vector3(coor31_130, 69.71f), nameof(src31_130_0));

        public static Tower src31_132_0 = new Tower(new Vector3(coor31_132, -333.65f), nameof(src31_132_0));
        public static Tower src31_132_1 = new Tower(new Vector3(coor31_132, -333.65f), nameof(src31_132_1));
        public static Tower src31_132_2 = new Tower(new Vector3(coor31_132, -333.65f), nameof(src31_132_2));
        public static Tower src31_132_3 = new Tower(new Vector3(coor31_132, -333.65f), nameof(src31_132_3));

        public static Tower src31_134_0 = new Tower(new Vector3(coor31_134, -41.90f), nameof(src31_134_0));

        public static List<Tower> towers = new List<Tower>
        {
            src31_6_0,
            src31_32_0,
            src31_32_1,
            src31_32_2,
            src31_32_3,
            src31_40_0,
            src31_130_0,
            src31_132_0,
            src31_132_1,
            src31_132_2,
            src31_132_3,
            src31_134_0
        };
    }
}
