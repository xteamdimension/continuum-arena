﻿using System;
using Microsoft.Xna.Framework;
using Continuum.Elements;
using Continuum.State;
using Continuum.Utilities;

namespace Continuum.Management
{

    /// <summary>
    /// Component di gestione di tutte le istanze dei tachioni.
    /// </summary>
    public class TachyonManager
    {
        private GameState gs;

        public TachyonManager(GameState gameState)
        {
            gs = gameState;
        }

        /// <summary>
        /// Aggiorna tutte le istanze della classe Tachyon presenti nella lista
        /// Ed elimina quelle che hanno terminato il loro compito
        /// </summary>
        public void Update()
        {
            if (!gs.pause)
            {
                Tachyon[] temp = new Tachyon[gs.tachyons.Count];
                int i = 0;

                //Aggiorna tutte le istanze della classe Tachyon presenti nella lista
                foreach (Tachyon x in gs.tachyons)
                {
                    x.Update();
                    if (!Utility.IsInScreenSpace(Utility.newRectangleFromCenterPosition(x.CurrentPosition, 30, 30)))
                    {
                        x.lifeState = LifeState.DEAD;
                    }

                    if (x.lifeState == LifeState.DELETING)
                    {
                        temp[i] = x;
                        i++;
                    }
                }

                //Elimina le istanze della classe Tachyon che hanno terminato il loro ciclo di vita
                for (i = 0; i < temp.Length && temp[i] != null; i++)
                {
                    gs.tachyons.Remove(temp[i]);
                }
            }
        }
    }
}
