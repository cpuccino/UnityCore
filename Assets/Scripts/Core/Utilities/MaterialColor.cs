using UnityEngine;
using System.Collections.Generic;

namespace UnityCore.Utilities
{
    public abstract class MaterialColorBase
    {
        public Color32 Primary { get; }
        Dictionary<int, Color32> _swatch;

        protected Color32 GetSwatchColor(int index)
        {
            var color = _swatch.ContainsKey(index) ? _swatch[index] : Primary;
            return color;
        }

        public MaterialColorBase(Color32 primary, Dictionary<int, Color32> swatch)
        {
            Primary = primary;
            _swatch = swatch;
        }

        public Color32 this[int key] 
        {
            get { return GetSwatchColor(key); } 
        }
    }

    public class MaterialColorMain : MaterialColorBase
    {
        public Color32 Shade50 { get { return GetSwatchColor(50); } }

        public Color32 Shade100 { get { return GetSwatchColor(100); } }

        public Color32 Shade200 { get { return GetSwatchColor(200); } }

        public Color32 Shade300 { get { return GetSwatchColor(300); } }

        public Color32 Shade400 { get { return GetSwatchColor(400); } }

        public Color32 Shade500 { get { return GetSwatchColor(500); } }

        public Color32 Shade600 { get { return GetSwatchColor(600); } }

        public Color32 Shade700 { get { return GetSwatchColor(700); } }

        public Color32 Shade800 { get { return GetSwatchColor(800); } }

        public Color32 Shade900 { get { return GetSwatchColor(900); } }

        public MaterialColorMain(Color32 primary, Dictionary<int, Color32> swatch): base(primary, swatch) { }
    }

    public class MaterialColorAccent: MaterialColorBase
    {
        public Color32 Shade100 { get { return GetSwatchColor(100); } }

        public Color32 Shade200 { get { return GetSwatchColor(200); } }

        public Color32 Shade400 { get { return GetSwatchColor(400); } }

        public Color32 Shade700 { get { return GetSwatchColor(700); } }

        public MaterialColorAccent(Color32 primary, Dictionary<int, Color32> swatch): base(primary, swatch) { }
    }

    public class MaterialColor
    {
        static Color32 redPrimary = new Color32(244, 67, 54, 255);
        
        public static readonly MaterialColorMain Red = new MaterialColorMain(redPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(255, 235, 238, 255),
                [100] = new Color32(255, 205, 210, 255),
                [200] = new Color32(239, 154, 154, 255),
                [300] = new Color32(229, 115, 155, 255),
                [400] = new Color32(239, 83, 80, 255),
                [500] = redPrimary,
                [600] = new Color32(229, 57, 53, 255),
                [700] = new Color32(211, 47, 47, 255),
                [800] = new Color32(198, 40, 40, 255),
                [900] = new Color32(183, 28, 28, 255)
            }
        );

        static Color32 pinkPrimary = new Color32(233, 30, 99, 255);

