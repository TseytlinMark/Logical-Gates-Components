using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a HalfAdder, taking as input 2 bits - 2 numbers and computing the result in the output, and the carry out.

    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; private set; }
        private AndGate And1;
        private XorGate Xor1;



        public HalfAdder()
        {
            And1 = new AndGate();
            Xor1 = new XorGate();
            //Input1 = new Wire();
            //Input2 = new Wire();
            CarryOutput = new Wire();
            And1.ConnectInput1(Input1);
            And1.ConnectInput2(Input2);
            Xor1.ConnectInput1(Input1);
            Xor1.ConnectInput2(Input2);
            Output.ConnectInput(Xor1.Output);
            CarryOutput.ConnectInput(And1.Output);

        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (CarryOutput.Value != 0 || Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (CarryOutput.Value != 0 || Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (CarryOutput.Value != 0 || Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (CarryOutput.Value != 1 || Output.Value != 0)
                return false;
            return true;
        }
    }
}
