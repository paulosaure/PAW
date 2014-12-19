using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using SurfacePaint.Properties;

namespace SurfacePaint
{
    /// <summary>
    /// Enumérateur des différents sons pouvant être lu
    /// </summary>
    public enum EnumSound { CRAYON, GOMME }

    /// <summary>
    /// Classe static permettant de lire une ressource audio
    /// </summary>
    public static class Sounds
    {
        private static SoundPlayer sp = new SoundPlayer();

        /// <summary>
        /// Lire une ressource audio
        /// </summary>
        /// <param name="es">La ressource à lire</param>
        public static void Play(EnumSound es)
        {
            bool isLoop = false;
            switch (es)
            {
                case EnumSound.CRAYON: sp.Stream = Resources.crayon; break;
                case EnumSound.GOMME: sp.Stream = Resources.gomme; break;
                default: return;
            }

            sp.Stop();

            if (isLoop)
                sp.PlayLooping();
            else
                sp.Play();
        }

        /// <summary>
        /// Stopper la lecture de la ressource audio
        /// </summary>
        public static void Stop()
        {
            sp.Stop();
        }

    }
}
