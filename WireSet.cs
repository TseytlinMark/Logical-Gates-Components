using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }
        
        public bool InputConected { get; private set; }

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;
        
        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            for (int i = 0; i < m_aWires.Length; i++)
            {
                m_aWires[i].Value = iValue % 2;
                iValue = iValue / 2;
            }
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            int value = 0;
            for (int i = 0; i < m_aWires.Length; i++)
            {
                if (m_aWires[i].Value == 1)
                    value += (int)(Math.Pow(2, i));
            }
            return value;
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            if (iValue >= 0)
            {
                SetValue(iValue);
            }
            else
            {
                //initializing solution array 
                this.SetValue(-1 * iValue);
                WireSet answer = new WireSet(Size);
                int carry = 0;
                for (int j = 0; j < m_aWires.Length; j++)
                {
                    if (this.m_aWires[j].Value == 1)
                    {
                        if (carry == 0)
                        {
                            answer.m_aWires[j].Value = 1;
                            carry = 1;
                        }
                        else
                        {
                            answer.m_aWires[j].Value = 0;
                            carry = 1;
                        }
                    }
                    else
                    {
                        if (carry == 1)
                        {
                            answer.m_aWires[j].Value = 1;
                            carry = 1;
                        }
                        else
                        {
                            answer.m_aWires[j].Value = 0;
                            carry = 0;
                        }
                    }

                }
                for (int i = 0; i < m_aWires.Length; i++)
                {
                    m_aWires[i].Value = answer[i].Value;
                }
            }
        }

        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            if (this.m_aWires[m_aWires.Length - 1].Value == 0)
                return this.GetValue();
            else
            {
                int carry = 0;
                WireSet comp = new WireSet(m_aWires.Length);
                for (int j = 0; j < m_aWires.Length; j++)
                {
                    if (this.m_aWires[j].Value == 1)
                    {
                        if (carry == 0)
                        {
                            comp.m_aWires[j].Value = 1;
                            carry = 1;
                        }
                        else
                        {
                            comp.m_aWires[j].Value = 0;
                            carry = 1;
                        }
                    }
                    else
                    {
                        if (carry == 1)
                        {
                            comp.m_aWires[j].Value = 1;
                            carry = 1;
                        }
                        else
                        {
                            comp.m_aWires[j].Value = 0;
                            carry = 0;
                        }
                    }

                }
                int finalnum = 0;
                for (int i = 0; i < m_aWires.Length; i++)
                {
                    if(comp[i].Value == 1)
                         finalnum += (int)(Math.Pow(2, i));
                }
                return (finalnum * -1);
            }
        }

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if(wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;
            
        }

    }
}
