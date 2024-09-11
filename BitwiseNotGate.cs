using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This bitwise gate takes as input one WireSet containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        //your code here

        public BitwiseNotGate(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            NotGate[] notGates = new NotGate[iSize];
            for (int i = 0; i < iSize; i++)
            {
                notGates[i] = new NotGate();
                notGates[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(notGates[i].Output);
            }
        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(not)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 0;
                if (Output[i].Value != 1)
                    return false;
            }
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
                if (Output[i].Value != 0)
                    return false;
            }
            Input[0].Value = 0;
            for (int i = 1; i < Size; i++)
            {
                Input[i].Value = 1;
                if (Output[i].Value != 0)
                    return false;
            }
            if (Output[0].Value != 1)
                return false;
            return true;

        }
    }
}
