using System;
using System.Collections.Generic;
using System.Threading;

namespace Controllers.Scenes
{
    class Machines2AndBufferSupervisor
    {

        // #### VARIABLE CREATION TO ALLOCATE IN MEMORY ####
        private int currentState;
        private int evento;
        private Dictionary<(int, int), int> transiciones;
        private Dictionary<string, int> eventLabels;

        private Dictionary<int, (string, string)> eventLabelsInverse;

        private Dictionary<int, string> stateLabels;

        public void CreateController()
        {
            transiciones = new Dictionary<(int, int), int>();
            eventLabels = new Dictionary<string, int>();
            eventLabelsInverse = new Dictionary<int, (string, string)>();
            stateLabels = new Dictionary<int, string>();

            currentState = 7;
            //#########  TRANSICIONES START ############

            transiciones.Add((0, 5), 1);
            transiciones.Add((1, 1), 8);
            transiciones.Add((2, 7), 1);
            transiciones.Add((2, 3), 0);
            transiciones.Add((3, 4), 6);
            transiciones.Add((4, 4), 7);
            transiciones.Add((5, 7), 4);
            transiciones.Add((5, 3), 3);
            transiciones.Add((5, 4), 8);
            transiciones.Add((6, 5), 7);
            transiciones.Add((6, 0), 9);
            transiciones.Add((7, 0), 10);
            transiciones.Add((8, 7), 7);
            transiciones.Add((8, 3), 6);
            transiciones.Add((8, 0), 11);
            transiciones.Add((9, 6), 0);
            transiciones.Add((9, 2), 3);
            transiciones.Add((9, 5), 10);
            transiciones.Add((10, 6), 1);
            transiciones.Add((10, 2), 4);
            transiciones.Add((11, 6), 2);
            transiciones.Add((11, 2), 5);
            transiciones.Add((11, 7), 10);
            transiciones.Add((11, 3), 9);

            //#########  TRANSICIONES END ############


            //#########  STATELABEL START ############

            stateLabels.Add(0, "BufferLleno.NoHaFalladoMaq11.maq1Espera.Maq2Falla");
            stateLabels.Add(1, "BufferLleno.NoHaFalladoMaq11.maq1Espera.maq2Espera");
            stateLabels.Add(2, "BufferLleno.NoHaFalladoMaq11.maq1Espera.maq2Trabaja");
            stateLabels.Add(3, "BufferVacio.FalloMaq1.maq1Falla.Maq2Falla");
            stateLabels.Add(4, "BufferVacio.FalloMaq1.maq1Falla.maq2Espera");
            stateLabels.Add(5, "BufferVacio.FalloMaq1.maq1Falla.maq2Trabaja");
            stateLabels.Add(6, "BufferVacio.NoHaFalladoMaq11.maq1Espera.Maq2Falla");
            stateLabels.Add(7, "BufferVacio.NoHaFalladoMaq11.maq1Espera.maq2Espera");
            stateLabels.Add(8, "BufferVacio.NoHaFalladoMaq11.maq1Espera.maq2Trabaja");
            stateLabels.Add(9, "BufferVacio.NoHaFalladoMaq11.maq1Trabaja.Maq2Falla");
            stateLabels.Add(10, "BufferVacio.NoHaFalladoMaq11.maq1Trabaja.maq2Espera");
            stateLabels.Add(11, "BufferVacio.NoHaFalladoMaq11.maq1Trabaja.maq2Trabaja");

            //#########  STATELABEL END ############

            //#########  EVENTLABEL START ############

            eventLabels.Add("e1", 0);
            eventLabels.Add("e2", 1);
            eventLabels.Add("f1", 2);
            eventLabels.Add("f2", 3);
            eventLabels.Add("r1", 4);
            eventLabels.Add("r2", 5);
            eventLabels.Add("t1", 6);
            eventLabels.Add("t2", 7);

            eventLabelsInverse.Add(0, ("e1", "c"));
            eventLabelsInverse.Add(1, ("e2", "c"));
            eventLabelsInverse.Add(2, ("f1", "nc"));
            eventLabelsInverse.Add(3, ("f2", "nc"));
            eventLabelsInverse.Add(4, ("r1", "c"));
            eventLabelsInverse.Add(5, ("r2", "c"));
            eventLabelsInverse.Add(6, ("t1", "nc"));
            eventLabelsInverse.Add(7, ("t2", "nc"));

            //#########  EVENTLABEL END ############

            Console.WriteLine("\nCurrent state is: " + stateLabels[currentState] + "\n");
            Console.WriteLine("List of active events. Choose one and press enter: \n");
            for (int i = 0; i < eventLabels.Count; i++)
            {
                if (transiciones.ContainsKey((currentState, i)) && eventLabelsInverse[i].Item2 == "c")
                {
                    Console.WriteLine(i + ": " + eventLabelsInverse[i].Item1 + "\n");
                }
            }
            Console.WriteLine("Type event number and press enter to execute or press button on Factory I/O interface:\n");        }

        public bool IsInActiveEventsLights(string newState)
        {
            int newStateInt = eventLabels[newState];
            if (transiciones.ContainsKey((currentState, newStateInt)))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public bool IsInActiveEvents(int newState)
        {
            if (transiciones.ContainsKey((currentState, newState)) && eventLabelsInverse[newState].Item2 == "c")
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public bool IsInActiveEventsString(string newStateString)
        {
            int newState = eventLabels[newStateString];
            if (transiciones.ContainsKey((currentState, newState)) && eventLabelsInverse[newState].Item2 == "c")
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public void ListOfActiveEvents()
        {
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("List of active events. Choose one and press enter or wait:\n");

            for (int i = 0; i < eventLabels.Count; i++)
            {
                if (transiciones.ContainsKey((currentState, i)) && eventLabelsInverse[i].Item2 == "c")
                {
                    Console.WriteLine(i + ": " + eventLabelsInverse[i].Item1);
                }
            }
            Console.WriteLine("\n----------------------------------------");
        }

        public string StateName(int eventNumber)
        {
            if (eventLabelsInverse.ContainsKey(eventNumber))
            {
                return (eventLabelsInverse[eventNumber].Item1);
            }
            else
            {
                Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                Console.WriteLine("\nEvent number pressed does not exist. Try again.\n");
                Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                ListOfActiveEvents();
                return ("Event number pressed does not exist");
            }
        }

        public bool On(string eventoLabel)
        {
            evento = eventLabels[eventoLabel];
            if (transiciones.ContainsKey((currentState, evento)))
            {
                currentState = transiciones[(currentState, evento)];
                if (evento != 2 && evento != 3 && evento != 6 && evento != 7)
                {
                    Console.WriteLine("oooooooooooooooooooooooooooooooooooooooo\n");
                    Console.WriteLine(eventoLabel + " event approved");
                    Console.WriteLine("Current state is: " + stateLabels[currentState]);
                    Console.WriteLine("\noooooooooooooooooooooooooooooooooooooooo");
                    ListOfActiveEvents();
                }
                else
                {
                    Console.WriteLine("oooooooooooooooooooooooooooooooooooooooo\n");
                    Console.WriteLine(eventoLabel + " event is uncontrollable and must be enabled");
                    Console.WriteLine("Current state is: " + stateLabels[currentState]);
                    Console.WriteLine("\noooooooooooooooooooooooooooooooooooooooo");
                    ListOfActiveEvents();
                }
                return true;
            } else
            {
                Console.WriteLine(eventoLabel + " event blocked");
                Thread.Sleep(800);
                return false;
            }
        }
    }
}