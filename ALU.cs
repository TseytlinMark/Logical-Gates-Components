using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class is used to implement the ALU
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        //inputs
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Control { get; private set; }

        //outputs
        public WireSet Output { get; private set; }
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }


        //your code here

        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            Output = new WireSet(Size);
            Control = new WireSet(6);
            Zero = new Wire();
            Negative = new Wire();
            BitwiseMultiwayMux Central = new BitwiseMultiwayMux(iSize, 6);
            WireSet z = new WireSet(iSize);
            WireSet one = new WireSet(iSize);
            BitwiseNotGate NotGate1 = new BitwiseNotGate(iSize);
            BitwiseNotGate NotGate2 = new BitwiseNotGate(iSize);
            MultiBitAdder Addx1 = new MultiBitAdder(iSize);
            MultiBitAdder Addy1 = new MultiBitAdder(iSize);
            MultiBitAdder xminus1 = new MultiBitAdder(iSize);
            MultiBitAdder yminus1 = new MultiBitAdder(iSize);
            MultiBitAdder xplusy = new MultiBitAdder(iSize);
            MultiBitAdder xminusy = new MultiBitAdder(iSize);
            MultiBitAdder yminusx = new MultiBitAdder(iSize);
            BitwiseAndGate bwAnd = new BitwiseAndGate(iSize);
            MultiBitAndGate mband1 = new MultiBitAndGate(iSize);
            MultiBitAndGate mband2 = new MultiBitAndGate(iSize);
            AndGate andGate = new AndGate();
            BitwiseOrGate bwOrg = new BitwiseOrGate(iSize);
            MultiBitOrGate orGate1 = new MultiBitOrGate(iSize);
            MultiBitOrGate orGate2 = new MultiBitOrGate(iSize);
            OrGate orGate = new OrGate();
            MultiBitOrGate zotp = new MultiBitOrGate(iSize);
            XorGate xor = new XorGate();


            z[0].Value = 0;
            one[0].Value = 1;
            for (int i = 1; i < Size; i++)
            {
                z[i].Value = 0;
                one[i].Value = 0;
            }
            Central.ConnectControl(Control);
            Central.ConnectInput(0, z);
            Central.ConnectInput(1, one);
            Central.ConnectInput(2, InputX);
            Central.ConnectInput(3, InputY);
            NotGate1.ConnectInput(InputX);
            NotGate2.ConnectInput(InputY);
            Central.ConnectInput(4, NotGate1.Output);
            Central.ConnectInput(5, NotGate2.Output);
            xplusy.ConnectInput1(InputX);
            xplusy.ConnectInput2(InputY);
            Central.ConnectInput(12, xplusy.Output);
            Addx1.ConnectInput1(InputX);
            Addx1.ConnectInput2(one);
            Central.ConnectInput(8, Addx1.Output);
            Addy1.ConnectInput1(InputY);
            Addy1.ConnectInput2(one);
            Central.ConnectInput(9, Addy1.Output);
            bwAnd.ConnectInput1(InputX);
            bwAnd.ConnectInput2(InputY);
            Central.ConnectInput(15, bwAnd.Output);
            mband1.ConnectInput(InputX);
            mband2.ConnectInput(InputY);
            andGate.ConnectInput1(mband2.Output);
            andGate.ConnectInput2(mband1.Output);
            if (andGate.Output.Value == 1)
                Central.ConnectInput(16, one);
            else
                Central.ConnectInput(16, z);
            orGate1.ConnectInput(InputX);
            orGate2.ConnectInput(InputY);
            orGate.ConnectInput1(orGate1.Output);
            orGate.ConnectInput2(orGate2.Output);
            if (orGate.Output.Value == 1)
                Central.ConnectInput(18, one);
            else
                Central.ConnectInput(18, z);
            bwOrg.ConnectInput1(InputY);
            bwOrg.ConnectInput2(InputX);
            Central.ConnectInput(17, bwOrg.Output);
            xminus1.ConnectInput1(InputX);
            int oneVal = one.GetValue();
            one.Set2sComplement(oneVal);
            xminus1.ConnectInput2(one);
            Central.ConnectInput(10, Addy1.Output);
            yminus1.ConnectInput1(InputY);
            yminus1.ConnectInput2(one);
            Central.ConnectInput(11, yminus1.Output);
            int xVal = InputX.GetValue();
            InputX.Set2sComplement(xVal);
            Central.ConnectInput(6, InputX);
            yminusx.ConnectInput1(InputY);
            yminusx.ConnectInput2(InputX);
            Central.ConnectInput(14, yminusx.Output);
            int yVal = InputX.GetValue();
            InputY.Set2sComplement(yVal);
            Central.ConnectInput(7, InputY);
            xVal = InputX.GetValue();
            InputX.Set2sComplement(xVal);
            xminusy.ConnectInput1(InputX);
            xminusy.ConnectInput2(InputY);
            Central.ConnectInput(13, xminusy.Output);
            zotp.ConnectInput(Central.Output);
            xor.ConnectInput1(zotp.Output);
            Wire positive = new Wire();
            positive.Value = 1;
            xor.ConnectInput2(positive);
            Zero.ConnectInput(xor.Output);
            Output.ConnectInput(Central.Output);
            Negative.ConnectInput(Output[Output.Size - 1]);
        }

        public override bool TestGate()
        {
            for (int i = 0; i < Control.Size; i++)
            {
                Control[i].Value = 0;
            }
            for (int i = 0; i < Output.Size; i++)
            {
                if (Output[i].Value != 0)
                    return false;
            }
            Control[1].Value = 1;
            for (int i = 0; i < Output.Size; i++)
            {
                if (Output[i].Value != InputX[i].Value)
                    return false;
            }
            return true;
        }
    }
}
