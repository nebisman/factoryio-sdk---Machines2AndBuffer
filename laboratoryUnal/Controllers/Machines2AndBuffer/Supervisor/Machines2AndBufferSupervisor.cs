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

            currentState = 6;
            //#########  TRANSICIONES START ############

            transiciones.Add((0, 5), 4);
            transiciones.Add((1, 5), 6);
            transiciones.Add((1, 6), 2);
            transiciones.Add((2, 0), 0);
            transiciones.Add((2, 2), 3);
            transiciones.Add((2, 5), 8);
            transiciones.Add((3, 5), 10);
            transiciones.Add((4, 4), 6);
            transiciones.Add((5, 1), 0);
            transiciones.Add((5, 3), 4);
            transiciones.Add((5, 4), 7);
            transiciones.Add((6, 6), 8);
            transiciones.Add((7, 1), 1);
            transiciones.Add((7, 3), 6);
            transiciones.Add((7, 6), 9);
            transiciones.Add((8, 0), 4);
            transiciones.Add((8, 2), 10);
            transiciones.Add((9, 0), 5);
            transiciones.Add((9, 1), 2);
            transiciones.Add((9, 2), 11);
            transiciones.Add((9, 3), 8);
            transiciones.Add((10, 7), 7);
            transiciones.Add((11, 1), 3);
            transiciones.Add((11, 3), 10);

            //#########  TRANSICIONES END ############


            //#########  STATELABEL START ############

            stateLabels.Add(0, "KO_E_d1_d2");
            stateLabels.Add(1, "KO_E_i1_d2");
            stateLabels.Add(2, "KO_E_w1_d2");
            stateLabels.Add(3, "KO_F_i1_d2");
            stateLabels.Add(4, "OK_E_d1_i2");
            stateLabels.Add(5, "OK_E_d1_w2");
            stateLabels.Add(6, "OK_E_i1_i2");
            stateLabels.Add(7, "OK_E_i1_w2");
            stateLabels.Add(8, "OK_E_w1_i2");
            stateLabels.Add(9, "OK_E_w1_w2");
            stateLabels.Add(10, "OK_F_i1_i2");
            stateLabels.Add(11, "OK_F_i1_w2");

            //#########  STATELABEL END ############

            //#########  EVENTLABEL START ############

            eventLabels.Add("b1", 0);
            eventLabels.Add("b2", 1);
            eventLabels.Add("f1", 2);
            eventLabels.Add("f2", 3);
            eventLabels.Add("r1", 4);
            eventLabels.Add("r2", 5);
            eventLabels.Add("s1", 6);
            eventLabels.Add("s2", 7);

            eventLabelsInverse.Add(0, ("b1", "nc"));
            eventLabelsInverse.Add(1, ("b2", "nc"));
            eventLabelsInverse.Add(2, ("f1", "nc"));
            eventLabelsInverse.Add(3, ("f2", "nc"));
            eventLabelsInverse.Add(4, ("r1", "c"));
            eventLabelsInverse.Add(5, ("r2", "c"));
            eventLabelsInverse.Add(6, ("s1", "c"));
            eventLabelsInverse.Add(7, ("s2", "c"));

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
                if (evento != 0 && evento != 1 && evento != 2 && evento != 3)
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