        public static readonly MaterialColorMain Pink = new MaterialColorMain(pinkPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(252, 228, 236, 255),
                [100] = new Color32(248, 187, 208, 255),
                [200] = new Color32(244, 143, 177, 255),
                [300] = new Color32(240, 98, 146, 255),
                [400] = new Color32(236, 64, 122, 255),
                [500] = pinkPrimary,
                [600] = new Color32(216, 27, 96, 255),
                [700] = new Color32(194, 24, 91, 255),
                [800] = new Color32(173, 20, 87, 255),
                [900] = new Color32(136, 14, 79, 255)
            }
        );

        static Color32 purplePrimary = new Color32(156, 39, 176, 255);

        public static readonly MaterialColorMain Purple = new MaterialColorMain(purplePrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(243, 229, 245, 255),
                [100] = new Color32(225, 190, 231, 255),
                [200] = new Color32(206, 147, 216, 255),
                [300] = new Color32(186, 104, 200, 255),
                [400] = new Color32(171, 71, 188, 255),
                [500] = purplePrimary,
                [600] = new Color32(142, 36, 170, 255),
                [700] = new Color32(123, 31, 162, 255),
                [800] = new Color32(106, 27, 154, 255),
                [900] = new Color32(74, 20, 140, 255)
            }
        );

        static Color32 deepPurplePrimary = new Color32(103, 58, 183, 255);

        public static readonly MaterialColorMain DeepPurple = new MaterialColorMain(deepPurplePrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(237, 231, 246, 255),
                [100] = new Color32(209, 196, 233, 255),
                [200] = new Color32(179, 157, 219, 255),
                [300] = new Color32(149, 117, 205, 255),
                [400] = new Color32(126, 87, 194, 255),
                [500] = deepPurplePrimary,
                [600] = new Color32(94, 53, 177, 255),
                [700] = new Color32(81, 45, 168, 255),
                [800] = new Color32(69, 39, 160, 255),
                [900] = new Color32(49, 27, 146, 255)
            }
        );

        static Color32 indigoPrimary = new Color32(63, 81, 181, 255);

        public static readonly MaterialColorMain Indigo = new MaterialColorMain(indigoPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(232, 234, 246, 255),
                [100] = new Color32(197, 202, 233, 255),
                [200] = new Color32(159, 168, 218, 255),
                [300] = new Color32(121, 134, 203, 255),
                [400] = new Color32(92, 107, 192, 255),
                [500] = indigoPrimary,
                [600] = new Color32(57, 73, 171, 255),
                [700] = new Color32(48, 63, 159, 255),
                [800] = new Color32(40, 53, 147, 255),
                [900] = new Color32(26, 35, 126, 255)
            }
        );

        static Color32 bluePrimary = new Color32(33, 150, 243, 255);

        public static readonly MaterialColorMain Blue = new MaterialColorMain(bluePrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(227, 242, 253, 255),
                [100] = new Color32(187, 222, 251, 255),
                [200] = new Color32(144, 202, 249, 255),
                [300] = new Color32(100, 181, 246, 255),
                [400] = new Color32(66, 165, 245, 255),
                [500] = bluePrimary,
                [600] = new Color32(30, 136, 229, 255),
                [700] = new Color32(25, 118, 210, 255),
                [800] = new Color32(21, 101, 192, 255),
                [900] = new Color32(13, 71, 161, 255)
            }
        );

        static Color32 lightBluePrimary = new Color32(3, 169, 244, 255);

        public static readonly MaterialColorMain LightBlue = new MaterialColorMain(lightBluePrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(255, 245, 254, 255),
                [100] = new Color32(179, 229, 252, 255),
                [200] = new Color32(129, 212, 250, 255),
                [300] = new Color32(79, 195, 247, 255),
                [400] = new Color32(41, 182, 246, 255),
                [500] = lightBluePrimary,
                [600] = new Color32(3, 155, 229, 255),
                [700] = new Color32(2, 136, 209, 255),
                [800] = new Color32(2, 119, 189, 255),
                [900] = new Color32(1, 87, 155, 255)
            }
        );

        static Color32 cyanPrimary = new Color32(0, 188, 212, 255);

        public static readonly MaterialColorMain Cyan = new MaterialColorMain(cyanPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(224, 247, 250, 255),
                [100] = new Color32(178, 235, 242, 255),
                [200] = new Color32(128, 222, 234, 255),
                [300] = new Color32(77, 208, 225, 255),
                [400] = new Color32(38, 198, 218, 255),
                [500] = cyanPrimary,
                [600] = new Color32(0, 172, 193, 255),
                [700] = new Color32(0, 151, 167, 255),
                [800] = new Color32(0, 131, 143, 255),
                [900] = new Color32(0, 96, 100, 255)
            }
        );

        static Color32 tealPrimary = new Color32(0, 150, 136, 255);

        public static readonly MaterialColorMain Teal = new MaterialColorMain(tealPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(224, 242, 241, 255),
                [100] = new Color32(178, 223, 219, 255),
                [200] = new Color32(128, 203, 196, 255),
                [300] = new Color32(77, 182, 172, 255),
                [400] = new Color32(38, 166, 154, 255),
                [500] = tealPrimary,
                [600] = new Color32(0, 137, 123, 255),
                [700] = new Color32(0, 121, 107, 255),
                [800] = new Color32(0, 105, 92, 255),
                [900] = new Color32(0, 77, 64, 255)
            }
        );

        static Color32 greenPrimary = new Color32(76, 175, 80, 255);

        public static readonly MaterialColorMain Green = new MaterialColorMain(greenPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(232, 245, 233, 255),
                [100] = new Color32(200, 230, 201, 255),
                [200] = new Color32(165, 214, 167, 255),
                [300] = new Color32(129, 199, 132, 255),
                [400] = new Color32(102, 187, 106, 255),
                [500] = greenPrimary,
                [600] = new Color32(67, 160, 71, 255),
                [700] = new Color32(56, 142, 60, 255),
                [800] = new Color32(46, 125, 50, 255),
                [900] = new Color32(27, 94, 32, 255)
            }
        );

        static Color32 lightGreenPrimary = new Color32(139, 195, 74, 255);

        public static readonly MaterialColorMain LightGreen = new MaterialColorMain(lightGreenPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(241, 248, 233, 255),
                [100] = new Color32(220, 237, 200, 255),
                [200] = new Color32(197, 225, 165, 255),
                [300] = new Color32(174, 213, 129, 255),
                [400] = new Color32(156, 204, 101, 255),
                [500] = lightGreenPrimary,
                [600] = new Color32(124, 179, 66, 255),
                [700] = new Color32(104, 159, 56, 255),
                [800] = new Color32(85, 139, 47, 255),
                [900] = new Color32(51, 105, 30, 255)
            }
        );

        static Color32 limePrimary = new Color32(205, 220, 57, 255);

        public static readonly MaterialColorMain Lime = new MaterialColorMain(limePrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(249, 251, 231, 255),
                [100] = new Color32(240, 244, 195, 255),
                [200] = new Color32(230, 238, 156, 255),
                [300] = new Color32(220, 231, 117, 255),
                [400] = new Color32(212, 225, 87, 255),
                [500] = limePrimary,
                [600] = new Color32(192, 202, 51, 255),
                [700] = new Color32(175, 180, 43, 255),
                [800] = new Color32(158, 157, 36, 255),
                [900] = new Color32(130, 119, 23, 255)
            }
        );

        static Color32 yellowPrimary = new Color32(255, 235, 59, 255);

        public static readonly MaterialColorMain Yellow = new MaterialColorMain(yellowPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(255, 254, 231, 255),
                [100] = new Color32(255, 249, 196, 255),
                [200] = new Color32(255, 245, 157, 255),
                [300] = new Color32(255, 241, 118, 255),
                [400] = new Color32(255, 238, 88, 255),
                [500] = yellowPrimary,
                [600] = new Color32(253, 216, 53, 255),
                [700] = new Color32(251, 192, 45, 255),
                [800] = new Color32(249, 168, 37, 255),
                [900] = new Color32(245, 127, 23, 255)
            }
        );

        static Color32 amberPrimary = new Color32(255, 193, 7, 255);

        public static readonly MaterialColorMain Amber = new MaterialColorMain(amberPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(255, 248, 225, 255),
                [100] = new Color32(255, 236, 179, 255),
                [200] = new Color32(255, 224, 130, 255),
                [300] = new Color32(255, 213, 79, 255),
                [400] = new Color32(255, 202, 40, 255),
                [500] = amberPrimary,
                [600] = new Color32(255, 179, 0, 255),
                [700] = new Color32(255, 160, 0, 255),
                [800] = new Color32(255, 143, 0, 255),
                [900] = new Color32(255, 111, 0, 255)
            }
        );

        static Color32 orangePrimary = new Color32(255, 152, 0, 255);

        public static readonly MaterialColorMain Orange = new MaterialColorMain(orangePrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(255, 243, 224, 255),
                [100] = new Color32(255, 224, 178, 255),
                [200] = new Color32(255, 204, 128, 255),
                [300] = new Color32(255, 183, 77, 255),
                [400] = new Color32(255, 167, 38, 255),
                [500] = orangePrimary,
                [600] = new Color32(251, 140, 0, 255),
                [700] = new Color32(245, 124, 0, 255),
                [800] = new Color32(129, 108, 0, 255),
                [900] = new Color32(120, 81, 0, 255)
            }
        );

        static Color32 deepOrangePrimary = new Color32(255, 87, 34, 255);

        public static readonly MaterialColorMain DeepOrange = new MaterialColorMain(deepOrangePrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(251, 233, 231, 255),
                [100] = new Color32(255, 204, 188, 255),
                [200] = new Color32(255, 171, 145, 255),
                [300] = new Color32(255, 138, 101, 255),
                [400] = new Color32(255, 112, 67, 255),
                [500] = deepOrangePrimary,
                [600] = new Color32(244, 81, 30, 255),
                [700] = new Color32(230, 74, 25, 255),
                [800] = new Color32(216, 67, 21, 255),
                [900] = new Color32(191, 54, 12, 255)
            }
        );

        static Color32 brownPrimary = new Color32(121, 85, 72, 255);

        public static readonly MaterialColorMain Brown = new MaterialColorMain(brownPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(239, 235, 233, 255),
                [100] = new Color32(215, 204, 200, 255),
                [200] = new Color32(188, 170, 164, 255),
                [300] = new Color32(161, 136, 127, 255),
                [400] = new Color32(141, 110, 99, 255),
                [500] = brownPrimary,
                [600] = new Color32(109, 76, 65, 255),
                [700] = new Color32(93, 64, 55, 255),
                [800] = new Color32(78, 52, 46, 255),
                [900] = new Color32(62, 39, 35, 255)
            }
        );

        static Color32 greyPrimary = new Color32(158, 158, 158, 255);

        public static readonly MaterialColorMain Grey = new MaterialColorMain(greyPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(250, 250, 250, 255),
                [100] = new Color32(245, 245, 245, 255),
                [200] = new Color32(238, 238, 238, 255),
                [300] = new Color32(224, 224, 224, 255),
                [400] = new Color32(189, 189, 189, 255),
                [500] = greyPrimary,
                [600] = new Color32(117, 117, 117, 255),
                [700] = new Color32(97, 97, 97, 255),
                [800] = new Color32(66, 66, 66, 255),
                [900] = new Color32(33, 33, 33, 255)
            }
        );

        static Color32 blueGreyPrimary = new Color32(96, 125, 139, 255);

        public static readonly MaterialColorMain BlueGrey = new MaterialColorMain(blueGreyPrimary, new Dictionary<int, Color32>()
            {
                [50] = new Color32(236, 239, 241, 255),
                [100] = new Color32(207, 216, 220, 255),
                [200] = new Color32(176, 190, 197, 255),
                [300] = new Color32(144, 164, 174, 255),
                [400] = new Color32(120, 144, 156, 255),
                [500] = blueGreyPrimary,
                [600] = new Color32(84, 110, 122, 255),
                [700] = new Color32(69, 90, 100, 255),
                [800] = new Color32(55, 71, 79, 255),
                [900] = new Color32(38, 50, 56, 255)
            }
        );

        static Color32 redAccentPrimary = new Color32(255, 82, 82, 255);

        public static readonly MaterialColorAccent RedAccent = new MaterialColorAccent(redAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(255, 138, 128, 255),
                [200] = redAccentPrimary,
                [400] = new Color32(255, 23, 68, 255),
                [700] = new Color32(213, 0, 0, 255)
            }
        );

        static Color32 pinkAccentPrimary = new Color32(255, 64, 129, 255);

        public static readonly MaterialColorAccent PinkAccent = new MaterialColorAccent(pinkAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(255, 128, 171, 255),
                [200] = pinkAccentPrimary,
                [400] = new Color32(245, 0, 87, 255),
                [700] = new Color32(197, 17, 98, 255)
            }
        );

        static Color32 purpleAccentPrimary = new Color32(224, 64, 251, 255);

        public static readonly MaterialColorAccent PurpleAccent = new MaterialColorAccent(purpleAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(234, 128, 252, 255),
                [200] = purpleAccentPrimary,
                [400] = new Color32(213, 0, 249, 255),
                [700] = new Color32(170, 0, 255, 255)
            }
        );

        static Color32 deepPurpleAccentPrimary = new Color32(124, 77, 255, 255);

        public static readonly MaterialColorAccent DeepPurpleAccent = new MaterialColorAccent(deepPurpleAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(179, 136, 255, 255),
                [200] = deepPurpleAccentPrimary,
                [400] = new Color32(101, 31, 255, 255),
                [700] = new Color32(98, 0, 234, 255)
            }
        );

        static Color32 indigoAccentPrimary = new Color32(83, 109, 254, 255);

        public static readonly MaterialColorAccent IndigoAccent = new MaterialColorAccent(indigoAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(140, 158, 255, 255),
                [200] = indigoAccentPrimary,
                [400] = new Color32(61, 90, 254, 255),
                [700] = new Color32(48, 79, 254, 255)
            }
        );

        static Color32 blueAccentPrimary = new Color32(68, 138, 255, 255);

        public static readonly MaterialColorAccent BlueAccent = new MaterialColorAccent(blueAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(130, 177, 255, 255),
                [200] = blueAccentPrimary,
                [400] = new Color32(41, 121, 255, 255),
                [700] = new Color32(41, 98, 255, 255)
            }
        );

        static Color32 lightBlueAccentPrimary = new Color32(64, 196, 255, 255);

        public static readonly MaterialColorAccent LightBlueAccent = new MaterialColorAccent(lightBlueAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(128, 216, 255, 255),
                [200] = lightBlueAccentPrimary,
                [400] = new Color32(0, 176, 255, 255),
                [700] = new Color32(0, 145, 234, 255)
            }
        );

        static Color32 cyanAccentPrimary = new Color32(24, 255, 255, 255);

        public static readonly MaterialColorAccent CyanAccent = new MaterialColorAccent(cyanAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(132, 255, 255, 255),
                [200] = cyanAccentPrimary,
                [400] = new Color32(0, 229, 255, 255),
                [700] = new Color32(0, 184, 212, 255)
            }
        );

        static Color32 tealAccentPrimary = new Color32(100, 255, 218, 255);

        public static readonly MaterialColorAccent TealAccent = new MaterialColorAccent(tealAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(167, 255, 235, 255),
                [200] = tealAccentPrimary,
                [400] = new Color32(29, 233, 182, 255),
                [700] = new Color32(0, 191, 165, 255)
            }
        );

        static Color32 greenAccentPrimary = new Color32(105, 240, 174, 255);

        public static readonly MaterialColorAccent GreenAccent = new MaterialColorAccent(greenAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(185, 246, 202, 255),
                [200] = greenAccentPrimary,
                [400] = new Color32(0, 230, 118, 255),
                [700] = new Color32(0, 200, 83, 255)
            }
        );

        static Color32 lightGreenAccentPrimary = new Color32(178, 255, 89, 255);

        public static readonly MaterialColorAccent LightGreenAccent = new MaterialColorAccent(lightGreenAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(204, 255, 144, 255),
                [200] = lightGreenAccentPrimary,
                [400] = new Color32(118, 255, 3, 255),
                [700] = new Color32(100, 221, 23, 255)
            }
        );

        static Color32 limeAccentPrimary = new Color32(238, 255, 65, 255);

        public static readonly MaterialColorAccent LimeAccent = new MaterialColorAccent(limeAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(244, 255, 129, 255),
                [200] = limeAccentPrimary,
                [400] = new Color32(198, 255, 0, 255),
                [700] = new Color32(174, 234, 0, 255)
            }
        );

        static Color32 yellowAccentPrimary = new Color32(255, 255, 0, 255);

        public static readonly MaterialColorAccent YellowAccent = new MaterialColorAccent(yellowAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(255, 255, 141, 255),
                [200] = yellowAccentPrimary,
                [400] = new Color32(255, 234, 0, 255),
                [700] = new Color32(255, 214, 0, 255)
            }
        );
        
        static Color32 amberAccentPrimary = new Color32(255, 215, 64, 255);

        public static readonly MaterialColorAccent AmberAccent = new MaterialColorAccent(amberAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(255, 229, 127, 255),
                [200] = amberAccentPrimary,
                [400] = new Color32(255, 196, 0, 255),
                [700] = new Color32(255, 171, 0, 255)
            }
        );

        static Color32 orangeAccentPrimary = new Color32(255, 171, 64, 255);

        public static readonly MaterialColorAccent OrangeAccent = new MaterialColorAccent(orangeAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(255, 209, 128, 255),
                [200] = orangeAccentPrimary,
                [400] = new Color32(255, 145, 0, 255),
                [700] = new Color32(255, 109, 0, 255)
            }
        );

        static Color32 deepOrangeAccentPrimary = new Color32(255, 110, 64, 255);

        public static readonly MaterialColorAccent DeepOrangeAccent = new MaterialColorAccent(deepOrangeAccentPrimary, new Dictionary<int, Color32>()
            {
                [100] = new Color32(255, 158, 128, 255),
                [200] = deepOrangeAccentPrimary,
                [400] = new Color32(255, 61, 0, 255),
                [700] = new Color32(221, 44, 0, 255)
            }
        );
    }
}
