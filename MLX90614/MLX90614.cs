using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tizen.Peripheral.I2c;

namespace ProMakers.Tizen.API
{
    class MLX90614
    {
        private const int Bus = 0x01;
        private const int Address = 0x5A;

        private const byte DATA_MLX90614_TA = 0x06;
        private const byte DATA_MLX90614_TOBJ1 = 0x07;

        private I2cDevice i2cDevice = null;

        public MLX90614()
        {
            i2cDevice = new I2cDevice(Bus, Address);
        }

        private float readTemp(byte reg)
        {
            ushort i2cdata = i2cDevice.ReadRegisterWord(reg);

            //
            //  NaN(Not a Number)
            //
            if (i2cdata == 0)
                return 9999.0f;

            float temp = i2cdata;

            temp *= .02f;
            temp -= 273.15f;

            return temp;
        }


        public float readAmbientTempC()
        {
            return readTemp(DATA_MLX90614_TA);
        }

        public float readAmbientTempF()
        {
            float temp = readAmbientTempC();

            if (temp != 9999.0f)
            {
                temp = (temp * 9 / 5) + 32;
            }

            return temp;
        }



        public float readObjectTempC()
        {
            return readTemp(DATA_MLX90614_TOBJ1);
        }

        public float readObjectTempF()
        {

            float temp = readObjectTempC();

            if (temp != 9999.0f)
            {
                temp = (temp * 9 / 5) + 32;
            }

            return temp;
        }



        ~MLX90614()
        {
            i2cDevice.Close();

            i2cDevice = null;
        }
    }

}
