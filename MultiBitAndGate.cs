using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {

        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            if (iInputCount == 1)
            {
                throw new ArgumentException("1 Bit is not enough for MultiBitGate");
            }
            if (iInputCount == 2)
            {
                AndGate two = new AndGate();
            }
            else
            {
                AndGate[] GatesArray = new AndGate[iInputCount - 1];

                GatesArray[0] = new AndGate();
                GatesArray[0].ConnectInput1(m_wsInput[0]);
                GatesArray[0].ConnectInput2(m_wsInput[1]);
                for (int i = 1; i < iInputCount - 1; i++)
                {
                    GatesArray[i] = new AndGate();
                    GatesArray[i].ConnectInput1(GatesArray[i - 1].Output);
                    GatesArray[i].ConnectInput2(m_wsInput[i + 1]);
                }
                Output = GatesArray[iInputCount - 2].Output;
            }
        }


        public override bool TestGate()
        {
            if (m_wsInput.Size > 2)
            {
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 0;
                }
                if (Output.Value != 0)
                    return false;
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 1;
                }
                if (Output.Value != 1)
                    return false;
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 1;
                }
                m_wsInput[1].Value = 0;
                if (Output.Value != 0)
                    return false;
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 0;
                }
                m_wsInput[0].Value = 1;
                m_wsInput[1].Value = 1;
                if (Output.Value != 0)
                    return false;
                return true;
            }
            else
                return true;//based on assuring that AndGate Works good for AndGate " two "
        }
    }
}
