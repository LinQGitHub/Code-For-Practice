using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    }
}