using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a mux with k input, each input with n wires. The output also has n wires.

    class BitwiseMultiwayMux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        //your code here

        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            int inputLength = (int)Math.Pow(2, cControlBits);
            BitwiseMux[] bitwiseMuxes = new BitwiseMux[inputLength -1];
            for (int i = 0; i < Inputs.Length; i++)
            {
                Inputs[i] = new WireSet(Size);

            }
            for (int i = 0; i < inputLength-1; i++)
            {
                bitwiseMuxes[i] = new BitwiseMux(Size);   
            }
            int controlCounter = 0;
            int GateNumber = 0;
            for (int i = 0; i < inputLength; i = i+2)
            {
                bitwiseMuxes[GateNumber].ConnectInput1(Inputs[i]);
                bitwiseMuxes[GateNumber].ConnectInput2(Inputs[i + 1]);
                bitwiseMuxes[GateNumber].ConnectControl(Control[controlCounter]);
                GateNumber++;
            }
            controlCounter++;
            int connection = 0;
            inputLength = inputLength / 2;
            int gatescounter = 0;
            while (GateNumber < bitwiseMuxes.Length)
            { 
                bitwiseMuxes[GateNumber].ConnectInput1(bitwiseMuxes[connection].Output);
                bitwiseMuxes[GateNumber].ConnectInput2(bitwiseMuxes[connection + 1].Output);
                bitwiseMuxes[GateNumber].ConnectControl(Control[controlCounter]);
                gatescounter++;
                connection = connection + 2;
                GateNumber++;
                if (gatescounter == inputLength/2)
                {
                    inputLength = inputLength / 2;
                    controlCounter++;
                    gatescounter = 0;
                }
            }
            Output.ConnectInput(bitwiseMuxes[(int)Math.Pow(2, cControlBits)-2].Output);

            

        }


        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }


        // TODO : Write Better Authentic Tests 
        public override bool TestGate()
        {
            for (int i = 0; i < Size; i++)
            {
                Inputs[0][i].Value = 1;
            }
            for (int i = 0; i < Control.Size; i++)
            {
                Control[i].Value = 0;
            }
            for (int i = 0; i < Size; i++)
            {
                if (this.Output[i].Value != 1)
                    return false;
            }
            for (int i = 0; i < Control.Size; i++)
            {
                Control[i].Value = 1;
            }
            for (int i = 0; i < Size; i++)
            {
                if (this.Output[i].Value != 0)
                    return false;
            }
            for (int i = 0; i < Size; i++)
            {
                Inputs[Inputs.Length - 1][i].Value = 0;
            }

            
            for (int i = 0; i < Size; i++)
            {
                if (this.Output[i].Value != 0)
                    return false;
            }
            return true;
        }

    }
}
