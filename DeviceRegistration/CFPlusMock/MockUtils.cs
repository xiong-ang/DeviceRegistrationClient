using DeviceRegistion;
using System;
using System.Collections.Generic;
using System.Linq;
using Version = DeviceRegistion.Version;

namespace CFPlusMock
{
    class MockUtils
    {
        #region Sigleton
        public static MockUtils Instance = new MockUtils();
        private MockUtils() { }
        #endregion Sigleton

        #region Public methods
        public IEnumerable<Device> GetMockDevices()
        {
            UserInfo userInfo= new UserInfo() { Ip = GetMockIP() };
            for (int i = 0; i < GetMockDeviceNumber(); i++)
            {
                yield return GetMockDevice(userInfo);
            }
        }
        #endregion Public methods

        #region Private methods
        private int GetMockDeviceNumber()
        {
            Random ran = new Random();
            return ran.Next(1, 100);
        }

        private Device GetMockDevice(UserInfo userInfo)
        {
            Device device = new Device();
            device.SerialNumber = Guid.NewGuid().ToString();
            device.TopologyInfo = new TopologyInfo() { Path = GetMockPath() };
            device.UserInfo = userInfo;
            device.CipKeys.AddRange(GetMockCipKeys());
            device.CatalogNumber = GetMockCatalogNumber();
            device.Serial = GetMockSerial();
            device.DeviceLifecycle = GetMockLifecycle();
            device.InDeviceVersion = GetMockVersion();
            device.FirmwareVersions.AddRange(GetMockVersions(device.InDeviceVersion));

            return device;
        }

        private string GetMockPath()
        {
            return Guid.NewGuid().ToString();
        }

        private string GetMockIP()
        {
            Random ran = new Random();
            return $"{ran.Next(1, 255)}.{ran.Next(1, 255)}.{ran.Next(1, 255)}.{ran.Next(1, 255)}";
        }

        private IEnumerable<CipKey> GetMockCipKeys()
        {
            Random ran = new Random();
            for (int i = 0; i < ran.Next(1, 5); i++)
            {
                yield return new CipKey { 
                    CodeID = ran.Next(1,255),
                    TypeID = ran.Next(1, 255),
                    VendorID = 0,
                };
            }
        }

        private string GetMockCatalogNumber()
        {
            List<string> catalogs = new List<string> { 
                "1756-L71",
                 "1756-L72",
                 "1756-L73",
                 "1756-L74",
                 "Micro810",
                 "Micro820",
                 "Micro830",
                 "Micro850",
                 "Micro870",
                 "En2tr",
                 "Enbt",
                 "PowerFlex",
                 "1400",
                 "1756-L81",
                 "1756-L82",
                 "1756-L83",
                 "1756-L84",
                 "1756-L85",
                 "1756-L8s",
            };
            Random ran = new Random();
            int index = ran.Next(0, catalogs.Count);
            return catalogs.ElementAt(index);
        }

        private string GetMockSerial()
        {
            List<string> serials = new List<string> {
                "A",
                "B",
                "C",
                ""
            };
            Random ran = new Random();
            int index = ran.Next(0, serials.Count);
            return serials.ElementAt(index);
        }

        private LifecycleEnum GetMockLifecycle()
        {
            Random ran = new Random();
            int index = ran.Next(0, 4);
            return (LifecycleEnum)index;
        }

        private Version GetMockVersion()
        {
            return new Version
            {
                Lifecycle = GetMockLifecycle(),
                Name = GetMockVersionString(),
                PSA = "https://api.rockwellautomation.com/somepath.pdf",
                ReleaseNotes = "https://api.rockwellautomation.com/somepath.pdf",
                Replacements = string.Empty
            };
        }

        private IEnumerable<Version> GetMockVersions(Version version)
        {
            Random ran = new Random();
            int number = ran.Next(0, 8);
            for (int i = 0; i < number; i++)
            {
                yield return GetMockVersion();
            }
            yield return version;
        }

        private string GetMockVersionString()
        {
            Random ran = new Random();
            return $"{ran.Next(0, 11)}.{ran.Next(0, 11)}.{ran.Next(0, 11)}";
        }
        #endregion Private methods
    }
}
