using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; private set; }

        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        //An overflow bit for the summation computation
        public Wire Overflow { get; private set; }
        public HalfAdder HA0 { get;  set; }


        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(Size);
            Input2 = new WireSet(Size);
            Output = new WireSet(Size);
            HalfAdder HA0 = new HalfAdder();
            Overflow = new Wire();
            FullAdder[] FullAdders = new FullAdder[iSize];
            for (int i = 0; i < FullAdders.Length; i++)
            {
                FullAdders[i] = new FullAdder();
            }
            HA0.ConnectInput1(Input1[0]);
            HA0.ConnectInput2(Input2[0]);
            Output[0].ConnectInput(HA0.Output);
            FullAdders[1].CarryInput.Value = HA0.CarryOutput.Value;
            FullAdders[1].ConnectInput1(Input1[1]);
            FullAdders[1].ConnectInput2(Input2[1]);
            Output[1].ConnectInput(FullAdders[1].Output);
            for (int i = 2; i < FullAdders.Length; i++)
            {
                FullAdders[i].ConnectInput1(Input1[i]);
                FullAdders[i].ConnectInput2(Input2[i]);
                FullAdders[i].CarryInput.Value = FullAdders[i - 1].CarryOutput.Value;
                Output[i].ConnectInput(FullAdders[i].Output);
            }
            Overflow.ConnectInput(FullAdders[FullAdders.Length-1].CarryOutput);
            

        }

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            for (int i = 0; i < Input1.Size; i++)
            {
                this.Input1[i].Value = 1;
                this.Input2[i].Value = 0;
            }
            for (int i = 0; i < Output.Size; i++)
            {
                if (this.Output[i].Value != 1)
                    return false;
            }
            for (int i = 0; i < Input1.Size; i++)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 1;
            }
            for (int i = 0; i < Output.Size; i++)
            {
                if (Output[i].Value != 0)
                    return false;
            }
            if (Overflow.Value != 1)
                return false;
            return true;
        }
    }
}
