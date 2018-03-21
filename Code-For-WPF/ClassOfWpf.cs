using System;
using System.Numerics;

using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;


namespace ClassOfWpf
{
    public partial class Class0
    {
        public void ClassMethod0(out Complex[] _result)
        {
            Complex[] x = { new Complex(2, 0), new Complex(2, 2) };
            //Fourier.Inverse(x, FourierOptions.Matlab);
            Fourier.Forward(x, FourierOptions.Matlab);
            _result = x;
        }
    }
}