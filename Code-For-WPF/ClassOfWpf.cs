using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Numerics;

using MathNet.Numerics;
using MathNet.Numerics.Integration;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.IntegralTransforms;


namespace ClassOfWpf
{
    public partial class Class0
    {
        public void ClassMethod0(out Complex32[] _result)
        {
            Complex32[] x = { new Complex32(2, 0), new Complex32(2, 2) };
            //Fourier.Inverse(x, FourierOptions.Matlab);
            Fourier.Forward(x, FourierOptions.Matlab);
            _result = x;
            
        }

        public void FileProcess(string _path, string _file_name, List<double> _source_data)
        {
            Directory.CreateDirectory(_path);


            List<string> sourceData = new List<string>();
            foreach (var item in _source_data)
            {
                sourceData.Add(item.ToString());
            }
            File.WriteAllLines(_path + "\\" + _file_name, sourceData);       
        }

        public void MathNetMethod(out string _result)
        {
            var M = Matrix<Complex32>.Build;
            var V = Vector<Complex32>.Build;

            var m = M.Dense(3, 4, (i, j) => new Complex32(i, j));
            var v = V.Dense(5, i => new Complex32(i,1));

            

            _result = m.ToString()+v.ToString();
        }
    }
}