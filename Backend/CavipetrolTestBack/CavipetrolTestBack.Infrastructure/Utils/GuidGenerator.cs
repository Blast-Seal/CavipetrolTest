using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CavipetrolTestBack.Infrastructure.Utils
{
    public static partial class GuidGenerator
    {
        public static Guid Create()
        {
            byte[] randomBytes = new byte[10];

            var p = RandomNumberGenerator.Create();
            p.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);

            return new Guid(guidBytes);
        }
    }
}
